using CSharpLab.A06_Cache;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace CSharpLab
{
    public class FeatureToggleService
    {
        private readonly static object _lock = new object();
        private List<FeatureToggle> _featureToggleList;
        private IFeatureToggleRepo _featureToggleRepo;

        public FeatureToggleService(IFeatureToggleRepo featureToggleRepo)
        {
            _featureToggleRepo = featureToggleRepo;
        }
        private List<FeatureToggle> GetCacheWithLock()
        {
            lock (_lock)
            {

                if (_featureToggleList != null)
                {
                    return _featureToggleList;
                }
                Thread.Sleep(1000);
                _featureToggleList = UpdateCache();
            }

            return _featureToggleList;
        }

        public FeatureToggle GetFeatureToggle(string code)
        {
            CheckData();
            var featureToggle = _featureToggleList.FirstOrDefault(x => x.Code == code);
            return featureToggle ?? new FeatureToggle { Status = FeatureToggleStatusType.Close };
        }

        public List<FeatureToggle> UpdateCache()
        {

            return _featureToggleRepo.GetAllByDatabase();
        }
        private void CheckData()
        {
            if (_featureToggleList == null)
            {
                _featureToggleList = GetCacheWithLock();
            }
        }
    }

    public interface IFeatureToggleRepo
    {
        List<FeatureToggle> GetAllByDatabase();
    }

    public class FeatureToggle
    {
        public long RecordId { get; set; }
        public string Name { get; set; }
        public FeatureToggleStatusType Status { get; set; }
        /// <summary>
        /// 測試人員 Member Id 用逗號分隔
        /// </summary>
        public string TesterMemberIdList { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }


        public string Code { get; set; }


    }

    public enum FeatureToggleStatusType
    {
        /// <summary>
        /// 關閉
        /// </summary>
        [Description("關閉")]
        Close = 0,
        /// <summary>
        /// 測試中
        /// </summary>
        [Description("測試中")]
        Testing = 1,
        /// <summary>
        /// 開放
        /// </summary>
        [Description("開放")]
        Open = 2
    }
}
