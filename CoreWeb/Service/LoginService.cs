using CoreWeb.MongoDbRepository;
using CoreWeb.MongoDbRepository.Entities;
using CoreWeb.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb.Service
{
    public class LoginService : ILoginService
    {
        public IMongoRepository<BackendAccount> BackendAccountRepo { get; }

        public LoginService(IMongoRepository<BackendAccount> backendAccountRepo)
        {
            BackendAccountRepo = backendAccountRepo;
        }

        public bool IsValidBackendAccount(string account, string watchword)
        {
            var sha1watchword = watchword.ToSha1();
            var backendAccount = BackendAccountRepo.FindOne(x => x.Account == account && x.Watchword == sha1watchword);

            if(backendAccount.IsNull()) return false;

            return true;
        }

      
    }
}
