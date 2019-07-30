using System;
using UnityEngine;
using Zenject;

namespace AttackSlot.Slot
{

    public class BehaviorAcceptor
        : MonoBehaviour,
          IInitializable,
          IDisposable
    {

        SlotAgent _slotAgent;

        [Inject]
        void Construct(SlotAgent slotAgent)
        {
            _slotAgent = slotAgent;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Attack()
        {
            _slotAgent.Attack();
        }

        public void Move()
        {
            _slotAgent.Move();
        }

    }

}
