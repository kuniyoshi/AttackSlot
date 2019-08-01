using System;
using AttackSlot.Data;
using AttackSlot.Immutable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Zenject;

namespace AttackSlot.Simple
{

    public class Agent
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

        Sequence _attackSequence;

        Transform _body;

        Enemy _enemy;

        NavMeshAgent _navMeshAgent;

        Slot _slot;

        public CampType CampType => CampType.A;

        [Inject]
        void Construct(AgentData agentData,
                       Enemy enemy,
                       NavMeshAgent navMeshAgent)
        {
            _agentData = agentData;
            _enemy = enemy;
            _navMeshAgent = navMeshAgent;

            _slot = _enemy.GetComponent<Slot>();
            Assert.IsNotNull(_slot, "_slot != null");

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
            Debug.Log($"### Attack");
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
            var isClose = ConditionService.IsCloseEnough(
                this,
                _enemy,
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
