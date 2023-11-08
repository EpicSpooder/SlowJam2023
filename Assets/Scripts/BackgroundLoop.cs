using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollspeed = 0.5f;
    private float offset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        //useless experiments to fix this going over the canvas buttons
        //var tr = GetComponent<Renderer>();
        //tr.sortingLayerName = "Behind";

        //Renderer myRenderer = GetComponent<Renderer>();
        //myRenderer.sortingLayerName = "Bottom";
        //myRenderer.sortingOrder = -999;


    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollspeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
