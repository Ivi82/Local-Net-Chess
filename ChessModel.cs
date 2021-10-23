using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace Chess
{



    public class ChessModel
    {

        //~ChessModel()
        //{
        //    MessageBox.Show("ChessModel close");
        //}




        internal Figure bp1, bp2, bp3, bp4, bp5, bp6, bp7, bp8, bh1, bh2, bb1, bb2, br1, br2, bk, bq;
        internal Figure wp1, wp2, wp3, wp4, wp5, wp6, wp7, wp8, wh1, wh2, wb1, wb2, wr1, wr2, wk, wq;

        internal ObservableCollection<Figure> Figures = new ObservableCollection<Figure>();
        internal Figure SelectedFigure;

        internal delegate void Delegate();

        internal event Delegate EventNextStep;
        internal event Delegate FigureYesNoSelected;
        internal event Delegate EventEndModelGame;

        internal string activeColor;



        internal string selectedColor;

        internal TcpServer server;



        // Метод возвращает номер фигуры в коллекции Figures по заданным координатам
        // или возвращает 999 если фигуры по этим координатам нет
        internal static int GetNumberFigure(ObservableCollection<Figure> Figures, int sqx, int sqy)
        {

            string a = sqx.ToString() + sqy.ToString();

            for (int x = 0; x < Figures.Count; x++)
            {
                //Если совпадают координаты клетки и фигуры 
                if (Figures[x].x.ToString() + Figures[x].y.ToString() == a) return x;
            }

            return 999;

        }

        // Метод возвращает номер фигуры в коллекции Figures если известно ее имя
        internal static int GetNumberFigure(ObservableCollection<Figure> Figures, string name)
        {

            int r = 0;

            for (int x = 0; x < Figures.Count; x++)
            {
                if (Figures[x].Name == name) r = x;
            }

            return r;

        }



        // Метод возвращает список listOfDangerFigures ("опасных фигур") которые ставят королю шах
        // если таких фигур нет - Count этого списка = 0
        internal static List<Figure> ListOfDangerFigures(ObservableCollection<Figure> Figures, string activeColor)
        {

            // Зная цвет короля - activeColor, находим номер короля в коллекции по его имени	
            int numberOfKing = activeColor == "White" ? ChessModel.GetNumberFigure(Figures, "WhiteKing") : ChessModel.GetNumberFigure(Figures, "BlackKing");

            List<Figure> listOfDangerFigures = new List<Figure>();

            // Проверка для всех фигур противника в цикле
            for (int i = 0; i < Figures.Count; i++)
            {

                if (Figures[i].Color != Figures[numberOfKing].Color && Figures[i].IsStepOn(Figures, Figures[numberOfKing].x, Figures[numberOfKing].y, "Attack") == true)

                {
                    listOfDangerFigures.Add(Figures[i]);
                }

            }

            return listOfDangerFigures;

        }


        // Метод обрабатывает клик по фигуре
        internal void Click(string name)
        {
            // Узнаем номер фигуры через ее имя
            int numberFigure = ChessModel.GetNumberFigure(Figures, name);

            // Если фигура своя...цвет фигуры должен совпадать с цветом игрока, который делает ход 
            if (name.StartsWith(activeColor))
            {
                //InformationMessage.Set("");
                SelectedFigure = Figures[numberFigure]; //Назначаем выбранную фигуру
                FigureYesNoSelected(); //Создаем событие подсветки выбранной выгуры	 
            }

            // А если это фигура противника...и своя фигура выделена - просчет хода к фигуре
            if (activeColor != Figures[numberFigure].Color && SelectedFigure != null)
            {
                if (SelectedFigure.Attack(Figures, numberFigure, "Active") == true) NextPlayer("Attack");
            }


        }


        // Метод обрабатывает клик по доске x,y - координаты клетки
        internal void Click(int x, int y)
        {

            int numberFigure = GetNumberFigure(Figures, x, y); //Узнаем, номер фигуры по координатам клетки	


            // Если фигура есть, т.е. результат не равен 999
            if (numberFigure != 999)
            {
                // Если фигура своя...цвет фигуры должен совпадать с цветом игрока, который делает ход
                if (Figures[numberFigure].Name.StartsWith(activeColor))
                {

                    SelectedFigure = Figures[numberFigure]; //Назначаем выбранную фигуру
                    FigureYesNoSelected(); //Создаем событие подсветки выбранной выгуры     	

                }

                // А если это фигура противника...и своя фигура выделена - просчет хода к фигуре
                if (activeColor != Figures[numberFigure].Color && SelectedFigure != null)

                {
                    if (SelectedFigure.Attack(Figures, numberFigure, "Active") == true) NextPlayer("Attack");
                }
            }

            else // Если фигуры нет - т.е. результат равен 999

     // Если выделена фигура и она своя - просчитываем ход	 к клетке
     if (SelectedFigure != null && SelectedFigure.Color == activeColor)

            {

                if (SelectedFigure.Step(Figures, x, y, "Active") == true) NextPlayer("Step");

            }

        }


        internal void RemoveFigure()
        {

            for (int i = 0; i < Figures.Count; i++)
            {
                if (Figures[i].Icon == null) Figures.RemoveAt(i);
            }

        }



        internal void NextPlayer(string Type_of_step)
        {

            InformationMessage.Set("");
            string message = Type_of_step + " " + SelectedFigure.Name + " " + SelectedFigure.x + " " + SelectedFigure.y;

            SelectedFigure = null; //Обнуляем выбранную фигуру									            
            FigureYesNoSelected(); //Создаем событие отмены подсветки выбранной фигуры

            activeColor = "Nothing";
            EventNextStep(); //Создаем событие завершения хода

            server.Send(message);

        }

        // Метод получает данные хода противника уже проверенные!!!
        // методом CheckStep противника  и отображает результат

        void Receive()
        {

            // Получаем сообщение от Противника в виде строки
            string message = server.message;

            // Делим строку на части. пробел - разделитель
            string[] words = server.message.Split(new char[] { ' ' });

            if (server.message == "Save!")

            {

                SaveFile.SaveGame(Figures, selectedColor, activeColor);
                InformationMessage.Set("Игра сохранена");
                return;
            }


            if (server.message == "Stop!")

            {

                GameOver();
                return;
            }


            if (message == "МАТ КОРОЛЮ ПРОТИВНИКА! ВЫ ВЫИГРАЛИ!") { Win(); return; }

            else if (words[0] == "!") InformationMessage.Set(message);

            else

            {
                InformationMessage.Set("");

                int number = ChessModel.GetNumberFigure(Figures, words[1]);

                // Первое слово в строке полученной по сети отображает специфику хода противника
                // Step - просто ход на другую клетку, Attack - атака фигуры

                switch (words[0])
                {

                    case "Step":

                        // Присваиваем фигуре противника новые координаты - те которые получены по сети
                        Figures[number].x = Int32.Parse(words[2]);
                        Figures[number].y = Int32.Parse(words[3]);

                        break;

                    case "Attack":

                        // Если была атака - нужно знать номер фигуры которую атаковали
                        // ищем эту номер этой фигуры по координатам

                        int enemyNumber = 0;

                        for (int i = 0; i < Figures.Count; i++)
                        {
                            if (Int32.Parse(words[2]) == Figures[i].x && Int32.Parse(words[3]) == Figures[i].y) enemyNumber = i;
                        }

                        // Присваиваем фигуре противника новые координаты - те которые получены по сети	
                        Figures[number].x = Int32.Parse(words[2]);
                        Figures[number].y = Int32.Parse(words[3]);

                        // Удаляем фигуру
                        Figures[enemyNumber].Icon = null;

                        break;

                }




                activeColor = selectedColor; // Переходим на свой цвет и...

                EventNextStep(); // Создаем событие завершения хода (перерисовка фигур)

                // Проверяем своего короля на шах и мат


                switch (Check.IsShahForKing(Figures, activeColor))
                {

                    case 0:
                        break;

                    case 1:

                        if (Check.IsCheckMate(Figures, activeColor) == true) Loss();

                        else
                        {

                            InformationMessage.Set("! Ваш Король под шахом !");
                            server.Send("! Король противника под шахом !"); // Посылаем сообщение противнику

                        }

                        break;

                    case 2:

                        InformationMessage.Set("! Ваш Король под шахом !");
                        server.Send("! Король противника под шахом !"); // Посылаем сообщение противнику

                        break;

                }



            }
        }






        void GameOver()

        {
            InformationMessage.Set("Игра окончена");

            server.ReceivedDataFromOpponentClient -= Receive;
            server.StopServer(); // Передаем серверу команду остановки           
            EventEndModelGame();

        }


        void Win()
        {

            InformationMessage.Set("МАТ КОРОЛЮ ПРОТИВНИКА! ВЫ ВЫИГРАЛИ!");
            MessageBox.Show("МАТ КОРОЛЮ ПРОТИВНИКА! ВЫ ВЫИГРАЛИ!");
            server.SendToMySelf("Stop!");

        }


        void Loss()
        {

            // Посылаем сообщение противнику о его Победе
            server.Send("МАТ КОРОЛЮ ПРОТИВНИКА! ВЫ ВЫИГРАЛИ!");
            InformationMessage.Set("МАТ ВАШЕМУ КОРОЛЮ! ВЫ ПРОИГРАЛИ!");
            MessageBox.Show("МАТ ВАШЕМУ КОРОЛЮ! ВЫ ПРОИГРАЛИ!");
            server.SendToMySelf("Stop!");

        }


        // Конструктор модели. 
        // Если параметр  colorOrLoad есть "load" - загружается сохраненная игра из файла save.cfg
        // Если параметр  colorOrLoad есть "White" или "Black" - начинается новая игра
        public ChessModel(string colorOrLoad)
        {

            if (colorOrLoad != "load")

            {

                selectedColor = colorOrLoad;

                activeColor = selectedColor == "White" ? "White" : "Nothing";

                Figures.Add(bp1 = new Pawn("Pawn", 0, 1, "Black", "BlackPawn1", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));

                Figures.Add(bp2 = new Pawn("Pawn", 1, 1, "Black", "BlackPawn2", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));

                bp3 = new Pawn("Pawn", 2, 1, "Black", "BlackPawn3", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp3);

                bp4 = new Pawn("Pawn", 3, 1, "Black", "BlackPawn4", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp4);

                bp5 = new Pawn("Pawn", 4, 1, "Black", "BlackPawn5", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp5);

                bp6 = new Pawn("Pawn", 5, 1, "Black", "BlackPawn6", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp6);

                bp7 = new Pawn("Pawn", 6, 1, "Black", "BlackPawn7", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp7);

                bp8 = new Pawn("Pawn", 7, 1, "Black", "BlackPawn8", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bp8);

                bh1 = new Horse("Horse", 1, 0, "Black", "BlackHorse1", BitmapFrame.Create(new Uri("bhorse80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bh1);

                bh2 = new Horse("Horse", 6, 0, "Black", "BlackHorse2", BitmapFrame.Create(new Uri("bhorse80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bh2);

                br1 = new Rook("Rook", 0, 0, "Black", "BlackRook1", BitmapFrame.Create(new Uri("brook80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(br1);

                br2 = new Rook("Rook", 7, 0, "Black", "BlackRook2", BitmapFrame.Create(new Uri("brook80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(br2);

                bb1 = new Bishop("Bishop", 2, 0, "Black", "BlackBishop1", BitmapFrame.Create(new Uri("bbishop80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bb1);

                bb2 = new Bishop("Bishop", 5, 0, "Black", "BlackBishop2", BitmapFrame.Create(new Uri("bbishop80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bb2);

                bq = new Queen("Queen", 4, 0, "Black", "BlackQueen", BitmapFrame.Create(new Uri("bqueen80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bq);

                bk = new King("King", 3, 0, "Black", "BlackKing", BitmapFrame.Create(new Uri("bking80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(bk);


                wp1 = new Pawn("Pawn", 0, 6, "White", "WhitePawn1", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp1);

                wp2 = new Pawn("Pawn", 1, 6, "White", "WhitePawn2", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp2);

                wp3 = new Pawn("Pawn", 2, 6, "White", "WhitePawn3", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp3);

                wp4 = new Pawn("Pawn", 3, 6, "White", "WhitePawn4", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp4);

                wp5 = new Pawn("Pawn", 4, 6, "White", "WhitePawn5", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp5);

                wp6 = new Pawn("Pawn", 5, 6, "White", "WhitePawn6", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp6);

                wp7 = new Pawn("Pawn", 6, 6, "White", "WhitePawn7", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp7);

                wp8 = new Pawn("Pawn", 7, 6, "White", "WhitePawn8", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wp8);

                wh1 = new Horse("Horse", 1, 7, "White", "WhiteHorse1", BitmapFrame.Create(new Uri("whorse80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wh1);

                wh2 = new Horse("Horse", 6, 7, "White", "WhiteHorse2", BitmapFrame.Create(new Uri("whorse80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wh2);

                wr1 = new Rook("Rook", 0, 7, "White", "WhiteRook1", BitmapFrame.Create(new Uri("wrook80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wr1);

                wr2 = new Rook("Rook", 7, 7, "White", "WhiteRook2", BitmapFrame.Create(new Uri("wrook80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wr2);

                wb1 = new Bishop("Bishop", 2, 7, "White", "WhiteBishop1", BitmapFrame.Create(new Uri("wbishop80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wb1);

                wb2 = new Bishop("Bishop", 5, 7, "White", "WhiteBishop2", BitmapFrame.Create(new Uri("wbishop80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wb2);

                wq = new Queen("Queen", 4, 7, "White", "WhiteQueen", BitmapFrame.Create(new Uri("wqueen80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wq);

                wk = new King("King", 3, 7, "White", "WhiteKing", BitmapFrame.Create(new Uri("wking80.png", UriKind.RelativeOrAbsolute)));
                Figures.Add(wk);


            }

            else
            {

                // Данные сохраненной игры помещаются в массив
                string[] ff = ReadFile.ReadGame();

                selectedColor = ff[1];
                activeColor = ff[2];

                for (int x = 2; x < ff.Length; x++)
                {

                    switch (ff[x])

                    {

                        case "BlackPawn1":
                            Figures.Add(bp1 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn1", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn2":
                            Figures.Add(bp2 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn2", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn3":
                            Figures.Add(bp3 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn3", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn4":
                            Figures.Add(bp4 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn4", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn5":
                            Figures.Add(bp5 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn5", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn6":
                            Figures.Add(bp6 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn6", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn7":
                            Figures.Add(bp7 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn7", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackPawn8":
                            Figures.Add(bp8 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackPawn8", BitmapFrame.Create(new Uri("bpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackHorse1":
                            Figures.Add(bh1 = new Horse("Horse", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackHorse1", BitmapFrame.Create(new Uri("bhorse80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackHorse2":
                            Figures.Add(bh2 = new Horse("Horse", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackHorse2", BitmapFrame.Create(new Uri("bhorse80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackRook1":
                            Figures.Add(br1 = new Rook("Rook", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackRook1", BitmapFrame.Create(new Uri("brook80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackRook2":
                            Figures.Add(br2 = new Rook("Rook", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackRook2", BitmapFrame.Create(new Uri("brook80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackBishop1":
                            Figures.Add(bb1 = new Bishop("Bishop", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackBishop1", BitmapFrame.Create(new Uri("bbishop80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackBishop2":
                            Figures.Add(bb2 = new Bishop("Bishop", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackBishop2", BitmapFrame.Create(new Uri("bbishop80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackQueen":
                            Figures.Add(bq = new Queen("Queen", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackQueen", BitmapFrame.Create(new Uri("bqueen80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "BlackKing":
                            Figures.Add(bk = new King("King", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "Black", "BlackKing", BitmapFrame.Create(new Uri("bking80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn1":
                            Figures.Add(wp1 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn1", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn2":
                            Figures.Add(wp2 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn2", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn3":
                            Figures.Add(wp3 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn3", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn4":
                            Figures.Add(wp4 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn4", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn5":
                            Figures.Add(wp5 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn5", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn6":
                            Figures.Add(wp6 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn6", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn7":
                            Figures.Add(wp7 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn7", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhitePawn8":
                            Figures.Add(wp8 = new Pawn("Pawn", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhitePawn8", BitmapFrame.Create(new Uri("wpawn80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteHorse1":
                            Figures.Add(wh1 = new Horse("Horse", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteHorse1", BitmapFrame.Create(new Uri("whorse80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteHorse2":
                            Figures.Add(wh2 = new Horse("Horse", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteHorse2", BitmapFrame.Create(new Uri("whorse80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteRook1":
                            Figures.Add(wr1 = new Rook("Rook", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteRook1", BitmapFrame.Create(new Uri("wrook80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteRook2":
                            Figures.Add(wr2 = new Rook("Rook", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteRook2", BitmapFrame.Create(new Uri("wrook80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteBishop1":
                            Figures.Add(wb1 = new Bishop("Bishop", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteBishop1", BitmapFrame.Create(new Uri("wbishop80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteBishop2":
                            Figures.Add(wb2 = new Bishop("Bishop", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteBishop2", BitmapFrame.Create(new Uri("wbishop80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteQueen":
                            Figures.Add(wq = new Queen("Queen", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteQueen", BitmapFrame.Create(new Uri("wqueen80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                        case "WhiteKing":
                            Figures.Add(wk = new King("King", Int32.Parse(ff[x + 1]), Int32.Parse(ff[x + 2]), "White", "WhiteKing", BitmapFrame.Create(new Uri("wking80.png", UriKind.RelativeOrAbsolute))));
                            x = x + 2;
                            break;

                    }


                }


            }

            server = new TcpServer();

            server.ReceivedDataFromOpponentClient += Receive;


        }
    }
}

