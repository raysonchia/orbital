using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class WeaponPickUp : PickUpInteraction
{
    [field: SerializeField]
    public WeaponScriptableObject weaponSO { get; private set; }
    [SerializeField]
    private GameObject weaponParent;
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private GameObject pickUpAnimation;
    private List<GameObject> currentWeapons;
    private Image activeWeapon;
    private SpriteRenderer sr;
    private DropAnimation dropAnimator;
    private bool interactable = false;


    private void Start()
    {
        weaponParent = GameObject.FindWithTag("Player")
            .transform
            .GetChild(0)
            .GetChild(0)
            .gameObject;

        activeWeapon = GameObject.Find("WeaponImage").GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        dropAnimator = GetComponent<DropAnimation>();
        StartCoroutine(
            dropAnimator.AnimCurveSpawnRoutine(
                animCurve,
                gameObject.transform));

        // wait for animation to finish before interaction
        StartCoroutine(Wait());
    }

    private void Update()
    {
        {
            if (InRange() && interactable)
            {
                PickUpWeapon();
            }
        }
    }

    private void PickUpWeapon()
    {
        DisableCurrentWeapon();
        ObjectPool.SpawnObject(pickUpAnimation, transform.position, Quaternion.identity);

        // swap attack to picked up weapon
        player.gameObject.GetComponent<PlayerMovement>().shoot =
            GetComponentInChildren<IWeapon>();

        Debug.Log(weaponParent.GetComponent<WeaponParent>());
        weaponParent.GetComponent<WeaponParent>().weaponRenderer = sr;
        activeWeapon.sprite = sr.sprite;
        activeWeapon.SetNativeSize();
        InventorySystem.Instance.PickUpWeapon(gameObject);
        MoveToPlayer();
        this.enabled = false;
    }

    private void MoveToPlayer()
    {
        gameObject.transform.SetParent(weaponParent.transform);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localPosition =
            new Vector3(weaponSO.WeaponPos.x, weaponSO.WeaponPos.y);
    }

    private void DisableCurrentWeapon()
    {
        currentWeapons = GetAllChildren(weaponParent);

        foreach (GameObject curr in currentWeapons)
        {
            if (curr.activeInHierarchy)
            {
                curr.SetActive(false);
                break;
            }
        }
    }

    private List<GameObject> GetAllChildren(GameObject parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            children.Add(child.gameObject);
            children.AddRange(GetAllChildren(child.gameObject));
        }

        return children;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        interactable = true;
    }
}
