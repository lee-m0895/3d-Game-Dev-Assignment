using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip collectSound;
    public string name, desc;
    public Consumables item;
    // Start is called before the first frame update
    void Start()
    {
        item = new Key(desc, name);
    }

    // Update is called once per frame
    void Update()
    {

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
