using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject controlManager;
    [SerializeField] private GameObject storyBoard;
    [SerializeField] private GameObject player;
    [SerializeField] private int currentLevel = 0;
    
    Dictionary<int, string> levelStories = new ()
    {
        {1, "Story Level 1"},
        {2, "Story Level 2"},
        {3, "Story Level 3"},
        {4, "Story Level 4"},
        {5, "Story Level 5"},

    };

    private void Start()
    {
        LoadNextLevel();
    }
    
    private void LoadNextLevel()
    {
        foreach (GameObject levelObject in levels)
        {
            levelObject.SetActive(false);
        }

        switch (currentLevel)
        {
            case 0:
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Fly through the ring to start level 1!");
                break;
            case 1:
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[1]);
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 1: Fly through all the rings!");
                break;
            case 2:
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[2]);
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 2: Collect the weapon!");
                controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
                player.GetComponent<PlayerScript>().ActivateTurboBoost();
                break; 
            case 3:
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[3]);
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 3: Destroy Jupiter!");
                controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
                player.GetComponent<PlayerScript>().ActivateTurboBoost();
                controlManager.GetComponent<ControlPanelScript>().ShowMissile();
                player.GetComponent<PlayerScript>().ActivateMissiles();
                break;
            case 4:
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[4]);
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 4: Destroy the enemy!");
                controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
                player.GetComponent<PlayerScript>().ActivateTurboBoost();
                controlManager.GetComponent<ControlPanelScript>().ShowMissile();
                player.GetComponent<PlayerScript>().ActivateMissiles();
                break;
        }
        
    }

    public void ActivateLevel()
    {
        levels[currentLevel].SetActive(true);
    }

    public void LevelComplete()
    {
        currentLevel++;
        LoadNextLevel();
    }
}