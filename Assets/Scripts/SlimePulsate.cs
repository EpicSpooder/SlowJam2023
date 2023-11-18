using UnityEngine;

public class SlimePulsate : MonoBehaviour
{
    public float pulsationFrequency = 1.0f;  // Adjust the frequency of pulsation
    public float pulsationAmplitude = 0.1f;   // Adjust the amplitude of pulsation
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
        // Calculate pulsation factor based on time and add the unique random offset
        float pulsationFactor = Mathf.Sin((Time.time + randomOffset) * pulsationFrequency)
                               * pulsationAmplitude;

        // Apply pulsation to the scale
        transform.localScale = initialScale * (1.0f + pulsationFactor);
    }
}