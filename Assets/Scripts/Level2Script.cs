using System.Collections;
using System.Collections.Generic;
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
        if (!collectable.activeSelf)
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
