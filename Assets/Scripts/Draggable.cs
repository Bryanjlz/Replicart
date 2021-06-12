using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    private float mousePosX;
    private float mousePosY;
    private float displacementX;
    private float displacementY;
    public bool isBeingHeld = false;

    [Header("Do not touch")]
    public Map map;

    private void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        print(map);
        if (map == null) {
            print("WHAT THE HELL");
        }
    }

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

    public void FirstPickUp() {
        isBeingHeld = true;
        displacementX = gameObject.transform.position.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        displacementY = gameObject.transform.position.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;


        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.3f);
        }
    }

    public void PickUp() {
        map.RemoveGroup(gameObject);
        isBeingHeld = true;
        displacementX = gameObject.transform.position.x - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
        displacementY = gameObject.transform.position.y - (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;


        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 0.3f);
        }
    }

    protected virtual void Release() {
        map.AddGroup(gameObject);
        isBeingHeld = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
        }
    }
}
