using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AttackSlot.Simple
{

    public class BehaviorAcceptor
        : MonoBehaviour
    {

        Agent _agent;

        NavMeshAgent _navMeshAgent;

        [Inject]
        void Construct(Agent agent,
                       NavMeshAgent navMeshAgent)
        {
            _agent = agent;
            _navMeshAgent = navMeshAgent;
        }

        public void Attack()
        {
            _agent.Attack();
        }

        public void Move()
        {
            _agent.Move();
        }

    }

}
