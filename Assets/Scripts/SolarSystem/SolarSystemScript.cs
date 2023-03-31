using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=kUXskc76ud8

public class SolarSystemScript : MonoBehaviour
{
    private float G = 100f;
    private List<GameObject> _celestialBodies;

    [SerializeField] private float sizeMultiplier = 10;
    [SerializeField] private float positionMultiplier = 1000;
    [SerializeField] private float positionOffset = 100;

    private void Start()
    {
        _celestialBodies = new List<GameObject>(GameObject.FindGameObjectsWithTag("CelestialBody"));

        InitializePlanets();
    
        InitialVelocity();
    }
    
    private void FixedUpdate()
    {
        for (int i = _celestialBodies.Count - 1; i >= 0; i--)
        {
            GameObject planet = _celestialBodies[i];

            if (planet == null)
            {
                _celestialBodies.RemoveAt(i);
            }
            else
            {
                planet.transform.Rotate(Vector3.up);
            }
        }
        
        Gravity();
    }

    private readonly Dictionary<string, (float scale, Vector3 position, float mass)> _planetData = new ()
    {
        {"Mercury", (0.383f, new Vector3(0.39f,0,0), 0.055f)},
        {"Venus", (0.949f, new Vector3(0.72f,0,0), 0.815f)},
        {"Earth", (1f, new Vector3(1f,0,0), 1f)},
        {"Moon", (0.0123f, new Vector3(1.0027f, 0, 0), 0.073f)},
        {"Mars", (0.532f, new Vector3(1.52f,0,0), 0.107f)},
        {"Jupiter", (11.209f, new Vector3(5.20f,0,0), 318f)},
        {"Saturn", (9.449f, new Vector3(9.54f,0,0), 95.2f)},
        {"Uranus", (4.007f, new Vector3(19.18f,0,0), 14.5f)},
        {"Neptune", (3.883f, new Vector3(30.07f,0,0), 17.1f)}
    };

    private void InitializePlanets()
    {
        foreach (GameObject celestialBody in _celestialBodies)
        {
            if (_planetData.TryGetValue(celestialBody.transform.name, out var data))
            {
                SetSizeAndLocationOfPlanet(celestialBody, data.scale, data.position);
                celestialBody.GetComponent<Rigidbody>().mass = data.mass;
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