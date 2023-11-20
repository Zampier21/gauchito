using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterDeathAndRestart : MonoBehaviour
{
    public bool isDead;
    public GameObject deathCanvas;
    public Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
    }

    public void Die()
    {
        isDead = true;
        GetComponent<CharacterController>().enabled = false;
        deathCanvas.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (isDead)
        {
            deathCanvas.SetActive(true);
        }
    }
}
