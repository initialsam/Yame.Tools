using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WebSocketServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer wsServer = new WebSocketServer();
            if (!wsServer.Setup("127.0.0.1", 8044))
            {

                Console.WriteLine("Server Setup失敗");
                Console.ReadKey();
            }


            Console.WriteLine("啟動 Web Socket Server...");
            if (!wsServer.Start())
            {
                Console.WriteLine("啟動Server失敗");
                Console.ReadKey();
            }

            wsServer.NewSessionConnected += (session) =>
            {
                Console.WriteLine("新的連線");
                Console.WriteLine(session.Connected);
                Console.WriteLine("=============================");

            };
            wsServer.SessionClosed += (session, reason) =>
            {
                Console.WriteLine("斷線");
                Console.WriteLine(session.Connected);
                Console.WriteLine(reason);
                Console.WriteLine("=============================");
            };
            wsServer.NewMessageReceived += (session, message) =>
            {
                Console.WriteLine("收到 NewMessage");
                Console.WriteLine(session.Connected);
                Console.WriteLine(message);
                Console.WriteLine("=============================");
            };
            wsServer.NewDataReceived += (session, bytes) =>
            {
                Console.WriteLine("收到 NewData");
                Console.WriteLine(session.Connected);
                Console.WriteLine(bytes);
                Console.WriteLine("=============================");
            };
            Timer timer = new Timer((data) =>
            {
                string msg = $@"server 時間：{DateTime.Now.ToString("HH:mm:ss")}";
                Console.WriteLine(msg);//Debug用


                //對目前已連接的所有session進行廣播
                foreach (WebSocketSession session in wsServer.GetAllSessions())
                {
                    session.Send($"{msg} SessionID: {session.SessionID}");
                }

            }, null, 0, 5000);//每1秒執行一次

            //暫停畫面
            Console.ReadKey();
        }
    }
}
