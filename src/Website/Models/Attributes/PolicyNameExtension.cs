using BrickAtHeart.Communities.Models.Authorization;
using System;
using System.Linq;

namespace BrickAtHeart.Communities.Models.Attributes
{
    public static class PolicyNameExtension
    {
        public static string GetPolicyName( this Right right )
        {
            Type enumType = right.GetType();
            string? name = Enum.GetName(enumType, right);
            PolicyNameAttribute? attribute = null;

            if (name != null)
            {
                attribute = enumType.GetField(name)?.GetCustomAttributes(false).OfType<PolicyNameAttribute>().SingleOrDefault();
            }

            if (attribute != null)
            {
                return attribute.PolicyName;
            }

            return string.Empty;
        }
    }
}