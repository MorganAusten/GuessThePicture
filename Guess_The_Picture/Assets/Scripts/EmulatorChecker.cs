using UnityEngine;

public static class EmulatorChecker
{
    public static bool isEmulated = SystemInfo.deviceModel.ToLower().Contains("emulator") ||
                                    SystemInfo.deviceModel.ToLower().Contains("ldplayer") ||
                                    SystemInfo.deviceModel.ToLower().Contains("blue")     ||
                                    SystemInfo.deviceModel.ToLower().Contains("genymotion")||
                                    SystemInfo.deviceModel.ToLower().Contains("nox");
}
