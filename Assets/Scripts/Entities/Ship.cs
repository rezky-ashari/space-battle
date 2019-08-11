using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Tooltip("How fast this bullet")]
    public float bulletSpeed = 5;

    /// <summary>
    /// Shoot the bullet to mouse pointer position.
    /// </summary>
    public void Shoot()
    {
        BulletManager.Instance.GetBullet(this).ShootToMousePointer(transform.position, bulletSpeed);
    }

    /// <summary>
    /// Shoot the bullet to a target.
    /// </summary>
    /// <param name="target"></param>
    public void ShootTo(Vector3 target)
    {
        BulletManager.Instance.GetBullet(this).ShootTo(target, transform.position, bulletSpeed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OnGotShot(collision.GetComponent<Bullet>().damage);
        }
    }

    protected virtual void OnGotShot(int damage)
    {
        Debug.Log(gameObject.name  + " got shot. Damage " + damage);
    }
}
