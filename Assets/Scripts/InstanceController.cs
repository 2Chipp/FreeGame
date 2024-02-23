using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceController : MonoBehaviour
{   
    public Transform[] instancePointsArray;
    //public GameObject zombie;

    public float instanceTimer;
    float conditionalTime;
    
    // Start is called before the first frame update
    void Start()
    {
        conditionalTime = 6;
    }
    void Instance()
    {
        int index=Random.Range(0,11);
        GameObject zombieInstance = ObjectPool.obInstance.GetObjectPool(ObjectPool.TypePoolObject.Zombie);
        zombieInstance.transform.position = instancePointsArray[index].position;
        zombieInstance.transform.rotation = Quaternion.identity;
        zombieInstance.SetActive(true);
        //Instantiate(zombie,instancePointsArray[index].transform.position,Quaternion.identity);
        instanceTimer=0;
        Debug.Log("Mas uno ok");
    }
    // Update is called once per frame
    void Update()
    {
       instanceTimer += Time.deltaTime;
       if(instanceTimer > conditionalTime)
       {
            Instance();
           Debug.Log("Tiempo ok");
       }
       if(GameManager.GameTime > GameManager.TotalGameTime/4)
       {           
           conditionalTime = 5;
       }
       if(GameManager.GameTime > GameManager.TotalGameTime/3)
       {           
           conditionalTime = 4;
       }
       if(GameManager.GameTime > GameManager.TotalGameTime/2)
       {           
           conditionalTime = 2;
       }
    }
}
