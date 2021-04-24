using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryObject;
    public List<GameObject> Inventory = new List<GameObject>();
    public Collider2D[] collider;


    void Start(){
        for(int i = 0; i < InventoryObject.transform.childCount; ++i) Inventory.Add(InventoryObject.transform.GetChild(i).gameObject);
 
    }

    void Update(){

        collider = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        // if(collider != null && collider.gameObject.tag == "Inventory"){

        //     Debug.Log("Ok, it's working");

        // }
    }
}
