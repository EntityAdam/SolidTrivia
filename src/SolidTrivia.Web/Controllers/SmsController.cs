using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolidTrivia.Game;
using Twilio;
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

            var response = Game.ProcessUserMessage(smsNumber, body);
            if (response.HasResponse)
            {
                var messagingResponse = new MessagingResponse();
                messagingResponse.Message(response.Body);
                return TwiML(messagingResponse);
            }
            return new OkResult();
        }
    }
}