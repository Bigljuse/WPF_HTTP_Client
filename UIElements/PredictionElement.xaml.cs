using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_HTTP_Client.HTTP;

namespace WPF_HTTP_Client.UIElements
{
    /// <summary>
    /// Interaction logic for PredictionElement.xaml
    /// </summary>
    public partial class PredictionElement : UserControl
    {
        public PredictionElement()
        {
            InitializeComponent();
        }

        private async void Button_SelectPrediction_Click(object sender, RoutedEventArgs e)
        {
            Predictions.SetPrediction(Prediction);

            var client = new HTTPServices().CreateHttpClient("Client", Server.Uri);
            var requests = new Requests(client);
            await requests.GeneratePredictionImage();

            client = new HTTPServices().CreateHttpClient("Client", Server.Uri);
            requests = new Requests(client);
            var imageInfo = await requests.GetPredictionImage();
            Predictions.SelectedPrediction.ImagePath = imageInfo?.FullName ?? String.Empty;
        }
    }
}
