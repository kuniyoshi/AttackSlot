using UnityEngine;

namespace AttackSlot.Slot.MessageData
{

    public class SlotData
    {

        public int SlotIndex { get; }

        public Vector3 ClientPosition { get; private set; }

        public SlotData(int slotIndex)
        {
            SlotIndex = slotIndex;
        }

        public void UpdatePosition(Vector3 newValue)
        {
            ClientPosition = newValue;
        }

    }

}
