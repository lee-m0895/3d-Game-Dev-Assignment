using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNpcRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    private bool facingNPC = false;
    private GameObject npc;
    public GameObject uiText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        Debug.Log("user is looking at npc:" + facingNPC);
        if (Input.GetKeyDown(KeyCode.G) && facingNPC == true)
        {
            npc.GetComponent<NPC>().PlayAudioFromOther();
            
        }
      






        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3) && hit.transform.tag == "NPC")
        {
            facingNPC = true;
            uiText.SetActive(true);
            npc = hit.collider.gameObject;
        }
        else
        {
            facingNPC = false;
            uiText.SetActive(false);
        }
    





    }
}
