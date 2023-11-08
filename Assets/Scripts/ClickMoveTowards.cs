using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClickMoveTowards : MonoBehaviour
{
    public GameObject floor;
    public float moveSpeed = 1f;
    private Vector3 moveTo;
    private bool shouldMove = false;
    public bool holdMouseToControl = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(floor != null);
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
        if (!holdMouseToControl && Input.GetMouseButtonDown(0))
        {
            shouldMove = true;
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
            if (!hit)
            {
                //Debug.Log("no hit");
            }
            else if (hitInfo.transform.gameObject.Equals(floor))
            {
                moveTo = hitInfo.point;
                //Debug.Log("hit " + hitInfo.transform.gameObject.name);
            }  
        }
        // Control method 2: click and hold to move player to the mouse position
        else if (holdMouseToControl && Input.GetMouseButton(0)) {
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
            if (!hit)
            {
            }
            else if (hitInfo.transform.gameObject.Equals(floor))
            {
                moveTo = hitInfo.point;
            }
            shouldMove = true;
        } else if (holdMouseToControl && Input.GetMouseButtonUp(0)) {
            shouldMove = false; // stop moving when mouse button released
        }
    }

    private void MoveToPoint()
    {
        if (shouldMove && moveTo != null)
        {
            // Move the object to a 3d coordinate, at a designated speed, until it gets there
            if (transform.position != moveTo)
                transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, moveTo, 0.5f), moveSpeed * Time.deltaTime);
        }
    }
}
