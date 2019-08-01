using UnityEngine;

namespace AttackSlot.Slot
{

    public static class SlotService
    {

        public static int CalculateCountOfSlots(float largeRadius, float smallRadius)
        {
            var theta = Mathf.Asin(smallRadius / (largeRadius + smallRadius));
            var count = Mathf.FloorToInt(2f * Mathf.PI / (2f * theta));

            return count;
        }

    }

}
