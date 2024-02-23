using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Zombie : MonoBehaviour
{
    [SerializeField] Collider hitCollider;
    PlayerManager player;  
    Collider myCollider;
    
    NavMeshAgent navMeshAgent;
    Animator animator;
    AudioSource audioSource;

    float playerDistance;
    bool dead = false;

    [SerializeField] float baseSpeed = 3f;
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        SpeedChange(GameManager.GameTime);
                
        

        Move();
    }

    private void OnEnable()
    {
        if (myCollider != null)
        {
            hitCollider.enabled = true;
            myCollider.enabled = true;
            dead = false;
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if((other.gameObject.CompareTag("Bala"))&&(dead==false))
        {
            animator.SetTrigger("Muerto");
            hitCollider.enabled=false;
            myCollider.enabled=false;
            dead=true;
            navMeshAgent.isStopped=true;   
            GameManager.EliminatedZombiesCount += 1;
            ObjectPool.obInstance.BackToPool(other.gameObject, ObjectPool.TypePoolObject.Bullet);
            Invoke("BkToPool", 2f);
            audioSource.Stop();
        }
        if(other.gameObject.CompareTag("Jugador"))
        {
            
            navMeshAgent.isStopped=true;
            animator.SetTrigger("Atacar_");
            Debug.Log("Ataca");
        }
        if(other.gameObject.CompareTag("Entorno"))
        {
            //Destroy(this.gameObject);  
            Debug.Log("destruido");          
        }                
    }
    void BkToPool()
    {
        ObjectPool.obInstance.BackToPool(this.gameObject, ObjectPool.TypePoolObject.Zombie);
    }

    void Init()
    {
        myCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerManager>();

        audioSource.Play();
    }
    public void SpeedChange(float elapsedGameTime)
    {
       if (elapsedGameTime > 30 && elapsedGameTime < 60)
       {
           speed = baseSpeed * Time.deltaTime;
           Debug.Log("Speed=1");
       }
       else if (elapsedGameTime > 60 && elapsedGameTime < 90)
       {
           speed= baseSpeed * 1.3f * Time.deltaTime;
           Debug.Log("Speed=1.3");
       }
       else if (elapsedGameTime > 90 && elapsedGameTime < 120)
       {
           speed= baseSpeed * 1.6f * Time.deltaTime;
           Debug.Log("Speed=1.6");
       }
       else if (elapsedGameTime > 120)
       {
           speed= baseSpeed * 2f * Time.deltaTime;
           Debug.Log("Speed=2");
       }
    }

    void Move()
    {
        if (!dead) navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(player.transform.position);
        playerDistance = navMeshAgent.remainingDistance;
        navMeshAgent.speed = speed;

        if ((playerDistance > 2f) && (dead == false))
        {
            navMeshAgent.isStopped = false;
        }

        if (navMeshAgent.isStopped == false)
        {
            animator.SetBool("Moviendose", true);
        }
        else
        {
            animator.SetBool("Moviendose", false);
        }

        if (transform.position.y < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
