using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AttackSlot.Slot
{

    public class BehaviorInformation
        : MonoBehaviour
    , IInitializable
    , IDisposable
    {

        NavMeshAgent _navMeshAgent;

        SlotAgent _slotAgent;

        [Inject]
        void Costruct(SlotAgent slotAgent,
                      NavMeshAgent navMeshAgent)
        {
            _slotAgent = slotAgent;
            _navMeshAgent = navMeshAgent;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public bool CanAttack()
        {
            return _slotAgent.CanAttack();
        }

        public bool IsCloseToTarget()
        {
            return _slotAgent.IsCloseToTarget();
        }

        public bool IsOnNavMesh()
        {
            return _navMeshAgent.isOnNavMesh;
        }

    }

}
