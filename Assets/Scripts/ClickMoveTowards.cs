using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClickMoveTowards : MonoBehaviour
{
    [HideInInspector]
    public GameObject floor;
    [HideInInspector]
    public GameObject movementIndicator;
    public float moveSpeed = 1f;
    [HideInInspector]
    public Vector3 moveTo;
    public bool canHoldMouseDown = true;
    public bool keepYPos = true;

    // Start is called before the first frame update
    void Awake()
    {
        if (floor == null)
            floor = GameObject.Find("Floor");
        if (movementIndicator == null)
            movementIndicator = GameObject.Find("Move Indicator");
        Debug.Assert(floor != null);
        Debug.Assert(movementIndicator != null);
        Debug.Assert(moveSpeed > 0);
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        MoveToPoint();
    }

    // Allows the player to click somewhere on our "floor", and the position of their click is saved
    private void RayCast()
    {
        // Control method 1: click to set the position player will travel to
        if (!canHoldMouseDown && Input.GetMouseButtonDown(0))
        {
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, 1);
            if (hit && hitInfo.transform.gameObject.Equals(floor))
                moveTo = hitInfo.point;
        }
        // Control method 2: click and hold to move player to the mouse position
        else if (canHoldMouseDown && Input.GetMouseButton(0)) {
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, Mathf.Infinity, 1);
            if (hit && hitInfo.transform.gameObject.Equals(floor))
                moveTo = hitInfo.point;
        }
    }

    private void MoveToPoint()
    {
        // correct the Y position of the destination if we want the object to maintain its previous Y
        if (keepYPos)
            moveTo.y = transform.position.y;
        // draw movement indicator
        if (movementIndicator != null)
            movementIndicator.transform.position = new Vector3(moveTo.x, movementIndicator.transform.position.y, moveTo.z);
        // Move the object to the destination, at a designated speed
        if (transform.position != moveTo)
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, moveTo, 0.5f), moveSpeed * Time.deltaTime);
    }
}
