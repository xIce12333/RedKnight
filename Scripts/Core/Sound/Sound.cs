using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip; 
    [Range(0f, 1f)] public float volume = 0.5f;
    [System.NonSerialized] public AudioSource source;
    public bool loop;

}
