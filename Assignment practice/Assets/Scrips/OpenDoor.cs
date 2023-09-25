using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public string keyName;
    private Animation ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        Character player = col.gameObject.GetComponent<Character>();
        bool hasKey = player.checkForKey(keyName);
        if (col.gameObject.tag == "Player" && hasKey == true)
        {
            ani.Play("open");
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
