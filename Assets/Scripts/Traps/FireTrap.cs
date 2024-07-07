using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private Hp playerHp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHp != null && active)
            playerHp.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHp = collision.GetComponent<Hp>();

            if (!triggered)
                StartCoroutine(ActivateFireTrap());

            if (active)
                collision.GetComponent<Hp>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerHp = null;
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("isActivated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("isActivated", false);
    }
}
