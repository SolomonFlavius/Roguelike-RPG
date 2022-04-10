using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun theGun;

    private bool canPick;

    public GameObject message;

    private void Update()
    {
        if(canPick)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.instance.PlaySFX(6);
                Gun gunClone = Instantiate(theGun);
                Instantiate(PlayerController.instance.theGun.gunPickup, transform.position, transform.rotation);
                Destroy(PlayerController.instance.theGun.gameObject);
                PlayerController.instance.theGun = gunClone;               
                gunClone.transform.parent = PlayerController.instance.gunArm;
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                


                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPick = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPick = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPick = false;
            message.SetActive(false);
        }
    }
}
