using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy 
{
    float timer = 0;
    float duration = 1;
    float t;
    protected bool inPosition = false;
    public GameObject projectileSpawnPos;
    public Boss()
    {
        health = 500;
        fireRate = 3;
        score = 500;
    }
    public override void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerController>();
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.volume = GameManager.volume;
        animatorController = GetComponent<Animator>();
        InvokeRepeating("Attack", startDelay, fireRate);
    }
    void FixedUpdate()
    {
        if(inPosition)
            SetMovement(); 
    }
    void SetMovement()
    {
        timer += Time.deltaTime;
        t = timer / duration;
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y, t));
        timer = 0;
        if (transform.position.y < -GameManager.yBound + 1)
            transform.position = new Vector3(transform.position.x, -GameManager.yBound + 1);
        else if (transform.position.y > GameManager.yBound + 1)
            transform.position = new Vector3(transform.position.x, GameManager.yBound + 1);
    }
    public IEnumerator SetPosition(float start, float end)
    {
        float timeElapsed = 0;
        float lerpValue;
        float delay = 4;
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
    void Attack()
    {
        animatorController.SetTrigger("Attack");
    }
}
