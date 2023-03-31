using UnityEngine;

public class MissileScript : MonoBehaviour
{
    [SerializeField] private AudioClip launchSound;

    [SerializeField] private GameObject explosionPrefab;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void FixedUpdate()
    {
        Accelerate();
    }

    private void Accelerate()
    {
        transform.GetComponent<Rigidbody>().AddForce(transform.forward * 3f, ForceMode.VelocityChange);
    }

    public void PlayLaunchSound()
    {
        audioSource.Play();
    }

    private void Explode(Vector3 location)
    {
        // Instantiate an explosion particle effect at the collision point
        GameObject explosion = Instantiate(explosionPrefab, location, Quaternion.identity);

        // Destroy the missile and explosion after a set amount of time
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.CompareTag("CelestialBody"))
        {
            Explode(collision.contacts[0].point);
        }
        else if (collisionObject.CompareTag("Player") && transform.parent == null)
        {
            Explode(collision.contacts[0].point);
        }else if (collisionObject.CompareTag("Enemy") && transform.parent == null)
        {
            Explode(collision.contacts[0].point);
        }
    }
}