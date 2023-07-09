using UnityEngine;
using System.Collections;

public class ShotgunPattern : PlayerAttacks, IWeapon
{
    [SerializeField]
    private int numOfProjectiles;

    // Use this for initialization
    public void ShootAction()
    {
        base.Shotty(numOfProjectiles);
    }
}