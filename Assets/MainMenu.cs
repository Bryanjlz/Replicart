using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Map map;
    [SerializeField]
    Transform blockParent;
    [SerializeField]
    GameObject blockPrefab;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float blockSpawnChance;

    [SerializeField]
    bool delete;
    [SerializeField]
    bool gen;

    private int width = 16;
    private int height = 6;

    // Start is called before the first frame update
    void Start()
    {
        map.AddGroup(blockParent.gameObject);
    }

    void GenerateBlocks () {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                float rand = Random.value;
                if (rand < blockSpawnChance) {
                    Vector2 pos = new Vector2(x, y);
                    GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity, blockParent);
                    block.GetComponent<Block>().colour = (Colour)(int)(Random.value * 4);
                    block.GetComponent<BoxCollider2D>().enabled = false;
                    block.layer = 2;
                }
            }
        }

        map.AddGroup(blockParent.gameObject);
    }

    void RemoveBlocks () {
        map.RemoveGroup(blockParent.gameObject);
        foreach (Transform t in blockParent) {
            Destroy(t.gameObject);
        }
    }


}
