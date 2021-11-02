using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace Chess
{
    internal class Pawn : Figure
    {
        internal Pawn(string type, int x, int y, string color, string name, BitmapFrame icon)

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

        internal bool noFirstStep = true; // Был ли первый ход, true - нет   false - да

        // Метод проверяет правильность траектории Xода (Step) и Aтаки (Attack) Пешки на клетку с координатами squareX, squareY
        internal override bool IsStepOn(ObservableCollection<Figure> Figures, int squareX, int squareY, string methodMode)
        {
            int stepY = Math.Abs(y - squareY);  // Шаг пешки по вертикали по модулю

            switch (methodMode)
            {
                case "Step":

                    // Если Пешка Белая и нет смещения по x и шаг пешки <=2 и y.пешки > y.клетки (нельзя ходить назад)
                    if (Color == "White" && x == squareX && stepY <= 2 && y > squareY)
                    {
                        // Если Пешка не делала первый ход и шаг пешки == 2
                        if (noFirstStep == true && stepY == 2)
                        {
                            // Если на пути Пешки стоит какая-либо фигура - return false
                            if (ChessModel.GetNumberFigure(Figures, x, y - 1) != 999) return false;

                            return true;
                        }

                        // Если Пешка уже делала первый ход и шаг пешки == 2 - return false
                        if (noFirstStep == false && stepY == 2) return false;

                        return true;
                    }

                    // Если Пешка Черная и нет смещения по x и шаг пешки <=2 и y.пешки < y.клетки (нельзя ходить назад)
                    if (Color == "Black" && x == squareX && stepY <= 2 && y < squareY)
                    {
                        if (noFirstStep == true && stepY == 2)
                        {
                            if (ChessModel.GetNumberFigure(Figures, x, y + 1) != 999) return false;

                            return true;
                        }

                        if (noFirstStep == false && stepY == 2) return false;

                        return true;
                    }

                    break;

                case "Attack":

                    if (stepY != 1) return false;

                    if (Color == "White" && y > squareY && (x == squareX + 1 || x == squareX - 1)) return true;

                    if (Color == "Black" && y < squareY && (x == squareX + 1 || x == squareX - 1)) return true;

                    break;
            }

            // Эта часть кода сработает если игрок ошибся с траекторией хода пешки
            return false; // FalseAndMessage("IsStepOn Такой ход пешкой невозможен!", methodMode);
        }

        // Метод просчитывает ход Пешки и если все верно, изменяет координаты фигуры для режима Active
        internal override bool Step(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {
            if (IsStepOn(Figures, x, y, "Step") == true)
            {
                int tempY = this.y;
                int tempX = this.x;

                this.y = y; //Присваиваем новые координаты своей фигуре

                // Проверка на шах
                // Если при новых x и y король своего цвета не находится под шахом - завершаем ход
                // т.е. возвращаем true - что считается корректным завершением хода

                if (ChessModel.ListOfDangerFigures(Figures, Color).Count == 0)
                {
                    if (methodMode == "Active")
                    {
                        noFirstStep = false;
                        return true;
                    }

                    if (methodMode == "Check")

                    {
                        this.y = tempY; // Возвращаем предыдущие координаты фигуры для режима Check
                        this.x = tempX;
                        return true;
                    }
                }

                this.y = tempY; // Возвращаем предыдущие координаты фигуры - отмена хода, т.к. король попадает под шах
                this.x = tempX;
                return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);
            }

            return FalseAndMessage("Пешка не может так ходить!", methodMode);
        }

        internal override bool Attack(ObservableCollection<Figure> Figures, int number, string methodMode)
        {
            if (Figures[number].Type == "King") return FalseAndMessage("Нельзя атаковать короля!", methodMode);

            if (IsStepOn(Figures, Figures[number].x, Figures[number].y, "Attack") == true)
            {
                // Присваиваем текущие координаты фигуры временным переменным
                int tempY = this.y;
                int tempX = this.x;

                y = Figures[number].y;
                x = Figures[number].x;

                // Проверка на шах
                // Метод ChessModel.ListOfDangerFigures возвращает список listOfDangerFigures "опасных фигур"
                // которые угрожают королю шахом на данный момент.

                List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, this.Color);

                if (listOfDangerFigures.Count == 0) // Если количество опасных фигур == 0 , то и шаха нет - Атака успешна

                {
                    if (methodMode == "Active")
                    {
                        noFirstStep = false;
                        Figures[number].Icon = null;
                        return true;
                    }

                    if (methodMode == "Check")
                    {
                        x = tempX; // Возвращаем предыдущие координаты фигуры
                        y = tempY;
                        return true;
                    }
                }

                if (listOfDangerFigures.Count == 1) // Если количество опасных фигур == 1 , шах королю!
                {
                    // Но если пешка атакует эту фигуру (т.е. их координаты совпадают) - Атака успешна
                    if (listOfDangerFigures[0].x == this.x && listOfDangerFigures[0].y == this.y)
                    {
                        if (methodMode == "Active")
                        {
                            noFirstStep = false;
                            Figures[number].Icon = null;
                            return true;
                        }

                        if (methodMode == "Check")
                        {
                            y = tempY; // Возвращаем предыдущие координаты фигуры для режима Check
                            x = tempX;
                            return true;
                        }
                    }
                    // Этот код сработает если опасная фигура одна, но координаты пешки и этой фигуры не совпадают
                    // Заодно код выполняет требование языка

                    this.x = tempX; // Возвращаем предыдущие координаты фигуры
                    this.y = tempY;
                    return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);
                }

                if (listOfDangerFigures.Count > 1) // Если опасных фигур > 1 - отмена хода, т.к. атака одной фигуры не спасет от других
                {
                    this.x = tempX; // Возвращаем предыдущие координаты фигуры
                    this.y = tempY;
                    MessageBox.Show("active - check >1 не спасает от шаха");

                    return FalseAndMessage("Атака этой фигуры не спасает от шаха другой!", methodMode);
                }
            }
            return FalseAndMessage("Такая атака пешкой невозможна!", methodMode);
        }
    }
}