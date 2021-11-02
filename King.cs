using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace Chess
{
    internal class King : Figure //internal - доступен из любого места текущей сборки. недоступен для др.программ и сборок
    {
        internal King(string type, int x, int y, string color, string name, BitmapFrame icon)

        {
            Type = type;
            this.x = x;
            this.y = y;
            Icon = icon;
            Name = name;
            Color = color;
            PropNameForX = Name + "_X"; 
            PropNameForY = Name + "_Y";
        }

        // Метод просчитывает ход короля на другое поле
        internal override bool Step(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {
            if (IsStepOn(Figures, x, y, methodMode) == true)
            {
                int tempX = this.x; // Запомним старые координаты
                int tempY = this.y;

                this.x = x; // Присваиваем королю новые координаты - по ним будет проверка не ходит ли король под шах
                this.y = y;

                if (ChessModel.ListOfDangerFigures(Figures, this.Color).Count == 0)

                {
                    // MessageBox.Show("кол-во опасных фигур = 0");

                    if (methodMode == "Active") return true; // Возвращаем положительный результат = корректный ход

                    if (methodMode == "Check")
                    {
                        this.x = tempX; // Возвращаем старые координаты короля
                        this.y = tempY;
                        return true;
                    }
                }

                this.x = tempX; // Возвращаем старые координаты короля
                this.y = tempY;
                return FalseAndMessage("Король не может ходить под шах!", methodMode);
            }

            // Эта часть кода сработает если игрок ошибся с траекторией хода короля
            return FalseAndMessage("Король не может так ходить!", methodMode);
        }

        // Метод просчитывает атаку короля на фигуру противника
        internal override bool Attack(ObservableCollection<Figure> Figures, int number, string methodMode)
        {
            if (IsStepOn(Figures, Figures[number].x, Figures[number].y, methodMode) == true)
            {
                int tempX = this.x; //Запомним старые координаты
                int tempY = this.y;

                this.x = Figures[number].x; // Присваиваем королю новые координаты - по ним будет проверка не ходит ли король под шах
                this.y = Figures[number].y;

                // Проверка на шах
                // Метод ChessModel.ListOfDangerFigures возвращает список listOfDangerFigures "опасных фигур"
                // которые угрожают королю шахом на данный момент.

                List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, this.Color);

                if (listOfDangerFigures.Count == 0) //Если количество опасных фигур == 0 , то и шаха нет - Атака успешна
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

                if (listOfDangerFigures.Count == 1) // Если количество опасных фигур == 1 , шах королю!
                {
                    // Но если король атакует эту опасную фигуру (т.е. их координаты совпадают) - Атака успешна
                    if (listOfDangerFigures[0].x == this.x && listOfDangerFigures[0].y == this.y)
                    {
                        if (methodMode == "Active")
                        {
                            Figures[number].Icon = null;
                            return true;
                        }

                        if (methodMode == "Check")
                        {
                            this.x = tempX; // Возвращаем предыдущие координаты фигуры
                            this.y = tempY;
                            return true;
                        }
                    }
                }

                this.x = tempX; // Возвращаем предыдущие координаты фигуры
                this.y = tempY;
                return FalseAndMessage("Нельзя атаковать под шах!", methodMode);
            }

            // Эта часть кода сработает если игрок ошибся с траекторией атаки короля
            return FalseAndMessage("Король не может так атаковать!", methodMode);
        }

        // Метод проверяет правильность траектории хода Короля на клетку с координатами squareX, squareY
        internal override bool IsStepOn(ObservableCollection<Figure> Figures, int squareX, int squareY, string methodMode)
        {
            if ((this.x - squareX == 1 && this.y - squareY == 1) || (this.x - squareX == 1 && squareY - this.y == 1) ||
               (squareX - this.x == 1 && this.y - squareY == 1) || (squareX - this.x == 1 && squareY - this.y == 1) ||
               (this.x == squareX && this.y - squareY == 1) || (this.x == squareX && squareY - this.y == 1) ||
               (this.x - squareX == 1 && this.y == squareY) || (squareX - this.x == 1 && this.y == squareY))
            {
                return true; // Траектория верна
            }

            return false; // Траектория ошибочна
        }
    }
}