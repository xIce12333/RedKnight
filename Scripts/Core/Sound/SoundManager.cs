using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}
    [Header("Reference")]
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        instance = this;
        AddSound();
        LoadVolume();
    }

    private void AddSound()     
    {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void LoadVolume()   // adjust game volume according to last game play     プレイヤーがゲームを再起動しても、前回のゲーム音量で起動するため
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1);
        volumeSlider.value = volume;
        AudioListener.volume = volume;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void PlaySoundIfNotPlaying(string name)      // prevent looping the same SFX for specific SFXs, such as wall sliding
    {                                                   // サウンドエフェクトがすでに再生されている場合、重ならないようにします
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying) {
            s.source.Play();
        }
            
        
    }
    public void StopSound(string name)              // stop specific SFX    特定のサウンドエフェクトの再生を止めます
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }

    
}
