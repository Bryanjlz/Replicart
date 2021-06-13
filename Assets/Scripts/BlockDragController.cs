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

        int sound_choice = Random.Range(1, 7);
        FindObjectOfType<AudioManager>().Play("snap" + sound_choice);

        BoxCollider2D my_box = GetComponent<BoxCollider2D>();

        lleft = new Vector2(my_box.bounds.min.x, my_box.bounds.min.y );
        uleft = new Vector2(my_box.bounds.min.x, my_box.bounds.max.y );
        lright = new Vector2(my_box.bounds.max.x, my_box.bounds.min.y );
        uright = new Vector2(my_box.bounds.max.x, my_box.bounds.max.y );

        if (validSpace.Contains(lleft) && validSpace.Contains(lright) && validSpace.Contains(uleft) && validSpace.Contains(uright)) {
        } else {
            if (source.amount != -1) {
                source.ModifyAmount(1);
            }
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