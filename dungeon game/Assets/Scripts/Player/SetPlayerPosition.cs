using UnityEngine;
using System.Collections;

public class SetPlayerPosition : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        PlayerMovement.Instance.transform.position = transform.position;
    }
}