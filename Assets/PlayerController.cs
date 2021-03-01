using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 mousePosition; // Used to transform Input.mousePosition into a world position.
    public Vector2 targetPosition; // What position we are moving the player towards, calculated by the x and y of mousePosition and the current set pivot point.
    public Transform pivot; // Determines which part of the body meets the clicked position.
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
            targetPosition = new Vector2(mousePosition.x - pivot.localPosition.x, mousePosition.y - pivot.localPosition.y);

            if(transform.position.x < targetPosition.x){
                sprite.flipX = true;
            } else
            {
                sprite.flipX = false;
            }
            walk = true;
        }

        print(Vector2.Distance(new Vector2(pivot.position.x, pivot.position.y), targetPosition));

        if(Vector2.Distance(new Vector2(pivot.position.x, pivot.position.y), targetPosition) > 1.4f && Vector2.Distance(new Vector2(pivot.position.x, pivot.position.y), targetPosition) < 1.41f){
            walk = false;
        }

        if(walk){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Translating the worlds position of the mouse to the screens position.
    }
}
