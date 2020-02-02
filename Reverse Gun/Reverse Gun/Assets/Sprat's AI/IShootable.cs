using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void TakeDamage(float amount);

    void HealDamage(float amount);
}
