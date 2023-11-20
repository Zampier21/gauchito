using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetiraPedra : MonoBehaviour
{
      [SerializeField] private GameObject retiraPedra;
    public void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            retiraPedra.SetActive(false);
            Destroy(gameObject);
        }
    }
}
