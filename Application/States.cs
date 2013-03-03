using System;

namespace Mossywell.BSR
{
    public enum RebootReason
    {
        Manual,
        AutomaticNoConnectivity,
        AutomaticLowSNR,
    }

    public enum RebootState
    {
        Normal,
        RebootRequestedPreDelay,
        RebootRequestedPostDelay,
        CustomCommandsRequestedPreDelay,
        CustomCommandsRequestedPostDelay,
    }

    public enum RunningState
    {
        Running,
        Paused,
        Closing,
    }

    public enum MaintenanceMode
    {
        On,
        Off,
    }

    public enum CustomCommandReason
    {
        AutomaticAfterReboot,
        Manual,
    }

    public static class States
    {
        // Default to a year ago just in case an immediate reboot is needed
        public static bool LogDisasterAlreadyDisplayed = false;
        public static CustomCommandReason CustomCommandReason = CustomCommandReason.AutomaticAfterReboot;
        public static RunningState RunningState = RunningState.Running;
        public static RebootState RebootState = RebootState.Normal;
        public static MaintenanceMode MaintenanceMode = MaintenanceMode.Off;
    }
}