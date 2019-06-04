using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [HideInInspector]
    public Vector2 direction;

    public int maxTimer;
    public float speed;

    private Rigidbody2D body;
    private float timerLimit;
    private float timer = 0;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            NewValues();
            timer = timerLimit;
        }
    }

    void FixedUpdate()
    {
        body.velocity = DataManager.isTimeRunnig ? direction * speed : Vector2.zero;
    }

    private void NewValues()
    {
        timerLimit = Random.Range(0, maxTimer);
        
        int x = (int) Random.Range(-1, 2);
        int y = (int) Random.Range(-1, 2);

        direction = new Vector2(x, y);
    }
    
}
