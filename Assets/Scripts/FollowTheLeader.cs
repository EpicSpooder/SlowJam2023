using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float followDistance = 2f;

    [HideInInspector]
    public FollowTheLeader inFrontOfMe = null;
    [HideInInspector]
    public FollowTheLeader behindMe = null;
    [HideInInspector]
    public bool lineLeader;
    public float leaderSwapCooldown = 1.5f;
    private float leaderSwapTime;
    private FollowTheLeader hat;
    public bool keepYPos = true;
    public bool collisionStopsMovement = true;
    private bool hasClickMove = true;
    public static int pointsPerSlime = 10;
    private static int numSlimesInLine = 1;
    private Color myColor = Color.clear;
    private bool ghosting = false;
    public static int totalPoints = 0;
    private Vector3 lastDirection = Vector3.zero;

    void Start()
    {
        // Find an active hat in the scene. Leaving the possiblity open for multiple types of hats by using a tag
        // The hat to be used should be enabled/active in the scene while other hats are disabled/inactive.
        if (hat == null)
            hat = GameObject.FindGameObjectWithTag("Hat").GetComponent<FollowTheLeader>();
        Debug.Assert(hat != null);

        // Slimes must have a color, assumed not to change at runtime so is stored for more efficient reference
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        if (myRenderer != null)
        {
            myColor = myRenderer.color;
            Debug.Assert(!myColor.Equals(Color.clear));
        }
        

        // Initial player slime MUST be named "Player" in the scene object hierarchy or have the player tag
        // Warning: having multiple ^ might break things!
        if (name.Equals("Player") || CompareTag("Player"))
        {
            lineLeader = true;
            Debug.Assert(Camera.main.GetComponent<FollowTheLeader>() != null);
            Debug.Assert(Camera.main.GetComponent<CameraControls>() != null);
            Camera.main.GetComponent<FollowTheLeader>().inFrontOfMe = GetComponent<FollowTheLeader>();
            Camera.main.GetComponent<CameraControls>().lookAtPosition = transform;
            hat.inFrontOfMe = GetComponent<FollowTheLeader>();
        } else
        {
            if (GetComponent<ClickMoveTowards>() != null)
                GetComponent<ClickMoveTowards>().enabled = false;
            else
                hasClickMove = false;
        }
         
    }

    void Update()
    {
        if (inFrontOfMe == null)
            UpdateInactive(); // Currently not used for anything- could change in the future if we want slimes to have behavior (wandering?) when not collected
        else
        {
            // Follow slime in front of me
            Follow();

            // Check for matches if I have reached the chain, there are at least 2 slimes in front to form a match with,
            // AND I am the backmost slime of this color
            if (!ghosting && inFrontOfMe != null && inFrontOfMe.inFrontOfMe != null && (behindMe == null || !behindMe.myColor.Equals(myColor)))
                Match3(0, myColor);
        }
            
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
                if (collisionStopsMovement)
                {
                    // stop movement from other script
                    ClickMoveTowards timeToStop = GetComponent<ClickMoveTowards>();
                    timeToStop.moveTo = transform.position;
                    timeToStop.movementIndicator.transform.position = new Vector3(timeToStop.moveTo.x, timeToStop.movementIndicator.transform.position.y, timeToStop.moveTo.z);
                }

                AddBehindMe(theOtherGuy);
            } 
            // Slime is already in our line. Transfer hat (note the cooldown between hat transfers)
            else if (lineLeader && Time.time > leaderSwapTime)
            {
                ReparentLine(theOtherGuy);
            }
        }
    }

    public void BecomeLeader()
    {
        // the internet told me GetComponent was an expensive call so I try to do it only once or twice when possible
        FollowTheLeader newLeader = GetComponent<FollowTheLeader>();

        if (hasClickMove)
            newLeader.GetComponent<ClickMoveTowards>().enabled = true;
        Camera.main.GetComponent<FollowTheLeader>().inFrontOfMe = newLeader;
        Camera.main.GetComponent<CameraControls>().lookAtPosition = newLeader.transform;
        hat.inFrontOfMe = newLeader;
        newLeader.lineLeader = true;
        newLeader.leaderSwapTime = Time.time + leaderSwapCooldown;
    }
    public void BecomeFollower()
    {
        if (hasClickMove)
            GetComponent<ClickMoveTowards>().enabled = false;
        lineLeader = false;
    }

    public void UpdateInactive()
    {
        // lol
    }

    public void Follow()
    {
        if (Vector3.Distance(inFrontOfMe.transform.position, transform.position) > followDistance)
        {
            // correct the Y position of the destination if we want the object to maintain its previous Y
            if (keepYPos)
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, new Vector3(inFrontOfMe.transform.position.x, transform.position.y, inFrontOfMe.transform.position.z), 0.5f), moveSpeed * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, inFrontOfMe.transform.position, 0.5f), moveSpeed * Time.deltaTime);
            
            // keep the direction we were traveling for use when we exit ghosting/join the chain
            if (ghosting)    
                lastDirection = (inFrontOfMe.transform.position - transform.position).normalized;
        } 
        else if (ghosting)
        {
            transform.position += lastDirection * inFrontOfMe.followDistance * 0.75f; 
            ToggleGhost();
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
            if (!lineLeader)
                toAdd.ToggleGhost();
            numSlimesInLine++;
        }
        
    }

    // behavior of a slime when it is "collected" so we can make sure it gets to the back of the line easily
    public void ToggleGhost()
    {
        if (ghosting)
        {
            ghosting = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<Rigidbody>().useGravity = true;
            followDistance = inFrontOfMe.followDistance;
            moveSpeed = inFrontOfMe.moveSpeed;
        } 
        else
        {
            ghosting = true;
            moveSpeed *= 2;
            followDistance = 0.2f;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().isTrigger = true;
        }
    }

    public void ReparentLine(FollowTheLeader newLeader)
    {
        // change leadership of line
        newLeader.BecomeLeader();
        BecomeFollower();

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

        if (collisionStopsMovement && hasClickMove) {
            // stop movement from other script
            ClickMoveTowards timeToStop = newLeader.GetComponent<ClickMoveTowards>();
            timeToStop.moveTo = newLeader.transform.position;
            timeToStop.movementIndicator.transform.position = new Vector3(timeToStop.moveTo.x, timeToStop.movementIndicator.transform.position.y, timeToStop.moveTo.z);
        }
        
    }

    public int Match3(int numMatchingSlimes, Color matchColor)
    {
        // When we come across a slime that does not match
        if (!myColor.Equals(matchColor))
            return numMatchingSlimes;

        numMatchingSlimes++;
        if (inFrontOfMe != null)
            numMatchingSlimes = inFrontOfMe.Match3(numMatchingSlimes, matchColor);
            
        if (numMatchingSlimes >= 3) {

            if (lineLeader && numSlimesInLine > numMatchingSlimes)
            {
                // behindme becomes leader
                behindMe.BecomeLeader();
                BecomeFollower();
            }
            else
            {
                if (behindMe != null)
                    behindMe.inFrontOfMe = inFrontOfMe;
            }
            
            // you're going to the shadow realm, jimbo
            AddPoints(pointsPerSlime);
            numSlimesInLine--;
            Destroy(gameObject);

            //ADD TO UI SLIME CANVAS.
            // Find the object with the specified tag
            GameObject uiSlimeCanvas = GameObject.FindWithTag("UISlimeCanvas");

            // Check if the object was found
            if (uiSlimeCanvas != null)
            {
                    // Try to get the script component
                    SpawnAndParent spawner = uiSlimeCanvas.GetComponent<SpawnAndParent>();

                    // Check if the script component was found
                    if (spawner != null)
                    {
                        // Call the method on the script
                        spawner.SpawnAndParentObject();
                    }
                    else
                    {
                        Debug.LogError("SpawnAndParent script not found on UISlimeCanvas.");
                    }
            }
            else
            {
                Debug.LogError("UISlimeCanvas object not found.");
            }

        }
        return numMatchingSlimes;
    }

    public void AddPoints(int pointsToAdd)
    {
        Debug.Log("player gained " + pointsToAdd + " points");
        totalPoints += pointsToAdd;
    }
}
