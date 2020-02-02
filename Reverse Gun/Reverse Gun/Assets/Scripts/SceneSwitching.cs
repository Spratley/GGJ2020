using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public void Close_game()//Closes the game
    {
        Application.Quit();
    }

    public void Load_Game() //Loads first level
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }
}