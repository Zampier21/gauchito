using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoAI : MonoBehaviour
{
    // Script de AI para controlar a movimentação dos inimigos, utilizando navmesh.
    //Adiciona o navmesh ao inimigo.
    public NavMeshAgent agentNav;
    //adiciona oque seria o player, para que o inimigo consiga seguir-lo
    public Transform player;
    //para o inimigo patrulhar
    private Vector3 walkPoint;
    //para checar onde o inimigo anda.
    public LayerMask whatisGround, whatIsPlayer;
    bool walkPointSet;
    [SerializeField]
    public float walkRange;
    //Animação
    public Animator anim;
    //para o inimigo atacar
    [SerializeField]
    private float attackSpeed;
    bool hasAttacked;

    //boleanas para ataques
    public float attackRange, sightRange;
    public bool playerinSight, playerinAttackRange;

    //Projeteis
    [SerializeField] public GameObject projetilPrefab;
    public Transform firePoint;
    public float forcaProjetil = 5;

    private void Awake()
    {
        //funções para quando instanciar um inimigo.
        //ira automaticamente detectar o player, e adicionar o navmesh ao script
        /////MUDAR ESQUELETO PARA O NOME DO PLAYER NO FUTURO!
        player = GameObject.Find("FirstPersonController").transform;
        agentNav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerinSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerinAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //se não ver o player, e não esta no range de ataque, patrulhar.
        if (!playerinSight && !playerinAttackRange)
        {
            Patrol();
        }
        //se ver o player, mas nao esta em range de ataque, ir atras do player
        else if(playerinSight && !playerinAttackRange)
        {
            ChasePlayer();
        }
        else if(playerinSight && playerinAttackRange)
        {
            AttackPlayer();
        }
    }


    //vai até o player.
    private void ChasePlayer()
    {
        agentNav.SetDestination(player.position);
        anim.SetBool("Follow",true);
    }
    private void Patrol()
    {
        //se não sabe onde ir, procura onde ir
        if(!walkPointSet)
        {
           SearchPatrol();
        }
        //se há onde ir, leve o para la
        if(walkPointSet)
        {
            agentNav.SetDestination(walkPoint);
            anim.SetBool("Follow",false);
        }
        //checa a distancia, e se for maior que o destino, não pode mais andar
        Vector3 distanceWalkPoint = transform.position - walkPoint;
        if(distanceWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchPatrol()
    {
        //cria duas variaveis, para randomicamente fazer o inimigo andar
        float randomZ = Random.Range(-walkRange, walkRange);
        float randomX = Random.Range(-walkRange, walkRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //checa se aonde o inimigo vai andar, o chão.
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
        {
            walkPointSet = true;
            Debug.Log("Patrol!");
        }
    }
    private void AttackPlayer()
    {
        //faz o inimigo não se mecher, para atacar o jogador.
    agentNav.SetDestination(transform.position);
    transform.LookAt(player);
    if(!hasAttacked)
    {
        //seu codigo de dano vai aqui !!
        //a não ser que o dano seja diretamente feito usando colliders.
        anim.SetTrigger("Hit");
        GameObject projetil = Instantiate(projetilPrefab, firePoint.position, firePoint.rotation);
        Rigidbody projetilRB = projetil.GetComponent<Rigidbody>();
        projetilRB.AddForce(firePoint.up * forcaProjetil);
        hasAttacked = true;
        Invoke(nameof(ResetAttack), attackSpeed);
    }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

}
