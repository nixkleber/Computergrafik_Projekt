using UnityEngine;

public class Level0Script : MonoBehaviour
{
    [SerializeField] private GameObject ring;

    [SerializeField] private UIManager uiManager;

    void Start()
    {
        uiManager.SetTarget(ring.transform.position);
    }

    private void FixedUpdate()
    {
        if (!ring.activeSelf)
        {
            LevelComplete();
        }
    }
    
    private void LevelComplete()
    {
        uiManager.SetTarget(Vector3.zero);
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().LevelComplete();
    }
}