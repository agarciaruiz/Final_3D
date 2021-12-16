using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupKey : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private Inventory inventory;
    private AudioSource audioSource;

    private void Start()
    {
        GetReferences();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Key newKey = this.GetComponent<ItemObj>().item as Key;
            inventory.AddItem(newKey);
            audioSource.clip = playerController.pickupClip;
            audioSource.Play();
            Destroy(this.gameObject);
        }
        
    }

    private void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        inventory = player.GetComponent<Inventory>();
    }
}
