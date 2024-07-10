using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minotaur : Enemy
{
    float t = 0.0f;
    float start = GameManager.xBound - 1;
    float end = -GameManager.xBound + 1;
    float temp;
    bool inPosition = false;
    public GameObject rockSpawnPos;
    public Minotaur()
    {
        health = 300;
        fireRate = 2;
        score = 250;
    }
    void FixedUpdate()
    {
        if (inPosition)
            SetMovement();
    }
    void SetMovement()
    {
        transform.position = new Vector3(Mathf.Lerp(start, end, t), transform.position.y);
        t += 0.35f * Time.deltaTime;
        if (t > 1.0f)
        {
            temp = end;
            end = start;
            start = temp;
            t = 0.0f;
        }
    }
    public IEnumerator SetPosition(float start, float end)
    {
        float timeElapsed = 0;
        float lerpValue;
        float delay = 1;
        while (timeElapsed < delay)
        {
            t = timeElapsed / delay;
            lerpValue = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            transform.position = new Vector3(lerpValue, transform.position.y);
            yield return null;
        }
        lerpValue = end;
        transform.position = new Vector3(lerpValue, transform.position.y);
        inPosition = true;
    }
    protected override void FireProjectile()
    {
        animatorController.SetTrigger("DropRock");
        Instantiate(enemyProjectile, rockSpawnPos.transform.position, enemyProjectile.transform.rotation);
        enemyAudio.PlayOneShot(projectileSFX, GameManager.volume);
    }
}
