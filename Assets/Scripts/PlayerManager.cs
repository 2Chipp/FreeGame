using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletInstancePoint;

    Vector3 dir;
    Vector3 dirWhitCam;

    CamController cam;
    Animator animator;
    AudioSource audioSource;
    
    [SerializeField] float baseSpeed;
    [SerializeField] float maxSpeed;
    float speed;

    [SerializeField] float shootSpeed;
    float fireRate;

    float health = 100;
    bool alive = true;

    
    float endGameDelay;
    
    void Start()
    {
        Init();        
    }
    private void Update() 
    {
        CheckHealth();
        Mov();
        Fire();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Golpe")
        {
            health-=Random.Range(5,8);
            GameManager.Health = health;
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }                   
        }
    }
    
    void Init()
    {
        GameManager.Health = health;
        cam = Camera.main.GetComponent<CamController>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        speed = baseSpeed;
    }

    void CheckHealth()
    {
        if ((health <= 0) && (alive = true))
        {
            animator.SetBool("Morir", true);
            alive = false;
            endGameDelay += Time.deltaTime;
        }
        if (endGameDelay > 1.5f) // <=== XXX
        {
            //zombies.EndGame();
        }
    }
    void Mov() 
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = maxSpeed;
        }
        else speed = baseSpeed;

        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");
        dir = new Vector3(movX, 0, movZ);
        if (((movZ != 0) || (movX != 0)) && (alive == true))
        {
            animator.SetBool("Correr", true);
        }
        else
        {
            animator.SetBool("Correr", false);
        }
        dirWhitCam = dir.x * cam.axisX + dir.z * cam.axisZ;
        transform.position += dirWhitCam.normalized * speed * Time.deltaTime;
        transform.LookAt(transform.position + dirWhitCam);
    }

    void Fire()
    {
        fireRate += Time.deltaTime;

        if ((Input.GetButtonDown("Jump")) && (fireRate > 1))
        {
            animator.SetTrigger("Fuego");
            GameObject bullet = ObjectPool.obInstance.GetObjectPool(ObjectPool.TypePoolObject.Bullet);
            bullet.transform.position = bulletInstancePoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootSpeed);
            bulletPrefab.GetComponent<AudioSource>().Play();
            fireRate = 0;
            StartCoroutine(BkToPool(bullet));
        }
    }

    IEnumerator BkToPool(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.obInstance.BackToPool(bullet, ObjectPool.TypePoolObject.Bullet);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
