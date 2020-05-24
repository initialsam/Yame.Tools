using MsSqlRepoitory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlRepoitory
{
    public class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            CreateLocationTagd(context);
            CreateSystemSetting(context);
        }
        private static void CreateSystemSetting(DataContext context)
        {
            if (!context.SystemSetting.Any())
            {
                context.SystemSetting.Add(new SystemSetting
                {
                    IsForceUpdateNote = false,
                    UpdateNote = String.Empty
                }); ;
            }
        }

        private static void CreateLocationTagd(DataContext context)
        {
            if (!context.LocationTags.Any())
            {
                context.LocationTags.Add(new LocationTag
                {
                    LocationName = "台北",
                });
                context.LocationTags.Add(new LocationTag
                {
                    LocationName = "高雄",
                });
            }
        }

 
    }
}
