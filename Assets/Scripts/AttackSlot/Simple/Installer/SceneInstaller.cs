using AttackSlot.Simple.Data;
using AttackSlot.Simple.Factory;
using UnityEngine.Assertions;
using Zenject;

namespace AttackSlot.Simple.Installer
{

    public class SceneInstaller : MonoInstaller
    {

        public AgentInstaller AgentInstaller;

        public Enemy Enemy;

        public GameData GameData;

        void Awake()
        {
            Assert.IsNotNull(GameData, "GameData != null");
            Assert.IsNotNull(AgentInstaller, "AgentInstaller != null");
            Assert.IsNotNull(Enemy, "Enemy != null");
        }

        public override void InstallBindings()
        {
            Container.Bind<GameData>()
                .FromInstance(GameData)
                .AsSingle();

//            Container.Bind<UserData>()
//                .FromResolve()

            Container.Bind<Enemy>()
                .FromInstance(Enemy)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Game>()
                .FromNew()
                .AsSingle();

            Container.BindFactory<AgentData, Agent, AgentFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<AgentInstaller>(AgentInstaller)
                .AsSingle();
        }

    }

}
