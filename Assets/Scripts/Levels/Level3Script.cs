using UnityEngine;

public class Level3Script : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private GameObject _jupiter;
    
    void Start()
    {
        _jupiter = GameObject.Find("Jupiter");
        
    }

    private void FixedUpdate()
    {
        if (_jupiter == null)
        {
            uiManager.SetTarget(Vector3.zero);
            gameObject.SetActive(false);
            
            Invoke("LevelComplete", 3f);
        }
        else
        {
            uiManager.SetTarget(_jupiter.transform.position);
        }
    }

    private void LevelComplete()
    {
        FindObjectOfType<GameManager>().LevelComplete();
    }
}
