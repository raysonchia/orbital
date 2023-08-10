using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderSwitcher : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClipEnter, audioClipExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            MusicManager.Instance.ChangeMusic(audioClipEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            //MusicManager.Instance.SetPreviousMusic();
            MusicManager.Instance.ChangeMusic(audioClipExit);
        }
    }
}