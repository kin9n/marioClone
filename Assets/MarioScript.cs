using UnityEngine;

public class MarioScript : MonoBehaviour
{
    public Rigidbody2D marioBody;
    private int marioSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Control Scheme
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            marioBody.linearVelocity = Vector2.up * 10;
        }
        if(Input.GetKey(KeyCode.A) == true)
        {
            marioBody.position += Vector2.left * marioSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            marioBody.position += Vector2.right * marioSpeed * Time.deltaTime;
        }


    }
}
