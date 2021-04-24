using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public PlayerManager playerManager; 

    public GameObject Hovering;
    public GameObject Clicked;
    public GameObject LastClicked;

    public GameObject Glow;

    public bool canClick = true;

    void Update(){
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ObjectStates(mouse);
    }

    void ObjectStates(Vector3 mouse){
        
        RaycastHit2D hit = Physics2D.Raycast(mouse, Camera.main.transform.forward * 1000.0f);

        if(hit.collider != null && hit.collider.gameObject.tag == "Object"){
            
            Hovering = hit.collider.gameObject;
            if(Hovering.GetComponent<SpriteRenderer>() != null) manage_outline(Hovering);

            if(Input.GetMouseButtonDown(1) && canClick && playerManager.dialogueManager.currentBubble == null){
                
                LastClicked = Clicked;
                Clicked = Hovering;

                StartCoroutine(playerManager.dialogueManager.Dialogue(Clicked.GetComponent<ObjectHandler>(), playerManager.player.gameObject));
                canClick = false;

            }   

            if(Input.GetKeyDown(KeyCode.E) && !playerManager.GetComponent<MinigameManager>().enabled){
                playerManager.gameManager.startMinigame();
            }

        } else{
            Glow.transform.position = new Vector3(1000,1000,1000);
            Glow.GetComponent<SpriteRenderer>().sprite = null;
            Hovering = null;   
        }
        
    }

    public IEnumerator return_click(bool f){
        if(canClick == false){

            yield return new WaitForSeconds(0.1f);

            canClick = f;

        }
    }

    private void manage_outline(GameObject Entity){
        Glow.transform.position = Entity.transform.position;
        Glow.transform.localScale = Entity.transform.localScale;    

        Glow.GetComponent<SpriteRenderer>().sprite = Entity.GetComponent<SpriteRenderer>().sprite;
        Glow.GetComponent<SpriteRenderer>().sortingOrder = Entity.GetComponent<SpriteRenderer>().sortingOrder + 1;
        


    }
}
