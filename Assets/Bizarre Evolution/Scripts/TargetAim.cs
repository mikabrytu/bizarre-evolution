using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{

    public Vector2 range = new Vector2(3, 3);
    public string ignoreTag;
    public int damage;
    public int timerLimit;

    private Vector2 aim = new Vector2(0, 0);
    private float x, y;
    private float timer;

    void Start()
    {
        timer = timerLimit;
    }

    void Update()
    {
        if (gameObject.tag == "Player Aim")
            PlayerTarget();
        else
        {
            GetComponent<Collider2D>().enabled = DataManager.isTimeRunnig;
            EnemyTarget();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.contacts[0].collider;
        if (collider.tag == ignoreTag)
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
        else if (gameObject.tag == "Enemy Aim" && collider.tag == "Player")
            Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
    }
    
    private void PlayerTarget()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            if (h != 0)
                x = (h > 0) ? 1 : -1;
            else
                x = 0;
            
            if (v != 0)
                y = (v > 0) ? 1 : -1;
            else
                y = 0;

            aim = new Vector2(
                x * range.x,
                y * range.y
            );
        }

        transform.localPosition = aim;
    }

    private void EnemyTarget()
    {
        Vector2 direction = transform.GetComponentInParent<EnemyMovement>().direction;
        transform.localPosition = direction * range;
    }
}
