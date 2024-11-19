using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnHealthLost;

    [SerializeField] public int MaxHealth ;

    [HideInInspector] public int CurrentHealth;
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
        CurrentHealth -= dmg;
        OnHealthLost?.Invoke(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            GameManager.Instance.StopGame(gameObject.name);
        }
    }

    public void Heal(int heal)
    {
        if (CurrentHealth + heal > MaxHealth) CurrentHealth = MaxHealth; else CurrentHealth += heal;
    }
}
