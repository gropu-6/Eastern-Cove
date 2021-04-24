using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    public PlayerManager playerManager;
    private GameObject canvas;
    public GameObject currentBubble;

    private float maxSpeed = 0.05f;
    public float textSpeed;
    public GameObject bubblePrefab;
    private string[] text;
    public bool end = false;

    void Start(){
        canvas = playerManager.gameManager.canvas;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){

            if(end == true)
                StartCoroutine(playerManager.interactionManager.return_click(true));
            else{

                textSpeed = 0.005f;

            }
        }
    }

    public IEnumerator Dialogue(ObjectHandler OH, GameObject Entity){
        textSpeed = maxSpeed;
        end = false;
        text = OH.texts;

        int textIndex = OH.numberOfClicks;
        if(OH.numberOfClicks + 1> OH.texts.Length){
            textIndex = Random.Range(0, OH.texts.Length);
            print("what");
        }

        GameObject temp_bubble = Instantiate(bubblePrefab, canvas.transform, false);
        temp_bubble.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Entity.transform.position.x, Entity.transform.position.y + 2.0f, temp_bubble.transform.position.z));
        temp_bubble.transform.SetParent(canvas.transform);
        temp_bubble.transform.GetChild(0).GetComponent<Text>().text = " ";

        currentBubble = temp_bubble;

        for(int i = 0; i < OH.texts[OH.numberOfClicks].Length; ++i){

            char t = OH.texts[OH.numberOfClicks][i];
            temp_bubble.transform.GetChild(0).GetComponent<Text>().text += t;

            yield return new WaitForSeconds(textSpeed);

        }

        int n = 0;
        end = true;
        
        while(playerManager.interactionManager.canClick == false){

            if(n % 2 == 0)
                temp_bubble.transform.GetChild(1).GetComponent<Text>().enabled = true;
            else
                temp_bubble.transform.GetChild(1).GetComponent<Text>().enabled = false;

            n += 1;

            yield return new WaitForSeconds(0.333f);
        }

        if(OH.numberOfClicks + 1 < OH.texts.Length) OH.numberOfClicks++;
        currentBubble = null;
        Destroy(temp_bubble);
    }

}
