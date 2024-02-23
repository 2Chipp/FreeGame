using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool obInstance;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject npcPrefab;

    List<GameObject> bulletList = new List<GameObject>();
    List<GameObject> zombieList = new List<GameObject>();
    List<GameObject> npcList = new List<GameObject>();

    [SerializeField] int poolSize;


    public enum TypePoolObject
    {
        Bullet,
        Zombie,
        Npc
    }

    private void Awake()
    {
        if(obInstance == null)
        {
            obInstance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartPool()
    {        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.SetActive(false);
            bulletList.Add(bulletInstance);

            GameObject zombieInstance = Instantiate(zombiePrefab);
            zombieInstance.SetActive(false);
            zombieList.Add(zombieInstance);

            GameObject npcInstance = Instantiate(npcPrefab);
            npcInstance.SetActive(false);
            npcList.Add(npcInstance);
        }
    }



    public GameObject GetObjectPool(TypePoolObject typeObj)
    {
        GameObject tempObj;

        switch (typeObj)
        {
            case TypePoolObject.Bullet:
                for (int i = 0; i < bulletList.Count; i++)
                {
                    if (!bulletList[i].activeInHierarchy)
                    {
                        tempObj = bulletList[i];
                        bulletList.Remove(bulletList[i]);
                        return tempObj;
                    }
                }
                break;
            case TypePoolObject.Zombie:
                for (int i = 0; i < zombieList.Count; i++)
                {
                    if (zombieList[i] != null)
                    {
                        tempObj = zombieList[i];
                        zombieList.Remove(zombieList[i]);
                        return tempObj;
                    }
                }
                break;
            case TypePoolObject.Npc:
                for (int i = 0; i < npcList.Count; i++)
                {
                    if (npcList[i] != null)
                    {
                        tempObj = npcList[i];
                        npcList.Remove(npcList[i]);
                        return tempObj;
                    }
                }
                break;
            default:
                Debug.Log("default");
                return null;
        }
        return CreateObject(typeObj);
    }
    GameObject CreateObject(TypePoolObject typeObj)
    {
        GameObject tempObj;

        switch (typeObj)
        {
            case TypePoolObject.Bullet:
                tempObj = Instantiate(bulletPrefab);
                return tempObj;
            case TypePoolObject.Zombie:
                tempObj = Instantiate(zombiePrefab);
                return tempObj;
            case TypePoolObject.Npc:
                tempObj = Instantiate(npcPrefab);
                return tempObj;
            default:
                return null;
        }
    }

    public void BackToPool(GameObject obj, TypePoolObject typeObj)
    {
        obj.SetActive(false);

        switch (typeObj)
        {
            case TypePoolObject.Bullet:
                bulletList.Add(obj);
                break;
            case TypePoolObject.Zombie:
                zombieList.Add(obj);
                break;
            case TypePoolObject.Npc:
                npcList.Add(obj);
                break;
            default:
                break;
        }
    }
}
