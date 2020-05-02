using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    namespace Xchange.WebApi2.SignalR
    {
        public class FakeCommentInfo
        {
            public int AccountID { get; set; }
            public string Name { get; set; }
            public string Comment { get; set; }
            public DateTime CreateDate { get; set; }
        }
        /// <summary>
        /// 悄悄話帳號分類
        /// </summary>
        public enum CommentAccountType
        {
            /// <summary>
            /// Post Account
            /// </summary>
            PostAccount = 0,
            /// <summary>
            /// Product Owner ID
            /// </summary>
            ProductOwnerID = 1,
        }
        public class CommentAccount
        {
            public CommentAccountType CommentAccountType { get; set; }
            public int Account { get; set; }
            public string ConnectionId { get; set; }

        }
        public class JoinRoomModel
        {
            public string MessageId { get; set; }
            public int UserId { get; set; }
        }

        public enum JoinRoomType
        {
            MessageId = 0,
            UserId = 1,
            privateRoomHub = 2
        }

       
        [HubName(nameof(JoinRoomType.privateRoomHub))]
        //[AuthorizeClaims]
        //[HubName("privateRoomHub")]
        public class PrivateRoomHub : Hub
        {
            private static Dictionary<string, List<CommentAccount>> RoomDictionary = new Dictionary<string, List<CommentAccount>>();

            public override Task OnConnected()
            {

                //var messageId = Context.QueryString[nameof(JoinRoomModel.MessageId)];
                //var userId = Context.QueryString[nameof(JoinRoomModel.UserId)];
                string messageId = GetMessageId();

                //var userId = Context.QueryString["UserId"];
                var name = Context.Headers["name"];
                var transport = Context.QueryString.First(p => p.Key == "transport").Value;
                Clients.All.connected($"OnConnected ConnectionId:{Context.ConnectionId} , Transport : {transport} , name : {name}");
                Groups.Add(Context.ConnectionId, messageId);

                return base.OnConnected();
            }
            public override Task OnReconnected()
            {
                string messageId = GetMessageId();
                var name = Context.Headers["name"];
                Clients.All.connected($"OnReconnected ConnectionId:{Context.ConnectionId}, name : {name}");
                Groups.Add(Context.ConnectionId, messageId);
                return base.OnReconnected();
            }

            private string GetMessageId()
            {
                var messageId = Context.Headers["MessageId"];
                messageId = Context.QueryString["MessageId"];
                return messageId;
            }

            public override Task OnDisconnected(bool stopCalled)
            {
                //var messageId = Context.QueryString[nameof(JoinRoomType.MessageId)];
                //var userId = Context.QueryString[nameof(JoinRoomType.UserId)];

                //var messageId = Context.QueryString["MessageId"];
                //var userId = Context.QueryString["UserId"];
                string messageId = GetMessageId();
                //Context.ConnectionId
                var name = Context.Headers["name"];
                Clients.All.connected($"OnDisconnected ConnectionId:{Context.ConnectionId}, name : {name}");
                Groups.Remove(Context.ConnectionId, messageId);
                return base.OnDisconnected(stopCalled);
            }
            /// <summary>
            /// 加入聊天室
            /// </summary>
            [HubMethodName("TestA")]
            public void TestA()
            {
                Clients.All.connected($"ConnectionId:{Context.ConnectionId},TestA");
            }

            /// <summary>
            /// 加入聊天室
            /// </summary>
            [HubMethodName("JoinRoom")]
            public void JoinRoom(string messageId, int userId)
            {
                //string messageId = GetMessageId();
                var commentAccount = new CommentAccount
                {
                    Account = userId,
                    ConnectionId = Context.ConnectionId,
                    CommentAccountType = CommentAccountType.PostAccount
                };
                var connectionIds = GetConnectionIdList(messageId, commentAccount);
                //Clients.All.connected($"ConnectionId:{Context.ConnectionId},messageId:{messageId},int userId:{userId}");
                //Clients.Clients(connectionIds).joinRoom(messageId, $"int userId:{userId}");
                Clients.Group(messageId).joinRoom(messageId, $"int userId:{userId}");
            }

           
            public void Broadcast(string messageId, int userId, string comment)
            {
                FakeCommentInfo result = new FakeCommentInfo { AccountID = userId, Name = userId.ToString(), Comment = comment, CreateDate = DateTime.Now };
                var json = JsonConvert.SerializeObject(result);
                var connectionIds = GetConnectionIdList(messageId);
                //Clients.Clients(connectionIds).sendMessage(json);
                Clients.Group(messageId).sendMessage(json);
                //Clients.All.showmessage(json);
            }

            private List<string> GetConnectionIdList(string messageId, CommentAccount commentAccount)
            {
                if (RoomDictionary.ContainsKey(messageId))
                {
                    //TODO 防止重覆加
                    RoomDictionary[messageId].Add(commentAccount);
                }
                else
                {
                    RoomDictionary.Add(messageId, new List<CommentAccount> { commentAccount });
                }
                return RoomDictionary[messageId].Select(x => x.ConnectionId).ToList();
            }

            private List<string> GetConnectionIdList(string messageId)
            {
                if (RoomDictionary.ContainsKey(messageId))
                {
                    return RoomDictionary[messageId].Select(x => x.ConnectionId).ToList();
                }

                return new List<string>();
            }
        }




    }
}