using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableProducer : MonoBehaviour
{
    public GameObject previewBlockPrefab;
    public GameObject produce;
    public int amount;
    public Bounds validSpace;

    public Map map;
    public Transform playerGroupParent;

    private const float DIMENSIONS = 50f;

    private void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        playerGroupParent = GameObject.Find("Player Groups").transform;

        // Get hitbox to see the size of the group
        BoxCollider2D hitBox = produce.GetComponent<BoxCollider2D>();
        float xGrid = hitBox.size.x;
        float yGrid = hitBox.size.y;

        // Instantiate arr to mimic how we used map (added space around to not deal with out of bounds)
        GameObject[][] blocks = new GameObject[(int)xGrid + 2][];
        for (int i = 0; i < blocks.Length; i++) {
            blocks[i] = new GameObject[(int)yGrid + 2];
        }

        // Dimensions of one preview block
        float ratioDimension = 12.5f;

        // Gets the minimum x and y values for the position of the gameobjects, used to translate the coordinates to array indices
        float xMin = 100000;
        float yMin = 100000;
        foreach (Transform bt in produce.transform) {
            if (bt.position.x < xMin) {
                xMin = bt.position.x;
            }
            if (bt.position.y < yMin) {
                yMin = bt.position.y;
            }
        }

        // coordinates for the bottom left corner
        float xStart = -(xGrid / 2) * ratioDimension + ratioDimension / 2;
        float yStart = -(yGrid / 2) * ratioDimension + ratioDimension / 2;

        foreach (Transform bt in produce.transform) {
            // Actual position of the preview block
            float xPos = xStart + ratioDimension * (bt.position.x - xMin);
            float yPos = yStart + ratioDimension * (bt.position.y - yMin);

            // Instantiate
            GameObject newBlock = Instantiate(previewBlockPrefab);

            // Add to pseudo-map
            blocks[(int)(bt.position.x - xMin + 1)][(int)(bt.position.y - yMin + 1)] = newBlock;

            // Edit the transform for scale and position
            RectTransform rt = newBlock.GetComponent<RectTransform>();
            rt.SetParent(gameObject.transform);
            rt.sizeDelta = new Vector2(ratioDimension, ratioDimension);
            rt.anchoredPosition = new Vector2(xPos, yPos);
            rt.localScale = new Vector3(1f, 1f, 1f);

            // Add colour to the preview block
            newBlock.GetComponent<PreviewBlock>().colour = bt.gameObject.GetComponent<Block>().colour;
        }

        // Gets the right sprite through using the pseudo map
        for (int x = 1; x < blocks.Length - 1; x++) {
            for (int y = 1; y < blocks[0].Length - 1; y++) {
                if (blocks[x][y] != null) {
                    string spriteType = "";
                    if (blocks[x - 1][y] != null && blocks[x - 1][y + 1] != null && blocks[x][y + 1] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x][y + 1] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x][y + 1] != null && blocks[x + 1][y + 1] != null && blocks[x + 1][y] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x + 1][y] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x + 1][y] != null && blocks[x + 1][y - 1] != null && blocks[x][y - 1] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x][y - 1] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x][y - 1] != null && blocks[x - 1][y - 1] != null && blocks[x - 1][y] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }
                    if (blocks[x - 1][y] != null) {
                        spriteType += "0";
                    } else {
                        spriteType += "1";
                    }

                    // Set new sprite
                    blocks[x][y].GetComponent<Image>().sprite = map.spriteTrees[(int)blocks[x][y].GetComponent<PreviewBlock>().colour].Get(spriteType);
                }
            }
        }
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