using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator fade;

    void Awake(){
        fade.SetBool("fadeOut", true);
    }
}
