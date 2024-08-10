using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float[] yLevels = new float[4] { 1f, 3f, 5f, 7f}; // Define your four y-levels here
    public float transitionTime = 1f; // Time it takes to move between levels
    public float speedFactor = 1f; // Speed factor to adjust the movement speed

    private int currentLevel = 3; // Index to track the current y-level
    private bool isMoving = false; // Flag to check if the camera is currently moving
    private Vector3 startPosition; // Starting position for the movement
    private Vector3 targetPosition; // The target position to move the camera to
    private float elapsedTime = 0f; // Elapsed time for the transition

    void start()
    {
        
    }

    void Update()
    {
        // Move the camera if it is in the process of transitioning
        if (isMoving)
        {
            elapsedTime += Time.deltaTime * speedFactor;
            float t = elapsedTime / transitionTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Check if the movement is completed
            if (t >= 1f)
            {
                isMoving = false;
                elapsedTime = 0f;
                transform.position = targetPosition; // Ensure the camera reaches the exact target position
            }
        }
    }

    public void MoveUp()
    {
        if (!isMoving && currentLevel < yLevels.Length - 1)
        {
            currentLevel++;
            StartMovement();
        }
    }

    public void MoveDown()
    {
        if (!isMoving && currentLevel > 0)
        {
            currentLevel--;
            StartMovement();
        }
    }

    void StartMovement()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, yLevels[currentLevel], transform.position.z);
        isMoving = true;
        elapsedTime = 0f; // Reset elapsed time for the new movement
    }
}