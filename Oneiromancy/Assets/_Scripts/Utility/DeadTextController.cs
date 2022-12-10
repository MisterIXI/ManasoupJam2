using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeadTextController : MonoBehaviour
{
    private TextMeshProUGUI _TextComp;
    // Start is called before the first frame update
    void Start()
    {
        _TextComp = gameObject.GetComponent<TextMeshProUGUI>();
        _TextComp.text = ReferenceManager.GameManager.OM_DeathText;
        Debug.Log("DeathText changed to: "+ ReferenceManager.GameManager.OM_DeathText);
    }

}
