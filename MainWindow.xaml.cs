using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WPF_HTTP_Client.HTTP;
using WPF_HTTP_Client.Pages;

namespace WPF_HTTP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthorizationPage p_Page_Authorization;
        private PredictionPage p_Page_Prediction;
        private PredictionsPage p_Page_Predictions;

        public MainWindow()
        {
            InitializeComponent();

            Server.StatusChanged += Server_StatusChanged;
        }

        private void Button_Authorize_Click(object sender, RoutedEventArgs e)
        {
            if (p_Page_Authorization is null)
                p_Page_Authorization = new AuthorizationPage();

            Frame_Main.Navigate(p_Page_Authorization);
        }

        private void Button_Prediction_Click(object sender, RoutedEventArgs e)
        {
            if (p_Page_Prediction is null)
                p_Page_Prediction = new PredictionPage();

            Frame_Main.Navigate(p_Page_Prediction);
        }

        private void Button_Predictions_Click(object sender, RoutedEventArgs e)
        {
            if (p_Page_Predictions is null)
                p_Page_Predictions = new PredictionsPage();

            Frame_Main.Navigate(p_Page_Predictions);
        }

        private async void Toggle_ConnectionCheckState_Checked(object sender, RoutedEventArgs e)
        {
            string ip = TextBlock_ServerIp.Text;
            string port = TextBlock_ServerPort.Text;

            Server.SetServer(ip, port);
            await Server.CheckServerConnection();

            while (Server.Status == ServerStatus.Connected && Toggle_ConnectionCheckState.IsChecked == true)
            {
                await Task.Delay(500);
                await Server.CheckServerConnection();
            }

            SetRectangleStatusFill(ServerStatus.Off);
            Toggle_ConnectionCheckState.IsChecked = false;
            Toggle_ConnectionCheckState_Unchecked(sender, e);
        }

        private void Toggle_ConnectionCheckState_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }

    public partial class MainWindow
    {
        private void SetRectangleStatusFill(ServerStatus status)
        {
            if (status == ServerStatus.Off)
                Rectangle_ServerConnectionStatus.Fill = Brushes.Gray;
            if (status == ServerStatus.Connected)
                Rectangle_ServerConnectionStatus.Fill = Brushes.Green;
        }

        private void Server_StatusChanged(ServerStatus status)
        {
            SetRectangleStatusFill(status);
        }
    }

    public partial class MainWindow
    {

    }
}