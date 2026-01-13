using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MailController
{
    public static void SendMails(List<Mail> mailList)
    {
        DocumentManager.instance.AddMails(mailList);
    }
}
