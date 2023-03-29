using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlanetScript : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject explosionPrefab;
    
    private Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.transform.Find("HealthBar").Find("Canvas").Find("Slider").GetComponent<Slider>();
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void Explode(Vector3 location)
    {
        GameObject explosion = Instantiate(explosionPrefab, location, Quaternion.identity);

        Destroy(explosion, 5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            healthBar.gameObject.SetActive(true);
            
            Debug.Log("HIT BY MISSILE");
            health -= 10;

            if (health <= 0)
            {
                Explode(collision.contacts[0].point);
            }
            
            healthBar.value = health;
        }
    }
}
