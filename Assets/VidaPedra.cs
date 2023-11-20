using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaPedra : MonoBehaviour
{
    [SerializeField] public Slider vidaPedra;
    public VidaAI pedra;

    public void Start()
    {
        pedra = GetComponentInParent<VidaAI>();
        UpdateHealth(pedra.currentHealth);
    }

    public void Update()
    {
        UpdateHealth(pedra.currentHealth);
    }
    private void UpdateHealth(float currentHealth)
{
    int vida = Mathf.RoundToInt(currentHealth);
    vidaPedra.value = vida;
}
}
