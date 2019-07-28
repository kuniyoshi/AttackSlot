using AttackSlot.Simple.Data;
using UnityEngine.AI;
using Zenject;

namespace AttackSlot.Simple.Installer
{

    public class AgentInstaller : MonoInstaller
    {

        public Agent AgentPrefab;

        [Inject]
        AgentData _agentData;

        public override void InstallBindings()
        {
            Container.Bind<AgentData>()
                .FromInstance(_agentData)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Agent>()
                .FromComponentInNewPrefab(AgentPrefab)
                .AsSingle();

            Container.Bind<NavMeshAgent>()
                .FromMethod(context =>
                {
                    var agent = context.Container.Resolve<Agent>();

                    return agent.GetComponent<NavMeshAgent>();
                })
                .AsSingle();

            Container.Bind<BehaviorAcceptor>()
                .FromMethod(context =>
                {
                    var agent = context.Container.Resolve<Agent>();

                    return agent.GetComponent<BehaviorAcceptor>();
                })
                .AsSingle();
        }

    }

}
