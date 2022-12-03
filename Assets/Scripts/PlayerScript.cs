using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float boost = 10;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float glide = 1f;

    private Rigidbody _rigidbody;

    private GameObject[] boostParticles;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.drag = glide;

        boostParticles = GameObject.FindGameObjectsWithTag("Boost");

        foreach (GameObject boostParticle in boostParticles)
        {
            boostParticle.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Turn();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void Turn()
    {
        float rotationHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        float rotationVertical = Input.GetAxis("Vertical") * Time.deltaTime * rotationSpeed;
        float rolling = Input.GetAxis("Rotate") * Time.deltaTime * rotationSpeed;
        
        transform.Rotate(rotationVertical, rotationHorizontal, rolling);
    }

    private void Move()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.forward * boost);

            foreach (GameObject boostParticle in boostParticles)
            {
                boostParticle.SetActive(true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _rigidbody.AddRelativeForce(Vector3.forward * boost * 50);
            
            foreach (GameObject boostParticle in boostParticles)
            {
                // VFXExposedProperty.
                
                boostParticle.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject boostParticle in boostParticles)
            {
                boostParticle.SetActive(false);
            }
        }
    }
    
    
    
    public static GameObject[] FindGameObjectInChildWithTag (GameObject parent, string tag)
    {
        GameObject[] childrenWithTag = new GameObject[] { };
        
        Transform t = parent.transform;
 
        for (int i = 0; i < t.childCount; i++) 
        {
            if(t.GetChild(i).gameObject.tag == tag)
            {
                childrenWithTag.Append(t.GetChild(i).gameObject);
            }
                 
        }
             
        return childrenWithTag;
    }

}