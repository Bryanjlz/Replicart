using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{

    [Header("Set in Editor (UI)")]
    public GameObject pauseMenu;
    public GameObject winScreen;
    public List<Button> buttons;
    public TextMeshProUGUI previewPrompt;
    public TextMeshProUGUI levelTitle;
    public Button nextButton;
    [Header("Set in Editor (In game)")]
    public Map map;
    public ExpectedResult expectedSolution;
    public SpriteRenderer grid;
    
    [Header("Internal Values")]
    public bool isPaused;
    public bool winningState;
    public bool inPreviewMode;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        winningState = false;
        ShowLevelTitle();
    }

    void ShowLevelTitle () {
        int lvlNum = SceneManager.GetActiveScene().buildIndex - 1;
        string lvlName = SceneManager.GetActiveScene().name.ToUpperInvariant();
        levelTitle.text = lvlNum + ". " + lvlName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (isPaused) {
                HidePauseMenu();
            } else {
                ShowPauseMenu();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            EmphasizePreview();
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            DeEmphasizePreview();
        }
    }

    public void ShowPauseMenu() {
        // Only display the menu for now
        if (!winningState) {
            pauseMenu.SetActive(true);
            FindObjectOfType<AudioManager>().Jank();
            isPaused = true;
        }
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void EmphasizePreview() {
        inPreviewMode = true;
        grid.sortingLayerName = "Promoted Background";
        previewPrompt.text = "PREVIEW (SHIFT) ON";
        expectedSolution.EmphasizePreview();
    }

    public void DeEmphasizePreview() {
        inPreviewMode = false;
        grid.sortingLayerName = "Background";
        previewPrompt.text = "PREVIEW (SHIFT) OFF";
        expectedSolution.DeEmphasizePreview();
    }

    public void CheckWin() {
        if (expectedSolution.Match()) {
            winningState = true;
            StartCoroutine("WinInHalfSecond");
        }
    }

    public void Undo() {

    }

    public void Redo() {

    }

    public void Win() {
        FindObjectOfType<AudioManager>().Play("win");
        int next = SceneUtility.GetBuildIndexByScenePath(SceneManager.GetActiveScene().name) + 1;
        if (next == SceneManager.sceneCountInBuildSettings) {
            nextButton.interactable = false;
        }
        winScreen.SetActive(true);
    }

    public void Restart() {
        if (!winningState) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Lose() {

    }

    public void Click()
    {
        FindObjectOfType<AudioManager>().Jank();
    }

    public void NextLevel() 
    {
        int next = SceneUtility.GetBuildIndexByScenePath(SceneManager.GetActiveScene().name) + 1;
        if (next < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(next);
        }
    }

    IEnumerator WinInHalfSecond() {
        // Upon win, do not allow block movement
        foreach (BoxCollider2D collider in FindObjectsOfType<BoxCollider2D>()) {
            collider.enabled = false;
        }
        // Disable buttons
        foreach (Button b in buttons) {
            b.interactable = false;
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Win();
    }
}
