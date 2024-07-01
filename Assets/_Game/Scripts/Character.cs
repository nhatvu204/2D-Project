using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;

    [SerializeField] protected CombatText combatTextPrefab;

    private float hp;
    private string currentAnimName;

    public bool IsDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.SetNewHp(hp);
            //Instantiate(combatTextPrefab, gameObject.transform.position, Quaternion.identity).OnInit(damage);
        }
    }

    public void OnHeal()
    {
        if (!IsDead)
        {
            hp += 30;

            if (hp > 100)
            {
                hp = 100;
            }

            healthBar.SetNewHp(hp);
        }
    }
}
