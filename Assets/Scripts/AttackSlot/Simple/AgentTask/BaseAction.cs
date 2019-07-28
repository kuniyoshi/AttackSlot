using BehaviorDesigner.Runtime.Tasks;

namespace AttackSlot.Simple.AgentTask
{

    public class BaseAction : Action
    {

        BehaviorAcceptor _behaviorAcceptor;

        protected BehaviorAcceptor BehaviorAcceptor =>
            _behaviorAcceptor != null
                ? _behaviorAcceptor
                : (_behaviorAcceptor = GetComponent<BehaviorAcceptor>());

    }

}
