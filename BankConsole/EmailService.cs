using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService{
    public static void SendMail(){

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Angel Manzo", "angel.manzo.rosas@gmail.com"));
        message.To.Add(new MailboxAddress("Angel Manzo Admin", "angelmanzotecno4@gmail.com"));
        message.Subject = "BankConsole: Usuarios Nuevos";

        message.Body = new TextPart("plain") {
            Text = GetEmailText()
        };

        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, false);
        client.Authenticate("Correo electronico", "Contraseña de 16 caracteres");
        client.Send(message);
        client.Disconnect(true);

    }

    private static string GetEmailText(){
        List<User> newUsers = Storage.GetListOfNewUsers();

        if (newUsers == null || newUsers.Count == 0){
            return "No hay usuarios nuevos.";
        }

        string emailText = "Usuarios agregados ahora:\n";

        foreach (User user in newUsers){
            emailText += $"{user.ShowData()}\n"; 
        }

        return emailText;
    }
    
}