using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public GameObject Front;
    public GameObject Left;
    public GameObject Right;
    public GameObject Back;
    public GameObject BackRight;
    public GameObject BackLeft;
    public GameObject FrontRight;
    public GameObject FrontLeft;
    Animator am;
    /*
    private void Start()
    {
        am = GetComponentInParent<Animator>();
    }
    */

    private void Update()
    {
        //Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // Facing right
        
        if ((transform.eulerAngles.z >= 345 && transform.eulerAngles.z <= 360) || (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 30))
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(true);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
            //am.Play("RightIdle");
        }

        if (transform.eulerAngles.z >= 30 && transform.eulerAngles.z <= 60)
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(true);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
        }

        if (transform.eulerAngles.z >= 60 && transform.eulerAngles.z <= 120)
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(true);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
        }

        if (transform.eulerAngles.z >= 120 && transform.eulerAngles.z <= 150)
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(true);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
        }

        if (transform.eulerAngles.z >= 150 && transform.eulerAngles.z <= 195)
        {
            Front.SetActive(false);
            Left.SetActive(true);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
        }

        if (transform.eulerAngles.z >= 195 && transform.eulerAngles.z <= 240)
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(true);
        }

        if (transform.eulerAngles.z >= 240 && transform.eulerAngles.z <= 300)
        {
            Front.SetActive(true);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(false);
            FrontLeft.SetActive(false);
        }

        if (transform.eulerAngles.z >= 300 && transform.eulerAngles.z <= 345)
        {
            Front.SetActive(false);
            Left.SetActive(false);
            Right.SetActive(false);
            Back.SetActive(false);
            BackRight.SetActive(false);
            BackLeft.SetActive(false);
            FrontRight.SetActive(true);
            FrontLeft.SetActive(false);
        }
        
    }
}
