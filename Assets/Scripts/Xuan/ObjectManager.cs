using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private List<GameObject> _objects = new List<GameObject>();

    public void RegisterObject(GameObject obj)
    {
        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
        }
    }

    public void UnregisterObject(GameObject obj)
    {
        if (_objects.Contains(obj))
        {
            _objects.Remove(obj);
        }
    }

    public void DeactivateAllObjects()
    {
        foreach (var obj in _objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
