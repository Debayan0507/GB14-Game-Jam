using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Customer : MonoBehaviour
{
    private Animator customerAnimator;
    public string[] dialogueLines;
    public string[] requiredItem;
    public string[] npcName;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public bool playerIsClose = false;
    public int line = 0;

    public PlayerMovement playerMovementScript;
    public GameObject waitingPoint;
    public GameObject spawnPoint;
    public GameObject endPoint;
    public GameObject player;
    public float speed = 2f;
    public bool isWaiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customerAnimator = GetComponent<Animator>();
        endPoint = GameObject.Find("Customer End Point");
        waitingPoint = GameObject.Find("Customer Waiting Point");
        spawnPoint = GameObject.Find("Customer Spawn Point");

        dialoguePanel = spawnPoint.GetComponent<CustomerSpawn>().dialoguePanel;
        dialogueText = spawnPoint.GetComponent<CustomerSpawn>().dialogueText;
        nameText = spawnPoint.GetComponent<CustomerSpawn>().nameText;

        playerMovementScript = FindAnyObjectByType<PlayerMovement>();

        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToStall();
        MoveToEnd();
        GiveItem();
        if (playerIsClose)
        {
            TypeLine();
        }
    }
    void TypeLine()
    {
        if (Input.GetKeyDown(KeyCode.E) && line < dialogueLines.Length)
        {
            playerMovementScript.ePlayer.SetActive(false);
            dialoguePanel.SetActive(true);
            nameText.text = npcName[0];
            dialogueText.text = dialogueLines[line];
            line++;
        }
        else if (line == dialogueLines.Length && Input.GetKeyDown(KeyCode.E))
        {
            playerMovementScript.ePlayer.SetActive(true);
            line = 0;
            dialoguePanel.SetActive(false);
        }
    }
    void GiveItem()
    {
        if (playerMovementScript.itemTaken == true && playerMovementScript.isCustomer == true && Input.GetKeyDown(KeyCode.X))
        {
            if (requiredItem[0] == playerMovementScript.items[playerMovementScript.j])
            {
                playerMovementScript.itemTaken = false;
                playerMovementScript.itemGiven = true;
                playerMovementScript.inventoryText.text = "Inventory : None";
                switch (playerMovementScript.j)
                {
                    case 0:
                        playerMovementScript.money += 20;
                        playerMovementScript.moneyText.text = $"Money : {playerMovementScript.money}";
                        break;
                    case 1:
                        playerMovementScript.money += 30;
                        playerMovementScript.moneyText.text = $"Money : {playerMovementScript.money}";
                        break;
                }
            }
        }
    }
    void MoveToStall()
    {
        if (isWaiting == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitingPoint.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waitingPoint.transform.position) < 0.1f)
            {
                customerAnimator.SetBool("isIdle", true);
                isWaiting = true;
            }
        }
    }
    void MoveToEnd()
    {
        if (playerMovementScript.itemGiven == true && isWaiting == true)
        {
            customerAnimator.SetBool("isIdle", false);
            customerAnimator.SetBool("moveUp", true);
            StartCoroutine(GoToEnd());
        }
    }
    IEnumerator GoToEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        playerMovementScript.itemGiven = false;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
