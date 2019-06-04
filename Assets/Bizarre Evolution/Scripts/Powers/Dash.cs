using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public enum State { Ready, CoolDown };

    public GameObject dashEffect;
    public Vector2 dashSpeed;
    public float timerLimit;
    public float cooldownLimit;

    private Rigidbody2D body;
    private PlayerMovement movement;
    private Vector2 savedSpeed;
    private State state;
    private AudioManager audioManager;
    private float timer;
    private float cooldown;
    private bool dashing = false;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();

        state = State.Ready;
    }

    void Update()
    {
        if (DataManager.unlockedPowers[0])
        {
            if (!dashing && 
                (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button0)) &&
                state == State.Ready)
            {
                dashing = true;
                timer = timerLimit;
                savedSpeed = movement.speed;
                audioManager.PlayDash();
                Instantiate(dashEffect, transform.position, dashEffect.transform.rotation);
            }

            if (dashing)
            {
                if (timer > 0)
                {
                    movement.speed = dashSpeed;
                    timer -= Time.deltaTime;
                } else
                {
                    dashing = false;
                    movement.speed = savedSpeed;
                    savedSpeed = Vector2.zero;

                    state = State.CoolDown;
                    cooldown = cooldownLimit;
                }
            }

            if (state == State.CoolDown)
            {
                if (cooldown > 0)
                    cooldown -= Time.deltaTime;
                else
                    state = State.Ready;
            }
        }
    }
}
