using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius;

    private Transform _target;
    private NavMeshAgent _agent;
    private CharacterCombat _combat;

    void Start()
    {
        lookRadius = 6f;

        _target = PlayerManager.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
        _combat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance <= lookRadius)
        {
            _agent.SetDestination(_target.position);

            if (distance <= _agent.stoppingDistance)
            {
                CharacterStats targetStats = _target.GetComponent<CharacterStats>();

                if (targetStats != null)
                {
                    _combat.Attack(targetStats);
                }
                FaceTarget();
            }
        }
    }

    private void FaceTarget ()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
