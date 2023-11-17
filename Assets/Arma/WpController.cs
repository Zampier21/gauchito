using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpController : MonoBehaviour
{
  public GameObject Espeto;
  public bool canAttack = true;
  public float AttackCooldown = 1.0f;

  public AudioClip SomEspeto;

  public void Update()
  {
    if(Input.GetMouseButtonDown(0))
    {
        if(canAttack)
        {
            EspetoAttack();
        }
    }
  }
  public void EspetoAttack()
    {
        canAttack = false;

        Animator anim = Espeto.GetComponent<Animator>();

        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SomEspeto);

        StartCoroutine(ResetAttackCoolDown());
    }

    IEnumerator ResetAttackCoolDown()
    {
        yield return  new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
}
