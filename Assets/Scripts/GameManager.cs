using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator fade;
    public Texture2D cursorTex;
    private CursorMode cursorMode = CursorMode.Auto;


    void Awake(){
        fade.SetBool("fadeOut", true);
        Cursor.SetCursor(cursorTex, new Vector2(0,0), cursorMode);
    }
}
