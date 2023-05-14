using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    // determines if movement is happened
    private bool isMoving;
    // how fast we going
    public float moveRate;
    public float maxSpeed;
    private float currentVelocity;
    private Animator animator;
    private float baseRate;
    public Vector2 touchPosition;
    private Vector3 turnSmoothVelocity;
    private SpriteRenderer sprite;
    public ParticleSystem ps;



    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        baseRate = moveRate;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ps.Stop();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            ps.Play();
            
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            currentVelocity += moveRate * Time.deltaTime;

        }
        else if(currentVelocity > 0)
        {
            isMoving = false;
            ps.Stop();
            animator.SetBool("isMoving", isMoving);
            currentVelocity -= moveRate * 2f * Time.deltaTime;
        }
        if (Input.touchCount > 0 && isMoving)
        {
            FaceMouse();
        }
        float rotZ = transform.rotation.z;
        
    }

    void FaceMouse()
    {
        
        Touch touch = Input.GetTouch(0);
        touchPosition = cam.ScreenToWorldPoint(touch.position);
        // calculate xDistance from touchPos to find out if flipY should happen
        float xDis = touchPosition.x - transform.position.x;
        if(xDis < 0)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }
        // face the position
        Vector3 dir = new Vector3(touchPosition.x - transform.position.x, touchPosition.y - transform.position.y,0);
        //transform.LookAt(touchPosition, Vector3.right);
        this.transform.right = Vector3.SmoothDamp(transform.right, dir, ref turnSmoothVelocity, 0.2f);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // normalized (0.1, 0.2) --> factor of 1 ALWAYS
        rb.velocity = transform.right * currentVelocity;

        currentVelocity = Mathf.Clamp(currentVelocity, 0, maxSpeed);
    }
}
