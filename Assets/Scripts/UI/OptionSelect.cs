using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelect : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons;
    private RectTransform select;
    private int currentPosition;

    private void Awake()
    {
        select = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        currentPosition = 0;
        ChangePosition(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int change)
    {
        currentPosition += change;

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;

        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition()
    {
        select.position = new Vector3(select.position.x, buttons[currentPosition].position.y);
    }

    private void Interact()
    {
        buttons[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
