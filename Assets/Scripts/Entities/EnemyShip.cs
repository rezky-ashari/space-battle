using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponentInChildren<Healthbar>();
        healthbar.health = 100;

        RezTween.Timer timer = new RezTween.Timer(3, gameObject)
        {
            onTick = Fire
        };
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject FindClosestShip()
    {
        GameObject closestShip = null;

        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            if (closestShip == null || Vector2.Distance(transform.position, ships[i].transform.position) < Vector2.Distance(transform.position, closestShip.transform.position))
            {
                closestShip = ships[i];
            }
        }

        return closestShip;
    }

    void Fire()
    {
        GameObject closestShip = FindClosestShip();
        if (closestShip != null)
        {
            ShootTo(closestShip.transform.position);
            transform.rotation = Util.GetRotationTo(closestShip.transform.position, transform.position, 90);
        }
        else
        {
            Debug.Log("No closest ship found. Maybe you forgot to set the tag.");
        }
    }

    protected override void OnGotShot(int damage)
    {
        Debug.Log("Got damage " + damage);
        healthbar.health -= damage;
        healthbar.UpdateHealth();
    }
}
