using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Bullet damage")]
    public int damage = 40;

    [Tooltip("Who shot this bullet")]
    public string source;

    /// <summary>
    /// Method to execute when this bullet disappear.
    /// </summary>
    public Action<Bullet> onDisappear;

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
        gameObject.SetActive(false);
        onDisappear?.Invoke(this);
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
