using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(10,10,0)*Time.deltaTime);
        
    }
}
