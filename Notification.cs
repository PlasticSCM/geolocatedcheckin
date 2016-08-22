using System.Windows.Forms;

namespace geolocation
{
    static class Notification
    {
        static internal void Show(string title, string text)
        {
            var form = new NotificationForm(title, text);

            form.Icon = geolocation.App;

            Application.Run(form);
        }
    }
}
