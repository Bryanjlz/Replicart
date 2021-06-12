using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDragController : Draggable
{
    [Header("The below are probably set automagically")]
    public Bounds validSpace;
    public DraggableProducer source;

    protected override void Release() {
        if (!validSpace.Intersects(GetComponent<BoxCollider2D>().bounds)) {
            print("Aw no");
            source.amount ++;
            Destroy(this.gameObject);
        } else {
            print(GetComponent<BoxCollider2D>().bounds);
            print(validSpace);
        }

        map.AddGroup(gameObject);
        isBeingHeld = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
        }
    }
}

//-1.205835
//0.1777401
//13.37845
//8.355679