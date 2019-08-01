using System.Collections.Generic;
using System.Linq;
using AttackSlot.Constant;
using AttackSlot.Slot.MessageData;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Zenject;

namespace AttackSlot.Slot.Slot
{

    public class PhasedSlot : BaseSlot
    {

        class OuterSlot
        {

            public List<Vector3> Slots { get; }

            public Dictionary<int, int> CountOf { get; }

            public OuterSlot()
            {
                Slots = new List<Vector3>();
                CountOf = new Dictionary<int, int>();
            }

        }

        class InnerSlot
        {

            public List<Vector3> Slots { get; }

            public Dictionary<int, int> CountOf { get; }

            public InnerSlot()
            {
                Slots = new List<Vector3>();
                CountOf = new Dictionary<int, int>();
            }

        }

        static bool IsLastPhase(Vector3 clientPosition, Vector3 targetPosition)
        {
            const float maxDistanceToSwitchPhase = 4f;

            var diff = targetPosition - clientPosition;
            var squaredDistance = diff.sqrMagnitude;

            return squaredDistance < maxDistanceToSwitchPhase * maxDistanceToSwitchPhase;
        }

        readonly InnerSlot _innerSlot = new InnerSlot();

        readonly OuterSlot _outerSlot = new OuterSlot();

        NavMeshAgent _navMeshAgent;

        Vector3 Center => transform.position;

        [Inject]
        void Costruct(SlotEnemy slotEnemy)
        {
            _navMeshAgent = slotEnemy.GetComponent<NavMeshAgent>();
        }

        public override void Initialize()
        {
            var countOfSlots = SlotService.CalculateCountOfSlots(_navMeshAgent.radius, AgentConstant.Radius);

            var rotation = Quaternion.Euler(new Vector3(0f, 360f / countOfSlots, 0f));
            var innerNextPosition = 2f * AgentConstant.Radius * Vector3.forward;
            var outerNextPosition = 6f * AgentConstant.Radius * Vector3.forward;

            for (var i = 0; i < countOfSlots; ++i)
            {
                _innerSlot.Slots.Add(innerNextPosition);
                _innerSlot.CountOf[i] = 0;

                innerNextPosition = rotation * innerNextPosition;

                _outerSlot.Slots.Add(outerNextPosition);

                outerNextPosition = rotation * outerNextPosition;
            }
        }

        public override void Dispose()
        {
        }

        public override Vector3 GetPosition(SlotData slotData)
        {
            Assert.IsTrue(slotData.SlotIndex >= 0, "slotData.SlotIndex >= 0");
            Assert.IsTrue(slotData.SlotIndex < _innerSlot.Slots.Count, "slotData.SlotIndex < _innerSlot.Slots.Count");
            Assert.IsTrue(slotData.SlotIndex < _outerSlot.Slots.Count, "slotData.SlotIndex < _outerSlot.Slots.Count");

            var isLastPhase = IsLastPhase(slotData.ClientPosition, transform.position);

            return isLastPhase
                ? Center + _innerSlot.Slots[slotData.SlotIndex]
                : Center + _outerSlot.Slots[slotData.SlotIndex];
        }

        public override SlotData GetSlot(Vector3 fromPosition)
        {
            var positions = _innerSlot.Slots.Select(slot => Center + slot)
                .ToArray();

            var minCount = _innerSlot.CountOf.Values.Min();
            var candidates = Enumerable.Range(0, _innerSlot.Slots.Count)
                .Where(index => _innerSlot.CountOf[index] == minCount);

            var selection = candidates
                .OrderBy(index =>
                {
                    var distanceA = (positions[index] - fromPosition).sqrMagnitude;

                    return distanceA;
                })
                .First();

            _innerSlot.CountOf[selection]++;

            var data = new SlotData(selection);

            return data;
        }

    }

}
