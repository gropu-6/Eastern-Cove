using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 mousePosition; // Used to transform Input.mousePosition into a world position.
    public Vector2 targetPosition; // What position we are moving the player towards, calculated by the x and y of mousePosition and the current set pivot point.
    public Transform pivot; // Determines which part of the body meets the clicked position.
    public BoxCollider2D stop;

    int layerToCheck;
    public Collider2D[] hits = new Collider2D[3];

    private Animator animator;
    private SpriteRenderer sprite;
    
    bool walk = false; // A check to see if the player is walking to perform animations.

    void Start(){

        animator = transform.GetChild(0).GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        

        animator.SetBool("walking", walk);

        if (Input.GetMouseButtonDown(0)) // If the player presses the left button
        {
            RayCastClick();
        }

        if(walk){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);

            hits = Physics2D.OverlapPointAll(pivot.position, 5);

            for(int i = 0; i < hits.Length; ++i){
                if(hits[i].gameObject.tag == "Mouse"){
                walk = false;
                }
            }
        
        }

        
    }

    private void RayCastClick(){

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Camera.main.transform.forward * 1000.0f);

        if(hit.collider != null){
            
            if(transform.position.x < hit.point.x) sprite.flipX = true; else sprite.flipX = false;
            targetPosition = new Vector2(hit.point.x - pivot.localPosition.x, hit.point.y - pivot.localPosition.y);
            stop.transform.position = hit.point;
            walk = true;
        }
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Translating the worlds position of the mouse to the screens position.
    }
}
