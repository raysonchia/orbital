using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopScriptableObject", menuName = "ScriptableObjects/Shop")]
public class ShopScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject itemPrefab;
    public GameObject ItemPrefab { get => itemPrefab; private set => itemPrefab = value; }

    [SerializeField]
    int minCost;
    public int MinCost { get => minCost; private set => minCost = value; }

    [SerializeField]
    int maxCost;
    public int MaxCost { get => maxCost; private set => maxCost = value; }

    [SerializeField]
    bool isWeapon;
    public bool IsWeapon { get => isWeapon; private set => isWeapon = value; }
}