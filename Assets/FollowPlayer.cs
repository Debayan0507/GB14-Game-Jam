using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float followSpeed;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
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
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        Vector3 playerPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerPos, followSpeed * Time.deltaTime);
    }
}
