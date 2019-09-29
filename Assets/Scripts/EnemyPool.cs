

public class EnemyPool : GenericObjectPool<EnemyBehaviour>
{
    public UnityEngine.GameObject[] prefabs;
    public override void AddObjects(int count)
    {
        var newObject = UnityEngine.GameObject.Instantiate(prefabs[UnityEngine.Random.Range(0,prefabs.Length)]);
        newObject.gameObject.SetActive(false);
        base.objects.Enqueue(newObject.GetComponent<EnemyBehaviour>());
    }
}
