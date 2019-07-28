using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    [TaskCategory("AttackSlot/Agent")]
    public class CanAttack : BaseConditional
    {

        public override TaskStatus OnUpdate()
        {
            var info = BehaviorInformation;

            return info.CanAttack() ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

}
