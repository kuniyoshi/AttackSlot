using System;
using AttackSlot.Constant;
using AttackSlot.Data;
using AttackSlot.Slot.Factory;
using UnityEngine;
using Zenject;

namespace AttackSlot.Slot
{

    public class SlotGame
        : ITickable
          , IInitializable
          , IDisposable
    {

        AgentFactory Factory { get; }

        GameData GameData { get; }

        int _spawnedCount;

        public SlotGame(GameData gameData,
                        AgentFactory factory)
        {
            Factory = factory;
            GameData = gameData;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            if (_spawnedCount < GameData.UserData.AgentCount)
            {
                var data = ScriptableObject.CreateInstance<AgentData>();
                data.InitialPoint = GameData.UserData.Point;
                data.AttackRange = AgentConstant.AttackRange;
                data.AttackIntervalSeconds = AgentConstant.AttackIntervalSeconds;

                Factory.Create(data);

                _spawnedCount++;
            }
        }

    }

}
