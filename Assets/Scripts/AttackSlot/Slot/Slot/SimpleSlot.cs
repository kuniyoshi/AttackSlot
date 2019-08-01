using System;
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

    public class SimpleSlot : BaseSlot
    {

        static int CalculateCountOfSlots(float largeRadius, float smallRadius)
        {
            var theta = Mathf.Asin(smallRadius / (largeRadius + smallRadius));
            var count = Mathf.CeilToInt(2f * Mathf.PI / (2f * theta));

            return count;
        }

        NavMeshAgent _navMeshAgent;

        List<Vector3> _slots;

        Dictionary<int, int> _countOf;

        public Vector3 Center => transform.position;

        [Inject]
        public void Construct(SlotEnemy slotEnemy)
        {
            _navMeshAgent = slotEnemy.GetComponent<NavMeshAgent>(); // :( require to use Zenject
            Assert.IsNotNull(_navMeshAgent, "_navMeshAgent != null");
        }

        public override void Initialize()
        {
            var countOfSlots = CalculateCountOfSlots(_navMeshAgent.radius, AgentConstant.Radius);
            Debug.Log($"### count of slots: {countOfSlots}");

            _slots = new List<Vector3>();
            _countOf = new Dictionary<int, int>();
            var rotation = Quaternion.Euler(new Vector3(0f, 360f / countOfSlots, 0f));
            var nextPosition = 2f * AgentConstant.Radius * Vector3.forward;

            for (var i = 0; i < countOfSlots; ++i)
            {
                _slots.Add(nextPosition);
                _countOf[i] = 0;

                nextPosition = rotation * nextPosition;
            }
        }

        public override void Dispose()
        {
        }

        public override Vector3 GetPosition(SlotData slotData)
        {
            Assert.IsTrue(slotData.SlotIndex >= 0, "slotData.SlotIndex >= 0");
            Assert.IsTrue(slotData.SlotIndex < _slots.Count, "slotData.SlotIndex < _slots.Count");

            return Center + _slots[slotData.SlotIndex];
        }

        public override SlotData GetSlot(Vector3 fromPosition)
        {
            var positions = _slots.Select(slot => Center + slot)
                .ToArray();

            var minCount = _countOf.Values.Min();
            var candidates = Enumerable.Range(0, _slots.Count)
                .Where(index => _countOf[index] == minCount);

            var selection = candidates
                .OrderBy(index =>
                {
                    var distanceA = (positions[index] - fromPosition).sqrMagnitude;

                    return distanceA;
                })
                .First();

            _countOf[selection]++;

            var data = new SlotData(selection);

            return data;
        }

    }

}
