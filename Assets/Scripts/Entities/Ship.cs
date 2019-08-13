using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Tooltip("How fast this bullet")]
    public float bulletSpeed = 5;

    [Tooltip("Transform of ship's sprite object")]
    public Transform spriteTransform;

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
            Bullet bullet = collision.GetComponent<Bullet>();
            OnGotShot(bullet.damage, bullet.source);
        }
        else
        {
            OnCollideWith(collision.gameObject);
        }
    }

    /// <summary>
    /// Called when this ship got hit by a bullet.
    /// </summary>
    /// <param name="damage">Damage cost</param>
    /// <param name="source">Who shot this bullet</param>
    protected virtual void OnGotShot(int damage, string source)
    {
        Debug.Log(gameObject.name  + " got shot. Damage " + damage);
    }

    /// <summary>
    /// Called when this ship collided with other game object.
    /// </summary>
    /// <param name="gameObject"></param>
    protected virtual void OnCollideWith(GameObject gameObject)
    {
        Debug.Log("Collide with trigger " + gameObject.name);
    }
}
