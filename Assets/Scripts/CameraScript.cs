using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float rotationSlerp = 1f;

    private Vector3 _offsetVector;

    void Start()
    {
        _offsetVector = transform.position - target.transform.localPosition;
    }
    
    void Update()
    {
        Vector3 rotationOffset = target.rotation * _offsetVector;

        transform.position = target.position + rotationOffset;

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSlerp);
    }
}