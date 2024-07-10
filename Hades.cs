using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades : Boss
{
    public Hades()
    {
        fireRate = 2;
        inPosition = true; 
    }
    protected override void FireProjectile()
    {
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position, enemyProjectile.transform.rotation);
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position + new Vector3(0, 3), enemyProjectile.transform.rotation);
        Instantiate(enemyProjectile, projectileSpawnPos.transform.position + new Vector3(0, -3), enemyProjectile.transform.rotation);
        enemyAudio.PlayOneShot(projectileSFX, GameManager.volume);
    }
}
