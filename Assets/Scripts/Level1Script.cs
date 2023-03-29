using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;

    private int _currentRingIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        DisableAllRings();
        EnableRing(_currentRingIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject ring = GameObject.FindWithTag("Ring");

        if (ring == null)
        {
            _currentRingIndex++;
            EnableRing(_currentRingIndex);
        }
    }

    private void DisableAllRings()
    {
        foreach (var ring in rings)
        {
            ring.SetActive(false);
        }
    }
    
    // Method to enable a ring at a given index
    private void EnableRing(int index)
    {
        rings[index].SetActive(true);
    }

}
