using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderSwitcher : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            MusicManager.Instance.ChangeMusic(audioClip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            MusicManager.Instance.SetPreviousMusic();
        }
    }
}