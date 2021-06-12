using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click1 : MonoBehaviour
{
    public void Click()
    {
        FindObjectOfType<AudioManager>().Play("click");
    }
}
