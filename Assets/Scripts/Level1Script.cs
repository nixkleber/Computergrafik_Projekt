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

        if (ring == null)
        {
            _currentRingIndex++;

            if (_currentRingIndex < rings.Length)
            {
                EnableRing(_currentRingIndex);
            }
            else
            {
                LevelComplete();
            }
        }
    }

    private void LevelComplete()
    {
        uiManager.SetTarget(Vector3.zero);
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Level1Complete();
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