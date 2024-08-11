using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    public Vector2 origin;
    public float cameraSpeed;
    public float distanceRange = 1f;

    private void Start()
    {
        origin = transform.localPosition;
    }

    // Camera follow mouse position
    void FixedUpdate()
    {
        // Find mouse screen position
        Vector2 pos = Input.mousePosition;
        Vector2 playerPos = new Vector2(Screen.width / 2, Screen.height / 2);
        float xPos = (pos.x - playerPos.x) / Screen.width;
        float yPos = (pos.y - playerPos.y) / Screen.height;
        //Debug.Log(xPos + " " + yPos);

        // Camera go to mouse
        Vector2 targetPos = new Vector2(-xPos, -yPos);
        transform.localPosition = Vector2.Lerp(transform.localPosition, origin + targetPos * distanceRange, cameraSpeed * Time.deltaTime);

    }
}