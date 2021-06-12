using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableProducer : MonoBehaviour
{
    public GameObject produce;
    public int amount;
    public Bounds validSpace;

    public Map map;
    public Transform playerGroupParent;

    private void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        playerGroupParent = GameObject.Find("Player Groups").transform;
    }

    public void Generate() {
        if (amount > 0) {
            int mousePosX = (int) (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x;
            int mousePosY = (int) (Camera.main.ScreenToWorldPoint(Input.mousePosition)).y;
            GameObject go = Instantiate(produce, new Vector3(mousePosX, mousePosY, 0), Quaternion.identity, playerGroupParent);
            map.AddGroup(go);
            // LOL
            go.GetComponent<BlockDragController>().PickUp();
            go.GetComponent<BlockDragController>().validSpace = validSpace;
            go.GetComponent<BlockDragController>().source = this;
            amount --;
        }
    }
}