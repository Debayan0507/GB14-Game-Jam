using System.Collections;
using TMPro;
using UnityEngine;

public class CustomerSpawn : MonoBehaviour
{
    public GameObject[] customerPrefab;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    int i = 0;
    public bool customerSpawned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(customerSpawned == false)
        {
            StartCoroutine(SpawnCustomer());
        }
    }
    IEnumerator SpawnCustomer()
    {
        customerSpawned = true;
        yield return new WaitForSeconds(5f);
        Instantiate(customerPrefab[i], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        i++;
    }
}
