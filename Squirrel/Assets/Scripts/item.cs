using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public string itemName = "Rama";
    public bool isCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            other.GetComponent<PlayerController>().CollectItem(this);
            gameObject.SetActive(false);
        }
    }
    

}