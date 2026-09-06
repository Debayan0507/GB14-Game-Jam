using UnityEngine;
using TMPro;
using System.Collections;
using System.Data.SqlTypes;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    public bool playerIsClose = false;
    public int line = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
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
            dialogueText.text = dialogueLines[line];
            line++;
        }
        if (line == dialogueLines.Length)
        {
            line = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsClose = false;
            line = 0;
            dialogueText.text = string.Empty;
        }
    }
}