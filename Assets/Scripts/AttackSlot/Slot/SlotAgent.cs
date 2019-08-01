using System;
using AttackSlot.Data;
using AttackSlot.Immutable;
using AttackSlot.Slot.MessageData;
using AttackSlot.Slot.Slot;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
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

        Sequence _attackSequence;

        Transform _body;

        NavMeshAgent _navMeshAgent;

        BaseSlot _slot;

        SlotEnemy _slotEnemy;

        public CampType CampType => CampType.A;

        [Inject]
        void Constrct(AgentData agentData,
                      NavMeshAgent navMeshAgent,
                      SlotEnemy slotEnemy,
                      BaseSlot slot)
        {
            _agentData = agentData;
            _navMeshAgent = navMeshAgent;
            _slotEnemy = slotEnemy;
            _slot = slot;

            _body = transform.GetChild(0);
            Assert.IsNotNull(_body, "_body != null");
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Attack()
        {
            Memory.LastAttackedAt = Time.time;
            _navMeshAgent.isStopped = true;

            _attackSequence?.Kill();
            _attackSequence = _body.DOJump(transform.position, 3f, 1, 0.5f);
            _attackSequence.onComplete += () => { _body.localPosition = new Vector3(0f, 1f, 0f); };
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
            Memory.SlotData.UpdatePosition(transform.position);
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
