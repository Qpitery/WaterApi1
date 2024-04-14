using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WaterApi1.DTO;

namespace WaterApi1.Controllers
{
    public class EmailService
    {
        public static async Task SendEmail(string email, string htmlMessage)
        {
            MailAddress fromAddress = new MailAddress("io3204024@gmail.com", "WaterBrend.ru");
            MailAddress toAddress = new MailAddress(email, "Пользователь");

            using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress))
            {
                mailMessage.Subject = "Восстановление пароля";

                // Создаем альтернативный вид сообщения в формате HTML
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlMessage, null, MediaTypeNames.Text.Html);
                mailMessage.AlternateViews.Add(htmlView);

                // Настройка SMTP клиента и отправка сообщения
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(fromAddress.Address, "rdqu eaud mimf ksii");

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

    }
}



    //   public static async Task SendEmail(string email, string message)
    //    {
    //        try
    //        {

    //            SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");

    //            // set smtp-client with basicAuthentication
    //            mySmtpClient.UseDefaultCredentials = false;
    //            System.Net.NetworkCredential basicAuthenticationInfo = new
    //            System.Net.NetworkCredential("89084574212@mail.ru", "199720052019Zz");
    //            mySmtpClient.Credentials = basicAuthenticationInfo;

    //            // add from,to mailaddresses
    //            MailAddress from = new MailAddress("89084574212@mail.ru", "Ваш пароль для входа");
    //            MailAddress to = new MailAddress(email, message);
    //            MailMessage myMail = new System.Net.Mail.MailMessage(from , to);

    //            // add ReplyTo
    //            MailAddress replyTo = new MailAddress("89084574212@mail.ru");
    //            myMail.ReplyToList.Add(replyTo);

    //            // set subject and encoding
    //            myMail.Subject = "Восстановление пароля";
    //            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

    //            // set body-message and encoding
    //            myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
    //            myMail.BodyEncoding = System.Text.Encoding.UTF8;
    //            // text or html
    //            myMail.IsBodyHtml = false;

    //            mySmtpClient.Send(myMail);
    //        }

    //        catch (SmtpException ex)
    //        {
    //            throw new ApplicationException
    //              ("SmtpException has occured: " + ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}



//vasia.pipk1in.97@mail.ru



 //    var client = new HttpClient();
        //    var url = "https://api.sendsay.ru/general/api/v100/json/";

        //    var data = new Dictionary<string, string>
        //    {
        //        { "action", "issue.send.test" },
        //        { "letter.subject", "Восстановление пароля" },
        //        { "letter.from.name", "Поддержка WaterBrend" },
        //        { "letter.from.email", "89084574212@mail.ru" },
        //        { "sendwhen", "send" },
        //        { "sendtest", email },
        //        { "message", message }
        //    };

        //    // Преобразование словаря в формат JSON
        //    var json = JsonConvert.SerializeObject(data);

        //    // Формирование контента запроса
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    // Добавление ключа API в заголовок запроса
        //    client.DefaultRequestHeaders.Add("X-API-KEY", "19mD7PBSsU7D8T1RdPJtvzKpKqe4XpE2skU9MwX6oDsCJ1N_6nbaI49TQDKHW_5OfhBkP7O9nsGxgIE57");

        //    // Отправка запроса
        //    var response = await client.PostAsync(url, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        // Обработка успешного ответа
        //    }
        //    else
        //    {
        //        // Обработка ошибки
        //    }