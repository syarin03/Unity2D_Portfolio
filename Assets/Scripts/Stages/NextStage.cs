using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField] private Transform previousStage;
    [SerializeField] private Transform nextStage;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewStage(nextStage);
                nextStage.GetComponent<Stage>().ActivateRoom(true);
                previousStage.GetComponent<Stage>().ActivateRoom(false);
            }
            else
            {
                cam.MoveToNewStage(previousStage);
                previousStage.GetComponent<Stage>().ActivateRoom(true);
                nextStage.GetComponent<Stage>().ActivateRoom(false);
            }
        }
    }
}
