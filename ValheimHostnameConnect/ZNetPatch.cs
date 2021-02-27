using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;

using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace ValheimHostnameConnect
{
    [HarmonyPatch]
    class ZNetPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), "OnPasswordEnter")]
        public static void ZNet_OnPasswordEnter(string pwd)
        {
            if (HostnameConnect.savePassword.Value)
            {
                HostnameConnect.serverPassword.Value = pwd;
            }
        }

        [HarmonyPatch(typeof(ZNet), "RPC_ClientHandshake")]
        public static class ZNet_RPC_ClientHandshake
        {
            private static string PasswordProvider()
            {
                return HostnameConnect.serverPassword.Value;
            }

            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> list = instructions.ToList<CodeInstruction>();
                // If we're not storing passwords, no need to patch the IL
                if (HostnameConnect.savePassword.Value)
                {
                    for (int index = 0; index < list.Count; ++index)
                    {
                        if (list[index].opcode == OpCodes.Ldstr)
                        {
                            // Replace ldstr "" with callvirt <ZNet_RPC_ClientHandshake.PasswordProvider>
                            list[index].opcode = OpCodes.Call;
                            list[index].operand = (object)AccessTools.Method(typeof(ZNetPatch.ZNet_RPC_ClientHandshake), "PasswordProvider");
                            break;
                        }
                    }
                }

                return list.AsEnumerable<CodeInstruction>();
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZNet), "RPC_Error")]
        public static void ZNet_RPC_Error(ZRpc rpc, int error)
        {
            // Dump the stored password if the server responded with an invalid password error
            if (error == (int)ZNet.ConnectionStatus.ErrorPassword)
            {
                HostnameConnect.serverPassword.Value = "";
            }
        }
    }
}
