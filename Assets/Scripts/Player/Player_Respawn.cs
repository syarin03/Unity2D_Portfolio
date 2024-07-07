using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Hp playerHp;
    private UIManager uiManager;

    private void Awake()
    {
        playerHp = GetComponent<Hp>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }

        playerHp.Respawn();
        transform.position = currentCheckpoint.position;

        Camera.main.GetComponent<CameraController>().MoveToNewStage(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
