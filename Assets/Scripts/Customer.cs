using System.Collections;
using TMPro;
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
    public float speed = 2f;
    public bool isWaiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customerAnimator = GetComponent<Animator>();
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
        MoveToSpawn();
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
            Time.timeScale = 0f;
            dialoguePanel.SetActive(true);
            nameText.text = npcName[0];
            dialogueText.text = dialogueLines[line];
            line++;
        }
        else if (line == dialogueLines.Length && Input.GetKeyDown(KeyCode.E))
        {
            line = 0;
            Time.timeScale = 1f;
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
    void MoveToSpawn()
    {
        if (playerMovementScript.itemGiven == true && isWaiting == true)
        {
            customerAnimator.SetBool("isIdle", false);
            customerAnimator.SetBool("moveUp", true);
            StartCoroutine(ReturnToSpawn());
        }
    }
    IEnumerator ReturnToSpawn()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPoint.transform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        playerMovementScript.itemGiven = false;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
