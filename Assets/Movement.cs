using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private float time;

    [Space]
    [Header("Forces")]
    public float speed = 10;
    public float wallJumpLerp = 10;
    public float jumpForce = 15;
    public float dashSpeed = 20;
    public float slideSpeed = 5;

    [Space]
    [Header("Checks")]
    public bool canMove;
    public bool isDashing;
    public bool wallGrab;
    public bool wallSlide;

    [Space]

    private bool groundTouch;
    private bool hasDashed;


    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y);

        Walk(dir);


        if (coll.onGround && !isDashing)
        {
            GetComponent<Jump>().enabled = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (coll.onGround){
                //rb.gravityScale = 3;
                Jump(Vector2.up);

            }

        }

        if(!wallSlide && !wallGrab && !isDashing){
            GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }

        if (Input.GetButtonDown("Boost") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0){
                GetComponent<Renderer>().material.color = new Color(0,191,255);
                Dash(xRaw, yRaw);
                //GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            }
        }

        if (Input.GetButtonDown("Grab") && coll.onWall && canMove)
        {
            wallGrab = true;
            wallSlide = false;
        }
        if (Input.GetButtonUp("Grab") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);
            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            JumpWhileWall();
        }
        else
        { 
            rb.gravityScale = 3; 

            JumpWhileWall();
        }

        if(!coll.onGround && coll.onWall && !wallGrab){
            time = Time.time;
        }

        if(coll.onWall && !coll.onGround && wallGrab){
            //Debug.Log("elapsed time: " + (Time.time - time));
            if((Time.time - time) >= 3){
                wallSlide = true;
                GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                WallSlide();
            }
        }

        if(!coll.onWall || coll.onGround) wallSlide = false;


        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (wallGrab || !canMove)
            return;

    }
    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    private void JumpWhileWall(){
        if (Input.GetButtonDown("Jump")){
            wallGrab =  false;
            rb.gravityScale = 3;
            Jump(Vector2.up);
        }
    }

    private void WallSlide(){
        if(!canMove) return;

        bool pushingWall = false;

        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall)){
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;

        //rb.velocity = new Vector2(push, -slideSpeed);
        if (Input.GetButtonDown("Jump")){
            //wallGrab =  false;
            rb.gravityScale = 3;
            Jump(Vector2.up);
        }
        wallGrab = false;



        //GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());

        rb.gravityScale = 0;
        GetComponent<Jump>().enabled = false;
        isDashing = true;
        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3;
        GetComponent<Jump>().enabled = true;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
