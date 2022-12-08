using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager: MonoBehaviour
{
    public float TrimSilence;
    private AudioSource OM_SoundPlayer;
    private PlayerSettings _playersettings;
    private void Awake() {
        ReferenceManager.OM_SoundManager = this;
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    private void Start() {
        _playersettings = gameObject.GetComponent<GameManager>().PlayerSettings;
        Debug.Log(" INIT" + _playersettings);
        OM_SoundPlayer = Camera.main.GetComponent<AudioSource>();
    }
    public void OnSceneChange(Scene oldScene, Scene newScene)
    {
        OM_SoundPlayer = Camera.main.GetComponent<AudioSource>();
        Debug.Log (" ONSCENECHANGE " + _playersettings);
        

    }
    public void PlaySound(int index)
    {
        Debug.Log (" ATTACK" + _playersettings);
        
        OM_SoundPlayer.PlayOneShot(_playersettings.OM_AudioClips[index]);
    }
    public void PlayBackgroundMusic()
    {
       OM_SoundPlayer.PlayOneShot(_playersettings.OM_AudioClips[1]);
    }
}
