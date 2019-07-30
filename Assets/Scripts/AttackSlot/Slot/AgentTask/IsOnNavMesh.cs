using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    [TaskCategory("AttackSlot/SlotAgent")]
    public class IsOnNavMesh : BaseConditional
    {

        public override TaskStatus OnUpdate()
        {
            var isOn = BehaviorInformation.IsOnNavMesh();

            return isOn ? TaskStatus.Success : TaskStatus.Failure;
        }

    }

}
