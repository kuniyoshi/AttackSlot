using System;
using AttackSlot.Data;
using AttackSlot.Immutable;
using AttackSlot.Slot.MessageData;
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

            public Vector3 Destination { get; set; }

            public float LastAttackedAt;

            public SlotData SlotData;

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
            var diff = Memory.Destination - transform.position;

            return diff.sqrMagnitude < _agentData.AttackRange * _agentData.AttackRange;
        }

        public void Move()
        {
            Memory.SlotData = _slot.GetSlot(transform.position);
            Memory.Destination = _slot.GetPosition(Memory.SlotData);
            _navMeshAgent.SetDestination(Memory.Destination);
        }

        public void UpdateSlot()
        {
            Memory.Destination = _slot.GetPosition(Memory.SlotData);
            _navMeshAgent.SetDestination(Memory.Destination);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(Memory.Destination, 0.5f);
        }

    }

}
