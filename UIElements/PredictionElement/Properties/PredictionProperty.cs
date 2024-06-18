﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using WPF_HTTP_Client.DataBase.Models;

namespace WPF_HTTP_Client.UIElements
{
    public partial class PredictionElement
    {
        public static readonly DependencyProperty PredictionProperty = DependencyProperty.Register(
            nameof(Prediction),
            typeof(PredictionModel),
            typeof(PredictionElement),
            new FrameworkPropertyMetadata(new PredictionModel()));
        public PredictionModel Prediction
        {
            get { return (PredictionModel)GetValue(PredictionProperty); }
            set { SetValue(PredictionProperty, value); }
        }
    }
}
