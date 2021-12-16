using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootData", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    public struct Probabilities
    {
        public int rarity;
        public GameObject reward;
    }

    public Probabilities[] probabilities; 
    
}
