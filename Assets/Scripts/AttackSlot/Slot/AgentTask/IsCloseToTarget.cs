using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
    public class IsCloseToTarget : BaseConditional
    {

        public override TaskStatus OnUpdate()
        {
            var info = BehaviorInformation;
            var isClose = info.IsCloseToTarget();

            return isClose ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

}
