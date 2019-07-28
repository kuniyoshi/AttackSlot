using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    [TaskCategory("AttackSlot/Agent")]
    public class IsOnNavMesh : BaseConditional
    {

        public override TaskStatus OnUpdate()
        {
            var isOn = BehaviorInformation.IsOnNavMesh();

            return isOn ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

}
