using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    private GameObject _turboBoost;
    private GameObject _missile;
    private GameObject _levelInstruction;
    private Slider _healthBar;

    void Start()
    {
        _levelInstruction = transform.Find("LevelInstruction").gameObject;
        _turboBoost = transform.Find("TurboBoost").gameObject;
        _missile = transform.Find("Missile").gameObject;
        _healthBar = transform.Find("Stats").Find("Slider").gameObject.GetComponent<Slider>();
        
        _turboBoost.SetActive(false);
        _missile.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _missile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Fire missile";
            _missile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Release";
        }else if (Input.GetKeyUp(KeyCode.F))
        {
            _missile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Deploy missile";
            _missile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Hold";
        }
    }

    public void SetHealth(int health)
    {
        _healthBar.value = health;
    }

    public void SetLevelInstruction(String instruction)
    {
        _levelInstruction.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = instruction;
    }

    public void ShowTurboBoost()
    {
        _turboBoost.SetActive(true);
    }
    
    public void ShowMissile()
    {
        _missile.SetActive(true);
    }
    
    
}
