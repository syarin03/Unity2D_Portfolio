using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressTheSpaceBar : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                SceneManager.LoadScene(2);
            else
            {
                Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}
