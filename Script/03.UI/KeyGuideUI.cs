using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuideUI : MonoBehaviour
{
    public GameObject portalUIImage;

    private void Start()
    {
        portalUIImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            portalUIImage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            portalUIImage.SetActive(false);
        }
    }

}
