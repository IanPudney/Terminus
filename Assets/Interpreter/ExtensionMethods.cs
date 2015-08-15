/*using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
         | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute() { }
    static public class ExtensionMethods
    {
        public static void Resize<T>(this List<T> list, int sz)
        {
            int cur = list.Count;
            if (sz < cur)
                list.RemoveRange(sz, cur - sz);
            else if (sz > cur)
            {
                if (sz > list.Capacity)//this bit is purely an optimisation, to avoid multiple automatic capacity changes.
                    list.Capacity = sz;
                //generate list
                List<T> toAppend = new List<T>(sz - cur);
                list.AddRange(toAppend);
            }
        }
    }
}
*/