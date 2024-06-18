using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WPF_HTTP_Client.DataBase.Models;

namespace WPF_HTTP_Client.HTTP
{
    public partial class Requests
    {
        private HttpClient Client;
    }

    public partial class Requests
    {
        public Requests(HttpClient client)
        {
            Client = client;
        }

        public async Task<bool> CheckServer()
        {
            try
            {
                var result = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Head, Client.BaseAddress));
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
        }

        public async Task<bool> AuthorizeUser(UserModel user)
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Type", "Authorization");

                string json = JsonSerializer.Serialize<UserModel>(user);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, HTTPContentTypes.ApplicationTypes[ApplicationTypes.json]);
                var result = await Client.PostAsync(Client.BaseAddress, httpContent);

                bool checkHeader = ContainsHeaderAndValue(result.Headers, "Type", "Authorization");
                if (checkHeader is false)
                    return false;

                Stream stream = await result.Content.ReadAsStreamAsync();
                ResultModel resultModel = await DeserializeInputStream(stream, new ResultModel());

                if (resultModel.Result == true)
                    return true;

                return false;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
        }

        public async Task<PredictionModel[]> GetPredictions()
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Type", "Predictions");
                var result = await Client.GetAsync(Client.BaseAddress);

                bool checkHeader = ContainsHeaderAndValue(result.Headers, "Type", "Predictions");
                if (checkHeader is false)
                    return new PredictionModel[0];

                Stream stream = await result.Content.ReadAsStreamAsync();
                PredictionModel[] resultModel = await DeserializeInputStream(stream, new PredictionModel[0]);

                return resultModel;
            }
            catch (HttpRequestException ex)
            {
                return new PredictionModel[0];
            }
        }

        public async Task GeneratePredictionImage()
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Type", "Prediction_Generation_Image");
                Client.DefaultRequestHeaders.Add("Type_Stage", "Generating");
                await Client.SendAsync(new HttpRequestMessage(HttpMethod.Head, Client.BaseAddress));
            }
            catch (HttpRequestException ex)
            {
                return;
            }
        }

        public async Task<FileInfo?> GetPredictionImage()
        {
            try
            {
                Client.DefaultRequestHeaders.Add("Type", "Prediction_Generation_Image");
                Client.DefaultRequestHeaders.Add("Type_Stage", "Result");
                var result = await Client.GetAsync(Client.BaseAddress);

                bool checkHeader = ContainsHeaderAndValue(result.Headers, "Type", "Prediction_Generation_Image");
                if (checkHeader is false)
                    return null;

                Stream stream = await result.Content.ReadAsStreamAsync();
                string inputDataString = await ReadInputStream(stream);
                return SaveImage(inputDataString);
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }
    }

    public partial class Requests
    {
        private IEnumerable<string> GetHeaderValues(HttpResponseHeaders headers, string name)
        {
            return headers.FirstOrDefault(x => x.Key == name).Value;
        }

        private string GetHeaderValue(HttpResponseHeaders headers, string name)
        {
            var header = headers.FirstOrDefault(x => x.Key == name).Value;

            if (header is null)
                return string.Empty;

            return header.FirstOrDefault() ?? String.Empty;
        }

        private async Task<string> ReadInputStream(Stream stream)
        {
            string stringResult = "";

            using (var streamReader = new StreamReader(stream))
            {
                stringResult = await streamReader.ReadToEndAsync();
            }

            return stringResult;
        }

        private async Task<T> DeserializeInputStream<T>(Stream stream, T defaultValue) where T : class
        {
            string content = await Task.Run(() => ReadInputStream(stream));
            T result = JsonSerializer.Deserialize<T>(content) ?? defaultValue;

            return result;
        }

        private bool ContainsHeaderAndValue(HttpResponseHeaders headers, string headerName, string Value)
        {
            string headerValue = GetHeaderValue(headers, headerName);
            if (headerValue == Value)
                return true;

            return false;
        }

        private FileInfo SaveImage(string inputDataString)
        {
            var imageData = Convert.FromBase64String(inputDataString);
            File.WriteAllBytes("Image.png", imageData);
            var fileInfo = new FileInfo("Image.png");
            return fileInfo;
        }
    }
}
