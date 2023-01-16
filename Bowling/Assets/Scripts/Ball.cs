using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody BallRB;   
    public bool launched;
    private int maxRot = 10;    
    public float rotSpeed = 0.5f;
    public int shiftSpeed = 10;
    public int ballForce = 1200;

    float resetTime = -1;
    float resetInterval = 15;

    public AudioSource ballBowled;
    public AudioSource barrier;

    EventHandler eh;

    // Start is called before the first frame update
    void Start()
    {
        BallRB = GetComponent<Rigidbody>();
        launched = false;
        eh = FindObjectOfType<EventHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //inputs and stuff:
        bool leftArrow = Input.GetKey(KeyCode.LeftArrow);
        bool rightArrow = Input.GetKey(KeyCode.RightArrow);
        bool leftA = Input.GetKey("a");
        bool rightD = Input.GetKey("d");
        bool space = Input.GetKey(KeyCode.Space);
        //Xlocal = transform.right;
        //Ylocal = transform.up;
        //Zlocal = transform.forward;

        //Ball aiming only allowed if not already launched
        if (!launched)
        {
            //Use left/right arrows to angle ball left/right
            if (leftArrow)
            {
                //BallRB.MoveRotation(Quaternion.Euler(0, -rotSpeed * maxRot * Time.deltaTime, 0));
                transform.Rotate(new Vector3(0, -rotSpeed * maxRot * Time.deltaTime, 0));

                //transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y + (-rotSpeed * Time.deltaTime), transform.eulerAngles.z);
            }
            if (rightArrow)
            {
                //BallRB.MoveRotation(Quaternion.Euler(0, rotSpeed * maxRot * Time.deltaTime, 0));
                transform.Rotate(new Vector3(0, rotSpeed * maxRot * Time.deltaTime, 0));
                //transform.eulerAngles.Set(transform.eulerAngles.x, transform.eulerAngles.y + (rotSpeed * Time.deltaTime), transform.eulerAngles.z);
            }
            //transform.rotation = Quaternion.Euler(transform.rotation.x,
            //                                      Mathf.Clamp(transform.eulerAngles.y, -maxRot, maxRot),
            //                                      transform.rotation.z);
            //use A/D to shift ball left/right
            if (leftA)
            {
                if (transform.position.x > -7.0)
                {
                    transform.Translate(new Vector3(-shiftSpeed * Time.deltaTime, 0, 0), Space.World);
                }
            }
            if (rightD)
            {
                if (transform.position.x < 7.0)
                {
                    transform.Translate(new Vector3(shiftSpeed * Time.deltaTime, 0, 0), Space.World);
                }
            }
            //use spacebar to launch ball forward
            if (space)
            {
                launched = true;
                ballBowled.Play();
                resetTime = Time.time + resetInterval;
                BallRB.AddForce(transform.forward * ballForce);
            }
        }

        if (! StillWaitingForPhysicsToSettle() || (resetTime != -1 && Time.time > resetTime))
        {
            ResetForBowling();
            launched = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bumper") {
            barrier.Play();
        }
    }

    bool StillWaitingForPhysicsToSettle()
    {

        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<Rigidbody>() == null)
            {
                continue;
            }
            if (! obj.GetComponent<Rigidbody>().IsSleeping())
            {
                return true; 
            }
        }
        return false;
    }

    void ResetForBowling()
    {
        if (launched)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            eh.renew();
            Scoreboard.round += 1;
            Pin.pointPerPin = 2;
        }
    }

    public void ActivateSpeedPowerUp()
    {
        BallRB.AddForce(new Vector3(0, 0, 2000));
    }

    public void ActivateSpeedPowerDown()
    {
        BallRB.AddForce(new Vector3(0, 0, -200));
    }
}
