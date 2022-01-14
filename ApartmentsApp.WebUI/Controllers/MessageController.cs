using ApartmentsApp.Models;
using ApartmentsApp.Models.Messages;
using ApartmentsApp.Models.Users;
using ApartmentsApp.Services.MessageServices;
using ApartmentsApp.Services.UserServices;
using ApartmentsApp.WebUI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.WebUI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;
        public MessageController(IMessageService messageService, JwtService jwtService, IUserService userService)
        {
            _messageService = messageService;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpGet]
        [Route("MessageStatus/{messageId}")]
        public BaseModel<MessageStatusModel> Status(int messageId)
        {
            BaseModel<MessageStatusModel> response = new();
            MessageStatusModel model = new()
            {
                isClosed = false,
                isSender = false
            };
            response.entity = model;
            var token = _jwtService.Verify(Request.Cookies["jwt"]);
            int currentUserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);
            var currentMessage = _messageService.GetById(messageId);
            if (currentMessage.entity.SenderId == currentUserId)
            {
                response.entity.isSender = true;
            }
            if(currentMessage.entity.ReceiverMessage != null)
            {
                response.entity.isClosed = true;
            }
            return response;
        }
        //tüm mesajları getir
        [HttpGet]
        [Route("GetMyMessages")]
        public BaseModel<MessageListModel> GetAll()
        {
            BaseModel<MessageListModel> response = new();
            var token = _jwtService.Verify(Request.Cookies["jwt"]);
            int currentUserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);
            response = _messageService.ListMyMessages(currentUserId);
            return response;
        }

        [HttpGet("{id}")]
        [Route("GetCurrentMessage/{id}")]
        public BaseModel<MessageDetailModel> GetById(int id)
        {
            BaseModel<MessageDetailModel> response = new();
            response = _messageService.GetById(id);
            return response;
        }

        [HttpPut("{messageId}")]
        [Route("SetReaded/{messageId}")]
        public BaseModel<bool> SetReaded(int messageId)
        {
            BaseModel<bool> response = new();
            var token = _jwtService.Verify(Request.Cookies["jwt"]);
            int currentUserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);
            bool isSender = false;
            if (_messageService.GetById(messageId).entity.SenderId == currentUserId)
            {
                isSender = true;
            }
            if (isSender)
            {
                _messageService.SetSenderReaded(messageId);
            }
            else
            {
                _messageService.SetReceiverReaded(messageId);
            }
            response.isSuccess = true;
            return response;
        }

        [HttpGet]
        [Route("PopulateList")]
        public BaseModel<UserSelectListModel> PopulateDropdown()
        {
            BaseModel<UserSelectListModel> response = new();
            var token = _jwtService.Verify(Request.Cookies["jwt"]);
            string role = token.Claims.FirstOrDefault(c => c.Type == "role").Value;
            if (role == "Admin")
            {
                response = _userService.FillDropdownWithUsers();
            }
            else
            {
                response = _userService.FillDropdownWithAdmins();
            }
            return response;
        }
        //kullanıcı için mesaj ekleme kımsını yap admine atmasın
        [HttpPost]
        [Route("Send")]
        public BaseModel<MessageSendModel> InsertOrUpdate([FromBody] MessageSendModel message)
        {
            BaseModel<MessageSendModel> response = new();
            if (message is { Id: > 0 })
            {
                response = _messageService.SendMessage(message);
            }
            else
            {
                var token = _jwtService.Verify(Request.Cookies["jwt"]);
                int currentUserId = Convert.ToInt32(token.Claims.FirstOrDefault(c => c.Type == "unique_name").Value);
                message.SenderId = currentUserId;
                response = _messageService.SendMessage(message);
            }

            return response;
        }
    }
}
