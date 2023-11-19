using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaAparecer : MonoBehaviour
{
    public GameObject arma;
    public Transform mano;
    public float tempoAparecimento;
    private bool podeAparecer = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && podeAparecer == false)
        {
            StartCoroutine(AparecerArma());
        }


    }
    IEnumerator AparecerArma()
    {
        podeAparecer = true;
        yield return new WaitForSeconds(tempoAparecimento);
        arma.SetActive(true);
        arma.transform.position = mano.position;
        arma.transform.rotation = mano.rotation;
    }
}
