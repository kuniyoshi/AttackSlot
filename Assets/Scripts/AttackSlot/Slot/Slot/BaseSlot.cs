using System;
using AttackSlot.Slot.MessageData;
using UnityEngine;
using Zenject;

namespace AttackSlot.Slot.Slot
{

    public abstract class BaseSlot
        : MonoBehaviour
          , IInitializable
          , IDisposable
    {

        public abstract void Initialize();

        public abstract void Dispose();

        public abstract Vector3 GetPosition(SlotData slotData);

        public abstract SlotData GetSlot(Vector3 fromPosition);

    }

}
