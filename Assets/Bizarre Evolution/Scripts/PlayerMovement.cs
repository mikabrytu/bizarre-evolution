using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]
    public Vector2 direction;
    
    public GameObject sprite;
    public Transform targetAim;
    public Vector2 speed = new Vector2(50, 50);

    private Rigidbody2D body;
    private Animator spriteAnimator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteAnimator = sprite.GetComponent<Animator>();
    }
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        direction = new Vector2(
            speed.x * x,
            speed.y * y
        );

        var spriteDirection = targetAim.position - sprite.transform.position;
        sprite.transform.up = direction;

        if (body.velocity != Vector2.zero)
            spriteAnimator.SetBool("moving", true);
        else if (body.velocity == Vector2.zero && spriteAnimator.GetBool("moving"))
            spriteAnimator.SetBool("moving", false);
    }

    void FixedUpdate()
    {
        body.velocity = direction;
    }
}
