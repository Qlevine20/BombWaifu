using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    #region Singleton
    public static GenericObjectPool<T> Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField]
    private T prefab;

    public Queue<T> objects = new Queue<T>();

    public T Get ()
    {
        if (objects.Count == 0)
            AddObjects(1);
        return objects.Dequeue();
    }

    public void ReturnToPool (T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    public virtual void AddObjects (int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }

}
