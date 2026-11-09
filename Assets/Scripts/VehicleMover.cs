using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    public float vehicleSpeed = 3f;
    [HideInInspector] public Vector2 moveDirection = Vector2.left;

    void Start()
    {
        Destroy(gameObject, 7f); //Destroy vehicle after 7 ticks
    }

    void Update()
    {
        transform.Translate(moveDirection * vehicleSpeed * Time.deltaTime, Space.World);
    }
}