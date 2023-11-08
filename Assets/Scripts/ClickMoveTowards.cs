using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClickMoveTowards : MonoBehaviour
{
    public GameObject floor;
    public float moveSpeed = 1f;
    private Vector3 moveTo;

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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
            if (!hit)
            {
                Debug.Log("no hit");
            }
            else if (hitInfo.transform.gameObject.Equals(floor))
            {
                moveTo = hitInfo.point;
                Debug.Log("hit " + hitInfo.transform.gameObject.name);
            }
        }
    }

    private void MoveToPoint()
    {
        if (moveTo != null)
        {
            // Move the object to a 3d coordinate, at a given speed, until it gets there
            if (transform.position != moveTo)
                transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);
        }
    }
}
