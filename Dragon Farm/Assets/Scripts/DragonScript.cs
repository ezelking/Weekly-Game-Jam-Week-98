using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    float strength;
    float durability = 10;
    float rechargeTimer;

    CharacterController controller;
    Animator animator;
    Image image;

    public float speed;
    float rotationSpeed;
    float gravity;
    bool die = false;
    GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        rechargeTimer = 0;
        
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();

        fire = GetComponentInChildren<ParticleSystem>().gameObject;
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rechargeTimer += Time.deltaTime;
        image.fillAmount = rechargeTimer / durability;

        if (rechargeTimer > durability)
        {
            animator.SetBool("BreatheFire", true);
            fire.SetActive(true);
            rechargeTimer = 0;
        }

        animator.SetFloat("Speed", speed);
        controller.Move(transform.forward *speed* Time.deltaTime);
            animator.SetBool("Die", die);
            die = false;
    }

    public void Kill()
    {
        die = true;
        image.gameObject.SetActive(false);
    }
}
