using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    private float mousePosX;
    private float mousePosY;
    private float displacementX;
    private float displacementY;
    public bool isBeingHeld = false;

    private void OnMouseDown()
    {
        PickUp();
    }

    private void OnMouseUp()
    {
        Release();
    }

    private void Update()
    {
        mousePosX = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        mousePosY = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;
        if (isBeingHeld == true)
        {
            this.gameObject.transform.localPosition = new Vector3((int) (mousePosX + displacementX), (int) (mousePosY + displacementY), 0);
            //This makes no sense lol
            if (Input.GetMouseButtonUp(0)) {
                Release();
            }
        } 
        
    }

    public void PickUp() {
        isBeingHeld = true;
        displacementX = this.gameObject.transform.position.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        displacementY = this.gameObject.transform.position.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;

        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.3f);
        }
    }

    protected virtual void Release() {
        isBeingHeld = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
        }
    }
}
