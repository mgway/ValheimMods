using HarmonyLib;

namespace ValheimHostnameConnect
{
    [HarmonyPatch(typeof(FejdStartup), "OnJoinIPOpen")]
    public static class FejdStartupPatch
    {
        private static void Postfix(FejdStartup __instance)
        {
            __instance.m_joinIPAddress.text = HostnameConnect.lastServerAddress.Value;
        }
    }
}
