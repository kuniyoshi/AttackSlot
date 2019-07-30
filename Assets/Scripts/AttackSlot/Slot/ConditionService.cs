using UnityEngine;
using UnityEngine.Assertions;

namespace AttackSlot.Slot
{

    public static class ConditionService
    {

        public static bool IsCloseEnough(SlotAgent agent,
                                         SlotEnemy enemy,
                                         float maxDistanceToClose)
        {
            var agentCollider = agent.GetComponentInChildren<Collider>();
            Assert.IsNotNull(agentCollider, "agentCollider != null");
            var enemyCollider = enemy.GetComponentInChildren<Collider>();
            Assert.IsNotNull(enemyCollider, "enemyCollider != null");

            return IsCloseEnough(
                agentCollider,
                enemyCollider,
                maxDistanceToClose
            );
        }

        public static bool IsCloseEnough(Collider agentCollider,
                                         Collider enemyCollider,
                                         float maxDistanceToClose)
        {
            // NOTE: both collider contains ground, and same width while bottom to top
            var enemyGround = enemyCollider.transform.position;
            enemyGround.y = 0f;
            var fromPoint = agentCollider.ClosestPointOnBounds(enemyGround);

            var agentGround = agentCollider.transform.position;
            agentGround.y = 0f;
            var toPoint = enemyCollider.ClosestPointOnBounds(agentGround);

            var squaredDistance = (toPoint - fromPoint).sqrMagnitude;

            return squaredDistance < maxDistanceToClose * maxDistanceToClose;
        }


    }

}
