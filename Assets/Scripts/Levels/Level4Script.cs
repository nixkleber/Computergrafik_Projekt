
using UnityEngine;

public class Level4Script : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private GameObject _enemy;
    
    void Start()
    {
        _enemy = GameObject.Find("Enemy");
        
    }

    private void FixedUpdate()
    {
        if (_enemy == null)
        {
            uiManager.SetTarget(Vector3.zero);
            gameObject.SetActive(false);
            
            Invoke("LevelComplete", 3f);
        }
        else
        {
            uiManager.SetTarget(_enemy.transform.position);
        }
    }

    private void LevelComplete()
    {
        FindObjectOfType<GameManager>().LevelComplete();
    }
}
