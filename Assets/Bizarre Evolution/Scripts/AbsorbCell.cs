using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbCell : MonoBehaviour
{

    public Transform targetPos;
    public string tag;
    
    private AudioManager audioManager;
    private HealthController foe;
    private HealthController healthController;
    private bool absorbing = false;
    private int damage;

    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        
        healthController = GetComponent<HealthController>();
    }

    void Update()
    {
        if (absorbing) {
            healthController.TakeDamage(damage);
            foe.AbsorbEnergy(damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tag) {
            if (collider.transform.parent.GetComponent<Animator>() != null)
                collider.transform.parent.GetComponent<Animator>().SetBool("attacking", true);

            foe = collider.GetComponentInParent<HealthController>();
            damage = collider.GetComponent<TargetAim>().damage;
            absorbing = true;
            
            if (gameObject.tag != "Player")
                audioManager.PlayAbsorb();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == tag) {
            if (collider.transform.parent.GetComponent<Animator>() != null)
                collider.transform.parent.GetComponent<Animator>().SetBool("attacking", false);
            
            foe = null;
            damage = 0;
            absorbing = false;

            if (gameObject.tag != "Player")
                audioManager.StopAbsorb();
        }
    }
    
}
