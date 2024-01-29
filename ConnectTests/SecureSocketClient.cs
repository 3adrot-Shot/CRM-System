using System.Net.Sockets;
using System.Text;

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
    }

    public void Connect()
    {
        try
        {
            tcpClient = new TcpClient(serverAddress, serverPort);
            networkStream = tcpClient.GetStream();
            Console.WriteLine("Connected to the server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the server: {ex.Message}");
        }
    }

    public string SendRequest(string requestData)
    {
        try
        {
            // Преобразование строки в байты для отправки
            byte[] data = Encoding.UTF8.GetBytes(requestData);

            // Отправка данных на сервер
            networkStream.Write(data, 0, data.Length);

            // Получение ответа от сервера
            byte[] buffer = new byte[4096];
            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string responseData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return responseData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending request: {ex.Message}");
            return null;
        }
    }

    public void CloseConnection()
    {
        try
        {
            // Закрытие соединения
            networkStream.Close();
            tcpClient.Close();
            Console.WriteLine("Connection closed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error closing connection: {ex.Message}");
        }
    }
}
