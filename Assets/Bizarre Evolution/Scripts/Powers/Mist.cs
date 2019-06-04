using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mist : MonoBehaviour
{

    public enum State { Ready, CoolDown };

    public Animator animator;
    public CircleCollider2D collider;
    public float timerLimit;
    public float cooldownLimit;

    private AudioManager audioManager;
    private State state;
    private float timer;
    private float cooldown;
    private bool misting = false;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        state = State.Ready;
    }

    void Update()
    {
        if (DataManager.unlockedPowers[1])
        {
            if (!misting && 
                (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Joystick1Button1)) &&
                state == State.Ready)
            {
                misting = true;
                timer = timerLimit;
                audioManager.PlayMist();
                animator.SetBool("mist", true);
            }

            if (misting)
            {
                if (timer > 0)
                {
                    collider.enabled = false;
                    timer -= Time.deltaTime;
                } else
                {
                    collider.enabled = true;
                    misting = false;
                    state = State.CoolDown;
                    cooldown = cooldownLimit;
                    if (animator.GetBool("mist"))
                        animator.SetBool("mist", false);
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
