using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [SerializeField] private float boost = 1f;
    [SerializeField] private float turboBoost = 500f;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float glide = 1f;

    private Rigidbody _rigidbody;
    private List<GameObject> _boostParticles = new List<GameObject>();

    [SerializeField] private GameObject ringAuraPrefab;
    [SerializeField] private GameObject healthDecreasePrefab;
    [SerializeField] private GameObject healthIncreasePrefab;
    [SerializeField] private GameObject upgradePrefab;

    [SerializeField] private bool missileActive = false;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileInitialVelocity = 1f;
    [SerializeField] private float missileLifetime = 20f;
    [SerializeField] private Vector3 missileOffset = new(0, -0.2f, 0);

    [SerializeField] private bool turboBoostActive;

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
        if (missileActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                DeployMissile();
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                FireMissile();
            }
        }
    }

    private void DeployMissile()
    {
        Vector3 missilePosition = transform.position - missileOffset;
        _missile = Instantiate(missilePrefab, missilePosition, Quaternion.identity);

        _missile.transform.SetParent(transform);
    }

    private void FireMissile()
    {
        _missile.transform.GetComponent<MissileScript>().PlayLaunchSound();

        Rigidbody missileRigidbody = _missile.GetComponent<Rigidbody>();
        missileRigidbody.AddForce(transform.forward * missileInitialVelocity, ForceMode.Impulse);

        _missile.transform.SetParent(null);

        StartCoroutine(DestroyAfterTime(_missile, missileLifetime));
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
        float rotationHorizontal = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * rotationSpeed;
        float rotationVertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime * rotationSpeed;
        float rolling = Input.GetAxis("Rotate") * Time.fixedDeltaTime * rotationSpeed;

        transform.Rotate(rotationVertical, rotationHorizontal, rolling);
    }

    private void Move()
    {
        bool isBoosting = Input.GetKey(KeyCode.Space);
        bool isTurboBoosting = Input.GetKey(KeyCode.LeftShift);

        if (isBoosting)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * boost);
            ActivateBoostParticles(false);
        }
        else if (isTurboBoosting && turboBoostActive)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * turboBoost);
            ActivateBoostParticles(true);
        }
        else
        {
            DeactivateBoostParticles();
        }
    }

    public void ActivateMissiles()
    {
        missileActive = true;
    }

    public void ActivateTurboBoost()
    {
        turboBoostActive = true;
    }

    private void ActivateBoostParticles(bool turbo)
    {
        foreach (GameObject boostParticle in _boostParticles)
        {
            VisualEffect vfx = boostParticle.GetComponent<VisualEffect>();

            if (turbo)
            {
                vfx.SetVector3("Velocity", new Vector3(0, 0.10f, 0));
                vfx.GetComponent<VisualEffect>().SetInt("SpawnRate", 64);
            }
            else
            {
                vfx.SetVector3("Velocity", new Vector3(0, 0.03f, 0));
                vfx.GetComponent<VisualEffect>().SetInt("SpawnRate", 32);
            }

            vfx.GetComponent<VisualEffect>().SendEvent("onPlay");
        }
    }

    private void DeactivateBoostParticles()
    {
        foreach (GameObject boostParticle in _boostParticles)
        {
            boostParticle.GetComponent<VisualEffect>().SendEvent("onStop");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ring"))
        {
            collider.gameObject.SetActive(false);

            GameObject ringAura = Instantiate(ringAuraPrefab, transform.position, transform.rotation);
            ringAura.transform.SetParent(transform);

            Destroy(ringAura, 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            health -= 5;
            FindObjectOfType<ControlPanelScript>().SetHealth(health);
            GameObject healthDecreaseAura = Instantiate(healthDecreasePrefab, transform.position, transform.rotation);
            healthDecreaseAura.transform.SetParent(transform);

            Destroy(healthDecreaseAura, 3f);
        }
        else if (collision.gameObject.CompareTag("CelestialBody"))
        {
            health -= 10;
            FindObjectOfType<ControlPanelScript>().SetHealth(health);
            GameObject healthDecreaseAura = Instantiate(healthDecreasePrefab, transform.position, transform.rotation);
            healthDecreaseAura.transform.SetParent(transform);

            Destroy(healthDecreaseAura, 3f);
        }
        else if (collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.SetActive(false);
            
            GameObject upgradeAura = Instantiate(upgradePrefab, transform.position, transform.rotation);
            upgradeAura.transform.SetParent(transform);

            Destroy(upgradeAura, 3f);
        }
    }
}