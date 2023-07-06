using UnityEngine;
using System.Collections;

public class NewGunPattern : PlayerAttacks, IWeapon
{
    // Use this for initialization
    public void ShootAction()
    {
        base.Shotty(3);
    }
}