using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> screens = new List<GameObject>(); // Switch between different screens with ease.
    public Animator fade;
    public Animator menu;
    
    public Texture2D cursorTex;
    private CursorMode cursorMode = CursorMode.Auto;
    private float weightAdditionNo;
    private bool start = false;

    private void Awake()
    {
        // changeScreen(0);
        Cursor.SetCursor(cursorTex, new Vector2(0,0), cursorMode);
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.Space) && weightAdditionNo == 0){
            menu.SetBool("Zoom", true);
            start = true;

        }

        if(start && weightAdditionNo <= 1.0f){
            weightAdditionNo += (1.5f - weightAdditionNo * 1.45f) * Time.deltaTime;
        }

        if(weightAdditionNo >= 1 && start){
            changeScreen(0);
            start = false;
        }

        menu.SetLayerWeight(menu.GetLayerIndex("Zoom"), weightAdditionNo);
    }

    public static void initiateGame()
    {
        // We're using the UnityEngine.UI library to load a scene.
            SceneManager.LoadScene(1); // This will load the main game scene which has an index of 1. It can be seen in File > Build Settings.
    }

    public void startCoroutineIn()
    {
        StartCoroutine(fadeIn());
    }

    private IEnumerator fadeIn()
    {
        fade.SetBool("fadeIn", true);

        yield return new WaitForSeconds(1.5f);

        initiateGame();
    }

    private void fadeOut()
    {

    }

    public void changeScreen(int index)
    {
        for (int i = 0; i < screens.Count; ++i)
            screens[i].SetActive(false);

        screens[index].SetActive(true);
        screens[index].GetComponent<Animator>().enabled = true;
    }

    public static void quitGame()
    {
        Application.Quit();
        GUIUtility.ExitGUI();
    }
}
