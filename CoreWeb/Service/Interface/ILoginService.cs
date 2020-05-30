namespace CoreWeb.Service
{
    public interface ILoginService
    {
        /// <summary>
        /// 後台帳號驗證成功
        /// </summary>
        /// <param name="account"></param>
        /// <param name="watchword"></param>
        /// <returns></returns>
        bool IsValidBackendAccount(string account, string watchword);
    }
}