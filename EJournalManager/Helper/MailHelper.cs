//using System.Web.Mail;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace EJournalManager.Helper
{
    public class MailHelper
    {

        public SmtpClient client = null;
        private static List<MailMessage> msgList = MailSettings.Instance.MessageList;

        /// <summary>
        /// send mail
        /// </summary>
        public string SendMail(MailMessage Message)
        {
            try
            {
               
                if (Message != null)
                    client.Send(Message);

                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed to send email: " + ex.Message;
            }
        }

        /// <summary>
        /// Static method to draft mail
        /// </summary>
        /// <param name="emaildraft"></param>
        /// <returns></returns>
        public string DraftMail(EmailMessage emaildraft)
        {
            try
            {
                StringBuilder strSentMail = new StringBuilder();
                MailMessage eMsg = new MailMessage();
                eMsg.From = new MailAddress(emaildraft.From);
               
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
               
                if (emaildraft.To != null)
                {
                    if (!emaildraft.To.Equals(""))
                    {
                        string[] argsTo = emaildraft.To.Split(',');
                        for (int i = 0; i <= argsTo.Length - 1; i++)
                        {
                            eMsg.To.Add(new MailAddress(argsTo[i]));
                            strSentMail.Append(argsTo[i] + "\n");
                        }
                    }
                }

                if (emaildraft.CC != null)
                {
                    if (!emaildraft.CC.Equals(""))
                    {
                        string[] argsCC = emaildraft.CC.Split(',');
                        for (int i = 0; i <= argsCC.Length - 1; i++)
                        {
                            eMsg.To.Add(new MailAddress(argsCC[i]));
                            strSentMail.Append(argsCC[i] + "\n");
                        }
                    }
                }

                if (emaildraft.BCC != null)
                {
                    if (!emaildraft.BCC.Equals(""))
                    {
                        string[] argsBCC = emaildraft.BCC.Split(',');
                        for (int i = 0; i <= argsBCC.Length - 1; i++)
                        {
                            eMsg.Bcc.Add(new MailAddress(argsBCC[i]));
                            strSentMail.Append(argsBCC[i] + "\n");
                        }
                    }
                }

                eMsg.Subject = emaildraft.Subject;
                //MailDefinition meassge = new MailDefinition();

                string BodyFileName = "";
                if (emaildraft.Template != null)
                {
                    if (!emaildraft.Template.Equals("") && !emaildraft.TemplatePath.Equals(""))
                    {
                        //meassge.BodyFileName = emaildraft.TemplatePath + emaildraft.Template + ".html";
                        BodyFileName = emaildraft.TemplatePath + emaildraft.Template + ".html";
                    }
                }

                eMsg.Body = emaildraft.Body;

                eMsg.IsBodyHtml = emaildraft.IsBodyHtml;

                if (emaildraft.IsBodyHtml)
                {
                    //MailMessage msgHtml = meassge.CreateMailMessage(emaildraft.To, emaildraft.MarkerList, new LiteralControl());
                    //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml.Body, null, "text/html");
                    string msgHtml = System.IO.File.OpenText(BodyFileName).ReadToEnd();

                    foreach (DictionaryEntry de in emaildraft.MarkerList)
                    {
                        msgHtml = msgHtml.Replace(de.Key.ToString(), de.Value.ToString());
                    }

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml, null, "text/html");

                    //create the LinkedResource (embedded image)
                    LinkedResource logo = new LinkedResource(emaildraft.TemplateLogo, MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    //add the LinkedResource to the appropriate view
                    htmlView.LinkedResources.Add(logo);

                    eMsg.AlternateViews.Add(htmlView);
                }

                if (emaildraft.Attachments != null)
                {
                    if (emaildraft.Attachments.Count > 0)
                    {
                        foreach (string strAttachment in emaildraft.Attachments)
                        {
                            if (File.Exists(strAttachment))
                            {
                                eMsg.Attachments.Add(new Attachment(strAttachment));
                            }
                        }
                    }
                }
                
                MailHelper mailhelper = new MailHelper();
                string message = SendMail(eMsg);
                //MailSettings.Instance.MessageList.Add(eMsg);

                return message.ToString();
            }
            catch (Exception ex)
            {
                return "Failed to send email: " + ex.Message;
            }
        }

        /// <summary>
        /// Use only to attach pdf as content but not as file.
        /// Used for sending filled PDF on click of final Submit button.
        /// </summary>
        /// <param name="emaildraft"></param>
        /// <returns></returns>
        public string DraftMailAttachPDF(EmailMessage emaildraft, Attachment pdf)
        {
            try
            {
                StringBuilder strSentMail = new StringBuilder();
                MailMessage eMsg = new MailMessage();
                eMsg.From = new MailAddress(emaildraft.From);

                if (emaildraft.To != null)
                {
                    if (!emaildraft.To.Equals(""))
                    {
                        string[] argsTo = emaildraft.To.Split(',');
                        for (int i = 0; i <= argsTo.Length - 1; i++)
                        {
                            eMsg.To.Add(new MailAddress(argsTo[i]));
                            strSentMail.Append(argsTo[i] + "\n");
                        }
                    }
                }

                if (emaildraft.CC != null)
                {
                    if (!emaildraft.CC.Equals(""))
                    {
                        string[] argsCC = emaildraft.CC.Split(',');
                        for (int i = 0; i <= argsCC.Length - 1; i++)
                        {
                            eMsg.To.Add(new MailAddress(argsCC[i]));
                            strSentMail.Append(argsCC[i] + "\n");
                        }
                    }
                }

                if (emaildraft.BCC != null)
                {
                    if (!emaildraft.BCC.Equals(""))
                    {
                        string[] argsBCC = emaildraft.BCC.Split(',');
                        for (int i = 0; i <= argsBCC.Length - 1; i++)
                        {
                            eMsg.Bcc.Add(new MailAddress(argsBCC[i]));
                            strSentMail.Append(argsBCC[i] + "\n");
                        }
                    }
                }

                eMsg.Subject = emaildraft.Subject;
                //MailDefinition meassge = new MailDefinition();

                string BodyFileName = "";
                if (emaildraft.Template != null)
                {
                    if (!emaildraft.Template.Equals("") && !emaildraft.TemplatePath.Equals(""))
                    {
                        //meassge.BodyFileName = emaildraft.TemplatePath + emaildraft.Template + ".html";
                        BodyFileName = emaildraft.TemplatePath + emaildraft.Template + ".html";
                    }
                }

                eMsg.Body = emaildraft.Body;

                //MailMessage msgHtml = meassge.CreateMailMessage(emaildraft.To, emaildraft.MarkerList, new LiteralControl());
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml.Body, null, "text/html");
                string msgHtml = System.IO.File.OpenText(BodyFileName).ReadToEnd();

                foreach (DictionaryEntry de in emaildraft.MarkerList)
                {
                    msgHtml = msgHtml.Replace(de.Key.ToString(), de.Value.ToString());
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(msgHtml, null, "text/html");

                //create the LinkedResource (embedded image)
                LinkedResource logo = new LinkedResource(emaildraft.TemplateLogo, MediaTypeNames.Image.Jpeg);
                logo.ContentId = "companylogo";
                //add the LinkedResource to the appropriate view
                htmlView.LinkedResources.Add(logo);

                eMsg.AlternateViews.Add(htmlView);

                eMsg.Attachments.Add(pdf);

                eMsg.IsBodyHtml = emaildraft.IsBodyHtml;
                MailHelper mailhelper = new MailHelper();
                string message = SendMail(eMsg);
                //MailSettings.Instance.MessageList.Add(eMsg);

                return message.ToString();
            }
            catch (Exception ex)
            {
                return "Failed to send email: " + ex.Message;
            }
        }


        /// <summary>
        /// Static Constructor to set email settings
        /// </summary>
        public MailHelper()
        {
            client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["SMTP_MAIL_SERVER"].ToString();
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"].ToString());
            client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailEnableSSL"].ToString());

            System.Net.NetworkCredential SMTPUserInfo;
            SMTPUserInfo = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FROM_ADDR"].ToString()
                , ConfigurationManager.AppSettings["FROM_ADDR_PASS"].ToString());
            //client.UseDefaultCredentials = true;
            client.Credentials = SMTPUserInfo;
        }


    }
}