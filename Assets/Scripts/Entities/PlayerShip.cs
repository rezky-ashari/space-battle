using RTools;
using UnityEngine;

public class PlayerShip : Ship
{
    [Tooltip("Player ship's move speed")]
    public float moveSpeed = 10;

    [Tooltip("Cooldown before next shoot")]
    public float cooldownTime = 1;

    [Tooltip("Lives indicator for player ship")]
    public LivesPanel livesPanel;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move based on keyboard input
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Shoot with cooldown time
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            time = cooldownTime;
        }

        // Always facing mouse pointer
        transform.rotation = Util.GetRotationToMouse(transform.position);
    }

    protected override void OnGotShot(int damage)
    {
        livesPanel.live--;
        if (livesPanel.live == 0)
        {            
            Scene.SendEvent("OnGameOver");
        }
    }
}
