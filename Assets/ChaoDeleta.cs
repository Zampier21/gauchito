using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoDeleta : MonoBehaviour
{
    [SerializeField] float tempoDelete;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,tempoDelete);
    }
}
