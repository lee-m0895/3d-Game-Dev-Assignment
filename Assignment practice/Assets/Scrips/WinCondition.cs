using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject winText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Character player = other.gameObject.GetComponent<Character>();
            if (player.checkForKey("Crystal"))
            {
                Debug.Log("the user has won");
                winText.SetActive(true);
                StartCoroutine(toMenu());
            }
            else
            {
                Debug.Log("the user dose not have the item");
            }

        }
    }


    private IEnumerator toMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }


}
