using System.Collections;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private LootTable lootTable;

    public static LootSystem Instance { get; private set; }

    private int probability;
    [SerializeField] private int total;

    private LootTable.Probabilities[] localProb;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        localProb = lootTable.probabilities;
        System.Array.Sort(localProb, new RarityComp());
        System.Array.Reverse(localProb);

        foreach(LootTable.Probabilities weight in lootTable.probabilities)
        {
            total += weight.rarity;
        }
    }

    public void SpawnLoot(Transform spawnPoint, int factor, int itemsToDrop, int dropChance)
    {
        probability = Random.Range(0, (total + 1));
        int myProb = probability * factor;

        int calcDropChance = Random.Range(0, 101);

        if(calcDropChance >= dropChance)
        {
            Debug.Log("No loot");
            return;
        }

        if(myProb >= total)
        {
            myProb = total;
        }

        for(int i = 0; i < localProb.Length; i++)
        {
            if(myProb <= localProb[i].rarity)
            {
                GameObject go = Instantiate(localProb[i].reward, spawnPoint.position, Quaternion.identity);

                if(itemsToDrop == 1)
                {
                    return;
                }
            }
            else
            {
                myProb -= localProb[i].rarity;
            }
        }
    }
}

public class RarityComp: IComparer
{
    public int Compare(object x, object y)
    {
        int n1 = ((LootTable.Probabilities)x).rarity;
        int n2 = ((LootTable.Probabilities)y).rarity;

        if(n1 > n2)
        {
            return 1;
        }
        else if (n1 == n2)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
}
