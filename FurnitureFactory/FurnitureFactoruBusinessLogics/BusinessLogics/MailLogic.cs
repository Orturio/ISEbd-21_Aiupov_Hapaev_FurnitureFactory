﻿using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FurnitureFactoryBusinessLogics.HelperModels;

namespace FurnitureFactoryBusinessLogics.BusinessLogics
{
    public class MailLogic
    {
        private static string smtpClientHost;

        private static int smtpClientPort;

        private static string mailLogin;

        private static string mailPassword;

        public static async void MailSendAsync(MailSendInfo info)
        {
            if (string.IsNullOrEmpty(smtpClientHost) || smtpClientPort == 0 || string.IsNullOrEmpty(mailLogin) ||
                string.IsNullOrEmpty(mailPassword) || string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) ||
                string.IsNullOrEmpty(info.Text))
            {
                return;
            }

            using (var objMailMessage = new MailMessage())
            {
                using (var objSmtpClient = new SmtpClient(smtpClientHost, smtpClientPort))
                {
                    try
                    {
                        objMailMessage.From = new MailAddress(mailLogin);
                        objMailMessage.To.Add(new MailAddress(info.MailAddress));
                        objMailMessage.Subject = info.Subject;
                        objMailMessage.Body = info.Text;
                        objMailMessage.SubjectEncoding = Encoding.UTF8;
                        objMailMessage.BodyEncoding = Encoding.UTF8;

                        Attachment data = new Attachment(info.ReportFile, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(info.ReportFile);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(info.ReportFile);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(info.ReportFile);
                        objMailMessage.Attachments.Add(data);

                        objSmtpClient.UseDefaultCredentials = false;
                        objSmtpClient.EnableSsl = true;
                        objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        objSmtpClient.Credentials = new NetworkCredential(mailLogin, mailPassword);

                        await Task.Run(() => objSmtpClient.Send(objMailMessage));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static void MailConfig(MailConfig config)
        {
            smtpClientHost = config.SmtpClientHost;
            smtpClientPort = config.SmtpClientPort;
            mailLogin = config.MailLogin;
            mailPassword = config.MailPassword;
        }
    }
}
