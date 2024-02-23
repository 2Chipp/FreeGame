using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{   [SerializeField] GameObject player;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

  private void OnTriggerEnter(Collider other) {
      if(other.gameObject.tag=="Jugador")
      {
        if(this.gameObject.name=="Teletransportar")
        {  
            player.transform.position=pos2.transform.position;
            player.transform.rotation=pos2.transform.rotation;
        }
        else
        {
            player.transform.position=pos1.transform.position;
            player.transform.rotation=pos1.transform.rotation;
        }
      }
  }
}
