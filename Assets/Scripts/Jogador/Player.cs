using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Vector3 entradasJogador;
    private CharacterController Chac;
    public float Velocidade;
    private Transform myCamera;
    private Animator Animate;

    

    private bool estaNoChao;
    [SerializeField] private Transform verificadorChao;
    [SerializeField] private LayerMask cenarioMask;

    [SerializeField] private float alturaSalto;
    private float gravidade = -9.81f;
    private float velocidadeVertical;
    private void Awake()
    {
        Animate = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Chac = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
    }
   
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.eulerAngles.y, transform.eulerAngles.z);

        entradasJogador = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        entradasJogador = transform.TransformDirection(entradasJogador);

        Chac.Move(entradasJogador * Time.deltaTime * Velocidade);

        estaNoChao = Physics.CheckSphere(verificadorChao.position, 0.3f, cenarioMask);

        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            velocidadeVertical = Mathf.Sqrt(alturaSalto * -2f * gravidade);
        }
        if(estaNoChao && velocidadeVertical < 0)
        {
            velocidadeVertical = -1f;
        }
        velocidadeVertical += gravidade * Time.deltaTime;
        Chac.Move(new Vector3(0, velocidadeVertical,0) * Time.deltaTime);
        //atacar
             if(Input.GetMouseButtonDown(0))
            {
              Animate.SetBool("Attack",true);
            }
            else
            {
                Animate.SetBool("Attack",false);
            }
            //correr
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Velocidade = 8f;
            }
            else
            {
                Velocidade = 3f;
            }
            if(Velocidade >= 6 && Velocidade <= 10)
            {
                Animate.SetBool("Run", true);
                Animate.SetBool("Walk",false);
            }
            else if (Velocidade < 15)
            {
                Animate.SetBool("Run", false);
                Animate.SetBool("Walk",true);
            }
    }
}

