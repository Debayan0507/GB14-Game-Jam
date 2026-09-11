using UnityEngine;
using TMPro;
using System.Collections;
using System.Data.SqlTypes;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public string[] dialogueLines;
    public string[] npcName;
    public bool playerIsClose = false;
    public int line = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
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