using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFireShoot : TurretsDecorator
{
    public DoubleFireShoot(Turret turret) : base(turret)
    {
    }

    public override string GetDescription()
    {
        return _turret.GetDescription() + ", with Double Fire Shoot";
    }

    public override int AreaHit()
    {
        return _turret.AreaHit() + 2;
    }

    public override void ImpactEffect()
    {
        _turret.ImpactEffect();
        Debug.Log("Congela3");
    }
}