using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject level0;
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    private GameObject[] _levels;
    
    [SerializeField] private int currentLevel = 0;

    [SerializeField] private GameObject controlManager;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _levels = new[] { level0, level1, level2, level3 };
        
        LoadLevel(currentLevel);
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
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Fly through the ring to start level 1!");
                level0.SetActive(true);
                break;
            case 1:
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 1: Fly through all the rings!");
                level1.SetActive(true);
                break;
            case 2:
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 2: Collect the weapon!");
                
                controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
                player.GetComponent<PlayerScript>().ActivateTurboBoost();
                
                level2.SetActive(true);
                break;
            case 3:
                controlManager.GetComponent<ControlPanelScript>().SetLevelInstruction("Level 3: Destroy jupiter!");

                controlManager.GetComponent<ControlPanelScript>().ShowTurboBoost();
                player.GetComponent<PlayerScript>().ActivateTurboBoost();
                
                controlManager.GetComponent<ControlPanelScript>().ShowMissile();
                player.GetComponent<PlayerScript>().ActivateMissiles();
                
                level3.SetActive(true);
                break;

        }
    }

    public void LevelComplete()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    
}
