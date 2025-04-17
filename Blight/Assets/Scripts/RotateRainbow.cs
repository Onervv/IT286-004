using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRainbow : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(30f, 45f, 60f); // Rotation speed
    public float colorCycleSpeed = 1f; // Speed of the color change

    private Light glowLight;
    private float hue;

    void Start()
    {
        glowLight = GetComponent<Light>();
    }

    void Update()
    {
        // Rotate the object
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Cycle through colors using HSV
        hue += colorCycleSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f; // Keep hue in range [0, 1]

        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        glowLight.color = rainbowColor;
    }
}
    