using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail; 

/// <summary>
///EmailUtil 的摘要说明
/// </summary>
public class EmailUtil
{
	public EmailUtil()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static void SendEmail(String email, String content)
    {
        MailMessage message;
        
        message = new MailMessage();
        message.From = new MailAddress("support@ccpn.gov.cn", "京价网");
        message.To.Add(new MailAddress(email));
        message.Subject = "京价网密码重置通知";
        message.Body = content;
        message.IsBodyHtml = true;
        
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.Host = "localhost";
        client.Send(message);
    }
}