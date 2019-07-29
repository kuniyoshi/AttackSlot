using System;
using AttackSlot.Data;
using AttackSlot.Slot.Factory;
using UnityEngine.Assertions;
using Zenject;

namespace AttackSlot.Slot.Installer
{

    public class SceneInstaller : MonoInstaller
    {

        public GameData GameData;

        public SlotEnemy SlotEnemy;

        public AgentInstaller AgentInstaller;

        void Awake()
        {
            Assert.IsNotNull(GameData, "GameData != null");
            Assert.IsNotNull(SlotEnemy, "SlotEnemy != null");
            Assert.IsNotNull(AgentInstaller, "AgentInstaller != null");
        }

        public override void InstallBindings()
        {
            Container.Bind<GameData>()
                .FromInstance(GameData)
                .AsSingle();

            Container.Bind<SlotEnemy>()
                .FromInstance(SlotEnemy)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<SlotGame>()
                .FromNew()
                .AsSingle();

            Container.BindFactory<AgentData, SlotAgent, AgentFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<AgentInstaller>(AgentInstaller)
                .AsSingle();
        }

    }

}
