using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask lavaLayer;
    public LayerMask objectsLayer;

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool onLava;
    public bool onLavaUp;
    public bool onLavaRight;
    public bool onLavaLeft;
    public bool hitObject;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 upOffset;
    private Color debugCollisionColor = Color.red;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);

        onLava = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, lavaLayer);
        onLavaUp = Physics2D.OverlapCircle((Vector2)transform.position + upOffset, collisionRadius, lavaLayer);
        onLavaLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, lavaLayer);
        onLavaRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, lavaLayer);

        hitObject = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + upOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, objectsLayer);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset, upOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + upOffset, collisionRadius);
    }
}
