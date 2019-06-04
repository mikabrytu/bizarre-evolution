using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    public Color selectedColor;
    public Animator animator;
    public Slider playerHP;
    public int health = 100;
    public int cellId;

    private GameManager gameManager;
    private AudioManager audioManager;
    private int maxHealth;
    private float lastHP;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        maxHealth = health;

        if (gameObject.tag == "Player")
            playerHP.maxValue = health;
    }
    
    void Update()
    {
        if (!DataManager.isTimeRunnig && gameObject.tag != "Player")
        {
            animator = GetComponent<Animator>();
            animator.enabled = false;
        } else if (DataManager.isTimeRunnig && animator != null && !animator.enabled)
            animator.enabled = true;

        if (health < 0)
        {
            if (gameObject.tag == "Superior Cells" && cellId == DataManager.goalEnemyId)
                DataManager.goalCount++;
            
            if (gameObject.tag == "Player")
            {
                if (animator != null)
                    animator.SetBool("dead", true);

                audioManager.PlayDeath();
                gameManager.GameOver();

                Destroy(gameObject, 0.5f);
            } else
                Destroy(gameObject);
        }

        if (gameObject.tag == "Player")
            playerHP.value = health;
        
        if (lastHP == health)
        {
            if (animator != null)
                animator.SetBool("attacking", false);

            if (gameObject.tag != "Player")
                GetComponent<SpriteRenderer>().color = Color.white;
        }

        lastHP = health;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (gameObject.tag != "Player" && GetComponent<SpriteRenderer>().color != selectedColor)
            GetComponent<SpriteRenderer>().color = selectedColor;
    }

    public void AbsorbEnergy(int points)
    {
        if (animator != null && !animator.GetBool("attacking"))
            animator.SetBool("attacking", true);
        
        if (health < maxHealth)
            health += points;
    }

}
