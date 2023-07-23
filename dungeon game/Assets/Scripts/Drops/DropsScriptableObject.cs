using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropsScriptableObject", menuName = "ScriptableObjects/Drops")]
[System.Serializable]
public class DropsScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject dropPrefab;
    public GameObject DropPrefab { get => dropPrefab; private set => dropPrefab = value; }

    [SerializeField]
    int dropChance;
    public int DropChance { get => dropChance; set => dropChance = value; }

    [SerializeField]
    int minAmount;
    public int MinAmount { get => minAmount; private set => minAmount = value; }

    [SerializeField]
    int maxAmount;
    public int MaxAmount { get => maxAmount; private set => maxAmount = value; }
}