using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{


//primele 2 sunt ptr spikeuri
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && PlayerController.instance.IsMoving())
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

     private void OnTriggerStay2D(Collider2D other)
     {
         if (other.tag == "Player" && PlayerController.instance.IsMoving())
         {
            PlayerHealthController.instance.DamagePlayer();
         }
     }

     private void OnCollisionEnter2D(Collision2D other)
     {
         
         if (other.gameObject.tag == "Player")
         {
            PlayerHealthController.instance.DamagePlayer();
         }
     }

     private void OnCollisionStay2D(Collision2D other)
     {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
     }
}
