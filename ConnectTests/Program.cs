using Newtonsoft.Json;

namespace ConnectTests
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Введите логин: ");
                string Login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string Password = Console.ReadLine();

                SecureSocketClient client = new SecureSocketClient("127.0.0.1", 8081);
                client.Connect();
                string request = ReturnJson(Login, Password);
                string response = client.SendRequest(request);
                Console.WriteLine($"Server response: \n{response}");
                client.CloseConnection();
                Console.WriteLine("\nНажмите чтобы повторить...");
                Console.ReadLine();
            }
        }
        public static string ReturnJson(string login, string password)
        {
            var jsonStructure = new {
                Login = new[]
                {
                    new
                    {
                        Login = login,
                        Password = password
                    }
                },
                ClientInfo = new[]
                {
                    new
                    {
                        Platform = "Windows",
                        Version = "v0.0.1b"
                    }
                } 
            };
            return JsonConvert.SerializeObject(jsonStructure, Formatting.Indented);
        }
    }
}