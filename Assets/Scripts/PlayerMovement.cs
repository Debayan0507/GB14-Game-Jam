using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float horizontalInput;
    public float verticalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        // Get the horizontal and vertical input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the player based on the input and move speed
        transform.Translate(Vector2.right * moveSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector2.up * moveSpeed * verticalInput * Time.deltaTime);
    }
}
