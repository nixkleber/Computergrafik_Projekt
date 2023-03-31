using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f; 
    [SerializeField] private Vector3 startPos; 
    [SerializeField] private Vector3 endPos = new Vector3(3,3,3);

    private void Start()
    {
        startPos = transform.position;
        endPos = transform.position + endPos;
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPos, endPos, pingPong);
        transform.Rotate(new Vector3(20,20,0)*Time.deltaTime);
    }
}