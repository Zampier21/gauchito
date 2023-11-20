using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;


        }
    }

    void Start(){
        Play("FundoMenu");
        Play("Fogueira");
    }


    public void Play(string name)
    {
       Sound s = Array.Find(sounds , sound => sound.name == name);
       if(s == null){
           Debug.LogWarning("Efeito:" + name + " Não encontrado");
           return;
       }

       s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Efeito: " + name + " não encontrado");
            return;
        }

        s.source.Stop();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Efeito: " + name + " não encontrado");
            return;
        }

        s.source.volume = volume;
    }


}