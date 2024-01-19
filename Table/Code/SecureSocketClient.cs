using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Table.Code
{
    class SecureSocketClient
    {
        private TcpClient tcpClient;

        public event Action<string> MessageReceived;

        public event Action Connected;

        public SecureSocketClient()
        {
            tcpClient = new TcpClient();
        }

        public async Task ConnectAsync(string ipAddress, int port)
        {
            try
            {
                await tcpClient.ConnectAsync(ipAddress, port);
                StartListening();
            }
            catch (Exception ex)
            {
                HandleConnectionError(ex.Message);
            }
        }

        private async void StartListening()
        {
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] buffer = new byte[4096];

                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        MessageReceived?.Invoke(message);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleConnectionError(ex.Message);
            }
        }

        public async Task SendDataAsync(object data)
        {
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                string jsonData = JsonConvert.SerializeObject(data);
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                await stream.WriteAsync(dataBytes, 0, dataBytes.Length);
            }
            catch (Exception ex)
            {
                HandleConnectionError(ex.Message);
            }
        }

        private void HandleConnectionError(string errorMessage)
        {
            Debug.Print($"Error connecting to the server: {errorMessage}");
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}
