using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
    public class CanAttack : BaseConditional
    {

        public override TaskStatus OnUpdate()
        {
            var info = BehaviorInformation;

            return info.CanAttack() ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

}
