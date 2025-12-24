using UnityEngine;
using UnityEngine.UI;

public class PlaceButton : MonoBehaviour
{
    public GameObject prefab;
    public bool isTrampoline;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Place);
    }

    public void Place()
    {
        PlacementManager.Instance.PlaceObject(prefab, isTrampoline);
    }
}
