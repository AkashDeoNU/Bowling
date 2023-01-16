using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleArrow : MonoBehaviour
{
    // Start is called before the first frame update
    private Ball ball;
    void Start()    
    {
        ball = transform.parent.gameObject.GetComponent<Ball>();
    }
    
    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(!ball.launched);
    }
}
