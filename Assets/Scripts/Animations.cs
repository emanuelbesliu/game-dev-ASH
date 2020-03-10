using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;
    public SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("OnGround", coll.onGround);
        anim.SetBool("IsDashing", move.isDashing);
        anim.SetBool("WallGrab", move.wallGrab);
        anim.SetBool("WallSlide", move.wallSlide);
        anim.SetBool("CanMove", move.canMove);
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat(("HorizontalAxis"), x);
        anim.SetFloat(("VerticalAxis"), y);
        anim.SetFloat(("VerticalVelocity"), yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
    public void Flip(bool side)
    {
        if (move.wallGrab || move.wallSlide)
        {
            if (!side && sp.flipX)
                return;
            if (side && !sp.flipX)
                return;

        }
        bool state = (side) ? false : true;
        sp.flipX = state;
    }
}
