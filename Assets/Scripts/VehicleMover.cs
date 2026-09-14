using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    public float vehicleSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector2.left * vehicleSpeed * Time.deltaTime);
        Destroy(gameObject, 7f);
    }
}
