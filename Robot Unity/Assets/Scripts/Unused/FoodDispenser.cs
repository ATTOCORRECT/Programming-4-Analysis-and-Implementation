using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    [SerializeField] Transform reticle;
    [SerializeField] GameObject FoodPrefab;

    Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0); // Ground plane

    // Update is called once per frame
    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance); // Mouse position onto the ground plane
        }
        worldPosition = Vector3.ClampMagnitude(worldPosition, 6);
        reticle.position = worldPosition + Vector3.down * 0.1f; // Update reticle position

        
        if (Input.GetMouseButtonDown(0)) // On click
        {
            GameObject FoodInstance = GameObject.Instantiate(FoodPrefab, worldPosition + Vector3.up * 5, Quaternion.identity); // Spawn new Food

            FoodInstance.name = "Food";
        }
    }
}
