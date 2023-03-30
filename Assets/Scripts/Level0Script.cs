using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Script : MonoBehaviour
{
    [SerializeField] private GameObject ring;

    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.SetTarget(ring.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
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