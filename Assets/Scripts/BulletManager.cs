using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Tooltip("Prefab for bullet")]
    public GameObject bulletPrefab;

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
        bullet.gameObject.SetActive(true);
        bullet.onDisappear = SaveBullet;

        Collider2D collider = bullet.GetComponent<Collider2D>();
        collider.enabled = true;
        Physics2D.IgnoreCollision(collider, ship.GetComponent<Collider2D>());
        
        return bullet;
    }

    /// <summary>
    /// Save bullet to the pool.
    /// </summary>
    /// <param name="bullet"></param>
    private void SaveBullet(Bullet bullet)
    {
        bullet.GetComponent<Collider2D>().enabled = false;
        pool.Add(bullet);
    }
}
