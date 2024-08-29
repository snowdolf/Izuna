using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestPortal : MonoBehaviour
{
    // Start is called before the first frame update
    bool canPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPortal = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPortal = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPortal == true)
        {
            SceneManager.LoadScene(5);
        }
    }
}