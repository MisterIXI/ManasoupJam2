using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*  #########################################################################
    #                        GameOverScript.cs                              #
    #-----------------------------------------------------------------------#
    #   This Script is located in the GameOver Canvas.                      #
    #   It is the logic for the Button in the GameOver Screen.              #
    #   It also pulls the stage data and displays it.                       #
    #-----------------------------------------------------------------------# 
    #   Line 34 -> GoToMainMenu(): Go to Main Menu                          #     
    #########################################################################
*/


public class GameOverScript : MonoBehaviour
{
    public Button MainMenuButton;
    public Text StageText;
    public GameObject StageNrVar;


    void Start()
    {
        StageText.text = StageNrVar.ToString(); // Send Stage text to screen
        MainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()   // Go to Main Menu
    {
        Debug.Log("Main Menu Button Pressed --> Switching Scene to Main Menu.");
        SceneManager.LoadScene(2);
    }
}
