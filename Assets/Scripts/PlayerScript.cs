using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float boost = 10f;
    [SerializeField] private float turboBoost = 50f;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float glide = 1f;

    private Rigidbody _rigidbody;
    private List<GameObject> _boostParticles = new List<GameObject>();

    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileSpeed = 1f;
    [SerializeField] private float missileLifetime = 10f;
    [SerializeField] private Vector3 missileOffset = new Vector3(0, -0.2f, 0);

    private GameObject _missile;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.drag = glide;

        _boostParticles.AddRange(GameObject.FindGameObjectsWithTag("Boost"));

        DeactivateBoostParticles();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Instantiate the missile below the spaceship
            Vector3 missilePosition = transform.position - missileOffset;
            _missile = Instantiate(missilePrefab, missilePosition, Quaternion.identity);

            // Set the missile's parent to the spaceship
            _missile.transform.SetParent(transform);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            // Fire the missile in a straight line
            Rigidbody missileRigidbody = _missile.GetComponent<Rigidbody>();
            missileRigidbody.AddForce(transform.forward * missileSpeed, ForceMode.Impulse);

            // Remove the missile's parent to make it move independently
            _missile.transform.SetParent(null);

            // Destroy the missile after the lifetime expires
            StartCoroutine(DestroyAfterTime(_missile, missileLifetime));
        }
    }


    private void FixedUpdate()
    {
        Turn();
        Move();

        if ((_missile != null) && _missile.transform.IsChildOf(transform))
        {
            _missile.transform.localPosition = missileOffset;
            _missile.transform.localRotation = Quaternion.identity;

            Rigidbody missileRigidbody = _missile.GetComponent<Rigidbody>();
            missileRigidbody.velocity = _rigidbody.velocity;
        }
    }

    private IEnumerator DestroyAfterTime(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
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
        bool isBoosting = Input.GetKey(KeyCode.Space);
        bool isTurboBoosting = Input.GetKey(KeyCode.LeftShift);

        if (isBoosting)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * boost);
            ActivateBoostParticles();
        }
        else if (isTurboBoosting)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * boost * turboBoost);
            ActivateTurboBoostParticles();
        }
        else
        {
            DeactivateBoostParticles();
        }
    }

    private void ActivateBoostParticles()
    {
        foreach (GameObject boostParticle in _boostParticles)
        {
            boostParticle.GetComponent<VisualEffect>().SetVector3("Velocity", new Vector3(0, 0.02f, 0));
            boostParticle.GetComponent<VisualEffect>().SetInt("SpawnRate", 32);
            boostParticle.GetComponent<VisualEffect>().SendEvent("onPlay");
        }
    }

    private void ActivateTurboBoostParticles()
    {
        foreach (GameObject boostParticle in _boostParticles)
        {
            boostParticle.GetComponent<VisualEffect>().SetVector3("Velocity", new Vector3(0, 0.10f, 0));
            boostParticle.GetComponent<VisualEffect>().SetInt("SpawnRate", 64);
            boostParticle.GetComponent<VisualEffect>().SendEvent("onPlay");
        }
    }

    private void DeactivateBoostParticles()
    {
        foreach (GameObject boostParticle in _boostParticles)
        {
            boostParticle.GetComponent<VisualEffect>().SendEvent("onStop");
        }
    }
}