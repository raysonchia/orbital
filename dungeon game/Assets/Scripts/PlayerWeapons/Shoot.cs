using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Shoot : PlayerAttacks, IWeapon
{
    public void ShootAction()
    {
        base.BasicShooting();
    }
}
