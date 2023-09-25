using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<AudioClip> dialogueList;
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void PlayAudioFromOther()
    {
        StartCoroutine(PlayAudio());
    }

   private IEnumerator PlayAudio()
    {
        m_Animator = gameObject.GetComponent<Animator>(); 
        m_Animator.SetBool("Idle", false);
        AudioSource audio = GetComponent<AudioSource>();   
        foreach (AudioClip dialogue in dialogueList)
        {
            audio.clip = dialogue;
            audio.Play();
            yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        }
         m_Animator.SetBool("Idle", true);
        m_Animator.SetBool(0, true);
           
        
       
    }
}
