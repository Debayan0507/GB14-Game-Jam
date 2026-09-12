using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Dialogue dialogueScript;
    public float moveSpeed = 5f;
    public float horizontalInput;
    public float verticalInput;
    public string[] items = {"Milk", "Tomato"};
    public int j;
    public bool isItem = false;
    public bool isCustomer = false;
    public bool itemGiven = false;
    public bool nearBin = false;
    public bool itemTaken = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PickItem();
        GiveItem();
        DumpItem();
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (collision.gameObject.tag == items[i] && isItem == false)
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
    }
}
