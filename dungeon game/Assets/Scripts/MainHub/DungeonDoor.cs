using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField]
    private Sprite doorClosed, doorOpen;
    private SpriteRenderer door;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door.sprite = doorOpen;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door.sprite = doorClosed;
        }
    }
}
