using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SavedObjectData
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}

[System.Serializable]
public class ContainerSaveData
{
    public List<SavedObjectData> objects = new List<SavedObjectData>();
}
