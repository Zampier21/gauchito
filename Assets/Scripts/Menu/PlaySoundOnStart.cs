using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] public AudioClip _clip;
    [SerializeField] public AudioClip _fogueira;
    void Start()
    {
        SoundManager.Instance.PlaySound(_clip);
        SoundManager.Instance.PlaySound(_fogueira);
    }

  
}
