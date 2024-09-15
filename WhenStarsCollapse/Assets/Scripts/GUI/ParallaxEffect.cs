using UnityEngine;

/// <summary>
/// Gives an object a Parallax Effect based on mouse movement.
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    private Vector2 startPosition;
    [SerializeField] int distanceRange = 75;
    [SerializeField] float speed = 2f;

     private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 camCenter = new(Screen.width / 2, Screen.height / 2);
        float t = speed * Time.deltaTime;

        float xPos = (mousePos.x - camCenter.x) / Screen.width;
        float yPos = (mousePos.y - camCenter.y) / Screen.height;
        Vector2 targetPos = new(-xPos, -yPos);

        transform.localPosition = Vector2.Lerp(transform.localPosition, startPosition + targetPos * distanceRange, t);
    }
}
