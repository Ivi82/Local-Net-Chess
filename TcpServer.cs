using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class TcpServer
    {


        byte[] data = new byte[256];
        int myPort;// = 11000;
        int enemyPort;// = 8005;
        TcpListener server;

        TcpClient client;


        IPAddress myIpAddr;
        IPAddress enemyIpAddr;
        NetworkStream stream;

        public string message = "";

        public event Delegate ReceivedDataFromOpponentClient;



        public TcpServer()
        {

            string[] path_from_file = ReadFile.ReadConf();

            myIpAddr = IPAddress.Parse(path_from_file[0]);
            myPort = int.Parse(path_from_file[1]);

            enemyIpAddr = IPAddress.Parse(path_from_file[2]);
            enemyPort = int.Parse(path_from_file[3]);


            try
            {

                // localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(myIpAddr, myPort);

                server.Start(); // запуск слушателя


                MethodAcceptClientAsync();

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }




        async void MethodAcceptClientAsync()
        {


            while (true)
            {

                // Асинхронно ожидаем входящее подключение        
                await Task.Run(() => client = server.AcceptTcpClient());

                StringBuilder response = new StringBuilder();

                // Получаем сетевой поток для чтения и записи
                stream = client.GetStream();

                byte[] data = new byte[256];

                // Читаем сообщение от клиента
                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }

                while (stream.DataAvailable); // Пока данные есть в потоке               

                message = response.ToString();

                stream.Close(); // Закрываем поток

                client.Close(); // Закрываем подключение


                if (message == "Stop!") break; // Выходим из цикла, чтобы не подвиснуть на server.AcceptTcpClient()

                ReceivedDataFromOpponentClient(); // Создаем событие получения данных от противника

            }



            ReceivedDataFromOpponentClient(); // Создаем событие получения данных от противника


        }



        public void SendToMySelf(string message)
        {

            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", myPort);

            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            NetworkStream stream = client.GetStream();

            data = System.Text.Encoding.UTF8.GetBytes(message);

            stream.Write(data, 0, data.Length);

            // Закрываем потоки
            stream.Close();
            client.Close();

        }


        // Метод отправляет сообщение противнику
        public void Send(string message)
        {

            TcpClient client = new TcpClient();
            client.Connect(enemyIpAddr, enemyPort);

            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            NetworkStream stream = client.GetStream();

            data = System.Text.Encoding.UTF8.GetBytes(message);

            stream.Write(data, 0, data.Length);

            // Закрываем потоки
            stream.Close();
            client.Close();  

        }


        public void StopServer()
        {

            server.Stop();

        }


        //~TcpServer()
        //{

        //    MessageBox.Show("TcpServer CLOSE");

        //}

    }

}