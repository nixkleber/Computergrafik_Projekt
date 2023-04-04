using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardScript : MonoBehaviour
{
    [SerializeField] private float delayBetweenChars = 0.01f;

    private string storyText;

    private TextMeshProUGUI storyTextMeshProUGUI;
    private Button continueButton;
    
    private void Start()
    {
        SetUpStoryBoard();
    }

    private void SetUpStoryBoard()
    {
        //Time.timeScale = 0;

        storyTextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        storyTextMeshProUGUI.text = "";

        continueButton = GetComponentInChildren<Button>();
        continueButton.interactable = false;
    }

    public void ShowLevelStory(String story)
    {
        SetUpStoryBoard();

        storyText = story;
        StartCoroutine(DisplayText());
    }


    IEnumerator DisplayText()
    {
        if (storyTextMeshProUGUI == null)
        {
            yield break;
        }
        
        foreach (char c in storyText)
        {
            storyTextMeshProUGUI.text += c;
            yield return new WaitForSeconds(delayBetweenChars);
        }

        continueButton.interactable = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f; 
        
        FindObjectOfType<GameManager>().ActivateLevel();
        
        gameObject.SetActive(false);
    }
}