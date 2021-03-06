using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 mousePosition; // Used to transform Input.mousePosition into a world position.
    public Vector2 targetPosition; // What position we are moving the player towards, calculated by the x and y of mousePosition and the current set pivot point.
    public Transform pivot; // Determines which part of the body meets the clicked position.
    public BoxCollider2D stop;

    public Collider2D[] hits = new Collider2D[3];

    public Animator[] animator;

    bool walk = false; // A check to see if the player is walking to perform animations.
    public float speed = 5;

    private GameObject head; // We're making the head look in the direction of the mouse, along with the eyes.

    void Start(){

        head = transform.GetChild(1).gameObject;

    }

    private void Update()
    {   
        
        LookAtMouse();
        
        for(int i = 0; i < animator.Length; ++i)
            animator[i].SetBool("walking", walk);

        if (Input.GetMouseButtonDown(0)) // If the player presses the left button
        {
            RayCastClick();
        }

        if(walk){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            hits = Physics2D.OverlapPointAll(pivot.position, 5);

            for(int i = 0; i < hits.Length; ++i){
                if(hits[i].gameObject.tag == "Mouse" || hits[i].gameObject.tag == "Object"){
                    walk = false;
                }
            }
        
        }
    }

    private void RayCastClick(){

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Camera.main.transform.forward * 1000.0f);

        if(hit.collider != null && hit.collider.gameObject.tag != "Object"){
            
            if(transform.position.x < hit.point.x) for(int i = 0; i < animator.Length; ++i) animator[i].GetComponent<SpriteRenderer>().flipX = true; else for(int i = 0; i < animator.Length; ++i) animator[i].GetComponent<SpriteRenderer>().flipX = false;
            
            targetPosition = new Vector2(hit.point.x - pivot.localPosition.x, hit.point.y - pivot.localPosition.y);
            stop.transform.position = hit.point;
            walk = true;
        }
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Translating the worlds position of the mouse to the screens position.
    }

    private void LookAtMouse(){

        Transform eyeballMask = head.transform.GetChild(0);
        Transform eyeball = eyeballMask.GetChild(0);

        
        
        Vector2 n = Vector2.Lerp(eyeball.localPosition, new Vector3(mousePosition.x, mousePosition.y, 0.0f) - eyeball.position, 0.1f);
        Vector3 direction = new Vector3(mousePosition.x, mousePosition.y, 0.0f) - head.transform.position;
        
        if(animator[0].GetComponent<SpriteRenderer>().flipX == false)  direction = -1.0f * ( new Vector3(mousePosition.x, mousePosition.y, 0.0f) - head.transform.position );


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -5, 5);

        eyeball.localPosition = Vector2.ClampMagnitude(n, 0.5f);

        head.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 



    }

    void LateUpdate(){
        Transform eyeballMask = head.transform.GetChild(0);
        if(animator[0].GetComponent<SpriteRenderer>().flipX == true) eyeballMask.localPosition = new Vector3(0.3911114f, eyeballMask.localPosition.y, 0.0f);
        else eyeballMask.localPosition = new Vector3(-0.3911114f, eyeballMask.localPosition.y, 0.0f);
    }
}
