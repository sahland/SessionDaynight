using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAnimation;

    const float locomationAnimationSmoothTime = 0.1f;

    private NavMeshAgent _agent;
    protected AnimationClip[] _currentAttackAnimSet;
    protected Animator _animator;
    protected CharacterCombat _combat;
    public AnimatorOverrideController overrideController;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _combat = GetComponent<CharacterCombat>();

        if (overrideController == null )
        {
            overrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        }
        _animator.runtimeAnimatorController = overrideController;

        _currentAttackAnimSet = defaultAnimation;
        _combat.OnAttack += OnAttack;
    }

    protected virtual void Update()
    {
        float speedPercent = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        _animator.SetBool("inCombat", _combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        _animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, _currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = _currentAttackAnimSet[attackIndex];
    }
}
