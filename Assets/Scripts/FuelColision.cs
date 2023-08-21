using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelColision : MonoBehaviour
{
 [SerializeField] float fuelAmount = 100f;
 [SerializeField] float minFuel = 0f;
 [SerializeField] float maxFuel = 100f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RocketFuel rocket = other.GetComponent<RocketFuel>();
            rocket.currentFuel += fuelAmount;
            rocket.currentFuel = Mathf.Clamp(rocket.currentFuel, minFuel, maxFuel);
            Destroy(gameObject);
        }
    }
}
