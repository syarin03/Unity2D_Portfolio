using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Hp playerHp;
    [SerializeField] private Image totalHpBar;
    [SerializeField] private Image currentHpBar;

    private void Start()
    {
        totalHpBar.fillAmount = playerHp.currentHp / 10;
    }

    private void Update()
    {
        currentHpBar.fillAmount = playerHp.currentHp / 10;
    }
}
