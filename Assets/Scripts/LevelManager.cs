using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [Header("Set in Editor (UI)")]
    public GameObject pauseMenu;
    public GameObject winScreen;
    public List<Button> buttons;
    [Header("Set in Editor (In game)")]
    public Map map;
    public ExpectedResult expectedSolution;
    
    [Header("Internal Values")]
    public bool isPaused;
    public bool winningState;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        winningState = false;
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
    }

    public void ShowPauseMenu() {
        // Only display the menu for now
        if (!winningState) {
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
        isPaused = false;
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
