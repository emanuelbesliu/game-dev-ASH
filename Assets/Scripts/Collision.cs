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
    public bool onRightUpWall;
    public bool onLeftUpWall;
    public bool onRightDownWall;
    public bool onLeftDownWall;
    public bool onLava;
    public bool onLavaUp;
    public bool onLavaRight;
    public bool onLavaLeft;
    public bool onLavaLeftDown;
    public bool onLavaLeftUp;
    public bool onLavaRightDown;
    public bool onLavaRightUp;
    public bool hitObject;
    public bool wallSide;
    public bool wallLowerSide;
    public bool wallUpperSide;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public Vector2 rightOffset;
    public Vector2 leftOffset;
    public Vector2 lowerRightOffset;
    public Vector2 lowerLeftOffset;
    public Vector2 rightUpOffset;
    public Vector2 leftUpOffset;
    public Vector2 rightDownOffset;
    public Vector2 leftDownOffset;
    public Vector2 upOffset;
    private Color debugCollisionColor = Color.red;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + rightDownOffset, collisionRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftDownOffset, collisionRadius, groundLayer);

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onLeftUpWall = Physics2D.OverlapCircle((Vector2)transform.position + leftUpOffset, collisionRadius, groundLayer);
        onLeftDownWall = Physics2D.OverlapCircle((Vector2)transform.position + lowerLeftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onRightUpWall = Physics2D.OverlapCircle((Vector2)transform.position + rightUpOffset, collisionRadius, groundLayer);
        onRightDownWall = Physics2D.OverlapCircle((Vector2)transform.position + lowerRightOffset, collisionRadius, groundLayer);

        onWall = onLeftWall || onLeftUpWall || onLeftDownWall || onRightWall || onRightDownWall || onRightUpWall;

        onLava = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, lavaLayer);
        onLavaUp = Physics2D.OverlapCircle((Vector2)transform.position + upOffset, collisionRadius, lavaLayer);
        onLavaLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, lavaLayer);
        onLavaRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, lavaLayer);

        onLavaLeftDown = Physics2D.OverlapCircle((Vector2)transform.position + leftDownOffset, collisionRadius, lavaLayer);
        onLavaLeftUp = Physics2D.OverlapCircle((Vector2)transform.position + leftUpOffset, collisionRadius, lavaLayer);
        onLavaRightDown = Physics2D.OverlapCircle((Vector2)transform.position + rightDownOffset, collisionRadius, lavaLayer);
        onLavaRightUp = Physics2D.OverlapCircle((Vector2)transform.position + rightUpOffset, collisionRadius, lavaLayer);

        hitObject = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + upOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, objectsLayer) ||
                    Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, objectsLayer);
        wallSide = onRightWall || onRightDownWall || onRightUpWall ? false : true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset, upOffset, rightDownOffset, rightUpOffset, leftUpOffset, leftDownOffset, lowerRightOffset, lowerLeftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + upOffset, collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + rightDownOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightUpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftUpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftDownOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + lowerLeftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + lowerRightOffset, collisionRadius);
    }
}
