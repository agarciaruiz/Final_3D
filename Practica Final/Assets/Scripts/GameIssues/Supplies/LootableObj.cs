using UnityEngine;

public class LootableObj : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int factor;
    [SerializeField]
    [Range(0,100)]private int dropChance;
    [SerializeField] private int itemsToDrop;

    private void Start()
    {
        if(spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    public void DropLoot()
    {
        LootSystem.Instance.SpawnLoot(spawnPoint, factor, itemsToDrop, dropChance);
    }
}
