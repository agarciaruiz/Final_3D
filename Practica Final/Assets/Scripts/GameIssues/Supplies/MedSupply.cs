using UnityEngine;

public class MedSupply : MonoBehaviour ,ISupply
{
    private GameObject player;
    private PlayerController playerController;
    private AudioSource audioSource;
    private CharacterStats characterStats;
    private int healthToAdd = 30;

    private void Start()
    {
        GetReferences();
    }

    public void PickupSupply()
    {
        if (characterStats.health == characterStats.maxHealth)
        {
            Debug.Log("Healt already full");
        }
        else
        {
            audioSource.clip = playerController.pickupClip;
            audioSource.Play();
            characterStats.Heal(healthToAdd);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupSupply();
        }
    }

    private void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
        playerController = player.GetComponent<PlayerController>();
        audioSource = player.GetComponent<AudioSource>();
    }
}
