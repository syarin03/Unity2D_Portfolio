using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Hp : MonoBehaviour
{
    [Header ("Hp")]
    [SerializeField] private float startingHp;
    public float currentHp { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHp = startingHp;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable) return;

        currentHp = Mathf.Clamp(currentHp - damage, 0, startingHp);

        if (currentHp > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("isGrounded", true);
                anim.SetTrigger("Die");

                dead = true;
            }
        }
    }

    public void HealHp(float value)
    {
        currentHp = Mathf.Clamp(currentHp + value, 0, startingHp);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 9, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        HealHp(startingHp);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components)
            component.enabled = true;

        dead = false;
    }
}
