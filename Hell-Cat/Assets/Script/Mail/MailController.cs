using UnityEngine;

public class MailController : MonoBehaviour
{
    public static MailController  instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SendMail(Mail mail)
    {
        
    }
}
