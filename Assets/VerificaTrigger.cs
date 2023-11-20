using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private AudioClip clip;
    
    public void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            spawner.SetActive(true);
        }
        AudioSource ac = GetComponent<AudioSource>();
        if(clip == null)
        {
            return;
        }
        ac.PlayOneShot(clip);
    }
}
