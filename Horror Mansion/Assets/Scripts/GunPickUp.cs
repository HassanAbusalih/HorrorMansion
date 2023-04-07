using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GameObject shrinkGunOnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        shrinkGunOnPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                this.gameObject.SetActive(false);
                shrinkGunOnPlayer.SetActive(true);
            }
        }
    }
}
