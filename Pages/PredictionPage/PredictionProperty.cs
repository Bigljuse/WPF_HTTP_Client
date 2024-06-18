using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_HTTP_Client.DataBase.Models;
using WPF_HTTP_Client.UIElements;

namespace WPF_HTTP_Client.Pages
{
    public partial class PredictionPage
    {
        public static readonly DependencyProperty PredictionProperty = DependencyProperty.Register(
            nameof(Prediction),
            typeof(PredictionModel),
            typeof(PredictionPage),
            new FrameworkPropertyMetadata(new PredictionModel()));

        public PredictionModel Prediction
        {
            get { return (PredictionModel)GetValue(PredictionProperty); }
            set { SetValue(PredictionProperty, value); }
        }
    }
}
