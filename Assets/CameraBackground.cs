using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour
{
    [Header("Choose your colour here")]
    public Colour colour;

    [Header("Background Colours")]
    public Color blue;
    public Color red;
    public Color yellow;
    public Color green;

    [Header("Frame Colours")]
    public Color blueF;
    public Color redF;
    public Color yellowF;
    public Color greenF;

    [Header("Add in Inspector")]
    public GameObject frame;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        switch (colour) {
            case Colour.BLUE:
                cam.backgroundColor = blue;
                ChangeFrame(blueF);
                break;
            case Colour.RED:
                cam.backgroundColor = red;
                ChangeFrame(redF);
                break;
            case Colour.YELLOW:
                cam.backgroundColor = yellow;
                ChangeFrame(yellowF);
                break;
            case Colour.GREEN:
                cam.backgroundColor = green;
                ChangeFrame(greenF);
                break;
        }
    }

    void ChangeFrame (Color c) {
        foreach (Transform t in frame.transform) {
            t.gameObject.GetComponent<SpriteRenderer>().color = c;
        }
    }
}
