using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlPanelScript : MonoBehaviour
{
    private GameObject _turboBoost;
    private GameObject _missile;
    
    // Start is called before the first frame update
    void Start()
    {
        _turboBoost = transform.Find("TurboBoost").gameObject;
        _missile = transform.Find("Missile").gameObject;
    }

    // Update is called once per frame
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
    
    
}
