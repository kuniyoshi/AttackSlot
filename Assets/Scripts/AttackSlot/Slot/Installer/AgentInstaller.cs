using AttackSlot.Data;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Zenject;

namespace AttackSlot.Slot.Installer
{

    public class AgentInstaller : MonoInstaller
    {

        public SlotAgent AgentPrefab;

        [Inject]
        AgentData _agentData;

        void Awake()
        {
            Assert.IsNotNull(AgentPrefab, "AgentPrefab != null");
        }

        public override void InstallBindings()
        {
            Container.Bind<AgentData>()
                .FromInstance(_agentData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<SlotAgent>()
                .FromComponentInNewPrefab(AgentPrefab)
                .AsSingle();

            Container.Bind<NavMeshAgent>()
                .FromMethod(context =>
                {
                    var agent = context.Container.Resolve<SlotAgent>();

                    return agent.GetComponent<NavMeshAgent>();
                })
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BehaviorAcceptor>()
                .FromMethod(context =>
                {
                    var agent = context.Container.Resolve<SlotAgent>();

                    return agent.GetComponent<BehaviorAcceptor>();
                })
                .AsSingle();

            Container.BindInterfacesAndSelfTo<BehaviorInformation>()
                .FromMethod(context =>
                {
                    var agent = context.Container.Resolve<SlotAgent>();

                    return agent.GetComponent<BehaviorInformation>();
                })
                .AsSingle();
        }

    }

}
