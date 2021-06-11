using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    protected Transform pos;
    protected Rigidbody2D rb;
    protected BoxCollider2D collide;
    [Header("For debugging purposes, do not change.")]
    public bool isGrounded;
    public bool isMovingIntoWall;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform;   
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.deltaTime;

        checkGrounded();
        checkWallCollision();

        if (isMovingIntoWall) {
            if (!isGrounded) {
                // Trying to move into a wall in midair sticks you into it
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            } else {
                direction *= -1;
                rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, 0);
            }
        } else {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, 0);
        }

    }

    void checkGrounded() {
        Color rayColor;
        float fudge = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(collide.bounds.center, Vector2.down, (collide.bounds.extents.y + fudge));
        if (hit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(pos.position, Vector3.down * (collide.bounds.extents.y + fudge), rayColor);
        isGrounded = hit.collider != null;  
    }

    void checkWallCollision() {
        Color rayColor;
        float fudge = 0.03f;
        RaycastHit2D hit = Physics2D.Raycast(collide.bounds.center, direction.normalized, (collide.bounds.extents.y + fudge));
        if (hit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(pos.position, direction.normalized * (collide.bounds.extents.y + fudge), rayColor);
        isMovingIntoWall = hit.collider != null;  
    }
}
