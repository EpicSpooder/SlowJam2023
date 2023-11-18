using UnityEngine;

public class SlimePulsate : MonoBehaviour
{
    public float pulsationFrequency = 1.0f;  // Adjust the frequency of pulsation
    public float pulsationAmplitudeX = 0.2f;  // Adjust the amplitude of pulsation along X
    public float pulsationAmplitudeY = 0.1f; // Adjust the amplitude of pulsation along Y
    public float randomOffsetRange = 0.1f;    // Introduce randomness for variation

    private Vector3 initialScale;
    private float randomOffset;

    void Start()
    {
        initialScale = transform.localScale;
        randomOffset = Random.Range(0f, 360f);  // Generate a random offset for each slime
    }

    void Update()
    {
        // Calculate pulsation factors based on time and add the unique random offset
        float pulsationFactorX = Mathf.Sin((Time.time + randomOffset) * pulsationFrequency)
                                * pulsationAmplitudeX;
        float pulsationFactorY = Mathf.Sin((Time.time + randomOffset) * pulsationFrequency)
                                * pulsationAmplitudeY;

        // Apply pulsation to the scale independently along X and Y
        Vector3 newScale = new Vector3(1.0f + pulsationFactorX, 1.0f + pulsationFactorY, 1.0f);
        transform.localScale = Vector3.Scale(initialScale, newScale);
    }
}