using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Bullet damage")]
    public int damage = 40;

    /// <summary>
    /// Who shot this bullet.
    /// </summary>
    [HideInInspector]
    public string source;

    /// <summary>
    /// Shooter gameObject.
    /// </summary>
    [HideInInspector]
    public Ship sourceObject;

    /// <summary>
    /// Method to execute when this bullet disappear.
    /// </summary>
    public Action<Bullet> onDisappear;

    private Collider2D Collider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider2D>();
            }
            return _collider;
        }
    }
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy();
    }

    public void Initialize(Ship ship)
    {
        source = ship is PlayerShip ? "Player" : "Enemy";
        sourceObject = ship;
        gameObject.SetActive(true);

        Physics2D.IgnoreCollision(Collider, sourceObject.GetComponent<Collider2D>(), true);
    }

    public void Destroy()
    {
        if (gameObject.activeSelf)
        {
            Collider.enabled = false;
            gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            onDisappear?.Invoke(this);

            Collider.enabled = true;
        }
    }

    public void ShootToMousePointer(Vector3 position, float speed = 3)
    {
        ShootTo(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), position, speed);
    }

    public void ShootTo(Vector2 target, Vector2 position, float speed = 3)
    {
        transform.position = position;

        Vector2 myPos = new Vector2(position.x, position.y);
        Vector2 direction = target - myPos;
        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        Quaternion rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90);
        transform.rotation = rotation;
    }
}
