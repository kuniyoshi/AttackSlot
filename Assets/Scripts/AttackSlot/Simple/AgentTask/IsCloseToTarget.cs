using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    [TaskCategory("AttackSlot/Agent")]
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
