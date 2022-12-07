using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathCamUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _virtualCam;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _cameraUI;
    void Start()
    {
        
    }
    public void StartDeathCam()
    {
        _gameOverUI.SetActive(false);
        _virtualCam.SetActive(true);
        _cameraUI.SetActive(true);
    }
    public void EndDeathCam()
    {
        Debug.Log("Main Menu Button Pressed --> Switching Scene to Main Menu.");
        SceneManager.LoadScene(0);
    }
}
