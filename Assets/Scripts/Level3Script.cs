using System.Collections;
using System.Collections.Generic;
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
            LevelComplete();
        }
        else
        {
            uiManager.SetTarget(_jupiter.transform.position);
        }
    }

    private void LevelComplete()
    {
        uiManager.SetTarget(Vector3.zero);
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().LevelComplete();
    }
}
