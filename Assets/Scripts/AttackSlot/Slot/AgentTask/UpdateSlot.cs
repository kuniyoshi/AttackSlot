using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
    public class UpdateSlot : BaseAction
    {

        public override TaskStatus OnUpdate()
        {
            var acceptor = BehaviorAcceptor;
            acceptor.UpdateSlot();

            return TaskStatus.Success;
        }

    }

}
