using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PaddlePower
{
    [HarmonyPatch]
    class ShipPatch
    {
        static FieldInfo AttachAnimField = AccessTools.Field(typeof(Player), "m_attachAnimation");

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ship), "OnTriggerEnter")]
        [HarmonyPatch(typeof(Ship), "OnTriggerExit")]
        public static void Ship_EnterExit(Collider collider, Ship __instance, List<Player> ___m_players)
        {
            Player component = ((Component)collider).GetComponent<Player>();
            // Don't bother updating for non-player entities
            if ((bool)(UnityEngine.Object)component && PaddlePower.CountingMethod.Value.Equals("ALL", System.StringComparison.CurrentCultureIgnoreCase))
            {
                __instance.m_backwardForce = ShipPatch.CalculateForce(___m_players.Count);

                if (PaddlePower.DebugLoggingEnabled.Value)
                    Debug.Log("Ship " + __instance.GetInstanceID() + " onboard player count:" + ___m_players.Count + " paddle force:" + __instance.m_backwardForce);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ship), "UpdateOwner")]
        public static void Ship_UpdateOwner(Ship __instance, List<Player> ___m_players)
        {
            // UpdateOwner is called every 2s so we'll piggyback on that to avoid enumerating all players on board every physics frame

            if (PaddlePower.CountingMethod.Value.Equals("ATTACHED", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var attachedPlayerCount = ___m_players.FindAll(p => p.IsAttached()).Count;
                __instance.m_backwardForce = ShipPatch.CalculateForce(attachedPlayerCount);

                if (PaddlePower.DebugLoggingEnabled.Value)
                    Debug.Log("Ship " + __instance.GetInstanceID() + " attached player count:" + attachedPlayerCount + " paddle force:" + __instance.m_backwardForce);
            }
            else if (PaddlePower.CountingMethod.Value.Equals("SEATED", System.StringComparison.CurrentCultureIgnoreCase))
            {
                var sittingPlayerCount = ___m_players.FindAll(p => p.IsAttached() && (string)AttachAnimField.GetValue(p) == "attach_chair").Count;
                __instance.m_backwardForce = ShipPatch.CalculateForce(sittingPlayerCount);

                if (PaddlePower.DebugLoggingEnabled.Value)
                    Debug.Log("Ship ID:" + __instance.GetInstanceID() + " sitting player count:" + sittingPlayerCount + " paddle force:" + __instance.m_backwardForce);
            }
        }

        public static float CalculateForce(int playerCount)
        {
            if (playerCount > 0 && PaddlePower.PluginEnabled.Value)
            {
                if (PaddlePower.ScalingMethod.Value.Equals("LINEAR", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    var calculatedForce = PaddlePower.LinearBaseAmount.Value + (PaddlePower.LinearAdditionalAmount.Value * (playerCount - 1));
                    return Mathf.Min(calculatedForce, PaddlePower.LinearMaximum.Value);
                }
                else
                {
                    var coefficient = PaddlePower.GeometricCoefficient.Value;
                    var ratio = Mathf.Clamp(PaddlePower.GeometricRatio.Value, 0.00001f, 0.99999f);
                    return coefficient * ((1f - Mathf.Pow(ratio, playerCount)) / (1 - ratio));
                }
            }

            return 0.5f;
        }

    }
}
