using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAndParent : MonoBehaviour
{
    public GameObject BasketGraphic;
    public GameObject objectToSpawn;   // The prefab you want to spawn
    public GameObject spawnLocation;    // The GameObject where you want to spawn the new object
    public GameObject parentObject;     // The GameObject you want to set as the parent
    public KeyCode spawnKey = KeyCode.Space; // The key to trigger spawning
    public float minZVelocity = -5f;  // Minimum Z velocity
    public float maxZVelocity = 5f;   // Maximum Z velocity

    void Update()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(spawnKey))
        {
         //   SpawnAndParentObject();
        }
    }

    public void SpawnAndParentObject()
    {
        if (objectToSpawn == null || spawnLocation == null || parentObject == null)
        {
            Debug.LogError("Please assign all necessary GameObjects in the inspector.");
            return;
        }

        // Get the position of the spawnLocation
        Vector3 spawnPosition = spawnLocation.transform.position;

        // Generate a random z rotation
        float randomZRotation = Random.Range(0f, 360f);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, randomZRotation);

        // Generate a random z velocity within specified bounds
        float randomZVelocity = Random.Range(minZVelocity, maxZVelocity);

        // Spawn the object at the specified location and rotation
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

        // Parent the spawned object to the specified parentObject
        spawnedObject.transform.parent = parentObject.transform;

        // Apply the random Z velocity to the spawned object
        Rigidbody2D spawnedRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
        if (spawnedRigidbody != null)
        {
            spawnedRigidbody.velocity = new Vector2(0f, 0f); // Optional: Reset other velocities
            spawnedRigidbody.angularVelocity = randomZVelocity;
        }

        // Get the RectTransform component of the UI element
        RectTransform rectTransform = BasketGraphic.GetComponent<RectTransform>();

        // Set the UI element as the last child in the hierarchy
        rectTransform.SetAsLastSibling();
    }

    public void SpawnAndParentObject(Color color)
    {
        if (objectToSpawn == null || spawnLocation == null || parentObject == null)
        {
            Debug.LogError("Please assign all necessary GameObjects in the inspector.");
            return;
        }

        // Get the position of the spawnLocation
        Vector3 spawnPosition = spawnLocation.transform.position;

        // Generate a random z rotation
        float randomZRotation = Random.Range(0f, 360f);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, randomZRotation);

        // Generate a random z velocity within specified bounds
        float randomZVelocity = Random.Range(minZVelocity, maxZVelocity);

        // Spawn the object at the specified location and rotation
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

        // Parent the spawned object to the specified parentObject
        spawnedObject.transform.parent = parentObject.transform;

        // Apply the random Z velocity to the spawned object
        Rigidbody2D spawnedRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
        if (spawnedRigidbody != null)
        {
            spawnedRigidbody.velocity = new Vector2(0f, 0f); // Optional: Reset other velocities
            spawnedRigidbody.angularVelocity = randomZVelocity;
        }

        // Get the RectTransform component of the UI element
        RectTransform rectTransform = BasketGraphic.GetComponent<RectTransform>();

        // Set the UI element as the last child in the hierarchy
        rectTransform.SetAsLastSibling();

        Image m_Image = spawnedObject.GetComponent<Image>();
        m_Image.color = color;
    }
}
