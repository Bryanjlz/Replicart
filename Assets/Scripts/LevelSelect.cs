﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{

    [Header("Set these in editor")]
    public int COLS, ROWS;
    public Transform levelGridLayout;
    public Button prevButton;
    public Button nextButton;
    public GameObject levelPrefab;
    public GameObject gridFillerPrefab;
    public TextMeshProUGUI titleText; 

    //public List<Scene> levels;
    public int levelIndexStart, levelIndexEnd;
    [Tooltip("I'm sorry")]
    public List<string> levelNames;

    //Mock values
    [Header("Debug, do not edit.")]
    public int pageNumber = 0;
    public int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        levelCount = levelIndexEnd - levelIndexStart + 1;
        LoadLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevels() {
        int startIndex = pageNumber * (COLS * ROWS);
        print(startIndex);
        foreach (Transform child in levelGridLayout) {
            Destroy(child.gameObject);
        }
        for (int i = startIndex; i < startIndex + COLS * ROWS; i++) {
            if (i < levelCount) {
                GameObject go = Instantiate(levelPrefab, levelGridLayout);
                go.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = string.Format("{0}", i + 1);
                go.GetComponent<LevelSelectButton>().loadIndex = i;
                go.GetComponent<LevelSelectButton>().manager = this;
                if (i < levelNames.Count) {
                    go.GetComponent<LevelSelectButton>().levelName = levelNames[i];
                } else {
                    go.GetComponent<LevelSelectButton>().levelName = "You didn't set enough level names. No seriously, you have to fix this.";
                }
            } else {
                Instantiate(gridFillerPrefab, levelGridLayout);
            }
        }

        prevButton.interactable = pageNumber > 0;
        nextButton.interactable = pageNumber < ((levelCount + COLS * ROWS - 1) / (COLS * ROWS) - 1);
    }

    public void Prev() {
        if (pageNumber > 0) {
            pageNumber--;
        }
        LoadLevels();
    }

    public void Next() {
        if (pageNumber < ((levelCount + COLS * ROWS - 1) / (COLS * ROWS) - 1)) {
            pageNumber++;
        }
        LoadLevels();
    }

    public void SetTitle(string text) {
        titleText.text = text;
    }
}