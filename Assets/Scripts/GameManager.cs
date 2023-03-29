using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    private GameObject[] _levels;
    
    [SerializeField] private int currentLevel = 1;

    [SerializeField] private GameObject controlManager;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _levels = new[] { level1, level2, level3 };
        
        LoadLevel(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void LoadLevel(int level)
    {
        foreach (GameObject levelObject in _levels)
        {
            levelObject.SetActive(false);
        }

        switch (level)
        {
            case 0:
                break;
            case 1:
                level1.SetActive(true);
                break;
            case 2:
                level2.SetActive(true);
                break;

        }
    }

    public void Level1Complete()
    {
        // Increase the current level and load the next level
        currentLevel++;
        LoadLevel(currentLevel);
        controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
        player.GetComponent<PlayerScript>().ActivateTurboBoost();
    }
    
}
