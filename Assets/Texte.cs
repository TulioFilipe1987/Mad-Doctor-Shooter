using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texte : MonoBehaviour
{
    public float speed = 3.5f;

    public float yPos, xPos;

    public Vector3 posTemp;


    public float minBound_X = -71f ,maxBound_X = 71f,
        minBound_Y = 3.3f ,maxBound_Y = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos = Input.GetAxisRaw("Horizontal");
        yPos = Input.GetAxisRaw("Vertical");

        posTemp = transform.position;

        posTemp.x += xPos * speed * Time.deltaTime;
        posTemp.y += yPos * speed * Time.deltaTime;


        


        if (posTemp.y < minBound_Y)
            posTemp.y = minBound_Y;

        if (posTemp.y > maxBound_Y)
            posTemp.y = maxBound_Y;







        transform.position = posTemp;









    }
}
