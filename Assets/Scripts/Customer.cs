using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public PlayerMovement playerMovementScript2;
    public GameObject waitingPoint;
    public GameObject spawnPoint;
    public float speed = 2f;
    public bool isWaiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isWaiting == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitingPoint.transform.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, waitingPoint.transform.position) < 0.1f)
            {
                isWaiting = true;
            }
        }
        if(playerMovementScript2.itemGiven == true && isWaiting == true)
        {
            StartCoroutine(ReturnToSpawn());
        }
    }
    IEnumerator ReturnToSpawn()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPoint.transform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        playerMovementScript2.itemGiven = false;
        Destroy(gameObject);
    }
}
