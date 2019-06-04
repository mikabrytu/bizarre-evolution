using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource poolSFX;
    [SerializeField]
    private AudioSource dash;
    [SerializeField]
    private AudioSource mist;
    [SerializeField]
    private AudioSource stopTime;
    [SerializeField]
    private AudioSource absorb;
    [SerializeField]
    private AudioSource death;
    [SerializeField]
    private AudioSource endGameST;

    private bool endGame = false;

    void Start()
    {
        poolSFX.Play();
    }

    void Update()
    {
        if (!DataManager.isTimeRunnig)
        {
            poolSFX.Pause();
        } else if (!poolSFX.isPlaying && !endGame)
        {
            poolSFX.Play();
        }
    }

    public void PlayDash()
    {
        dash.Play();
    }

    public void PlayMist()
    {
        mist.Play();
    }

    public void PlayStopTime()
    {
        if (absorb.isPlaying)
            StopAbsorb();
        
        stopTime.Play();
    }

    public void PlayAbsorb()
    {
        if (!absorb.isPlaying)
            absorb.Play();
    }

    public void StopAbsorb()
    {
        if (absorb.isPlaying)
            absorb.Pause();
    }

    public void PlayDeath()
    {
        absorb.Play();
    }

    public void PlayEndGame()
    {
        endGame = true;

        if (poolSFX.isPlaying)
            poolSFX.Stop();
        if (dash.isPlaying)
            dash.Stop();
        if (mist.isPlaying)
            mist.Stop();
        if (stopTime.isPlaying)
            stopTime.Stop();
        if (absorb.isPlaying)
            absorb.Stop();
        if (death.isPlaying)
            death.Stop();
        if (endGameST.isPlaying)
            endGameST.Stop();

        endGameST.Play();
    }
}
