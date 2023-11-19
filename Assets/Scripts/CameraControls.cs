using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControls : MonoBehaviour
{
    public float rotateSens = 1.5f;
    [HideInInspector]
    public Transform lookAtPosition = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(lookAtPosition != null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtPosition);

        // Camera Tumble
        if (Input.GetMouseButton(1))
        {

            // https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
            if (Input.GetAxis("Mouse X") > 0)
            {
                ComputeVerticalOrbit(1f);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                ComputeVerticalOrbit(-1f);
            }

            if (Input.GetAxis("Mouse Y") > 0)
            {
                ComputeHorizontalOrbit(-1f);
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                ComputeHorizontalOrbit(1f);
            }

        }
    }

    public void ComputeHorizontalOrbit(float Direction)
    {
        // 1. Rotation of the viewing direction via right axis
        Quaternion q = Quaternion.AngleAxis(Direction * rotateSens, transform.right);

        // 2. Rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-lookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);


        if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) > 0.7071f) // this is about 45-degrees
        {
            return;
        }

        //transform.localPosition = newCameraPos;
        //transform.localRotation = q * transform.localRotation;
        transform.SetLocalPositionAndRotation(newCameraPos, q * transform.localRotation);
    }

    public void ComputeVerticalOrbit(float Direction)
    {
        // 1. Rotation of the viewing direction via right axis
        Quaternion q = Quaternion.AngleAxis(Direction * rotateSens, transform.up);

        // 2. Rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-lookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);

        //transform.localPosition = newCameraPos;
        //transform.localRotation = q * transform.localRotation;
        transform.SetLocalPositionAndRotation(newCameraPos, q * transform.localRotation);

    }
}
