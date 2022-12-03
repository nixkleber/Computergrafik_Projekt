using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=kUXskc76ud8

public class SolarSystemScript : MonoBehaviour
{
    private float G = 100f;
    private GameObject[] _celestialBodies;

    // Start is called before the first frame update
    void Start()
    {
        _celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
        
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Gravity();

        foreach (GameObject planet in _celestialBodies)
        {
            planet.transform.Rotate(Vector3.up);
        }
    }

    void Gravity()
    {
        foreach (GameObject a in _celestialBodies)
        {
            foreach (GameObject b in _celestialBodies)
            {
                if (!a.Equals(b))
                {
                    float mass1 = a.GetComponent<Rigidbody>().mass;
                    float mass2 = b.GetComponent<Rigidbody>().mass;


                    float radius = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized *
                                                         (G * (mass1 * mass2) / (radius * radius)));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject a in _celestialBodies)
        {
            foreach (GameObject b in _celestialBodies)
            {
                if (!a.Equals(b))
                {
                    float mass2 = b.GetComponent<Rigidbody>().mass;
                    float radius = Vector3.Distance(a.transform.position, b.transform.position);
                    
                    a.transform.LookAt(b.transform);
                    
                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * mass2) / radius);
                }
            }
        }
    }
}