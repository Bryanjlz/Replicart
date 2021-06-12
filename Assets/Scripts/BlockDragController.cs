using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDragController : Draggable
{
    [Header("The below are probably set automagically")]
    public Bounds validSpace;
    public DraggableProducer source;

    // rectangle places
    private Vector2 lleft;
    private Vector2 uleft;
    private Vector2 lright;
    private Vector2 uright;

    public override void Release() {

        BoxCollider2D my_box = GetComponent<BoxCollider2D>();

        lleft = new Vector2(my_box.bounds.min.x, my_box.bounds.min.y );
        uleft = new Vector2(my_box.bounds.min.x, my_box.bounds.max.y );
        lright = new Vector2(my_box.bounds.max.x, my_box.bounds.min.y );
        uright = new Vector2(my_box.bounds.max.x, my_box.bounds.max.y );

        if (validSpace.Contains(lleft) && validSpace.Contains(lright) && validSpace.Contains(uleft) && validSpace.Contains(uright)) {
            print(my_box.bounds);
            print(validSpace);
        } else {
            print("Aw no");
            source.amount++;
            map.RemoveGroup(gameObject);
            Destroy(gameObject);
        }

        isBeingHeld = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            Color current = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1f);
        }
        GameObject.Find("Canvas").GetComponent<LevelManager>().CheckWin();
    }
}

//-1.205835
//0.1777401
//13.37845
//8.355679