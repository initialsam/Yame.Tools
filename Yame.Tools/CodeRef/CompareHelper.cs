using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yame.Tools.CodeRef
{
    public class HotfixInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// KB123456
        /// </summary>
        public string Hotfix { get; set; }

        /// <summary>
        /// 201404 Windows安全性更新 (KB123456)
        /// </summary>
        public string FullTitle { get; set; }
    }

    public class CompareResult
    {
        public List<HotfixInfo> ToBeAdded { get; set; }
        public List<HotfixInfo> ToBeUpdated { get; set; }
        public List<HotfixInfo> ToBeDeleted { get; set; }
    }

    public static class CompareHelper
    {
        public static CompareResult CompareList(List<HotfixInfo> nowList, List<HotfixInfo> originalList )
        {
            var result = new CompareResult();
            result.ToBeAdded = nowList.Where(x => !originalList.Select(m => m.Hotfix).Contains(x.Hotfix)).ToList();
            result.ToBeDeleted = originalList.Where(x => !nowList.Select(c => c.Hotfix).Contains(x.Hotfix)).ToList();
            result.ToBeUpdated = nowList.Where(x => originalList.Any(m => m.Hotfix == x.Hotfix && m.FullTitle != x.FullTitle)).ToList();

            return result;
        }
    }
}
