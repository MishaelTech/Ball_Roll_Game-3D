using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    private Crowd crowd;

    private float angle;
    private float startingYPosition;
    private float yOffset;
    private float randomSpeed;

    private void Start()
    {
        crowd = FindObjectOfType<Crowd>();

        startingYPosition = transform.position.y;
        randomSpeed = Random.Range(crowd.defaultSpeed - crowd.randomnessFactor, crowd.defaultSpeed + crowd.randomnessFactor);

        ChooseRandomColor();
    }

    private void FixedUpdate()
    {
        yOffset = startingYPosition + crowd.maximumHeight;
        angle += crowd.currentSpeedFactor * 0.1f * randomSpeed;

        Vector3 newPos = new Vector3(transform.position.x, yOffset + Mathf.Sin(angle) * crowd.maximumHeight, transform.position.z);
        transform.position = newPos;
    }

    private void ChooseRandomColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;

        material.color = new Color(Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255f, Random.Range(0, 256) / 255F);

        renderer.material = material;
    }
}
