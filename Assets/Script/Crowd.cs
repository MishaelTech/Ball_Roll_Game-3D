using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    private PlayerController player;

    [Range(0, 1)] public float defaultSpeed;
    [Range(1, 5)] public float cheeringSpeed;
    [Range(0, 1)] public float randomnessFactor;

    public float maximumHeight;

    [HideInInspector] public float currentSpeedFactor;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Awake()
    {
        currentSpeedFactor = defaultSpeed;
    }

    private void Update()
    {
        if (player)
        {
            UpdateState("Cheer");
            // Call when you want crowd to cheer
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdateState("Idle");
        }
    }

    private void UpdateState(string state)
    {
        switch (state)
        {
            case "Idle":
                currentSpeedFactor = defaultSpeed;
                // Set the speed to default value

                break;

            case "Cheer":
                currentSpeedFactor = cheeringSpeed;
                // Set the speed to cheering value
                // ADD a cheering sound, fireworks and animation

                break;
        }
    }
}
