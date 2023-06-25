using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLab.A10_Stream
{
    public class A10_Stream_Demo
    {
        public void Test()
        {
            //一個中文字 2個Byte
            //var a = "一二三四五六七八九";
            var a = "abcd1234@";
            var bytes = Encoding.UTF8.GetBytes(a);

            var ms = new MemoryStream(bytes);

            // convert stream to string
            var reader = new StreamReader(ms, System.Text.Encoding.UTF8);
            var text = reader.ReadToEnd();
            Console.WriteLine(text);
            ms.Position = 0;
            var read = new byte[40];
            //Read 第0開始 取四個Byte 所以是一二
            ms.Read(read, 0, 4);
            Console.WriteLine(read.GetString());

            //Read第8開始 取2個Byte 所以是一二+2(空白)+2(空白)+ =一二(空白)(空白)三
            ms.Read(read, 8, 2);
            Console.WriteLine(read.GetString());
            Console.WriteLine($"Position : {ms.Position}");

            //ms Position 取2個Byte所以是五
            //Read 第0開始 取2個Byte 五二(空白)(空白)三
            ms.Position = 8;
            ms.Read(read, 0, 2);
            Console.WriteLine(read.GetString());


        }

        public void Test2()
        {
            byte[] buffer = null;

            string testString = "Stream!Hello world";
            char[] readCharArray = null;
            byte[] readBuffer = null;
            string readString = string.Empty;
            //關於MemoryStream 我會在後續章節詳細闡述
            using (MemoryStream stream = new MemoryStream())
            {
                Console.WriteLine("初始字串為：{0}", testString);
                //如果該流可寫
                if (stream.CanWrite)
                {
                    //首先我們嘗試將testString寫入流中
                    //關於Encoding我會在另一篇文章中詳細說明，暫且通過它實現string->byte[]的轉換
                    buffer = Encoding.Default.GetBytes(testString);
                    //我們從該陣列的第一個位置開始寫，長度為3，寫完之後 stream中便有了資料
                    //對於新手來說很難理解的就是資料是什麼時候寫入到流中，在冗長的項目程式碼面前，我碰見過很
                    //多新手都會有這種經歷，我希望能夠用如此簡單的程式碼讓新手或者老手們在溫故下基礎
                    stream.Write(buffer, 0, 3);

                    Console.WriteLine("現在Stream.Postion在第{0}位置", stream.Position + 1);

                    //從剛才結束的位置（當前位置）往後移3位，到第7位
                    long newPositionInStream = stream.CanSeek ? stream.Seek(3, SeekOrigin.Current) : 0;

                    Console.WriteLine("重新定位後Stream.Postion在第{0}位置", newPositionInStream + 1);
                    if (newPositionInStream < buffer.Length)
                    {
                        //將從新位置（第7位）一直寫到buffer的末尾，注意下stream已經寫入了3個資料“Str”
                        stream.Write(buffer, (int)newPositionInStream, buffer.Length - (int)newPositionInStream);
                    }


                    //寫完後將stream的Position屬性設定成0，開始讀流中的資料
                    stream.Position = 0;

                    // 設定一個空的盒子來接收流中的資料，長度根據stream的長度來決定
                    readBuffer = new byte[stream.Length];


                    //設定stream總的讀取數量 ，
                    //注意！這時候流已經把資料讀到了readBuffer中
                    int count = stream.CanRead ? stream.Read(readBuffer, 0, readBuffer.Length) : 0;


                    //由於剛開始時我們使用加密Encoding的方式,所以我們必須解密將readBuffer轉化成Char陣列，這樣才能重新拼接成string

                    //首先通過流讀出的readBuffer的資料求出從相應Char的數量
                    int charCount = Encoding.Default.GetCharCount(readBuffer, 0, count);
                    //通過該Char的數量 設定一個新的readCharArray陣列
                    readCharArray = new char[charCount];
                    //Encoding 類的強悍之處就是不僅包含加密的方法，甚至將解密者都能建立出來（GetDecoder()），
                    //解密者便會將readCharArray填充（通過GetChars方法，把readBuffer 逐個轉化將byte轉化成char，並且按一致順序填充到readCharArray中）
                    Encoding.Default.GetDecoder().GetChars(readBuffer, 0, count, readCharArray, 0);
                    for (int i = 0; i < readCharArray.Length; i++)
                    {
                        readString += readCharArray[i];
                    }
                    Console.WriteLine("讀取的字串為：{0}", readString);
                }

                stream.Close();
            }

        }

    }

    static class ByteArrayExtendMethod
    {
        public static string GetString(this byte[] data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            if (data.Length == 0)
            {
                return string.Empty;
            }
            try
            {
                return Encoding.Default.GetString(data);
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    static class StringExtendMethod
    {
        public static byte[] GetBytes(this string str)
        {
            if (str == null)
            {
                return null;
            }
            if (str.Length == 0)
            {
                return null;
            }
            if (str.Trim().Length == 0)
            {
                return null;
            }
            try
            {
                return Encoding.Default.GetBytes(str);
            }
            catch
            {
                return null;
            }
        }
    }
}