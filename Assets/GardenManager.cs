using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager Instance;
    private Stack<GameObject> placedPlants = new Stack<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlant(GameObject plant)
    {
        placedPlants.Push(plant);
    }

    public void UndoLastPlant()
    {
        if (placedPlants.Count > 0)
        {
            GameObject last = placedPlants.Pop();
            Destroy(last);
        }
    }
}
