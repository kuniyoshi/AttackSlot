using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    [TaskCategory("AttackSlot/Agent")]
    public class Move : BaseAction
    {

        public override TaskStatus OnUpdate()
        {
            var acceptor = BehaviorAcceptor;
            acceptor.Move();

            return TaskStatus.Success;
        }

    }

}
