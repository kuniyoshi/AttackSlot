using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
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
