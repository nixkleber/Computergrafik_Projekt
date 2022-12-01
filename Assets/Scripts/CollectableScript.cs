using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(1.0f, 1.0f, (Time.deltaTime * rotationSpeed));

        GameObject particles = transform.GetChild(4).gameObject;
        particles.transform.rotation = Quaternion.Euler(1.0f, 1.0f, 1.0f);
    }
}