using System.Collections;
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
    public Camera cam;

    //public List<Scene> levels;
    public int levelIndexStart, levelIndexEnd;
    List<string> levelNames;

    //Mock values
    [Header("Debug, do not edit.")]
    public int pageNumber = 0;
    public int levelCount;

    // Start is called before the first frame update
    void Start()
    {
        Color[] colours = { new Color(224 / 255f, 166 / 255f, 175 / 255f), new Color(92 / 255f, 188 / 255f, 205 / 255f), 
                            new Color (138/255f, 198/255f, 132/255f), new Color (219/255f, 199/255f, 156/255f)};
        LoadLevels();
        cam.backgroundColor = colours[(int)(Random.value * 4)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevels() {
        //Get levels
        levelNames = new List<string>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            string currentScene = SceneUtility.GetScenePathByBuildIndex(i);
            currentScene = currentScene.Substring(currentScene.LastIndexOf("/") + 1, currentScene.LastIndexOf(".") - currentScene.LastIndexOf("/") - 1);
            if (!currentScene.Equals("Level Select") && !currentScene.Equals("Main Menu")) {
                levelNames.Add(currentScene);
            }
        }
        levelCount = levelNames.Count;

        int startIndex = pageNumber * (COLS * ROWS);
        foreach (Transform child in levelGridLayout) {
            Destroy(child.gameObject);
        }
        for (int i = startIndex; i < startIndex + COLS * ROWS; i++) {
            if (i < levelCount) {
                GameObject go = Instantiate(levelPrefab, levelGridLayout);
                go.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = string.Format("{0}", i + 1);
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
