using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
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
