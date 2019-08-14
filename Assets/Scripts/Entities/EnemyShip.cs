using RTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    /// <summary>
    /// Called when this enemy ship has been destroyed.
    /// </summary>
    public Action onDestroyed;

    Healthbar healthbar;
    RezTween.Timer timer;

    private void OnEnable()
    {
        if (healthbar == null)
        {
            healthbar = GetComponentInChildren<Healthbar>();
        }
        healthbar.health = 100;

        timer = new RezTween.Timer(3, gameObject)
        {
            onTick = Fire
        };
        timer.Start();
    }

    /// <summary>
    /// Get the nearest ship.
    /// </summary>
    /// <returns></returns>
    GameObject FindClosestShip()
    {
        GameObject closestShip = null;

        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i] == gameObject) continue;
            if (closestShip == null || Vector2.Distance(transform.position, ships[i].transform.position) < Vector2.Distance(transform.position, closestShip.transform.position))
            {
                closestShip = ships[i];
            }
        }

        return closestShip;
    }

    /// <summary>
    /// Fire the bullet.
    /// </summary>
    void Fire()
    {
        GameObject closestShip = FindClosestShip();
        if (closestShip != null)
        {              
            spriteTransform.rotation = Util.GetRotationTo(closestShip.transform.position, transform.position, 90);
            GetComponent<Rigidbody2D>().AddForce((closestShip.transform.position - transform.position) * 10, ForceMode2D.Force);
            RezTween.DelayedCall(1f, () =>
            {
                spriteTransform.rotation = Util.GetRotationTo(closestShip.transform.position, transform.position, 90);
                ShootTo(closestShip.transform.position);
            });
        }
        else
        {
            Debug.Log("No closest ship found. Maybe you forgot to set the tag.");
        }
    }

    protected override void OnGotShot(int damage, string source)
    {
        if (source.Equals("Player"))
        {
            SessionData.score++;
            Scene.SendEvent("OnGotHitByPlayer");
        }

        healthbar.health -= damage;
        if (healthbar.health <= 0)
        {
            OnEnemyDestroyed();
            if (source.Equals("Player"))
            {
                SessionData.enemiesKilled++;
                Scene.SendEvent("OnEnemyKilled");
            }
        }
        healthbar.UpdateHealth();
    }

    private void OnDestroy()
    {
        timer?.Stop(10);
    }

    void OnEnemyDestroyed()
    {
        onDestroyed?.Invoke();
        CoinManager.Instance.SpawnCoinAt(transform.position);
        Destroy(gameObject);
    }
}
