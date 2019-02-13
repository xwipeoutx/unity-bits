using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlantState
{
    Planted,
    Grown,
    Fallow
}

public class PlantTimelineOrchestrator : MonoBehaviour
{
    [SerializeField] PlayableDirector growDirector;
    [SerializeField] PlayableDirector harvestDirector;
    [SerializeField] PlayableDirector plantDirector;

    public PlantState plantState;
    
    public void Grow()
    {
        if (plantState == PlantState.Planted)
        {
            growDirector.Play();
            plantState = PlantState.Grown;
        }
    }

    public void Harvest()
    {
        if (plantState == PlantState.Grown)
        {
            harvestDirector.Play();
            plantState = PlantState.Fallow;
        }
    }
    
    public void Plant()
    {
        if (plantState == PlantState.Fallow)
        {
            plantDirector.Play();
            plantState = PlantState.Planted;
        }
    }
}
