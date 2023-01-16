using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public GameObject scoreMult;
    public GameObject speedUp;
    public GameObject getBigger;
    public GameObject scoreDeMult;
    public GameObject speedDown;
    public GameObject getSmaller;
    Dictionary<int, GameObject> powerChangers = new Dictionary<int, GameObject>();
    List<int> x_pos_slots;

    //number of prefabs to spawn on reset.
    private int num_to_spawn = 3;

    // Start is called before the first frame update
    void Start()
    {
        //store powerChangers in dictionary
        powerChangers.Add(1, scoreMult);
        powerChangers.Add(2, speedUp);
        powerChangers.Add(3, getBigger);
        powerChangers.Add(4, scoreDeMult);
        powerChangers.Add(5, speedDown);
        powerChangers.Add(6, getSmaller);

        x_pos_slots = new List<int> { -3, 0, 3 };

        renew();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            renew();
            Scoreboard.round += 1;
            Pin.pointPerPin = 2;
        }
    }

    public void renew()
    {
        //instantiate prefabs randomly

        for (int i = 0; i < num_to_spawn; i++)
        {
            System.Random rnd = new System.Random();
            //the type of prefab is randomized
            int p_num = rnd.Next(1, 6);
            //then instantiate the prefab
            Vector3 rand_pos = new Vector3(x_pos_slots[i], -4, 30);
            Instantiate(powerChangers[p_num], rand_pos, Quaternion.identity);
        }
    }
}
