using System.Collections;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] vehicles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnVehicle()
    {
        while (true)
        {
            int vehicle = Random.Range(0, vehicles.Length);
            yield return new WaitForSeconds(4f);
            Instantiate(vehicles[vehicle], transform.position, transform.rotation);
        }
    }
}
