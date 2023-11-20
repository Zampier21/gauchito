using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
  public float coldownAttack;
  public bool canAttack;
  public void Update()
  {
    if(coldownAttack > 0)
    {
      canAttack = false;
      coldownAttack -= Time.deltaTime;
    }
    else
    {
      canAttack = true;
    }
  }
  private void OnTriggerStay(Collider other)
  {
    if(other.CompareTag("Player"))
    {
      if(canAttack == true)
      {
        FirstPersonController.OnTakeDamage(50);
        coldownAttack = 1f;
      }
    }
        
  }
}
