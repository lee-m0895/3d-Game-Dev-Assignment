using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    public AudioClip collectSound;
    public int health;
    public string name, desc;
    public Health item;
    // Start is called before the first frame update
    void Start()
    {
        item = new Health(name,desc, health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addItemToInv()
    {
        
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = collectSound;
            audio.Play();
            Character player = col.gameObject.GetComponent<Character>();
            player.AddToInventory(item);
            gameObject.SetActive(false);
        }
    }


}
