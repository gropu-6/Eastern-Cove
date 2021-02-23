using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Mouse Position
    public Vector2 mousePosition = new Vector2(0.0f, 0.0f);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If the player presses the left button
        {
            // Do something here.
            Debug.Log(mousePosition[0].ToString() + " " + mousePosition[1].ToString());

        }
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Translating the worlds position of the mouse to the screens position.
    }
}
