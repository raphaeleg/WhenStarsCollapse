using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Shrink : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public bool triggeredShrink = true;

    public bool isShrinking() { return triggeredShrink; }
    public void disableShrink() { triggeredShrink = true; }
    public void enableShrink() { triggeredShrink = false; }

    public void ShrinkUntilDestroy(GameObject collider)
    {
        if (triggeredShrink) { return; }
        disableShrink();

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
            MoveTowards(collider.transform.position,RATE);
            yield return new WaitForSeconds(INTERVAL);
        }
        Destroy(gameObject);
    }

    public void ShrinkBy(Vector3 step) {
        transform.localScale -= step;
    }

    public void MoveTowards(Vector3 target, float step) {
        var direction = (transform.position - target)*step;
        transform.position -= direction;
    }
}
