using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    Dictionary<Vector2, LinkedList<GameObject>> map;

    [SerializeField]
    GameObject LevelGroups;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddGroup (GameObject group) {
        foreach (Transform ct in group.transform) {
            Vector2 pos = new Vector2(ct.position.x, ct.position.y);
            if (map.ContainsKey(pos)) {
                map[pos].AddLast(ct.gameObject);
            } else {
                LinkedList<GameObject> blockLayers = new LinkedList<GameObject>();
                blockLayers.AddLast(ct.gameObject);
                map.Add(pos, blockLayers);
            }
        }
    }

    void RemoveGroup (GameObject group) {
        foreach (Transform ct in group.transform) {
            Vector2 pos = new Vector2(ct.position.x, ct.position.y);
            map[pos].Remove(ct.gameObject);
        }
    }
}
