using System;
using HarmonyLib;
using System.Net;

namespace ValheimHostnameConnect
{
    [HarmonyPatch(typeof(ZSteamMatchmaking), "QueueServerJoin")]
    public static class ZSteamMatchmakingPatch
    {
        private static void Prefix(ref string addr)
        {
            string[] strArray = addr.Split(':');

            try
            {
                IPAddress.Parse(strArray[0]);

                // Save IP for later
                HostnameConnect.serverAddress.Value = strArray[0];
                HostnameConnect.serverPort.Value = strArray[1];
            } catch (FormatException)
            {
                // May be a hostname. Sync call
                var ipEntry = Dns.GetHostEntry(strArray[0]);
                if (ipEntry.AddressList.Length > 0)
                {
                    // Save hostname for later
                    HostnameConnect.serverAddress.Value = strArray[0];
                    HostnameConnect.serverPort.Value = strArray[1];

                    addr = ipEntry.AddressList[0] + ":" + strArray[1];
                }
            }
        }
    }
}
