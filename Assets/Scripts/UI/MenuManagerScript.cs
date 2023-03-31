using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
        Debug.Log("Game restarted");
    }

    public void onRestartButtonClick()
    {
        RestartGame();
    }

    public void onResumeButtonClick()
    {
        
        ResumeGame();
    }
}