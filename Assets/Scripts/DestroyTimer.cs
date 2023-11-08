using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyTimer : MonoBehaviour
{
    public float TimeToDestruction = 0.5f;

    private float GenericTimer;

    // Update is called once per frame
    void Update()
    {
        GenericTimer += Time.deltaTime;
        if (GenericTimer >= TimeToDestruction)
        {

            //Destroy self
            Destroy(gameObject);
        }
    }
}
