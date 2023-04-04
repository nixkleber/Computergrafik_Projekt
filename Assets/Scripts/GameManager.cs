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
        {0, "Not a long time ago in a galaxy close to you... an alien species arrived to take over your home planet Earth. You found an ancient space ship in the basement of Berghain and are the chosen one to defend humanity from the invaders! \nFly through the magic ring to start your adventure..."},
        {1, "Your ship is equipped with a booster that will only allow you to travel slowly. To enhance the engine of your ship and be able traverse the solar system quickly you have to harvest the energy of all magic rings... but beware the obstacles ahead!"},
        {2, "The engine of your ship is now equipped with a powerful engine!\nFor defending your home planet against the invading enemy you're still missing a weapon. A legend says that there's an ancient artifact in the nebula some light minutes away...\nPress Left Shift to activate the Turbo Boost. "},
        {3, "You are now equipped with a powerful weapon! The invading aliens set up their base on Jupiter, while sending the first waves of attack to planet Earth! You see a chance in beating them... by sacrifycing Jupiter and end the alien invasion once and for all... \nHold F to deploy a missile and release F for firing it."},
        {4, "Jupiter is destroyed and you see yourself victorious...but you get a message transmitted via com: \n'One enemy ship left jupiter before the bombing and survived! Destroy it to free our solar system from the aliens!'" },
        {5, "You won the epic battle and your people are free and will live in peace forever!"},

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
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[0]);
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
            case 5:
                storyBoard.SetActive(true);
                storyBoard.GetComponent<StoryBoardScript>().ShowLevelStory(levelStories[5]);
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("You won! You can discover the solar system freely now!");
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