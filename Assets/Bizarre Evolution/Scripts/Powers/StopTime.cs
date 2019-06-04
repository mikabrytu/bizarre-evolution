using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime : MonoBehaviour
{

    public enum State { Ready, CoolDown };

    public float timerLimit;
    public float cooldownLimit;

    private AudioManager audioManager;
    private State state;
    private float timer;
    private float cooldown;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

        state = State.Ready;
    }
    
    void Update()
    {
        if (DataManager.unlockedPowers[2])
        {
            if (DataManager.isTimeRunnig && 
                (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Joystick1Button2)) &&
                state == State.Ready)
            {
                DataManager.isTimeRunnig = false;
                timer = timerLimit;
                audioManager.PlayStopTime();
            }

            if (!DataManager.isTimeRunnig)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                } else
                {
                    DataManager.isTimeRunnig = true;
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
