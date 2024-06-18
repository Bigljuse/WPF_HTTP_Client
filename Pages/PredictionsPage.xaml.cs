using System.Windows.Controls;
using WPF_HTTP_Client.DataBase.Models;
using WPF_HTTP_Client.HTTP;
using WPF_HTTP_Client.UIElements;

namespace WPF_HTTP_Client.Pages
{
    /// <summary>
    /// Interaction logic for PredictionsPage.xaml
    /// </summary>
    public partial class PredictionsPage : Page
    {
        public PredictionsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadPredictions();
        }
    }

    public partial class PredictionsPage
    {
        private async void LoadPredictions()
        {
            StackPanel_Predictions.Children.Clear();

            var client = new HTTPServices().CreateHttpClient("Client", Server.Uri);
            var requests = new Requests(client);
            PredictionModel[] predictions = await requests.GetPredictions();

            foreach(var prediction in predictions)
            {
                StackPanel_Predictions.Children.Add(CreatePredictionElement(prediction));
            }
        }

        private PredictionElement CreatePredictionElement(PredictionModel prediction)
        {
            PredictionElement predictionElement = new();
            predictionElement.Prediction = prediction;
            return predictionElement;
        }
    }
}
