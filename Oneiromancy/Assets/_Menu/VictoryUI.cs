using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public void OnButtonContinue()
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        Debug.Log("Continue Button Pressed --> next wave 11.");
        
        ReferenceManager.GameManager.SetState(GameManager.GameState.Portal);

    }
    public void OnButtonExit()
    {
        ReferenceManager.OM_SoundManager.PlaySound(9,1f);
        Debug.Log("Exit Button Pressed --> Exiting Game.");
        Application.Quit();
    }
}
