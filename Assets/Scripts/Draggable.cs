using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    private float mousePosX;
    private float mousePosY;
    private float displacementX;
    private float displacementY;
    public bool isBeingHeld = false;
    public bool isHighlighted = false;

    [Header("Do not touch")]
    public Map map;

    private void Awake() {
        map = GameObject.Find("Map").GetComponent<Map>();
    }

    private void Update()
    {
        mousePosX = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        mousePosY = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;

        if (isBeingHeld == true)
        {
            int xMove = (int)(mousePosX + displacementX);
            int yMove = (int)(mousePosY + displacementY);
            if (xMove != gameObject.transform.localPosition.x || yMove != gameObject.transform.localPosition.y) {
                map.RemoveGroup(gameObject);
                gameObject.transform.localPosition = new Vector3(xMove, yMove, 0);
                map.AddGroup(gameObject);

            }
            //This makes no sense lol
            if (Input.GetMouseButtonUp(0)) {
                Release();
            }
        } 
        
    }

    public void PickUp() {

        isBeingHeld = true;
        displacementX = gameObject.transform.position.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        displacementY = gameObject.transform.position.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;
        print((int)(Camera.main.ScreenToWorldPoint(Input.mousePosition)).x + " " + (int)(Camera.main.ScreenToWorldPoint(Input.mousePosition)).y);


        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.6f);
        }

        map.RemoveGroup(gameObject);
        map.AddGroup(gameObject);
    }

    public void Highlight()
    {
        if (Input.GetMouseButton(0) == false) { 
            foreach (Transform bt in transform) {
                Color current = bt.GetComponent<SpriteRenderer>().color;
                bt.GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.6f);
            }
        }
    }

    public void LowLight() {
        if (Input.GetMouseButton(0) == false) {
            foreach (Transform bt in transform)
            {
                Color current = bt.GetComponent<SpriteRenderer>().color;
                bt.GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
            }
        }
    }

    public virtual void Release() {
        isBeingHeld = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
        }
    }
}
