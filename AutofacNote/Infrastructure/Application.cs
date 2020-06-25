using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacNote.Infrastructure
{
    internal class Application: IApplication
    {
        private ITimeService TimeService { get; set; }
        private ISingleton Singleton { get; set; }

        public Application(ITimeService timeService,ISingleton singleton)
        {
            this.TimeService = timeService;
            this.Singleton = singleton;
        }

        public void Go()
        {
            this.TimeService.GetNow();
            this.Singleton.ShowUniqueKey();
        }
    }
}
