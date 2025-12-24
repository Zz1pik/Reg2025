using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Transform levelRoot;
    public GameObject carPrefab;
    public Transform spawnPoint;

    private void Start()
    {
        LoadLevel();
        Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void LoadLevel()
    {
        string path = Path.Combine(Application.dataPath, "PlacementContainerSave.json");

        if (!File.Exists(path))
        {
            Debug.LogError("Файл уровня не найден: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        ContainerSaveData data = JsonUtility.FromJson<ContainerSaveData>(json);

        foreach (var objData in data.objects)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + objData.name);
            if(prefab == null)
            {
                Debug.LogError($"Prefab {objData.name} не найден в Resources/prefabs!");
                continue;
            }

            GameObject obj = Instantiate(prefab, levelRoot);
            obj.transform.position = objData.position;
            obj.transform.eulerAngles = objData.rotation;
            obj.transform.localScale = objData.scale;
        }
    }
}
