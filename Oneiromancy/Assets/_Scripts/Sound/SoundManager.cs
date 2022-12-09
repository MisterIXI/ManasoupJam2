using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager: MonoBehaviour
{
    // public float TrimSilence;
    private AudioSource OM_SoundPlayer;
    private PlayerSettings _playersettings;
    private void Awake() {
        ReferenceManager.OM_SoundManager = this;
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    private void Start() {
        _playersettings = gameObject.GetComponent<GameManager>().PlayerSettings;
        OM_SoundPlayer = Camera.main.GetComponent<AudioSource>();
    }
    public void OnSceneChange(Scene oldScene, Scene newScene)
    {
        _playersettings = gameObject.GetComponent<GameManager>().PlayerSettings;
        OM_SoundPlayer = Camera.main.GetComponent<AudioSource>();
        
        

    }
    public void PlaySound(int index, float volume)
    {
        if(OM_SoundPlayer != null)
            OM_SoundPlayer.PlayOneShot(_playersettings.OM_AudioClips[index],0.4f);
    }
    public IEnumerator FadeOut( float FadeTime)
    {
        float startVolume = OM_SoundPlayer.volume;

        while (OM_SoundPlayer.volume > 0)
        {
           OM_SoundPlayer.volume -= startVolume * Time.fixedUnscaledTime/FadeTime;
            yield return null;
        }
        OM_SoundPlayer.Stop();
        OM_SoundPlayer.volume=startVolume;
    }
    
}
