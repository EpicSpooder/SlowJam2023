using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float followDistance = 2f;

    public FollowTheLeader inFrontOfMe = null;
    public FollowTheLeader behindMe = null;
    public bool lineLeader;
    public static float leaderSwapCooldown = 1.25f;
    private float leaderSwapTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(GetComponent<ClickMoveTowards>() != null);
        // Initial player slime must be named "Player" in the scene object hierarchy
        if (name.Equals("Player"))
        {
            lineLeader = true;
            GetComponent<ClickMoveTowards>().enabled = true;
            Debug.Assert(Camera.main.GetComponent<FollowTheLeader>() != null);
            Debug.Assert(Camera.main.GetComponent<CameraControls>() != null);
            Camera.main.GetComponent<FollowTheLeader>().inFrontOfMe = GetComponent<FollowTheLeader>();
            Camera.main.GetComponent<CameraControls>().lookAtPosition = transform;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (inFrontOfMe == null)
            UpdateInactive();
        else
            Follow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FollowTheLeader theOtherGuy = collision.gameObject.GetComponent<FollowTheLeader>();
        if (theOtherGuy != null)
        {
            if (lineLeader && theOtherGuy.inFrontOfMe == null)
            {
                Debug.Log("add to back");
                AddBehindMe(theOtherGuy);
            } 
            else if (lineLeader && Time.time > leaderSwapTime)
            {
                Debug.Log("new leader");
                ReparentLine(theOtherGuy);
            }
        }
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

    public void AddBehindMe(FollowTheLeader toAdd)
    {
        if (behindMe != null)
            behindMe.AddBehindMe(toAdd);
        else
        {
            behindMe = toAdd;
            toAdd.inFrontOfMe = GetComponent<FollowTheLeader>();
        }
            
    }

    public void ReparentLine(FollowTheLeader newLeader)
    {
        // change leadership of line
        newLeader.GetComponent<ClickMoveTowards>().enabled = true;
        GetComponent<ClickMoveTowards>().enabled = false;
        Camera.main.GetComponent<FollowTheLeader>().inFrontOfMe = newLeader;
        Camera.main.GetComponent<CameraControls>().lookAtPosition = newLeader.transform;
        lineLeader = false;
        newLeader.lineLeader = true;
        newLeader.leaderSwapTime = Time.time + leaderSwapCooldown;

        // realign the slimes that were in front/behind the new leader
        FollowTheLeader formerlyInFront = newLeader.inFrontOfMe;
        FollowTheLeader formerlyBehind = newLeader.behindMe;
        newLeader.inFrontOfMe = null;
        newLeader.behindMe = null;
        formerlyInFront.behindMe = formerlyBehind;
        if (formerlyBehind != null)
            formerlyBehind.inFrontOfMe = formerlyInFront;
        newLeader.behindMe = GetComponent<FollowTheLeader>();
        inFrontOfMe = newLeader;

        // stop movement
        newLeader.GetComponent<ClickMoveTowards>().moveTo = newLeader.transform.position;
    }
}
