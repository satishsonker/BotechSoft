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
                var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                var temp = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "emailTemplate", "query.html"));
                var message = new Message(new string[] { "btech.csit@gmail.com", "infotovikas@gmail.com" }, "User Query from Botechsoft.com - " + queryModel.UserName, temp.Replace("##location##", queryModel.UserLocation).Replace("##designation##", queryModel.UserDesignation).Replace("##company##", queryModel.UserCompany).Replace("##querytype##", queryModel.QueryType).Replace("##email##", queryModel.UserEmail).Replace("##name##", queryModel.UserName).Replace("##userquery##", queryModel.Query).Replace("##mobile##",queryModel.IsdCode+"-"+queryModel.UserMobile),null);
                _emailSender.SendEmail(message);

                return Json("Sent");
            }
            catch (Exception ex)
            {
                return Json(new { ex });
            }
        }

        [HttpPost]
        public ActionResult SendEmail([FromForm] QueryModel queryModel)
        {
            try
            {
                var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                var temp = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "emailTemplate", "submitCV.html"));
                var message = new Message(new string[] { "btech.csit@gmail.com", "infotovikas@gmail.com" }, "Job Application from Botechsoft.com - ",temp.Replace("##exp#", queryModel.UserExperience).Replace("##location#", queryModel.UserLocation).Replace("##email#", queryModel.UserEmail).Replace("##name##",queryModel.UserName), files);
                _emailSender.SendEmail(message);

                return RedirectToAction("contactus","home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("contactus", "home");
            }
        }
    }
}