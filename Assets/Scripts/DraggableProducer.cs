using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableProducer : MonoBehaviour
{
    public GameObject produce;
    public int amount;
    public Bounds validSpace;

    public Map map;

    private void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
    }

    public void Generate() {
        if (amount > 0) {
            float mousePosX = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
            float mousePosY = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;
            GameObject go = Instantiate(produce, new Vector3((int) mousePosX, (int) mousePosY, 0), Quaternion.identity);
            map.AddGroup(go);
            // LOL
            go.GetComponent<BlockDragController>().PickUp();
            go.GetComponent<BlockDragController>().validSpace = validSpace;
            go.GetComponent<BlockDragController>().source = this;
            amount --;
        }
    }
}