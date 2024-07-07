using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float camSpeed;

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private float lookAhead;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currentPosX, transform.position.y, transform.position.z),
            ref velocity, speed);

        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * camSpeed);
    }

    public void MoveToNewStage(Transform newStage)
    {
        currentPosX = newStage.position.x;
    }
}
