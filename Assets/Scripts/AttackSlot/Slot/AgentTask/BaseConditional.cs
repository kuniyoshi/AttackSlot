using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Slot.AgentTask
{

    public class BaseConditional : Conditional
    {

        BehaviorAcceptor _behaviorAcceptor;

        BehaviorInformation _behaviorInformation;

        protected BehaviorAcceptor BehaviorAcceptor =>
            _behaviorAcceptor != null
                ? _behaviorAcceptor
                : (_behaviorAcceptor = GetComponent<BehaviorAcceptor>());

        protected BehaviorInformation BehaviorInformation =>
            _behaviorInformation != null
                ? _behaviorInformation
                : (_behaviorInformation = GetComponent<BehaviorInformation>());

    }

}
