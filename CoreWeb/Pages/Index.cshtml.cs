using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWeb.MongoDbRepository;
using CoreWeb.MongoDbRepository.Entities;
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
                var sha1 = System.Security.Cryptography.SHA1.Create();

                var crypto = sha1.ComputeHash(Encoding.Default.GetBytes("admin"));
                string sha1Watchword = Convert.ToBase64String(crypto);
                BackendAccountRepo.InsertOne(new BackendAccount
                {
                     Account = "admin",
                      Watchword = sha1Watchword
                });
            }
        }
    }
}
