﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Set in Editor")]
    public GameObject pauseMenu;
    public GameObject winScreen;
    
    [Header("Internal Values")]
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPauseMenu() {
        // Only display the menu for now
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Win() {
        winScreen.SetActive(true);
    }

    public void Replay() {
        
    }

    public void Lose() {

    }
}
