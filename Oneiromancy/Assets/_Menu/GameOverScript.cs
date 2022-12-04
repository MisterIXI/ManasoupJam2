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


    void Start()
    {
        StageText.text = ReferenceManager.GameManager.CurrentLayer.ToString(); // Send Stage text to screen
        MainMenuButton.onClick.AddListener(GoToMainMenu);
        MainMenuButton.interactable = false;
        StartCoroutine(DelayedActivate()); 
   }

    void GoToMainMenu()   // Go to Main Menu
    {
        Debug.Log("Main Menu Button Pressed --> Switching Scene to Main Menu.");
        SceneManager.LoadScene(0);
        ReferenceManager.GameManager.SetState(GameManager.GameState.MainMenu);
    }

    public IEnumerator DelayedActivate()
    {
        yield return new WaitForSecondsRealtime(1);
        MainMenuButton.interactable = true;
    }
}