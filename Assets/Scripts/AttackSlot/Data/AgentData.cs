using UnityEngine;

namespace AttackSlot.Data
{

    [CreateAssetMenu(menuName = "AttackSlot/Agent")]
    public class AgentData : ScriptableObject
    {

        public Vector3 InitialPoint;

        public float AttackRange;

        public float AttackIntervalSeconds;

    }

}
