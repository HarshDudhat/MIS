// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.Account
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common.Resources;

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Http;
    using System.Web.ModelBinding;
    using Newtonsoft.Json;
    using System.Text;
    using System.Web;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.IO;
    using System.Web.UI;
    using System.Net.Mail;
    #endregion
    public class AccountController : BaseController
    {
        private MVCProjectEntities entities;

        public AccountController()
        {
            this.entities = new MVCProjectEntities();
        }

        [HttpPost]
        public ApiResponse Login(MIS_Users users)
        {
            var user = entities.USP_MIS_AuthenticateUser(users.Email, users.Password).SingleOrDefault();
            if (user == null)
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.IncorrectCredentials));
            }
            else
            {
                UserContext userContext = new UserContext();
                userContext.UserId = user.UserId;
                userContext.UserName = user.Email;
                userContext.RoleId = (int)user.RoleId;
                userContext.Ticks = DateTime.Now.Ticks;
                //userContext.Token = SecurityUtility.GetToken(userContext);
                userContext.TimeZoneMinutes = 330;
                return this.Response(MessageTypes.Success, string.Empty, userContext);
            }
        }

        [HttpGet]
        public ApiResponse getusersdetails(string user)
        {
            var data = this.entities.MIS_Users
                .Where(x => x.IsActive.Value && x.Email == user)
                .Select(x => new
                {
                    Id = x.UserId,
                    email = x.Email,
                    //EmailId = x.EmpId > 0 ? this.entities.MIS_Users.FirstOrDefault(y => y.UserId == x.UserId).EmailId : string.Empty
                })
                .ToList()
                .Select(x => new
                {
                    Id = x.Id,
                    EmailId = MaskEmail(x.email)
                })
                .ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return email;
            }

            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                return email;
            }

            var maskedFirstPart = parts[0].Substring(0, 2) + new string('x', parts[0].Length - 4) + parts[0].Substring(parts[0].Length - 2);
            var maskedSecondPart = parts[1].Substring(0, 2) + new string('x', parts[1].Length - 4) + parts[1].Substring(parts[1].Length - 2);
            return maskedFirstPart + "@" + maskedSecondPart;
        }


        [HttpPost]
        public ApiResponse GetRandomString(int id)
        {
            int length = 6;
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }


            MIS_Users userdetails = (from a in this.entities.MIS_Users
                                      where a.UserId == id
                                      select a).SingleOrDefault();

            userdetails.SecurityCode = res.ToString();

            this.entities.SaveChanges();
            var userList = (from s in this.entities.MIS_Users
                                          .AsEnumerable()
                                     
                                      let TotalRecords = this.entities.MIS_Users.AsEnumerable()
                                      where s.UserId == id
                                      select new
                                      {
                                          UserId = s.UserId,
                                          Email = s.Email,
                                          SecurityCode = s.SecurityCode,
                                          TotalRecords,

                                      }).FirstOrDefault();
            var recipientEmail = userList.Email;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("noreplyehstesting78@gmail.com");
                mail.To.Add(recipientEmail);
                mail.Subject = "Reset Password";
                mail.Body = $"<html>\r\n<head>\r\n    <style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            font-size: 16px;\r\n            color: #333333;\r\n            line-height: 1.5;\r\n        }}\r\n        h1 {{\r\n            font-size: 24px;\r\n            color: #333333;\r\n            margin: 0;\r\n            padding: 0;\r\n        }}\r\n        p {{\r\n            margin-top: 0;\r\n            margin-bottom: 20px;\r\n        }}\r\n        .security-code {{\r\n            font-size: 18px;\r\n            font-weight: bold;\r\n            color: #007bff;\r\n            margin: 20px 0;\r\n            padding: 10px;\r\n            border: 2px solid #007bff;\r\n            border-radius: 4px;\r\n        }}\r\n        .contact {{\r\n            margin-top: 20px;\r\n            font-size: 14px;\r\n        }}\r\n        .contact a {{\r\n            color: #007bff;\r\n            text-decoration: none;\r\n        }}\r\n        .signature {{\r\n            margin-top: 40px;\r\n            font-size: 14px;\r\n            color: #666666;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>Reset Your Password</h1>\r\n    <p>Dear [{userList.Email}],</p>\r\n    <p>You recently requested to reset your password for account. To complete the process, please use the following security code:</p>\r\n    <p class=\"security-code\">[{userList.SecurityCode}]</p>\r\n    <p>Please note that this security code is valid for the next 10 minutes only. If you do not use it within this time, you will need to request a new security code.</p>\r\n    <p>If you did not request to reset your password, please contact our support team immediately at ASK EHS</a>.</p>\r\n    <div class=\"contact\">\r\n        <p>Thank you,</p>\r\n        <p>[ASK EHS] Team</p>\r\n     </div>\r\n    <div class=\"signature\">\r\n        <p>This email was sent to you by [ASK EHS].</p>\r\n   </div>\r\n</body>\r\n</html>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("noreplyehstesting78@gmail.com", "cntpraqgsaqjlxpi");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: Resource.Success);
        }

        [HttpPost]
        public ApiResponse VerifyCode(MIS_Users code)
        {
            var dbUser = entities.MIS_Users.Where(u => u.SecurityCode == code.SecurityCode && u.Email == code.Email).FirstOrDefault();
            if (dbUser == null)
            {
                return new ApiResponse { /*Status = "Error",*/ Message = "Invalid Security Code" };
            }
            else
            {
                //return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.LoginSuccessfully));
                //return new ApiResponse { Status = "Success", Message = "Login successful." };
                //return this.RedirectToAction("Index", "Home");
                return this.Response(Utilities.MessageTypes.Success, responseToReturn: "Verify Successful");
            }

        }

        //update password
        [HttpPost]
        public ApiResponse UpdatePassword(MIS_Users user)
        {
            MIS_Users userdetails = this.entities.MIS_Users
                .Where(u => u.UserId == user.UserId).FirstOrDefault();

                      userdetails.Password = user.Password;

            this.entities.SaveChanges();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: "Upadated Successful");
        }

    }
}
