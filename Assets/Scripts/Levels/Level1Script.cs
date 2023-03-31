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
                GameObject.Find("Player").GetComponent<PlayerScript>().ShowUpgradeAura();
        
                uiManager.SetTarget(Vector3.zero);
                gameObject.SetActive(false);

                Invoke("LevelComplete", 3f);
            }
        }
    }

    private void LevelComplete()
    {
        FindObjectOfType<GameManager>().LevelComplete();
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