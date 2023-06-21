using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]

public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 20, walkLength = 20;
    public bool startRandomlyEachIteration = true;
}
