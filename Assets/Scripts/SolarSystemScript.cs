using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// https://www.youtube.com/watch?v=kUXskc76ud8

public class SolarSystemScript : MonoBehaviour
{
    private float G = 100f;
    private GameObject[] _celestialBodies;

    [SerializeField] private float sizeMultiplier = 10;
    [SerializeField] private float positionMultiplier = 1000;
    [SerializeField] private float positionOffset = 100;

    // Start is called before the first frame update
    private void Start()
    {
        _celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");

        InitializePlanets();
    
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

    private readonly Dictionary<string, (float, Vector3)> _planetData = new Dictionary<string, (float, Vector3)>()
    {
        {"Mercury", (0.383f, new Vector3(0.39f,0,0))},
        {"Venus", (0.949f, new Vector3(0.72f,0,0))},
        {"Earth", (1f, new Vector3(1f,0,0))},
        {"Mars", (0.532f, new Vector3(1.52f,0,0))},
        {"Jupiter", (11.209f, new Vector3(5.20f,0,0))},
        {"Saturn", (9.449f, new Vector3(9.54f,0,0))},
        {"Uranus", (4.007f, new Vector3(19.18f,0,0))},
        {"Neptune", (3.883f, new Vector3(30.07f,0,0))}
    };

    private void InitializePlanets()
    {
        foreach (GameObject celestialBody in _celestialBodies)
        {
            if (_planetData.TryGetValue(celestialBody.transform.name, out var data))
            {
                SetSizeAndLocationOfPlanet(celestialBody, data.Item1, data.Item2);
            }
        }
    }

    private void SetSizeAndLocationOfPlanet(GameObject celestialBody, float size, Vector3 position )
    {
        celestialBody.transform.localScale = new Vector3(size,size,size) * sizeMultiplier;
        celestialBody.transform.position = (position * positionMultiplier) + new Vector3(positionOffset,0,0);
    }

    private void Gravity()
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

    private void InitialVelocity()
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