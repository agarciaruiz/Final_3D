using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 0 = primary, 1 = secondary
    [SerializeField] private Item[] items;
    [SerializeField] private Weapon[] weapons;

    private int newItemIndex = 0;
    private int maxNumOfItems = 10;
    public bool hasKey = false;

    private void Start()
    {
        InitVariables();
    }

    public void AddItem(Item newItem)
    {
        if(newItem is Key)
        {
            AddKey(newItem as Key);
        }

        if(newItem is Weapon)
        {
            AddWeapon(newItem as Weapon);
        }
        newItemIndex++;
    }

    public void AddKey(Key newKey)
    {
        hasKey = true;
        if (newItemIndex < maxNumOfItems)
        {
            items[newItemIndex] = newKey;
        }
        else
            Debug.Log("Max number of items stored reached");
    }

    public void AddWeapon(Weapon newWeapon)
    {
        int newItemIndex = (int)newWeapon.weaponStyle;

        if (weapons != null)
        {
            if (weapons[newItemIndex] != null)
            {
                RemoveItem(newItemIndex);
            }
            weapons[newItemIndex] = newWeapon;
        }

        WeaponClass weaponClass = newWeapon.prefab.GetComponent<WeaponClass>();
        weaponClass.InitWeapon();
    }

    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }

    public Weapon GetItem(int index)
    {
        return weapons[index];
    }

    private void InitVariables()
    {
        items = new Item[maxNumOfItems];
    }
}
