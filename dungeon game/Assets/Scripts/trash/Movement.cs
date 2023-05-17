using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Vert;
    public float Hori;
    public float MovementSpeed;
    public Rigidbody2D Rb;
    public GameObject[] sides;

    // Start is called before the first frame update
    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Hori = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        Vert = Input.GetAxisRaw("Vertical") * MovementSpeed;
    }

    private void FixedUpdate()
    {
        
        Rb.velocity = new Vector2(Hori, Vert);
    }
}
