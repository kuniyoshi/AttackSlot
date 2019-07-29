using AttackSlot.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AttackSlot.Simple
{

    public class BehaviorInformation
        : MonoBehaviour
    {

        Agent _agent;

        AgentData _agentData;

        NavMeshAgent _navMeshAgent;

        [Inject]
        void Construct(AgentData agentData,
                       NavMeshAgent navMeshAgent,
                       Agent agent)
        {
            _agentData = agentData;
            _navMeshAgent = navMeshAgent;
            _agent = agent;
        }

        public bool CanAttack()
        {
            return _agent.CanAttack();
        }

        public bool IsCloseToTarget()
        {
            return _agent.IsCloseToTarget();
        }

        public bool IsOnNavMesh()
        {
            return _navMeshAgent.isOnNavMesh;
        }

    }

}
