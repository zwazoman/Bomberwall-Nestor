using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public event Action<int> OnDamageAmount;
    public event Action OnDamage;

    public void ApplyDamage(int dmg)
    {
        OnDamage?.Invoke();
        OnDamageAmount?.Invoke(dmg);
    }
}
