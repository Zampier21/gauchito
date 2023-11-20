using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager Instance;
   [SerializeField] public AudioSource _musicSource;
   [SerializeField] public AudioSource _effectSource;

   public void Awake()
   {
    if(Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
   }

   public void PlaySound(AudioClip clip)
   {
    _effectSource.PlayOneShot(clip);
   }

   public void ChangeMasterVolume(float value)
   {
    AudioListener.volume = value;
   }
}
