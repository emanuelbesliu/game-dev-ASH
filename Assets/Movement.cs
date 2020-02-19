using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Tilemap tilemap;

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
    public bool lavaCollide;

    [Space]

    private bool groundTouch;
    private bool hasDashed;
    private bool lavaTouch;

    private Vector3 playerPos;
    

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        playerPos = transform.position;
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

        if(coll.hitObject && canMove){
            GroundTouch();
        }

        foreach(GameObject gos in GameObject.FindGameObjectsWithTag("Lava")){
            if(coll.onLava || coll.onLavaRight || coll.onLavaLeft || coll.onLavaUp){
                if(gos.name == "Lav(Clone)"){
                    Destroy(gos);
                }
                lavaCollide = true;
                GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+2);
                StartCoroutine(DisableMovement(.4f));  
            }       
        }

        if(coll.onLava && canMove){
            lavaCollide = true;
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+2);
            StartCoroutine(DisableMovement(.4f));
        }else if(coll.onLavaLeft){
            lavaCollide = true;
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            rb.velocity = new Vector2(rb.velocity.x+2, rb.velocity.y);
            StartCoroutine(DisableMovement(.4f));
        }else if(coll.onLavaRight){
            lavaCollide = true;
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            rb.velocity = new Vector2(rb.velocity.x-2, rb.velocity.y);
            StartCoroutine(DisableMovement(.4f));
        }else if(coll.onLavaUp){
            lavaCollide = true;
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y-2);
            StartCoroutine(DisableMovement(.4f));
        }

        if (Input.GetButtonDown("Jump") && canMove)
        {
            if (coll.onGround){
                //rb.gravityScale = 3;
                Jump(Vector2.up);
            }
        }

        if(!wallSlide && !wallGrab && !isDashing && !lavaCollide){
            GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }

        if (Input.GetButtonDown("Boost") && !hasDashed && canMove)
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

        if (wallGrab && !isDashing && canMove)
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
            if(coll.onWall)
                JumpWhileWall();
        }

        if(coll.onWall && hasDashed){
            hasDashed = false;
            isDashing = false;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Reset")
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts){
                //Debug.Log(hit.point);
                hitPosition.x = hit.point.x - 0.2f;
                hitPosition.y = hit.point.y - 0.2f;
                Vector3Int cell = new Vector3Int((int)hitPosition.x, (int)hitPosition.y, 0);

                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);

                hitPosition.x = hit.point.x + 0.2f;
                hitPosition.y = hit.point.y + 0.2f;

                cell = new Vector3Int((int)hitPosition.x, (int)hitPosition.y, 0);

                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
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

    IEnumerator DashWait(){
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
        
        transform.position = playerPos;
        canMove = true;

        if(lavaCollide) lavaCollide = false;
    }
}
