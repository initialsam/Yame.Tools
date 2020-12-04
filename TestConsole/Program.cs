using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace TestConsole
{


    class Program
    {
        static void Main(string[] args)
        {


            List<ShippingOrderItem> BatchList = new List<ShippingOrderItem>()
            {
                 new ShippingOrderItem{ OrderNo="00124", FlowType = "N" },
                 new ShippingOrderItem{ OrderNo="00125", FlowType = "N" },
                 new ShippingOrderItem{ OrderNo="00126", FlowType = "R" },
                 new ShippingOrderItem{ OrderNo="00128", FlowType = "R" },
                 new ShippingOrderItem{ OrderNo="00129", FlowType = "" },
            };

            List<ShippingOrderItem> DayList = new List<ShippingOrderItem>()
            {
                new ShippingOrderItem { OrderNo = "00123", FlowType = "N" },
                 new ShippingOrderItem { OrderNo = "00124", FlowType = "N" },
                 new ShippingOrderItem { OrderNo = "00125", FlowType = "N" },
                 new ShippingOrderItem { OrderNo = "00126", FlowType = "R" },
                 new ShippingOrderItem { OrderNo = "00127", FlowType = "R" },
                 new ShippingOrderItem { OrderNo = "00128", FlowType = "R" },
                 new ShippingOrderItem{ OrderNo="00129", FlowType = "R" },
                 new ShippingOrderItem{ OrderNo="00130", FlowType = "" },
            };

            var ExceptList1 = BatchList.Except(DayList, new  ShippingOrderItemComparer()).ToList();
            var ExceptList2 = DayList.Except(BatchList, new ShippingOrderItemComparer()).ToList();


        }

    }

    /// <summary>
    /// 貨運單資料
    /// </summary>
    public class ShippingOrderItem//: IEquatable<ShippingOrderItem>
    {
        /// <summary>
        /// 貨運單號
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 進退貨狀態代碼
        /// </summary>
        public string FlowType { get; set; }


        //public bool Equals(ShippingOrderItem other)
        //{
        //    if (other == null) return false;
        //    return this.OrderNo == other.OrderNo &&
        //           this.FlowType == other.FlowType;
        //}
        //public override bool Equals(object obj) => Equals(obj as ShippingOrderItem);

        //public override int GetHashCode()
        //{
        //    int hashCode = -1367433319;
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderNo);
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FlowType);
        //    return hashCode;
        //}
    }
    public class ShippingOrderItemComparer : IEqualityComparer<ShippingOrderItem>
    {
        public bool Equals(ShippingOrderItem x, ShippingOrderItem y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.OrderNo == y.OrderNo &&
                   x.FlowType == y.FlowType;
        }

        public int GetHashCode(ShippingOrderItem obj)
        {
            if (object.ReferenceEquals(obj, null)) return 0;

            int hashCode = -1367433319;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(obj.OrderNo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(obj.FlowType);
            return hashCode;
        }
    }

}
