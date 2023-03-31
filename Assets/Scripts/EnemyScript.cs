using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health = 100;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float missileLifetime = 10f;
    [SerializeField] private float missileSpeed = 10f;
    [SerializeField] private float fireInterval = 5f;
    [SerializeField] private float moveDelay = 5f;

    [SerializeField] private GameObject explosionPrefab;

    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    private bool _canMove = true;
    
    private GameObject _missile;

    private Slider healthBar;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
        
        healthBar = gameObject.transform.Find("HealthBar").Find("Canvas").Find("Slider").GetComponent<Slider>();
        healthBar.gameObject.SetActive(false);
        
        transform.position = _playerTransform.position + _playerTransform.forward * 10f;
    }
    

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 direction = _playerTransform.position - transform.position;
        if (_canMove)
        {
            _rigidbody.velocity = direction.normalized * moveSpeed;
        }
        transform.LookAt(_playerTransform);
    }

    private IEnumerator MoveDelayCoroutine()
    {
        _canMove = false;
        yield return new WaitForSeconds(moveDelay);
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (Time.time % fireInterval < Time.fixedDeltaTime)
        {
            FireMissile();
            StartCoroutine(MoveDelayCoroutine());
        }
    }

    private void FireMissile()
    {
        _missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        
        _missile.transform.SetParent(transform);
        
        Rigidbody missileRigidbody = _missile.GetComponent<Rigidbody>();
        missileRigidbody.velocity = transform.forward * missileSpeed;
        
        StartCoroutine(DestroyAfterTime(_missile, missileLifetime));
    }
    
    private IEnumerator DestroyAfterTime(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
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
            
            health -= 25;

            if (health <= 0)
            {
                Explode(collision.contacts[0].point);
            }
            
            healthBar.value = health;
        }
    }
}