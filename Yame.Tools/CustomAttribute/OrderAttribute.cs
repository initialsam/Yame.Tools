using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Yame.Tools.NetCore.CustomAttribute
{
    /// <summary>
    /// 指定物件屬性順序 讓反射取值時 用自訂的順序排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int order_;
        public OrderAttribute([CallerLineNumber]int order = 0)
        {
            order_ = order;
        }

        public int Order { get { return order_; } }
    }
}
