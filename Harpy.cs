using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Enemy
{
    float duration = 4;
    float t;
    float lerpValue;
    public Harpy()
    {
        health = 100;
        fireRate = 2;
        score = 100;
    }
    public IEnumerator SetMovement(float start, float end)
    {
        float timeElapsed = 0;
        while(timeElapsed < duration)
        {
            t = timeElapsed / duration;
            lerpValue = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            transform.position = new Vector3(lerpValue, transform.position.y);
            yield return null;
        }
        lerpValue = end;
        transform.position = new Vector3(lerpValue, transform.position.y);
    }
}
