using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTree {
    public SpriteTree left;
    public SpriteTree right;
    public Sprite value;

    public SpriteTree() {
    }

    public SpriteTree (SpriteTree left, SpriteTree right) {
        this.left = left;
        this.right = right;
    }

    public void Add (Sprite s, string n) {
        if (n.Length == 0) {
            value = s;
        } else if (n.Substring(0, 1).Equals("1")) {
            right.Add(s, n.Substring(1));
        } else {
            left.Add(s, n.Substring(1));
        }
    }

    public Sprite Get (string n) {
        if (n.Length == 0) {
            return value;
        } else if (n.Substring(0, 1).Equals("1")) {
            return right.Get(n.Substring(1));
        } else {
            return left.Get(n.Substring(1));
        }
    }
}
