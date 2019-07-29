using System;
using AttackSlot.Constant;
using AttackSlot.Simple.Data;
using AttackSlot.Simple.Factory;
using UnityEngine;
using Zenject;

namespace AttackSlot.Simple
{

    public class Game
        : IInitializable
          , ITickable
          , IDisposable
    {

        GameData GameData { get; }

        AgentFactory Factory { get; }

        int _spawnedCount;

        public Game(GameData gameData,
                    AgentFactory factory)
        {
            GameData = gameData;
            Factory = factory;
        }

        public void Initialize()
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

        public void Dispose()
        {
        }

    }

}
