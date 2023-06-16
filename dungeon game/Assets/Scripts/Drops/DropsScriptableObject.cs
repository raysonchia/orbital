using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropsScriptableObject", menuName = "ScriptableObjects/Drops")]
public class DropsScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject dropPrefab;
    public GameObject DropPrefab { get => dropPrefab; private set => dropPrefab = value; }

    [SerializeField]
    int dropChance;
    public int DropChance { get => dropChance; private set => dropChance = value; }

    [SerializeField]
    int maxAmount;
    public int MaxAmount { get => maxAmount; private set => maxAmount = value; }
}