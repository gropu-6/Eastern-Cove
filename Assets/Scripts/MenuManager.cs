using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> screens = new List<GameObject>(); // Switch between different screens with ease.
    public Animator fade;

    private void Awake()
    {
        changeScreen(0);
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
    }

    public static void quitGame()
    {
        Application.Quit();
        GUIUtility.ExitGUI();
    }
}
