using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Npc : MonoBehaviour
{   
    AudioSource audioSource;
    
    public Transform[] puntos;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Jugador")
        {            
            GameManager.NpcRescuedCount += 1;
            audioSource.Play();
            transform.position=puntos[Random.Range(0,11)].transform.position;
        }
    }
}
