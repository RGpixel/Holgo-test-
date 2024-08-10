using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Define the three z positions
    public float[] zPositions = new float[3] { 0f, 5f, 10f };
    
    // The speed of the transition
    public float transitionSpeed = 2.0f;
    
    // Internal variables to manage the state
    private int currentZIndex = 0;
    private int targetZIndex = 0;
    private bool isTransitioning = false;
    
    void Update()
    {
        // Check if a button is pressed to initiate the transition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToNextHeight();
        }

        // Handle the transition if it's active
        if (isTransitioning)
        {
            PerformTransition();
        }
    }

    void MoveToNextHeight()
    {
        // Increment the target index, wrapping around if necessary
        targetZIndex = (currentZIndex + 1) % zPositions.Length;
        isTransitioning = true;
    }

    void PerformTransition()
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        // Calculate the target position
        Vector3 targetPosition = new Vector3(currentPosition.x, zPositions[targetZIndex], currentPosition.z);

        // Move the camera towards the target position
        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * transitionSpeed);

        // Check if the camera is close enough to the target position to stop the transition
        if (Vector3.Distance(currentPosition, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            currentZIndex = targetZIndex;
            isTransitioning = false;
        }
    }
}
