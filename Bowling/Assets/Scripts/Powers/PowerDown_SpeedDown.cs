using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDown_SpeedDown : MonoBehaviour
{
    Ball ball;
    public AudioSource sound;
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            ball.ActivateSpeedPowerDown();
            sound.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
