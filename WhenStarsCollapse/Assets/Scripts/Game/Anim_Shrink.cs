using System.Collections;
using UnityEngine;

/// <summary>
/// Attached to an object, plays an animation when it collides with a BlackHole.
/// </summary>
public class Anim_Shrink : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    private bool triggeredShrink = true;

    public bool IsShrinking() 
    { 
        return triggeredShrink; 
    }
    public void DisableShrink() 
    { 
        triggeredShrink = true; 
    }
    public void EnableShrink() 
    { 
        triggeredShrink = false; 
    }

    public void ShrinkUntilDestroy(GameObject collider)
    {
        if (triggeredShrink) 
        { 
            return; 
        }
        DisableShrink();

        StartCoroutine(Shrink(collider));
    }

    public IEnumerator Shrink(GameObject collider)
    {
        const float INTERVAL = 0.01f;
        float RATE = INTERVAL*speed;

        Vector3 scale = transform.localScale;
        float ratio = scale.x/scale.y;
        Vector3 scaleStep = new(RATE*ratio, RATE, 0);
        
        while (transform.localScale.x > INTERVAL)
        {
            ShrinkBy(scaleStep);
            if (collider is not null)
            {
                MoveTowards(collider.transform.position,RATE);
            }
            yield return new WaitForSeconds(INTERVAL);
        }
        Destroy(gameObject);
    }

    public void ShrinkBy(Vector3 step) 
    {
        transform.localScale -= step;
    }

    public void MoveTowards(Vector3 target, float step) 
    {
        var direction = (transform.position - target)*step;
        transform.position -= direction;
    }
}
