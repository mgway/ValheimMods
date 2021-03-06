using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;

using HarmonyLib;
using UnityEngine;

namespace ValheimHostnameConnect
{
    [HarmonyPatch]
    class PlayerPatch
    {
        [HarmonyPatch(typeof(Player), "SetControls")]
        public static class Player_SetControls_Patch
        {
            static MethodInfo setCrouchMethod = AccessTools.Method(typeof(Player), "SetCrouch", new System.Type[] { typeof(bool) });

            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> list = instructions.ToList<CodeInstruction>();
                

                for (int index = 0; index < list.Count; ++index)
                {
                    if (list[index].opcode == OpCodes.Callvirt && list[index].operand.ToString() == setCrouchMethod.ToString())
                    {
                        list[index - 2].opcode = OpCodes.Nop;
                        list[index - 1].opcode = OpCodes.Nop;
                        list[index].opcode = OpCodes.Nop;
                        break;
                    }
                }

                return list.AsEnumerable<CodeInstruction>();
            }
        }
    }
}
