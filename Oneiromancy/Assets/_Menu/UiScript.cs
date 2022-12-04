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
    public Image HP1;
    public Image HP2;
    public Image HP3;
    public int CurrentHealth = 3;   // HIER DAS MOMENTANE LEBEN EINFÜGEN

    public Text StageText;
    public int StageNr = 1; // HIER WIEDER DIE STAGE(LVL) EINFÜGEN


    void Update()
    {
        UpdateHpBar();
        UpdateStage();
    }

    void UpdateHpBar()   // shows/hides hearth based on hp
    {
        if(CurrentHealth == 3)
        {
            HP1.enabled = true;
            HP2.enabled = true;
            HP3.enabled = true;
        }
        else if(CurrentHealth == 2)
        {
            HP1.enabled = true;
            HP2.enabled = true;
            HP3.enabled = false;
        }
        else if(CurrentHealth == 1)
        {
            HP1.enabled = true;
            HP2.enabled = false;
            HP3.enabled = false;
        }
        else
        {
            HP1.enabled = false;
            HP2.enabled = false;
            HP3.enabled = false;
        }
    }

    void UpdateStage() // updates Stage lvl
    {
        StageText.text = StageNr.ToString(); // HIER WIEDER DIE STAGE(LVL) EINFÜGEN
    }
}
