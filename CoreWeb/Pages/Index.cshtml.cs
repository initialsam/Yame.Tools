using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWeb.MongoDbRepository;
using CoreWeb.MongoDbRepository.Entities;
using CoreWeb.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoreWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IMongoRepository<BackendAccount> BackendAccountRepo { get;  }
        
        public IndexModel(ILogger<IndexModel> logger
                          , IMongoRepository<BackendAccount> backendAccountRepo)
        {
            _logger = logger;
            BackendAccountRepo = backendAccountRepo;
        }

        public void OnGet()
        {
            if(BackendAccountRepo.AsQueryable().Any() == false)
            {
                string sha1Watchword = "admin".ToSha1();
                BackendAccountRepo.InsertOne(new BackendAccount
                {
                    Account = "admin",
                    Watchword = sha1Watchword
                });
            }
        }

    }
}
