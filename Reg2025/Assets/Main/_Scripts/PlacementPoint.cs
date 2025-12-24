using UnityEngine;

public class PlacementPoint : MonoBehaviour
{
    public bool IsOccupied => currentObject != null;
    public GameObject currentObject;

    public void Place(GameObject obj)
    {
        currentObject = obj;
        obj.transform.position = transform.position;
    }

    public void Clear()
    {
        currentObject = null;
    }
}
