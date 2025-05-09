using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera marioCamera;
    public Rigidbody2D marioBody;
   
    public float marioSpeed = 8f;
    public float marioJumpHeight = 5f;
    public float marioJumpTime = 1f;
    
    private Vector2 velocity;
    private float inputAxis;

    public float jumpForce => (2f * marioJumpHeight) / (marioJumpTime / 2f);
    public float gravity => (-2f * marioJumpHeight) / Mathf.Pow((marioJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();

        grounded = marioBody.Raycast(Vector2.down);

        if(grounded)
        {
            groundedMovement();
        }

        ApplyGravity();
    }

    private void Awake()
    {
        marioBody = GetComponent<Rigidbody2D>();
        marioCamera = Camera.main;
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * marioSpeed, marioSpeed * Time.deltaTime);
    }
    private void groundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float mulitplier = falling ? 2f : 1f;
        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
    private void FixedUpdate()
    {
        Vector2 position = marioBody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = marioCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = marioCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        marioBody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
       {
            if(transform.DotProduct(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
       }
    }
}
