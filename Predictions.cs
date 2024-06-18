using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_HTTP_Client.DataBase.Models;

namespace WPF_HTTP_Client
{
    public static class Predictions
    {
        public static PredictionModel SelectedPrediction { get; private set; } = new PredictionModel();

        public delegate void PredictionDelegate(PredictionModel prediction);
        public static event PredictionDelegate SelectedPredictionChanged = new((prediction) => { });

        public static void SetPrediction(PredictionModel prediction)
        {
            SelectedPrediction = prediction;
            SelectedPredictionChanged.Invoke(prediction);
        }
    }
}
