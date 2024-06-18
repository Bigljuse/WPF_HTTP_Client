using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_HTTP_Client.Pages
{
    /// <summary>
    /// Interaction logic for PredictionPage.xaml
    /// </summary>
    public partial class PredictionPage : Page
    {
        public PredictionPage()
        {
            InitializeComponent();
            Predictions.SelectedPredictionChanged += Predictions_SelectedPredictionChanged;
        }

        private void Predictions_SelectedPredictionChanged(DataBase.Models.PredictionModel prediction)
        {
            Prediction = Predictions.SelectedPrediction;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Prediction = Predictions.SelectedPrediction;

            if (Prediction.ImagePath == string.Empty)
                return;

            

            var imageData = File.ReadAllBytes(Prediction.ImagePath);
            var memoryStream = new MemoryStream(imageData);
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = memoryStream;
            imageSource.EndInit();
            imageSource.Freeze();
            Image_Viewer.Source = imageSource;
        }
    }
}
