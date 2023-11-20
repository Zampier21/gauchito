using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   public WpController wp;
   float damage = 35;
   //public GameObject hitPartcile;

   private void  OnTriggerEnter(Collider other)
   {
    if(other.tag == "Enemy")
    {
        other.GetComponent<Animator>().SetTrigger("Hit");
        other.GetComponent<VidaAI>().TakeDamage(damage);
    }
   }
}
