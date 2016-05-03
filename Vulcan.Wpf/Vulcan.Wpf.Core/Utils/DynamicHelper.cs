using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    internal static class DynamicHelper
    {
        /// <summary>
        /// Get objects from DynamicObject objects
        /// </summary>
        /// <param name="obj">The DynamicObject instance</param>
        /// <param name="memberName">The alias of the member to get</param>
        /// <returns></returns>
        public static object GetDynamicMember(object obj, string memberName)
        {
            var binder = Binder.GetMember(CSharpBinderFlags.None, memberName, obj.GetType(),
                new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
            var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);

            try
            {
                return callsite.Target(callsite, obj);
            }
            catch (Exception ex)
            {
                AppHelper.Logger.Log($"Error in DynamicHelper.GetDynamicMember - returning null. memberName: {memberName}. Error: {ex.Message}");
                return null;
            }
            
        }
    }
}
