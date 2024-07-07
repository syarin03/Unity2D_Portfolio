using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Move Parameters")]
    [SerializeField] private float speed;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private Vector3 initScale;
    private bool moveLeft;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("isMoving", false);
    }

    private void Update()
    {
        if (moveLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("isMoving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            moveLeft = !moveLeft;
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("isMoving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
