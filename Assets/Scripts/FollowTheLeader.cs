using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float followDistance = 2f;

    public GameObject inFrontOfMe = null;
    
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (inFrontOfMe == null)
            UpdateInactive();
        else
            Follow();
    }

    public void UpdateInactive()
    {

    }

    public void Follow()
    {
        if (Vector3.Distance(inFrontOfMe.transform.position, transform.position) > followDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, inFrontOfMe.transform.position, 0.5f), moveSpeed * Time.deltaTime);
        }
    }
}
