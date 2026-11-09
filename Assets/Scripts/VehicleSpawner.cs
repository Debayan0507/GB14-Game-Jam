using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] vehicles;
    public Transform[] rightToLeftSpawnPoints;
    public Transform[] leftToRightSpawnPoints;

    public float spawnDelay = 4f;

    void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            if (vehicles == null || vehicles.Length == 0) continue; // Safety check

            int vehicleIndex = Random.Range(0, vehicles.Length);
            GameObject prefab = vehicles[vehicleIndex];

            bool movingLeft = Random.value > 0.5f;
            Transform spawnPoint = transform; // Fallback position
            Vector2 direction = movingLeft ? Vector2.left : Vector2.right;

            // Assign lane if arrays are populated
            if (movingLeft && rightToLeftSpawnPoints.Length > 0)
            {
                spawnPoint = rightToLeftSpawnPoints[Random.Range(0, rightToLeftSpawnPoints.Length)];
            }
            else if (!movingLeft && leftToRightSpawnPoints.Length > 0)
            {
                spawnPoint = leftToRightSpawnPoints[Random.Range(0, leftToRightSpawnPoints.Length)];
            }

            GameObject vehicle = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            VehicleMover mover = vehicle.GetComponent<VehicleMover>();
            if (mover != null) mover.moveDirection = direction;

            // Flip sprite for right-moving traffic
            if (direction == Vector2.right)
            {
                SpriteRenderer sr = vehicle.GetComponent<SpriteRenderer>();
                if (sr != null) sr.flipX = true;
            }
        }
    }
}