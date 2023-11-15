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
    private FollowTheLeader hat;
    public bool keepYPos = true;

    void Start()
    {
        // Find an active hat in the scene. Leaving the possiblity open for multiple types of hats by using a tag
        // The hat to be used should be enabled/active in the scene while other hats are disabled/inactive.
        if (hat == null)
            hat = GameObject.FindGameObjectWithTag("Hat").GetComponent<FollowTheLeader>();
        Debug.Assert(hat != null);

        // Initial player slime MUST be named "Player" in the scene object hierarchy or have the player tag
        // Warning: having multiple ^ might break things!
        if (name.Equals("Player") || CompareTag("Player"))
        {
            lineLeader = true;
            GetComponent<ClickMoveTowards>().enabled = true;
            Debug.Assert(Camera.main.GetComponent<FollowTheLeader>() != null);
            Debug.Assert(Camera.main.GetComponent<CameraControls>() != null);
            Camera.main.GetComponent<FollowTheLeader>().inFrontOfMe = GetComponent<FollowTheLeader>();
            Camera.main.GetComponent<CameraControls>().lookAtPosition = transform;
            hat.inFrontOfMe = GetComponent<FollowTheLeader>();
        }
         
    }

    void Update()
    {
        // Currently not used for anything- could change in the future if we want slimes to have behavior (wandering?) when not collected
        if (inFrontOfMe == null)
            UpdateInactive();
        else
            Follow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Should only care about collisions with other slime(s)
        FollowTheLeader theOtherGuy = collision.gameObject.GetComponent<FollowTheLeader>();
        if (theOtherGuy != null)
        {
            // Slime is not following anything i.e. not collected
            if (lineLeader && theOtherGuy.inFrontOfMe == null)
            {
                AddBehindMe(theOtherGuy);
            } 
            // Slime is already in our line. Transfer hat (note the cooldown between hat transfers)
            else if (lineLeader && Time.time > leaderSwapTime)
            {
                ReparentLine(theOtherGuy);
            }
        }
    }

    public void UpdateInactive()
    {
        // lol
    }

    public void Follow()
    { 
        // If there's something to follow, follow it.
        if (Vector3.Distance(inFrontOfMe.transform.position, transform.position) > followDistance)
        {
            // correct the Y position of the destination if we want the object to maintain its previous Y
            if (keepYPos)
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, new Vector3(inFrontOfMe.transform.position.x, transform.position.y, inFrontOfMe.transform.position.z), 0.5f), moveSpeed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, inFrontOfMe.transform.position, 0.5f), moveSpeed * Time.deltaTime);
        }
    }

    public void AddBehindMe(FollowTheLeader toAdd)
    {
        // Keep deferring to the slime in the very back when we add a new slime to the line
        if (behindMe != null)
            behindMe.AddBehindMe(toAdd);
        else // We are the back-most slime
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
        hat.inFrontOfMe = newLeader;
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
