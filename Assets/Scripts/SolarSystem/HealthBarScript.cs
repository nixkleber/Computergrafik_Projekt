using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(_camera.transform.position);
    }
}
