using UnityEngine;

public class Level2Script : MonoBehaviour
{
    [SerializeField] private GameObject collectable;

    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.SetTarget(collectable.transform.position);
    }

    private void FixedUpdate()
    {
        if (collectable == null)
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