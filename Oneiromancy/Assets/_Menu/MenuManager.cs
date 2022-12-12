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
    #   Line 48 -> ToggleControls(): Hides tabs and shows Controls          #
    #                                or hides itself if open                #
    #   Line 64 -> ToggleCredits(): Hides tabs and shows Credits            #
    #                               or hides itself if open                 #
    #   Line 64 -> ToggleResources(): Hides tabs and shows Resources        #
    #                               or hides itself if open                 #
    #   Line 79 -> ExitGame(): Exits Game                                   #       
    #########################################################################
*/


public class MenuManager : MonoBehaviour
{
    public Button PlayButton;
    public Button ControlsButton;
    public Button CreditsButton;
    public Button LeaderboardButton;
    public Button ExitButton;

    public GameObject ControlsPanel;
    public GameObject CreditsPanel;
    [Header("HighScore")]
    public GameObject LeaderboardPanel;
    public GameObject[] LeaderboardEntrys;
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;
    public TMPro.TextMeshProUGUI[] rDates;
    HighScores myScores;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        ControlsButton.onClick.AddListener(ToggleControls);
        CreditsButton.onClick.AddListener(ToggleCredits);
        LeaderboardButton.onClick.AddListener(ToggleResources);
        ExitButton.onClick.AddListener(ExitGame);
        myScores = GetComponent<HighScores>();
        
    }

    void StartGame()   // Starts the Game
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        Debug.Log("Play Button Pressed --> Starting Game.");
        SceneManager.LoadScene(1);
    }

    void ToggleControls() // Hides Credits and shows Controls / hides itself if open
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        if(ControlsPanel.activeSelf == false)
        {
            Debug.Log("Controls Button Pressed --> Showing Controls.");
            CreditsPanel.SetActive(false);
            LeaderboardPanel.SetActive(false);
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
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        if(CreditsPanel.activeSelf == false)
        {
            Debug.Log("Credits Button Pressed --> Showing Credits.");
            ControlsPanel.SetActive(false);
            LeaderboardPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Credits Button Pressed --> Hiding Credits.");
            CreditsPanel.SetActive(false);
        } 
    }
    void ToggleResources() // Hides Controls and shows Credits / hides itself if open
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        if(LeaderboardPanel.activeSelf == false)
        {
            Debug.Log("Resources Button Pressed --> Showing Resources.");
            ControlsPanel.SetActive(false);
            CreditsPanel.SetActive(false);
            LeaderboardPanel.SetActive(true);
            LoadHighscore();
        }
        else
        {
            Debug.Log("Resources Button Pressed --> Hiding Resources.");
            LeaderboardPanel.SetActive(false);
        } 
    }

    void ExitGame() // Exits Game
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        Debug.Log("Exit Button Pressed --> Exiting Game.");
        Application.Quit();
    }
    void LoadHighscore() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". Fetching...";
        }
        
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(HighScores.PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                rScores[i].text = highscoreList[i].score.ToString();
                rNames[i].text = highscoreList[i].username;
                rDates[i].text = highscoreList[i].date;
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }

}
