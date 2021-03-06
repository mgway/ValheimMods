using System.Collections.Generic;

using HarmonyLib;
using UnityEngine;

namespace PaddlePower
{
    [HarmonyPatch]
    class ShipPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ship), "OnTriggerEnter")]
        [HarmonyPatch(typeof(Ship), "OnTriggerExit")]
        public static void Ship_EnterExit(Collider collider, Ship __instance, List<Player> ___m_players)
        {
            Player component = ((Component)collider).GetComponent<Player>();
            // Don't bother updating for non-player entities
            if ((bool)(UnityEngine.Object)component)
            {
                __instance.m_backwardForce = ShipPatch.CalculateForce(___m_players.Count);
                Debug.Log("Setting paddle force to " + __instance.m_backwardForce);
            }
        }

        public static float CalculateForce(int playerCount)
        {
            var coefficient = PaddlePower.Coefficient.Value;
            var ratio = Mathf.Clamp(PaddlePower.Ratio.Value, 0.00001f, 0.99999f);

            if (playerCount > 0 && PaddlePower.PluginEnabled.Value)
            {
                return coefficient * ((1f - Mathf.Pow(ratio, playerCount)) / (1 - ratio));
            }

            return 0.5f;
        }

    }
}
