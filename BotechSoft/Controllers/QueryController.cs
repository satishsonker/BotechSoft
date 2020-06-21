using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotechSoft.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace BotechSoft.Controllers
{
    public class QueryController : Controller
    {
        private readonly IEmailSender _emailSender;
        public QueryController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public JsonResult SendQuery([FromBody] QueryModel queryModel)
        {
            try
            {
                string templatPath = Path.Combine(Directory.GetCurrentDirectory(), "emailTemplate", "query.html");
                var emailHtmlTemplat = System.IO.File.ReadAllText(templatPath);
                //List<string> receivers = new List<string>() { "btech.csit@gmail.com", "infotovikas@gmail.com" };
                List<string> receivers = new List<string>() { "hr@botechsoft.com", "infotoshalinisingh@gmail.com" };
                string subject = "User Query from Botechsoft.com - " + queryModel.UserName;
                string body = emailHtmlTemplat.Replace("##location##", queryModel.UserLocation)
                                                .Replace("##designation##", queryModel.UserDesignation)
                                                .Replace("##company##", queryModel.UserCompany)
                                                .Replace("##querytype##", queryModel.QueryType)
                                                .Replace("##email##", queryModel.UserEmail)
                                                .Replace("##name##", queryModel.UserName)
                                                .Replace("##userquery##", queryModel.Query)
                                                .Replace("##mobile##", queryModel.IsdCode + "-" + queryModel.UserMobile);
                var message = new Message(receivers, subject, body, null);
                _emailSender.SendEmail(message);
                return Json("Sent");
            }
            catch (Exception ex)
            {
                return Json(new {error= ex.Message,code=500 });
            }
        }

        [HttpPost]
        public JsonResult SendEmail([FromForm] QueryModel queryModel)
        {
            try
            {
                var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
                //List<string> receivers = new List<string>() { "btech.csit@gmail.com", "infotovikas@gmail.com" };
                List<string> receivers = new List<string>() { "hr@botechsoft.com", "infotoshalinisingh@gmail.com" };
                var emailHtmlTemplat = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "emailTemplate", "submitCV.html"));
                string subject = "Job Application from Botechsoft.com - " + queryModel.UserSkill;
                string body = emailHtmlTemplat.Replace("##exp##", queryModel.UserExperience)
                                                .Replace("##skill##", queryModel.UserSkill)
                                                .Replace("##location##", queryModel.UserLocation)
                                                .Replace("##email##", queryModel.UserEmail)
                                                .Replace("##name##", queryModel.UserName);
                var message = new Message(receivers,subject,body,files);
                _emailSender.SendEmail(message);
                return Json("Sent");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, code = 500 });
            }
        }
    }
}