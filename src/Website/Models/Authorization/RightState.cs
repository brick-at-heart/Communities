using Microsoft.Extensions.Primitives;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public enum RightState
    {
        Denied,
        Granted,
        Unknown
    }

    public static class RightStateHelper
    {
        public static RightState ToRightState(this bool? value)
        {
            switch (value)
            {
                case false:
                    return RightState.Denied;
                case true:
                    return RightState.Granted;
                default:
                    return RightState.Unknown;
            }
        }

        public static RightState ToRightState(this StringValues value)
        {
            switch (value.ToString())
            {
                case "0":
                    return RightState.Denied;
                case "1":
                    return RightState.Granted;
                default:
                    return RightState.Unknown;
            }
        }

        public static bool? ToNullableBool(this RightState value)
        {
            switch (value)
            {
                case RightState.Denied:
                    return false;
                case RightState.Granted:
                    return true;
                default:
                    return null;
            }
        }
    }
}