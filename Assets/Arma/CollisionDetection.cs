using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   public WpController wp;
   public GameObject hitPartcile;

   private void  OnTriggerEnter(Collider other)
   {
    if(other.tag == "Enemy" && wp.isAttacking)
    {
        Debug.Log(other.name);
        other.GetComponent<Animator>().SetTrigger("Hit");

        Instantiate(hitPartcile, new Vector3(other.transform.position.x, 
        transform.position.y, other.transform.position.z), 
        other.transform.rotation);
    }
   }
}
