using System;
using AttackSlot.Data;
using AttackSlot.Immutable;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AttackSlot.Slot
{

    public class SlotAgent
        : MonoBehaviour
          , IInitializable
          , IDisposable
    {

        class ShortTermMemory
        {

            public float LastAttackedAt;

        }

        ShortTermMemory Memory { get; } = new ShortTermMemory();

        AgentData _agentData;

        NavMeshAgent _navMeshAgent;

        Slot _slot;

        SlotEnemy _slotEnemy;

        public CampType CampType => CampType.A;

        [Inject]
        void Constrct(AgentData agentData,
                      NavMeshAgent navMeshAgent,
                      SlotEnemy slotEnemy,
                      Slot slot)
        {
            _agentData = agentData;
            _navMeshAgent = navMeshAgent;
            _slotEnemy = slotEnemy;
            _slot = slot;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Attack()
        {
            Debug.Log($"### Attack");
            Memory.LastAttackedAt = Time.time;

            _navMeshAgent.isStopped = true;
        }

        public bool CanAttack()
        {
            var isInCoolTime = Time.time < Memory.LastAttackedAt + _agentData.AttackIntervalSeconds;

            if (isInCoolTime)
            {
                return false;
            }

            return IsCloseToTarget();
        }

        public bool IsCloseToTarget()
        {
            var isClose = ConditionService.IsCloseEnough(
                this,
                _slotEnemy,
                _agentData.AttackRange
            );

            return isClose;
        }

        public void Move()
        {
            _navMeshAgent.SetDestination(_slot.Center);
        }

    }

}
