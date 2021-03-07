using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

namespace Yame.Tools.Helper
{
    public static class FtpHelper
    {
        //這是 WinSCP 去下載FTP的範例程式
        /*
         public class ShippingFtp
    {
        private string FTP_Url { get; }
        private string FTP_PortNumber { get; }
        private string FTP_UserName { get; }
        private string FTP_Password { get; }
        private string FTP_MoveFile { get; }
        private string FTP_SshHostKeyFingerprint { get; }
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="ftpSettingDto"></param>
        public ShippingFtp(FtpSettingDto ftpSettingDto)
        {
            FTP_Url = ftpSettingDto.FTP_Url;
            FTP_PortNumber = ftpSettingDto.FTP_PortNumber;
            FTP_UserName = ftpSettingDto.FTP_UserName;
            FTP_Password = ftpSettingDto.FTP_Password;
            FTP_MoveFile = ftpSettingDto.FTP_MoveFile;
            FTP_SshHostKeyFingerprint = ftpSettingDto.FTP_SshHostKeyFingerprint;
        }

        public List<FullPathDto> GetDirectoryFileBySftpAndMove(PathFolderDto pathFolderDto)
        {
            //int result = 0;
            var result = new List<FullPathDto>();
            try
            {
                SessionOptions so = GetSessionOptions();
                using (Session session = new Session())
                {
                    session.Open(so);
                    RemoteDirectoryInfo directory = session.ListDirectory(pathFolderDto.FtpWorkFolder);
                    foreach (RemoteFileInfo fileInfo in directory.Files)
                    {
                        // FileType '-' 表示檔案 不是檔案就下一個 
                        if (fileInfo.FileType != '-') continue;

                        var fullPathDto = PathUtility.GetFullPath(pathFolderDto, fileInfo.Name);
                        session.GetFiles(fullPathDto.FtpWorkZipFullPath, fullPathDto.LocalZipFullPath).Check();
                        byte[] contents = File.ReadAllBytes(fullPathDto.LocalZipFullPath);
                        if(FTP_MoveFile == "true")
                        {
                            session.MoveFile(fullPathDto.FtpWorkZipFullPath, fullPathDto.FtpOkZipFullPath);
                        }
                      
                        //沒有內容不用加入 就下一個 
                        if (contents.Length == 0) continue;

                        result.Add(fullPathDto);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelp.ShowLog($"<GetFileBySftp> remotePath {pathFolderDto.FtpWorkFolder} , exception message={ex}");
            }

            return result;
        }



        /// <summary>
        /// 連線到FTP下載檔案 跟 成功移動到 OK 資料夾
        /// </summary>
        /// <param name="pathDto"></param>
        /// <returns></returns>
        public int GetFileBySftpAndMove(FullPathDto fullPathDto)
        {
            int result = 0;
            try
            {
                SessionOptions so = GetSessionOptions();
                using (Session session = new Session())
                {
                    session.Open(so);

                    if (session.FileExists(fullPathDto.FtpWorkZipFullPath) == false)
                    {
                        CommonHelp.ShowLog($"<GetFileBySftp> remote=[{fullPathDto.FtpWorkZipFullPath}] not exists ");
                        return result;
                    }

                    session.GetFiles(fullPathDto.FtpWorkZipFullPath, fullPathDto.LocalZipFullPath).Check();

                    byte[] contents = File.ReadAllBytes(fullPathDto.LocalZipFullPath);

                    result = contents.Length;
                    CommonHelp.ShowLog($"<GetFileBySftp> Get file from remote=[{fullPathDto.FtpWorkZipFullPath}] to local=[{fullPathDto.LocalZipFullPath}] totalLength=[{result}]");
                    //session.MoveFile(pathDto.FtpWorkZipFullPath, pathDto.FtpOkZipFullPath);
                }
            }
            catch (Exception ex)
            {
                CommonHelp.ShowLog($"<GetFileBySftp> remotePath {fullPathDto.FtpWorkZipFullPath} , exception message={ex}");
            }

            return result;
        }

        private SessionOptions GetSessionOptions()
        {
            return new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = FTP_Url,
                PortNumber = Convert.ToInt32(FTP_PortNumber),
                UserName = FTP_UserName,
                Password = FTP_Password,
                FtpSecure = FtpSecure.None,
                SshHostKeyFingerprint = FTP_SshHostKeyFingerprint
            };
        }
    }
        */

    }

}
