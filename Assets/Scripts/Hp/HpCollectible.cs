using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCollectible : MonoBehaviour
{
    [SerializeField] private float hpValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Hp>().HealHp(hpValue);
            gameObject.SetActive(false);
        }
    }
}
