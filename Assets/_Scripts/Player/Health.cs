using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnDamageTaken;

    [SerializeField] int _maxHealth = 3;

    int _health;
    Damage _dmg;

    private void Awake()
    {
        TryGetComponent<Damage>(out _dmg);
    }

    private void Start()
    {
        _dmg.OnDamageAmount += Damage;
    }

    void Damage(int dmg)
    {
        print("Damage");
        _health -= dmg;
        OnDamageTaken?.Invoke(_health);
        if (_health <= 0)
        {
            GameManager.Instance.StopGame(gameObject.name);
        }
    }

    void Heal(int heal)
    {

    }
}
