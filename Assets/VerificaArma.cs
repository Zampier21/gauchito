using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaArma : MonoBehaviour
{
    [SerializeField] private GameObject coletaArma;
    
    public void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            coletaArma.SetActive(true);
            Destroy(gameObject);
        }
    }
}
