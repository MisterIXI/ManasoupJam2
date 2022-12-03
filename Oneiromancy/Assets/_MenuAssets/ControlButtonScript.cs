using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] public GameObject PanelControls;
    public GameObject PanelCredits;

    public void OpenPane()
    {
        if(PanelControls != null)
        {
            PanelControls.SetActive(true);
            PanelCredits.SetActive(false);
        }
        else
        {
            PanelControls.SetActive(false);
            PanelCredits.SetActive(false);
        }
    }
}
