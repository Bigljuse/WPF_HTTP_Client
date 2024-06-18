using System.Windows;
using System.Windows.Controls;
using WPF_HTTP_Client.DataBase.Models;
using WPF_HTTP_Client.HTTP;

namespace WPF_HTTP_Client.Pages
{
    public enum AuthorizationStatus
    {
        Off,
        Success,
        Failed
    }

    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void Button_Authorize_Click(object sender, RoutedEventArgs e)
        {
            ChangeAuthorizationStatusVisibility(AuthorizationStatus.Off);

            UserModel user = new();
            user.Login = TextBox_Login.Text;
            user.Password = PasswordBox_Password.Password;

            var client = new HTTPServices().CreateHttpClient("Client", Server.Uri);
            var requests = new Requests(client);
            bool status = await requests.AuthorizeUser(user);

            ChangeAuthorizationStatusVisibility(status);
        }
    }
    public partial class AuthorizationPage
    {
        private void ChangeAuthorizationStatusVisibility(bool status)
        {
            if (status == true)
                ChangeAuthorizationStatusVisibility(AuthorizationStatus.Success);
            else
                ChangeAuthorizationStatusVisibility(AuthorizationStatus.Failed);
        }

        private void ChangeAuthorizationStatusVisibility(AuthorizationStatus status)
        {
            if (status == AuthorizationStatus.Success)
            {
                Rectangle_AuthorizationOK.Visibility = Visibility.Visible;
                Rectangle_AuthorizationFailed.Visibility = Visibility.Collapsed;
            }
            else if (status == AuthorizationStatus.Failed)
            {
                Rectangle_AuthorizationOK.Visibility = Visibility.Collapsed;
                Rectangle_AuthorizationFailed.Visibility = Visibility.Visible;
            }
            else
            {
                Rectangle_AuthorizationOK.Visibility = Visibility.Collapsed;
                Rectangle_AuthorizationFailed.Visibility = Visibility.Collapsed;
            }
        }
    }
}
