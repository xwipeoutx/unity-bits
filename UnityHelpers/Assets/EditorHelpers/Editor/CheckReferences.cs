using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheckReferences : EditorWindow
{
    private static string NamespacePrefix => typeof(CheckReferences)?.Namespace?.Split('.').FirstOrDefault() ?? "";

    private static HashSet<string> _skipPropertyTypes = new HashSet<string>()
    {
        "string",
        "int",
        "uint",
        "bool",
        "Enum",
        "float",
        "vector",
        "Vector2",
        "Vector3",
        "Color",
        "FloatMaterialProperty",
        "VectorMaterialProperty",
        "MatrixMaterialProperty",
        "Box"
    };

    [MenuItem("Helpers/Check References")]
    public static void CheckSceneReferences()
    {
        Debug.ClearDeveloperConsole();
        var rootObjects = new List<GameObject>();
        var scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        var components = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)).Cast<MonoBehaviour>();
        foreach (var component in components.Where(c =>
            ShouldCheck(c, rootObjects)))
        {
            var obj = new SerializedObject(component);
            var prop = obj.GetIterator();
            prop.Next(true);
            do
            {
                if (_skipPropertyTypes.Contains(prop.type))
                    continue;

                if (prop.type.Contains("PPtr<$"))
                {
                    var value = prop.objectReferenceValue;

                    if (string.IsNullOrEmpty(value?.ToString()))
                    {
                        Debug.LogWarning($"{prop.name} ({prop.type}) on {component.name} is not set", component);
                    }
                }
                else if (!prop.name.StartsWith("m_"))
                {
                    switch (prop.type)
                    {
                        case "UnityEvent":
                            CheckEvent(component, prop);
                            break;
                        case "CustomTypeHandler":
                        case "CustomTypeHandler2":
                            break;
                        default:
                            Debug.Log($"No checks for {prop.name}: {prop.type} (on {component.name})", component);
                            break;
                    }
                }
            } while (prop.Next(false));
        }
    }

    private static void CheckEvent(MonoBehaviour component, SerializedProperty prop)
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                          BindingFlags.IgnoreCase;

        var cProperty = component.GetType().GetProperty(prop.name, bindingFlags);
        var cField = component.GetType().GetField(prop.name, bindingFlags);
        var unityEvent = (UnityEvent) (cProperty?.GetValue(component) ?? cField.GetValue(component));

        var persistentEventCount = unityEvent.GetPersistentEventCount();
        for (var i = 0;
            i < persistentEventCount;
            i++)
        {
            var target = unityEvent.GetPersistentTarget(i);
            var methodName = unityEvent.GetPersistentMethodName(i);

            if (target == null)
            {
                Debug.LogWarning($"{prop.name} ({prop.type}) on {component.name} has an invalid target", component);
            }
            else if (string.IsNullOrEmpty(methodName) || target.GetType().GetMethod(methodName) == null)
            {
                Debug.LogWarning(
                    $"{prop.name} ({prop.type}) on {component.name} has an invalid method: {methodName}",
                    component);
            }
        }
    }

    private static bool ShouldCheck(MonoBehaviour component, List<GameObject> rootObjects)
    {
        var isInSameNamespace =
            string.IsNullOrEmpty(NamespacePrefix) && string.IsNullOrEmpty(component.GetType().Namespace)
            || (component.GetType().Namespace?.StartsWith(NamespacePrefix) ?? false);

        return isInSameNamespace && IsInScene(component, rootObjects);
    }

    public static bool IsInScene(MonoBehaviour component, List<GameObject> rootObjects)
    {
        return IsInScene(component.transform, rootObjects);
    }

    public static bool IsInScene(Transform transform, List<GameObject> rootObjects)
    {
        return rootObjects.Contains(transform.root.gameObject);
    }
}