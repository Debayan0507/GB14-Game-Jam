using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public Dialogue dialogueScript;

    private Vector2 movement;

    public float moveSpeed = 2.5f;
    public float horizontalInput;
    public float verticalInput;

    public int j;

    public string[] items = {"Milk", "Tomato"};

    public bool isItem = false;
    public bool isCustomer = false;
    public bool itemGiven = false;
    public bool nearBin = false;
    public bool itemTaken = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PickItem();
        GiveItem();
        DumpItem();
        AnimatePlayer();
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
    void GiveItem()
    {
        if (itemTaken == true && isCustomer == true && Input.GetKeyDown(KeyCode.X))
        {
            if (dialogueScript.requiredItem[0] == items[j])
            {
                itemTaken = false;
                itemGiven = true;
            }
        }
    }
    void DumpItem()
    {
        if(itemTaken == true && nearBin == true && Input.GetKeyDown(KeyCode.X))
        {
            itemTaken = false;
        }
    }
    void PickItem()
    {
        if(isItem == true && Input.GetKeyDown(KeyCode.X))
        {
            itemTaken = true;
            isItem = false;
        }
    }
    void AnimatePlayer()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update animator parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (collision.gameObject.tag == items[i] && itemTaken == false)
            {
                j = i;
                isItem = true;
                itemGiven = false;
            }  
        }
        if (collision.gameObject.CompareTag("Dustbin"))
        {
            nearBin = true;
        }
        if (collision.gameObject.CompareTag("Customer"))
        {
            isCustomer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dustbin"))
        {
            nearBin = false;
        }
        if (collision.gameObject.CompareTag("Customer"))
        {
            isCustomer = false;
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (collision.gameObject.tag == items[i] && itemTaken == false)
            {
                isItem = false;
            }
        }
    }
}
