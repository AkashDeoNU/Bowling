using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_ScoreMult : MonoBehaviour
{
    public int scoreMultiplier = 5;
    public AudioSource sound;
    private void OnTriggerEnter(Collider other)
    {           
        if (other.gameObject.GetComponent<Ball>() != null) { 
            Pin.pointPerPin = scoreMultiplier;
            sound.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2f);
        }
    }

}
