﻿using Microsoft.AspNetCore.Mvc;
using SolidTrivia.Game;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace SolidTrivia.Web.Controllers
{
    [Route("api/[controller]")]
    public class SmsController : TwilioController
    {
        public SmsController(ISolidTrivia game)
        {
            Game = game;
        }

        public ISolidTrivia Game { get; }

        public IActionResult Index(SmsRequest incomingMessage)
        {
            var smsNumber = incomingMessage.From;
            var body = incomingMessage.Body;

            var response = Game.ProcessUserSmsMessage(smsNumber, body);
            if (response.HasMessage)
            {
                var messagingResponse = new MessagingResponse();
                messagingResponse.Message(response.Body);
                //return TwiML(messagingResponse);
                return new OkResult();
            }
            return new OkResult();
        }
    }
}