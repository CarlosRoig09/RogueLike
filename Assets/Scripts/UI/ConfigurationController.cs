using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigurationController : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _GeneralGroup;
    [SerializeField]
    private AudioMixerGroup _Music;
    [SerializeField]
    private AudioMixerGroup _Sounds;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void GeneralGroupMod(Slider slider)
    {
        _GeneralGroup.audioMixer.SetFloat("GeneralVolume", slider.value);
        AudioManager.instance.Play("PlayerGameOver");
    }

    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void UnMute()
    {
        AudioListener.volume = 1;
    }

    public void MusicGroupMod(Slider slider)
    {
        _Music.audioMixer.SetFloat("MusicVolume", slider.value);
    }

    public void SoundGroupMod(Slider slider)
    {
        _Sounds.audioMixer.SetFloat("SoundEffectVolume", slider.value);
        AudioManager.instance.Play("PlayerGameOver");
    }
}
