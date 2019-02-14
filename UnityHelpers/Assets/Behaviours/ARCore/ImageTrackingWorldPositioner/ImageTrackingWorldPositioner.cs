using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ImageTrackingWorldPositioner : MonoBehaviour
{
    [SerializeField] ARCoreSessionConfig config;
    [SerializeField] Transform sceneRoot;
    [SerializeField] Transform debugPosition;
    
    private List<AugmentedImage> tempAugmentedImages = new List<AugmentedImage>();

    void Awake()
    {
#if UNITY_EDITOR
        CenterRootAt(new Pose(debugPosition.position, debugPosition.rotation), null);
#else
        DestroyImmediate(debugPosition.gameObject);
#endif
    }
    
    void Update()
    {
        #if !UNITY_EDITOR
        CenterRootAtFoundImage();
        #endif
    }

    private void CenterRootAtFoundImage()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }
        
        Session.GetTrackables(tempAugmentedImages, TrackableQueryFilter.New);

        foreach (var image in tempAugmentedImages)
        {
            if (image.TrackingState == TrackingState.Tracking)
            {
                var imagePose = image.CenterPose;

                CenterRootAt(imagePose, image);
            }
        }
    }

    private void CenterRootAt(Pose imagePose, Trackable trackable)
    {
        sceneRoot.rotation = imagePose.rotation * Quaternion.Inverse(transform.localRotation);
        sceneRoot.position = imagePose.position - sceneRoot.rotation * transform.localPosition;

        var worldRootPose = new Pose(sceneRoot.position, sceneRoot.rotation);
        var anchor = Session.CreateAnchor(worldRootPose, trackable);

        sceneRoot.SetParent(anchor.transform, true);
    }

    #if UNITY_EDITOR
    // `.Texture` is only defined in the editor.  Don't need a gizmo if we're not in the editor!
    private void OnDrawGizmos()
    {
        var image = config.AugmentedImageDatabase[0];

        var width = image.Width;
        var height = image.Width * image.Texture.height / image.Texture.width;
        
        var topLeft = new Vector3(-width, 0, height)/2;
        var topRight = new Vector3(width, 0, height)/2;
        var bottomLeft = new Vector3(-width, 0, -height)/2;
        var bottomRight = new Vector3(width, 0, -height)/2;
        
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
    #endif
}