using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    [TaskCategory("AttackSlot/Agent")]
    public class Attack : BaseAction
    {

        public override TaskStatus OnUpdate()
        {
            var acceptor = BehaviorAcceptor;
            acceptor.Attack();

            return TaskStatus.Success;
        }

    }

}
