using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  #########################################################################
    #                            UiScript.cs                                #
    #-----------------------------------------------------------------------#
    #   This Script is located in the Ui Canvas.                            #
    #   Ít updates the hearts that are displayed in the top left.           #
    #-----------------------------------------------------------------------# 
    #   Line 33 -> UpdateHpBar(): shows/hides hearth based on hp            #     
    #   Line 62 -> UpdateStage(): updates Stage lvl                         #  
    #########################################################################
*/

public class UiScript : MonoBehaviour
{
    [SerializeField] public PlayerSettings PlayerSettings;
    public Image[] healthImage;
    // public Image HP2;
    // public Image HP3;
    public int CurrentHealth = 3;   // HIER DAS MOMENTANE LEBEN EINFÜGEN
    [SerializeField] private Slider _bossSlider;
    public Text StageText;
    public int StageNr = 1; // HIER WIEDER DIE STAGE(LVL) EINFÜGEN
    public int CurrentBossHealth;
    public int MaxBossHealth;
   
    void FixedUpdate()
    {
        UpdateHpBar();
        UpdateStage();
        UpdateBossHealthBar();
    }

    void UpdateHpBar()   // shows/hides hearth based on hp
    {
        
            
        for (int i = 0; i < PlayerSettings.MaxHealth; i++)
        {
            if (i+1 <= CurrentHealth)
                healthImage[i].enabled=true;
            else
                healthImage[i].enabled=false;
        }
    
    }

    void UpdateStage() // updates Stage lvl
    {
        StageText.text = StageNr.ToString(); // HIER WIEDER DIE STAGE(LVL) EINFÜGEN
    }

    public void ToggleBossHealthBar(bool toggle)
    {
        _bossSlider.gameObject.SetActive(toggle);
    }
    private void UpdateBossHealthBar()
    {
        _bossSlider.value = CurrentBossHealth;
        _bossSlider.maxValue = MaxBossHealth;
    }
}
