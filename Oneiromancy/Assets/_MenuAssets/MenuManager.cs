using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*  #########################################################################
    #                           MenuManager.cs                              #
    #-----------------------------------------------------------------------#
    #   This Script is located in the MainMenu Canvas.                      #
    #   It is the logic for the Buttons in the Main Menu.                   #
    #-----------------------------------------------------------------------# 
    #   Line 42 -> StartGame(): Starts the Game                             #
    #   Line 48 -> ToggleControls(): Hides Credits and shows Controls       #
    #                                or hides itself if open                #
    #   Line 64 -> ToggleCredits(): Hides Controls and shows Credits        #
    #                               or hides itself if open                 #
    #   Line 79 -> ExitGame(): Exits Game                                   #       
    #########################################################################
*/


public class MenuManager : MonoBehaviour
{
    public Button PlayButton;
    public Button ControlsButton;
    public Button CreditsButton;
    public Button ExitButton;

    public GameObject ControlsPanel;
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        ControlsButton.onClick.AddListener(ToggleControls);
        CreditsButton.onClick.AddListener(ToggleCredits);
        ExitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()   // Starts the Game
    {
        Debug.Log("Play Button Pressed --> Starting Game.");
        SceneManager.LoadScene("YannikScene");
    }

    void ToggleControls() // Hides Credits and shows Controls / hides itself if open
    {
        if(ControlsPanel.activeSelf == false)
        {
            Debug.Log("Controls Button Pressed --> Showing Controls.");
            CreditsPanel.SetActive(false);
            ControlsPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Controls Button Pressed --> Hiding Controls.");
            ControlsPanel.SetActive(false);
        }

    }

    void ToggleCredits() // Hides Controls and shows Credits / hides itself if open
    {
        if(CreditsPanel.activeSelf == false)
        {
            Debug.Log("Credits Button Pressed --> Showing Credits.");
            ControlsPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Credits Button Pressed --> Hiding Credits.");
            CreditsPanel.SetActive(false);
        } 
    }

    void ExitGame() // Exits Game
    {
        Debug.Log("Exit Button Pressed --> Exiting Game.");
        Application.Quit();
    }
}
