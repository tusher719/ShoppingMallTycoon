using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterBase : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract void Initialize();

    public void MoveTo(Vector3 target)
    {
        if (agent != null && agent.isOnNavMesh)
            agent.SetDestination(target);
    }

    public void PlayAnim(string trigger)
    {
        if (animator != null)
            animator.SetTrigger(trigger);
    }

    public void StopMoving()
    {
        if (agent != null && agent.isOnNavMesh)
            agent.ResetPath();
    }

    public bool HasReachedDestination()
    {
        if (agent == null) return true;
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
}
