using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offsetZ = new Vector3(0f, 0f, -10f);
    public float followSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Vector3 playerPos = player.transform.position + offsetZ;
        transform.position = Vector3.Lerp(transform.position, playerPos, followSpeed * Time.deltaTime);
    }
}
