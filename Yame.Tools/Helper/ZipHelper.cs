using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class ZipHelper
    {

        public static string CreateZipArchive(string sourceFile)
        {
            string workPath = Path.GetDirectoryName(sourceFile);
            string createEntryFileName = Path.GetFileName(sourceFile);
            string targetZipFileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(sourceFile), ".zip");
            string distinationFile = Path.Combine(workPath, targetZipFileName);
            if (System.IO.File.Exists(distinationFile))
            {
                return distinationFile;
            }
            FileStream f = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);

            try
            {
                using (var fileStream = new FileStream(distinationFile, FileMode.CreateNew))
                {
                    using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
                    {
                        byte[] xlsxBytes = BinaryReadToEnd(f);

                        var zipArchiveEntry = archive.CreateEntry(createEntryFileName, CompressionLevel.Fastest);
                        BinaryReader bs = new BinaryReader(f);

                        using (var zipStream = zipArchiveEntry.Open())
                        {
                            zipStream.Write(xlsxBytes, 0, xlsxBytes.Length);
                        }
                    }
                }
            }
            finally
            {
                f.Close();
            }

            return distinationFile;
        }

        public static byte[] BinaryReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        /// <summary>
        /// 解壓縮後刪除Zip檔
        /// </summary>
        /// <param name="fullPathDto"></param>
        /// <param name="zipPassword"></param>
        /// <returns></returns>
        public static bool UnZipAndDelete(FullPathDto fullPathDto, string zipPassword)
        {
            if (File.Exists(fullPathDto.LocalZipFullPath))
            {
                try
                {
                    var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                    using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(fullPathDto.LocalZipFullPath, options))
                    {
                        zip.Password = zipPassword; // 解壓密碼
                        zip.ExtractAll(fullPathDto.LocalFolder, ExtractExistingFileAction.OverwriteSilently);  // 解壓全部
                    }
                    File.Delete(fullPathDto.LocalZipFullPath);
                    if (File.Exists(fullPathDto.LocalXmlFullPath))
                    {
                        Console.WriteLine($"解ZIP 成功 XML: {fullPathDto.LocalXmlFullPath}");
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"解ZIP {fullPathDto.LocalZipFullPath} 發生錯誤 {ex}");
                }
            }

            return false;
        }
    }

    public class FullPathDto
    {
        public string FtpWorkZipFullPath { get; set; }
        public string FtpOkZipFullPath { get; set; }
        public string FtpErrZipFullPath { get; set; }

        public string LocalFolder { get; set; }
        public string LocalZipFullPath { get; set; }
        public string LocalXmlFullPath { get; set; }

    }
}
