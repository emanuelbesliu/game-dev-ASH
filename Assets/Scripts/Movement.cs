using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public Tilemap tilemap;
    public SpriteRenderer sp;
    private Color defaultCol;
    private Collision coll;
    public Animations anim;
    public Ghost ghost;
    [HideInInspector]
    public Rigidbody2D rb;
    public LevelLoader ll;
    public Camera camera;
    public Canvas canvas;

    [Space]
    [Header("Forces")]
    public float speed = 10;
    public float wallJumpLerp = 10;
    public float jumpForce = 15;
    public float dashSpeed = 30;
    public float slideSpeed = 5;
    public float grabTime = 2;
    public float oldGravity;
    public float oldLavaGravity;
    public Vector2 oldVelocity;
    public Vector2 oldLavaVelocity;
    private float time;

    [Space]
    [Header("Checks")]
    public bool canMove;
    public bool isDashing;
    public bool wallGrab;
    public bool wallSlide;
    public bool lavaCollide;
    public bool firstGrab;
    public bool tutorial = true;
    public bool checkTutorial = false;
    public bool start = false;

    [Space]

    private bool groundTouch;
    private bool hasDashed;
    private bool lavaTouch;
    public bool side = true;
    public bool room2 = false;

    public Vector3 playerPos;


    void Start()
    {
        defaultCol = sp.color;
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animations>();
        canMove = false;
        playerPos = transform.position;
        rb.gravityScale = 0;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(AutomaticJump());
        }
        InvokeRepeating("PlayWalk", 0.0f, 0.35f);
        InvokeRepeating("PlayClimb", 0.0f, 0.35f);

    }


    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //Debug.Log("X este: " + x + " Y este: " + y);

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        if (canMove)
            anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (coll.onGround && !isDashing)
        {
            GetComponent<Jump>().enabled = true;
        }

        if (this.transform.position.y >= 19.5 && this.transform.position.x >= 95)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, rb.velocity.y + 5f);
            //StartCoroutine(AutomaticJump());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !canvas.gameObject.activeInHierarchy && start)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && gameObject.transform.position.x>48f) { 
                if (!FindObjectOfType<Cubes>().bird.gameObject.activeInHierarchy)
                    esc();}
            else esc();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canvas.gameObject.activeInHierarchy)
        {
            FindObjectOfType<Cubes>().lavaFall = true;
            foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava"))
                if ((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.GetComponent<Rigidbody2D>().gravityScale == 0f)
                {
                    gos.GetComponent<Rigidbody2D>().gravityScale = oldLavaGravity;
                    gos.GetComponent<Rigidbody2D>().velocity = oldLavaVelocity;
                }
            rb.velocity = oldVelocity;
            canMove = true;
            rb.gravityScale = oldGravity;
            canvas.gameObject.SetActive(false);
        }

        /*if(coll.hitObject && canMove){
            GroundTouch();
        }*/
        if (transform.position.y < -30 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = playerPos;
            FindObjectOfType<AudioManager>().Play("Death");

            if (this.transform.position.x < -12)
            {
                tutorial = true;
                checkTutorial = false;
            }
            GetComponent<TutorialFall>().detectionFall.gameObject.SetActive(true);
            GetComponent<TutorialFall>().timestop.gameObject.SetActive(true);
            GetComponent<TutorialFall>().platform.gravityScale = 0;
            GetComponent<TutorialFall>().platform.gameObject.SetActive(true);
            GetComponent<TutorialFall>().platform.transform.position = GetComponent<TutorialFall>().platformPosition;
        }
        if (transform.position.y < 44 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            rb.velocity = new Vector2(0, 0);
            transform.position = playerPos;
        }
        /*foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava")) {
            if (coll.onLava || coll.onLavaLeft || coll.onLavaRight || coll.onLavaUp || coll.onLavaLeftDown || coll.onLavaLeftUp || coll.onLavaRightDown || coll.onLavaRightUp)
            {
                FindObjectOfType<AudioManager>().Play("Death");
                if (gos.name == "Lav(Clone)") {
                    Destroy(gos);
                }
                lavaCollide = true;
                sp.color = new Color(255, 0, 0);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.5f);
                StartCoroutine(DisableMovement(.4f));
            }
        }
        if ((coll.onLava || coll.onLavaLeft || coll.onLavaRight || coll.onLavaUp || coll.onLavaLeftDown || coll.onLavaLeftUp || coll.onLavaRightDown || coll.onLavaRightUp) && canMove)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            lavaCollide = true;
            sp.color = new Color(255, 0, 0);
            if (coll.onLava && canMove)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 2);
            }
            else if (coll.onLavaLeft)
            {
                rb.velocity = new Vector2(rb.velocity.x + 2, rb.velocity.y);
            }
            else if (coll.onLavaRight)
            {
                rb.velocity = new Vector2(rb.velocity.x - 2, rb.velocity.y);
            }
            else if (coll.onLavaUp)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 2);
            }
            else if (coll.onLavaLeftDown)
            {
                rb.velocity = new Vector2(rb.velocity.x + 2, rb.velocity.y + 2);
            }
            else if (coll.onLavaLeftUp)
            {
                rb.velocity = new Vector2(rb.velocity.x + 2, rb.velocity.y - 2);
            }
            else if (coll.onLavaRightDown)
            {
                rb.velocity = new Vector2(rb.velocity.x - 2, rb.velocity.y + 2);
            }
            else if (coll.onLavaRightUp)
            {
                rb.velocity = new Vector2(rb.velocity.x - 2, rb.velocity.y - 2);
            }
            StartCoroutine(DisableMovement(.4f));
        }*/
        if (Input.GetButtonDown("Jump") && canMove)
        {
            anim.SetTrigger("Jump");
            if (coll.onGround) {
                //rb.gravityScale = 3;
                Jump(Vector2.up);
            }
        }

        if (!wallSlide && !wallGrab && !isDashing && !lavaCollide) {
            sp.color = defaultCol;
        }

        if (Input.GetButtonDown("Boost") && !hasDashed && !tutorial)
        {
            if (!tutorial)
            {
                canMove = true;
                checkTutorial = false;
            }
            wallGrab = false;
            if (xRaw != 0 || yRaw != 0) {
                sp.color = new Color(0, 191, 255);
                Dash(xRaw, yRaw);
                //GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            }
        }

        if (Input.GetButtonDown("Grab") && coll.onWall && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side ? false : true);
            if (!wallSlide) {
                wallGrab = true;
                wallSlide = false;
            }
        }
        if (Input.GetButtonUp("Grab") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (wallGrab && !isDashing && canMove)
        {
            sp.color = defaultCol;
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);
            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            JumpWhileWall();
        }
        else
        {
            if (!checkTutorial && start && !canvas.gameObject.activeInHierarchy)
                rb.gravityScale = 3;
            if (coll.onWall)
                JumpWhileWall();
        }

        if (coll.onWall && hasDashed) {
            hasDashed = true;
            isDashing = false;
        }

        if (coll.onGround && !firstGrab)
            firstGrab = true;
        else if (wallGrab)
            firstGrab = false;

        if (firstGrab) {
            time = Time.time;
        }

        if (!coll.onGround && wallGrab) {

            //Debug.Log("elapsed time: " + (Time.time - time));
            if ((Time.time - time) >= grabTime) {
                wallSlide = true;
                sp.color = new Color(255, 0, 0);
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround) wallSlide = false;


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
        if (x > 0 && !wallSlide && canMove)
        {
            side = true;
            anim.Flip(side);
        }

        if (!isDashing) {
            ghost.makeGhost = false;
        }

        if (x < 0 && !wallSlide && canMove)
        {
            side = false;
            anim.Flip(side);
        }
    }

    void PlayWalk()
    {
        if (Input.GetButton("Horizontal") && coll.onGround && !wallGrab)
            FindObjectOfType<AudioManager>().Play("Walking");
    }
    void PlayClimb()
    {
        if (Input.GetButton("Vertical") && wallGrab)
            FindObjectOfType<AudioManager>().Play("Climb");
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sp.flipX ? false : true;
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
    }

    public void Jump(Vector2 dir)
    {
        FindObjectOfType<AudioManager>().Play("Jump");
        if ((Time.time - time) < grabTime) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += dir * jumpForce;
        }
    }

    private void Dash(float x, float y)
    {
        FindObjectOfType<AudioManager>().Play("Dash");
        ghost.makeGhost = true;
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        hasDashed = true;

        anim.SetTrigger("Boost");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        if (y != 0 && x == 0)
            rb.velocity += dir.normalized * dashSpeed / 2;
        else if (y != 0)
            rb.velocity += dir.normalized * dashSpeed / 1.5f;
        else
            rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    private void JumpWhileWall() {
        if (Input.GetButtonDown("Jump") && wallGrab) {
            wallGrab = false;
            rb.gravityScale = 3;
            Jump(Vector2.up);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Reset")
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts) {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reset"))
        {
            GroundTouch();
            StartCoroutine(ObjectReset(other.gameObject));
        }
        if (other.gameObject.name == "AdditionalPopUp")
        {
            room2 = true;
            playerPos = new Vector2(-6.720411f, -9.548742f);

        }
        if (other.gameObject.CompareTag("Lava"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.5f);
            FindObjectOfType<AudioManager>().Play("Death");
            foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava"))
                if (gos.name == "Lav(Clone)")
                    Destroy(gos);
            lavaCollide = true;
            sp.color = new Color(255, 0, 0);
            StartCoroutine(DisableMovement(.25f));
        }
    }
    private void WallSlide() {
        if (side != coll.wallSide)
            anim.Flip(side == true ? false : true);

        if (!canMove) return;

        bool pushingWall = false;

        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall)) {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;

        //rb.velocity = new Vector2(push, -slideSpeed);
        if (Input.GetButtonDown("Jump")) {
            //wallGrab =  false;
            rb.gravityScale = 3;
            Jump(Vector2.up);
        }
        wallGrab = false;

        //GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    private void esc(){
        FindObjectOfType<Cubes>().lavaFall = false;
        foreach (GameObject gos in GameObject.FindGameObjectsWithTag("Lava"))
            if ((gos.name == "lava(Clone)" || gos.name == "lava2(Clone)") && gos.GetComponent<Rigidbody2D>().gravityScale != 0f)
            {
                oldLavaGravity = gos.GetComponent<Rigidbody2D>().gravityScale;
                oldLavaVelocity = gos.GetComponent<Rigidbody2D>().velocity;
                gos.GetComponent<Rigidbody2D>().gravityScale = 0f;
                gos.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            }

        canMove = false;
        oldGravity = rb.gravityScale;
        rb.gravityScale = 0;
        oldVelocity = rb.velocity;
        rb.velocity = new Vector2(0, 0);
        isDashing = false;
        canvas.gameObject.SetActive(true);
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
       
    IEnumerator ObjectReset(GameObject other)
    {
        FindObjectOfType<AudioManager>().Play("Reset");
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        other.gameObject.SetActive(true);
    }
    IEnumerator AutomaticJump()
    {
        yield return new WaitForSeconds(1f);
        Jump(new Vector2(0, 2f));
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(-10, rb.velocity.y);
        anim.Flip(!side);
        yield return new WaitForSeconds(0.2f);
        tilemap.gameObject.SetActive(true);
        canMove = true;
        rb.gravityScale = 3;
        tutorial = false;
        start = true;
        playerPos = transform.position;
        camera.GetComponent<Cubes>().lavaFall = true;
        camera.GetComponent<Cubes>().delay = 1f;
    }
}
