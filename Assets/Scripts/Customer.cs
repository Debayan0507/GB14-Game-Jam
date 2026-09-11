using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject waitingPoint;
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
    }
}
