using System.Timers;

namespace WPF_HTTP_Client.HTTP
{
    public enum ServerStatus
    {
        Off,
        Connected
    }

    public static partial class Server
    {
        public static string Ip { get; private set; } = "";

        public static string Port { get; private set; } = "";

        public static Uri Uri { get; private set; } = new Uri("http://127.0.0.1:8888/");

        public static ServerStatus Status { get; private set; } = ServerStatus.Off;

        public delegate void StatusDelegate(ServerStatus status);
        public static event StatusDelegate StatusChanged = new((status) => { });
    }

    public static partial class Server
    {
        public static void SetServer(string ip, string port)
        {
            Ip = ip;
            Port = port;
            Uri = new Uri($"http://{Ip}:{Port}/");
        }

        public static async Task<ServerStatus> CheckServerConnection()
        {
            var client = new HTTPServices().CreateHttpClient("Client", Server.Uri);
            var requests = new Requests(client);
            var serverStatus = await requests.CheckServer();

            if (serverStatus is true)
            {
                Status = ServerStatus.Connected;
                StatusChanged.Invoke(ServerStatus.Connected);
            }
            else
            {
                Status = ServerStatus.Off;
                StatusChanged.Invoke(ServerStatus.Off);
            }

            return Status;
        }
    }

    public static partial class Server
    {
    }
}
