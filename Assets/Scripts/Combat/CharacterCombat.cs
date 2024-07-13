using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed;
    public float attackDelay;
    public event System.Action OnAttack;
    public bool InCombat { get; private set; }

    private CharacterStats _myStats;
    private CharacterStats _opponentStats;
    private float _attackCooldown;
    private float lastAttackTime;

    const float combatCooldown = 5f;

    private void Start()
    {
        attackSpeed = 1f;
        attackDelay = 0.6f;

        _attackCooldown = 0f;
        _myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            _opponentStats = targetStats;
            if (OnAttack != null)
            {
                OnAttack();
            }

            _attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        _opponentStats.TakeDamage(_myStats.damage.GetValue());
        if (_opponentStats.currentHealth <= 0f)
        {
            InCombat = false;
        }
    }
}
