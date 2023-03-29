using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;

    [SerializeField] private UIManager uiManager;
    
    private int _currentRingIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        DisableAllRings();
        EnableRing(_currentRingIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject ring = GameObject.FindWithTag("Ring");

        if (_currentRingIndex == rings.Length)
        {
            uiManager.SetTarget(Vector3.zero);
            gameObject.SetActive(false);
            
            FindObjectOfType<GameManager>().Level1Complete();
        }
        else if (ring == null)
        {
            _currentRingIndex++;
            EnableRing(_currentRingIndex);
        }
    }

    private void DisableAllRings()
    {
        foreach (var ring in rings)
        {
            ring.SetActive(false);
        }
    }
    
    // Method to enable a ring at a given index
    private void EnableRing(int index)
    {
        rings[index].SetActive(true);
        uiManager.SetTarget(rings[index].transform.position);
    }

}
