using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject xDustbin;
    public GameObject xPlayer;
    public GameObject ePlayer;
    private Animator animator;

    private Vector2 movement;

    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI moneyText;

    public float moveSpeed = 2.5f;
    public float horizontalInput;
    public float verticalInput;

    public int j;
    public int money = 0;
   public string[] items = {"Milk", "Tomato", "Candy", "Potato"};

    public bool isItem = false;
    public bool isCustomer = false;
    public bool itemGiven = false;
    public bool nearBin = false;
    public bool itemTaken = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryText = GameObject.Find("InventoryText").GetComponent<TextMeshProUGUI>();
        inventoryText.text = "Inventory : None";
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        moneyText.text = "Money : 0";
        ePlayer = GameObject.Find("EPlayer");
        xPlayer = GameObject.Find("XPlayer");
        ePlayer.SetActive(false);
        xPlayer.SetActive(false);
        xDustbin = GameObject.Find("XDustbin");
        xDustbin.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PickItem();
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
    void DumpItem()
    {
        if(itemTaken == true && nearBin == true)
        {
            xDustbin.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                itemTaken = false;
                xDustbin.SetActive(false);
                inventoryText.text = "Inventory : None";
            }
        }
    }
    void PickItem()
    {
        if(isItem == true)
        {
            xPlayer.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                itemTaken = true;
                isItem = false;
                itemGiven = false;
                xPlayer.SetActive(false);
                inventoryText.text = "Inventory : " + items[j];
            }
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
            if (collision.gameObject.tag == items[i])
            {
                if (itemTaken == false)
                {
                    j = i;
                    isItem = true;
                }
            }  
        }
        if (collision.gameObject.CompareTag("Dustbin"))
        {
            nearBin = true; 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dustbin"))
        {
            nearBin = false;
            xDustbin.SetActive(false);
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (collision.gameObject.tag == items[i])
            {
                xPlayer.SetActive(false);
                if (itemTaken == false)
                {
                    isItem = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Customer"))
        {
            ePlayer.SetActive(true);
            isCustomer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Customer"))
        {
            ePlayer.SetActive(false);
            isCustomer = false;
        }
    }
}
