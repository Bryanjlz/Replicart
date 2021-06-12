using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [Header("values set by scripts")]
    public string levelName;
    public LevelSelect manager;

    public void Load() {
        SceneManager.LoadScene(levelName);
    }

    public void SetTitle() {
        manager.SetTitle(levelName.ToUpperInvariant());
    }
}
