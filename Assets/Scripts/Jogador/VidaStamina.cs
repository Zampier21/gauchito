using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaStamina : MonoBehaviour
{
[SerializeField] public Slider healthSlider;
[SerializeField] public Slider staminaSlider;

private void OnEnable(){
    FirstPersonController.OnDamage += UpdateHealth;
    FirstPersonController.OnHeal += UpdateHealth;
    FirstPersonController.OnStaminaChange += UpdateStamina;
}

private void OnDisable(){
    FirstPersonController.OnDamage -= UpdateHealth;
    FirstPersonController.OnHeal -= UpdateHealth;
    FirstPersonController.OnStaminaChange -= UpdateStamina;
}

private void Start(){
    UpdateHealth(100);
    UpdateStamina(100);
}
private void UpdateHealth(float currentHealth)
{
    int vida = Mathf.RoundToInt(currentHealth);
    healthSlider.value = vida;
}

private void UpdateStamina(float currentStamina){
    int stamina = Mathf.RoundToInt(currentStamina);
    staminaSlider.value = stamina;
}
}
