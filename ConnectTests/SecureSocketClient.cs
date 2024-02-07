using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class SecureSocketClient
{
    private TcpClient tcpClient;
    private NetworkStream networkStream;
    private string serverAddress;
    private int serverPort;

    public SecureSocketClient(string address, int port)
    {
        serverAddress = address;
        serverPort = port;
        tcpClient = new TcpClient();
    }

    public async Task<bool> ConnectAsync()
    {
        try
        {
            await tcpClient.ConnectAsync(serverAddress, serverPort);
            networkStream = tcpClient.GetStream();
            Console.WriteLine("Connected to the server.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the server: {ex.Message}");
            return false;
        }
    }

    public async Task<string> SendRequestAsync(string requestData)
    {
        if (!tcpClient.Connected)
        {
            Console.WriteLine("Not connected to the server. Attempting to reconnect...");
            if (!await ConnectAsync())
            {
                return "Connection failed.";
            }
        }

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(requestData);
            await networkStream.WriteAsync(data, 0, data.Length);

            byte[] buffer = new byte[4096];
            int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            string responseData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return responseData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении или отправке запроса: \n{ex.Message}");
            return null;
        }
    }


    public void CloseConnection()
    {
        if (networkStream != null)
            networkStream.Close();
        if (tcpClient != null)
            tcpClient.Close();

        Console.WriteLine("Connection closed.");
    }
}
