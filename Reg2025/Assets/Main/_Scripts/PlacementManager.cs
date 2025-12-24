using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [Header("Все объекты спавнятся сюда")]
    public Transform objectsRoot;
    public static PlacementManager Instance;

    public PlacementPoint[] points;

    private GameObject activeTrampoline;
    private Stack<GameObject> placedObjects = new Stack<GameObject>();
    private GameObject currentObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            UndoLast();

        if (currentObject != null && Input.GetKeyDown(KeyCode.R))
            currentObject.transform.Rotate(0f, 90f, 0f);
    }

    public void PlaceObject(GameObject prefab, bool isTrampoline)
    {
        if (isTrampoline)
        {
            if (activeTrampoline != null)
                activeTrampoline.SetActive(false);

            prefab.SetActive(true);
            activeTrampoline = prefab;
            currentObject = prefab;
            return;
        }

        foreach (var point in points)
        {
            if (!point.IsOccupied)
            {
                GameObject obj = Instantiate(prefab, objectsRoot);
                point.Place(obj);
                placedObjects.Push(obj);
                currentObject = obj;
                return;
            }
        }

        Debug.Log("Нет свободных точек!");
    }

    private void UndoLast()
    {
        if (placedObjects.Count == 0) return;

        GameObject obj = placedObjects.Pop();

        foreach (var point in points)
        {
            if (point.currentObject == obj)
            {
                point.Clear();
                break;
            }
        }

        Destroy(obj);
        currentObject = null;
    }

    // ================== СОХРАНЕНИЕ ==================

    public void SaveToJson()
    {
        if (objectsRoot == null)
        {
            Debug.LogError("objectsRoot не назначен!");
            return;
        }

        ContainerSaveData data = new ContainerSaveData();

        foreach (Transform child in objectsRoot)
        {
            if (!child.gameObject.activeSelf)
                continue;

            data.objects.Add(new SavedObjectData
            {
                name = child.name.Replace("(Clone)", "").Replace(" (1)", ""), 
                position = child.position,
                rotation = child.eulerAngles,
                scale = child.localScale
            });
        }

        string json = JsonUtility.ToJson(data, true);

        string path = Path.Combine(Application.dataPath, "PlacementContainerSave.json");
        File.WriteAllText(path, json);

    #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
    #endif

        Debug.Log($"Сохранено {data.objects.Count} активных объектов в {path}");
    }
}

