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

    public bool Connect()
    {
        try
        {
            tcpClient = new TcpClient(serverAddress, serverPort);
            networkStream = tcpClient.GetStream();
            Console.WriteLine("Подключение к серверу.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения к серверу: \n{ex.Message}.");
            return false;
        }
    }

    public string SendRequest(string requestData)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(requestData);

            networkStream.Write(BitConverter.GetBytes(data.Length), 0, 4); // Отправляем длину данных

            networkStream.Write(data, 0, data.Length);

            // Получение ответа от сервера
            byte[] lengthBuffer = new byte[4];
            networkStream.Read(lengthBuffer, 0, 4);
            int responseDataLength = BitConverter.ToInt32(lengthBuffer, 0);

            byte[] buffer = new byte[responseDataLength];
            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
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
