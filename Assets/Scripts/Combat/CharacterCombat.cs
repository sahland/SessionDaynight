using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed;
    public float attackDelay;
    public event System.Action OnAttack;

    private CharacterStats _myStats;
    private float _attackCooldown;

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
    }

    public void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            _attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(_myStats.damage.GetValue());
    }
}
