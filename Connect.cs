namespace Chess
{

    public class Connect
    {
        TcpServer server;
        string myChoice;
        string opponentChoice = "0 0";

        // Метод принимает сообщение от Противника в виде строки    
        void Receive()
        {

            if (server.message == "Stop!") StartGame();
            opponentChoice = server.message;

        }


        internal void Ok(string mes)
        {
            myChoice = mes;

            if (myChoice == "0 0")
            {
                System.Windows.MessageBox.Show("Вы не выбрали фигуры!");

            }


            else
            {
                // Если противник уже выбрал себе фигуры и послал сообщение о своем выборе
                if (opponentChoice != "0 0")
                {
                    // Если выбранные фигуры у игрока и противника различаются по цвету
                    if (opponentChoice != myChoice)
                    {
                        server.Send("Stop!"); // Посылаем сообщение противнику о согласии играть
                        server.SendToMySelf("Stop!");

                    }

                    // Если выбранные фигуры у игрока и противника совпадают по цвету
                    if (opponentChoice == myChoice) System.Windows.MessageBox.Show("Совпадение цветов! Выбирайте снова!");
                }

                else


                {
                    server.Send(myChoice); // Посылаем сообщение противнику - какими фигурами хотим играть
                    System.Windows.MessageBox.Show("Противник еще не выбрал фигуры! ");
                }

            }



        }

        void StartGame()
        {

            server.ReceivedDataFromOpponentClient -= Receive; // Отписываемся от события доставки сообщения
            server.StopServer(); // Передаем серверу команду остановки


            string selectColorFigure;

            selectColorFigure = myChoice == "1 0" ? "White" : "Black";

            // Метод StartNewGame(selectColorFigure) создает событие  EventStartNewGame (x)
            // на которое подписан класс BoardWindowVM - он запускает метод StartNewGame(int selectColorFigure) - новую игру
            Events.StartNewGame(selectColorFigure);

        }


        public Connect()
        {

            server = new TcpServer();
            server.ReceivedDataFromOpponentClient += Receive; // Подписываемся на событие доставки сообщения


        }


        //~Connect()
        //{
        //    System.Windows.MessageBox.Show("Connect close");

        //}





    }
}
