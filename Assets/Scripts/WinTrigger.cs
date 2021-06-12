using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public LevelManager manager;

    void OnTriggerEnter2D(Collider2D other) {
        // Let's assume it's only the robot boi
        manager.Win();
    }
}
