using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaItemScript : MonoBehaviour
{
    public AudioClip collectSound;
    public int manaValue;
    public string name, desc;
    public Mana item;
    // Start is called before the first frame update
    void Start()
    {
        item = new Mana(name, desc, manaValue);
    }




    


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = collectSound;
            audio.Play();
            Character player = col.gameObject.GetComponent<Character>();
            player.AddToInventory(item);
            gameObject.SetActive(false);
        }
    }
}
