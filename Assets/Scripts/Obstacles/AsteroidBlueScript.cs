using UnityEngine;

public class AsteroidBlueScript : MonoBehaviour
{
    private Vector3 _randomRotation;
    private float _rotationRange;
    
    void Start()
    {
        _rotationRange = 0.1f;
        _randomRotation = new Vector3(Random.Range(-_rotationRange, _rotationRange), Random.Range(-_rotationRange, _rotationRange), Random.Range(-_rotationRange, _rotationRange));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_randomRotation);
    }
}
