using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAnimName, doorCloseAnimName;
    void update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.gameObject.tag == "porteira")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                intText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");
                    }
                    if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                }
            }else{
                intText.SetActive(false);
            }
        }
        else
        {
            intText.SetActive(false);
        }
    }
}
