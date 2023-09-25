using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler
{
    private AttackDB attackdb = new AttackDB();

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("howvered over");
        string value = gameObject.GetComponentInChildren<Text>().text;
        Attack attack = attackdb.GetAttack(value);
        GameObject textBox = GameObject.FindGameObjectWithTag("AttackInfo");
        textBox.GetComponentInChildren<Text>().text = attack.GetDesc();
    }
    
}
