using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float boost = 10;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float glide = 1f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.drag = glide;
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
            //transform.Translate(Vector3.forward * (Time.deltaTime * movingSpeed));
            _rigidbody.AddRelativeForce(Vector3.forward * boost);
        }
        else
        {
            
        }
    }
}