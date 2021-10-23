using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Horse : Figure
    {

        internal Horse(string type, int x, int y, string color, string name, BitmapFrame icon)

        {

            Type = type;
            this.x = x;
            this.y = y;
            Icon = icon;
            Name = name;
            Color = color;
            PropNameForX = Name + "_X"; //"BlackPawn1_X";
            PropNameForY = Name + "_Y";
        }




        internal override bool Step(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {

            //MessageBox.Show(" x клетки= " + x + " y клетки= " + y + " " + this.x + " " + this.y);


            if (IsStepOn(Figures, x, y, methodMode) == true)
            {

                int tempX = this.x; //Присваиваем текущие координаты фигуры временным переменным
                int tempY = this.y;

                this.x = x; // Присваиваем новые координаты своей фигуре
                this.y = y;

                // Проверка на шах
                // Если при новых x и y король своего цвета не находится под шахом - завершаем ход
                // т.е. возвращаем true - что считается корректным завершением хода

                if (ChessModel.ListOfDangerFigures(Figures, this.Color).Count == 0)
                {
                    if (methodMode == "Active") return true;

                    if (methodMode == "Check")
                    {
                        this.x = tempX; //Возвращаем предыдущие координаты фигуры 
                        this.y = tempY;
                        return true;
                    }

                }

                this.x = tempX; //Возвращаем предыдущие координаты фигуры - отмена хода, т.к. король попадает под шах
                this.y = tempY;
                return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);


            }
            return FalseAndMessage("Конь не может ходить по такой траектории!", methodMode);

        }



        internal override bool Attack(ObservableCollection<Figure> Figures, int number, string methodMode)
        {

            if (Figures[number].Type == "King") return FalseAndMessage("Нельзя атаковать короля!", methodMode);

            if (IsStepOn(Figures, Figures[number].x, Figures[number].y, methodMode) == true)
            {

                int tempX = this.x; // Присваиваем текущие координаты фигуры временным переменным
                int tempY = this.y;

                this.x = Figures[number].x;  // Присваиваем новые координаты
                this.y = Figures[number].y;  // (ранее - координаты противника) своей фигуре

                // Проверка на шах
                // Метод ChessModel.IsShahForKing возвращает список listOfDangerFigures "опасных фигур" 
                // которые угрожают королю шахом на данный момент.

                List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, this.Color);

                // Если количество опасных фигур == 0 , т.е. нет шаха Королю - Можно атаковать
                if (listOfDangerFigures.Count == 0)
                {

                    if (methodMode == "Active")
                    {
                        Figures[number].Icon = null;
                        return true;
                    }

                    if (methodMode == "Check")
                    {
                        this.x = tempX; //Возвращаем предыдущие координаты фигуры
                        this.y = tempY;
                        return true;
                    }

                }


                // Если количество опасных фигур == 1 , т.е. есть шах Королю!
                if (listOfDangerFigures.Count == 1)
                {

                    // Но если Конь атакует эту фигуру (т.е. их координаты совпадают) - Можно атаковать
                    if (listOfDangerFigures[0].x == this.x && listOfDangerFigures[0].y == this.y)
                    {

                        if (methodMode == "Active")
                        {
                            Figures[number].Icon = null;
                            return true;
                        }

                        if (methodMode == "Check")
                        {
                            this.x = tempX; //Возвращаем предыдущие координаты фигуры
                            this.y = tempY;
                            return true;
                        }

                    }

                    //Если координаты Коня и опасной фигуры не совпадают
                    this.x = tempX; //Возвращаем предыдущие координаты фигуры
                    this.y = tempY;
                    return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);

                }

                // Если опасных фигур > 1 - отмена хода, т.к. атака одной фигуры не спасет от других
                if (listOfDangerFigures.Count > 1)
                {
                    this.x = tempX; //Возвращаем предыдущие координаты фигуры 
                    this.y = tempY;
                    return FalseAndMessage("Атака этой фигуры не спасает от шаха другой!", methodMode);
                }



            }

            return FalseAndMessage("Конь не может атаковать по такой траектории!", methodMode);
        }





        // Метод проверяет правильность траектории хода Коня на клетку с координатами squareX, squareY  
        internal override bool IsStepOn(ObservableCollection<Figure> Figures, int squareX, int squareY, string methodMode)
        {

            if ((squareX == this.x + 1 && squareY == this.y - 2) ||
                (squareX == this.x - 1 && squareY == this.y - 2) ||
                (squareX == this.x + 1 && squareY == this.y + 2) ||
                (squareX == this.x - 1 && squareY == this.y + 2) ||
                (squareX == this.x + 2 && squareY == this.y - 1) ||
                (squareX == this.x - 2 && squareY == this.y - 1) ||
                (squareX == this.x + 2 && squareY == this.y + 1) ||
                (squareX == this.x - 2 && squareY == this.y + 1))
            {
                return true;

            }
            else
                return false;
        }


    }
}
