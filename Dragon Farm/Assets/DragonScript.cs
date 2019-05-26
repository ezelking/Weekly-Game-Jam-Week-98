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

    // Start is called before the first frame update
    void Start()
    {
        rechargeTimer = 0;
        
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        rechargeTimer += Time.deltaTime;
        image.fillAmount = rechargeTimer / durability;

        if (rechargeTimer > durability)
        {
            animator.SetBool("BreatheFire", true);
            rechargeTimer = 0;
        }

        animator.SetFloat("Speed", speed);
        controller.Move(transform.forward *speed* Time.deltaTime);

    }
}
