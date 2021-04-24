using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDLE : MonoBehaviour
{
    public bool idle = true;

    void Update(){
        GetComponent<Animator>().SetBool("isIdle", idle);


    }


}
