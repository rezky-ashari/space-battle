using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Tooltip("Prefab for bullet")]
    public GameObject bulletPrefab;

    [Tooltip("Prefab for rocket")]
    public GameObject rocketPrefab;

    /// <summary>
    /// Instance for bullet manager.
    /// </summary>
    public static BulletManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("BulletManager").GetComponent<BulletManager>();
                if (_instance == null)
                {
                    Debug.LogError("Please add a new GameObject with name 'BulletManager'. Add BulletManager component, then assign the bullet prefab");
                }
            }
            return _instance;
        }
    }
    private static BulletManager _instance;

    [Tooltip("Contains all bullets available to use")]
    public List<Bullet> pool = new List<Bullet>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Instantiate a rocket.
    /// </summary>
    /// <param name="ship">Source ship</param>
    /// <returns></returns>
    public Bullet GetRocket(Ship ship)
    {
        Bullet rocket = Instantiate(rocketPrefab, transform).GetComponent<Bullet>();
        rocket.Initialize(ship);
        rocket.onDisappear = (r) =>
        {
            Debug.Log("On Disappear rocket " + r.name);
            Destroy(r.gameObject);
        };
        return rocket;
    }

    /// <summary>
    /// Get bullet from the pool.
    /// </summary>
    /// <returns></returns>
    public Bullet GetBullet(Ship ship)
    {
        Bullet bullet = null;
        if (pool.Count > 0)
        {
            bullet = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        }
        bullet.Initialize(ship);
        bullet.onDisappear = SaveBullet;

        return bullet;
    }

    /// <summary>
    /// Save bullet to the pool.
    /// </summary>
    /// <param name="bullet"></param>
    private void SaveBullet(Bullet bullet)
    {
        pool.Add(bullet);
    }

    /// <summary>
    /// Clear all bullet instances from scene.
    /// </summary>
    public void ClearBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].activeInHierarchy) bullets[i].GetComponent<Bullet>().Destroy();
        }
    }
}
