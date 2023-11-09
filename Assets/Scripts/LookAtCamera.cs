using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (theCamera == null)
            theCamera = FindObjectOfType<Camera>();
        Debug.Assert(theCamera != null);
    }

    private void OnWillRenderObject()
    {
        transform.rotation = theCamera.transform.rotation;
    }
}
