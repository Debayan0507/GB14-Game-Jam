using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class FinalDay : MonoBehaviour
{
    public string[] dialogueLines;
    public string[] npcName;

    private Animator customerAnimator;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public int line = 0;
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

        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToStall();
        if (isWaiting)
        {
            TypeLine();
        }
    }
    void TypeLine()
    {
        if (line < dialogueLines.Length)
        {
            dialoguePanel.SetActive(true);
            nameText.text = npcName[0];
            dialogueText.text = dialogueLines[line];
            if (Input.GetKeyDown(KeyCode.E))
            {
                line++;
            }
        }
        if (line == dialogueLines.Length)
        {
            dialoguePanel.SetActive(false);
            MoveToSpawn();
        }
    }
    void MoveToStall()
    {
        if (isWaiting == false && line!=dialogueLines.Length)
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
        if (line >= dialogueLines.Length)
        {
            customerAnimator.SetBool("isIdle", false);
            customerAnimator.SetBool("moveUp", true);
            transform.position = Vector3.MoveTowards(transform.position, spawnPoint.transform.position, speed * Time.deltaTime);
            Destroy(gameObject, 5f);
        }
    }
}
