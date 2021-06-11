using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void LoadInt(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void LoadString(string scene) {
        SceneManager.LoadScene(scene);
    }
}