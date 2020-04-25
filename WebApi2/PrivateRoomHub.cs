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
        //[HubName("privateRoomHub")]
        public class PrivateRoomHub : Hub
        {
            private static Dictionary<string, List<CommentAccount>> RoomDictionary = new Dictionary<string, List<CommentAccount>>();

            public override Task OnConnected()
            {

                var messageId = Context.QueryString[nameof(JoinRoomModel.MessageId)];
                var userId = Context.QueryString[nameof(JoinRoomModel.UserId)];

                //var messageId = Context.QueryString["MessageId"];
                //var userId = Context.QueryString["UserId"];

                return base.OnConnected();
            }

            public override Task OnDisconnected(bool stopCalled)
            {
                var messageId = Context.QueryString[nameof(JoinRoomType.MessageId)];
                var userId = Context.QueryString[nameof(JoinRoomType.UserId)];

                //var messageId = Context.QueryString["MessageId"];
                //var userId = Context.QueryString["UserId"];

                //Context.ConnectionId
                return base.OnDisconnected(stopCalled);
            }

            /// <summary>
            /// 加入聊天室
            /// </summary>
            public void JoinRoom(string messageId, int userId)
            {
                var commentAccount = new CommentAccount
                {
                    Account = userId,
                    ConnectionId = Context.ConnectionId,
                    CommentAccountType = CommentAccountType.PostAccount
                };
                var connectionIds = GetConnectionIdList(messageId, commentAccount);

                Clients.Clients(connectionIds).joinRoom(messageId, userId);
            }
            public void Broadcast(string messageId, int userId, string comment)
            {
                FakeCommentInfo result = new FakeCommentInfo { AccountID = userId, Name = userId.ToString(), Comment = comment, CreateDate = DateTime.Now };
                var json = JsonConvert.SerializeObject(result);
                var connectionIds = GetConnectionIdList(messageId);
                Clients.Clients(connectionIds).sendMessage(json);
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