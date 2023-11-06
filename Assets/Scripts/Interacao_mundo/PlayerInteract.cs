using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private List<Transform> npcList;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
        float intercatRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, intercatRange);
        foreach(Collider collider in colliderArray)
        {
          if (collider.TryGetComponent(out NpcInteract npcInteractable)){
            npcInteractable.Interact();
          }
        }
    }
}
}
