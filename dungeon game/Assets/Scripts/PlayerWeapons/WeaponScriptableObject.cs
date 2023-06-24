using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public int ID => GetInstanceID();

    public int MaxStackSize { get; private set; } = 1;
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string Description { get; private set;  }
    [field: SerializeField]
    public Sprite ItemImage { get; private set;  }

    [SerializeField]
    GameObject weaponPrefab;
    public GameObject WeaponPrefab { get => weaponPrefab; private set => weaponPrefab = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float fireRate;
    public float FireRate { get => fireRate; private set => fireRate = value; }

    [SerializeField]
    float knockback;
    public float Knockback { get => knockback; private set => knockback = value; }

    [SerializeField]
    Vector2 weaponPos;
    public Vector2 WeaponPos { get => weaponPos; private set => weaponPos = value; }
}