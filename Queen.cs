﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Queen : Figure
    {

        internal Queen(string type, int x, int y, string color, string name, BitmapFrame icon)

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

        // Метод проверяет правильность траектории Xода Ферзя на клетку с координатами squareX, squareY
        internal override bool IsStepOn(ObservableCollection<Figure> Figures, int squareX, int squareY, string methodMode)
        {

            if (this.x == squareX && this.y == squareY) return false;

            // Проверяем диагонали
            if (Math.Abs(this.x - squareX) == Math.Abs(this.y - squareY))
            {

                // Диагональ -x -y
                if (this.x > squareX && this.y > squareY)
                {
                    int n = this.x - squareX;

                    for (int i = 1; i <= n - 1; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, this.x - i, this.y - i) != 999) return false;
                    }
                }

                // Диагональ -x +y
                if (this.x > squareX && this.y < squareY)
                {
                    int n = this.x - squareX;

                    for (int i = 1; i <= n - 1; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, this.x - i, this.y + i) != 999) return false;
                    }
                }

                // Диагональ +x -y
                if (this.x < squareX && this.y > squareY)
                {
                    int n = this.y - squareY;

                    for (int i = 1; i <= n - 1; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, this.x + i, this.y - i) != 999) return false;
                    }
                }

                // Диагональ +x +y
                if (this.x < squareX && this.y < squareY)
                {
                    int n = squareY - this.y;

                    for (int i = 1; i <= n - 1; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, this.x + i, this.y + i) != 999) return false;
                    }
                }

            }

            // Просчет вертикали
            else if (this.x == squareX && this.y != squareY)
            {

                if (y > squareY)

                    for (int i = squareY + 1; i < y; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, squareX, i) != 999) return false;
                    }

                if (y < squareY)

                    for (int i = y + 1; i < squareY; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, squareX, i) != 999) return false;
                    }

            }


            // Просчет горизонтали
            else if (y == squareY && x != squareX)
            {

                if (x > squareX)

                    for (int i = squareX + 1; i < x; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, i, squareY) != 999) return false;
                    }

                if (x < squareX)

                    for (int i = x; i < squareX - 1; i++)
                    {
                        if (ChessModel.GetNumberFigure(Figures, i + 1, squareY) != 999) return false;
                    }

            }

            // Эта часть кода сработает если игрок ошибся с траекторией хода ферзя	
            else return false;

            return true;

        }




        internal override bool Step(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {

            if (IsStepOn(Figures, x, y, methodMode) == true)
            {

                // Присваиваем текущие координаты фигуры временным переменным
                int tempY = this.y;
                int tempX = this.x;

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
                        this.y = tempY; //Возвращаем предыдущие координаты фигуры 
                        this.x = tempX;
                        return true;
                    }

                }


                this.y = tempY; // Возвращаем предыдущие координаты фигуры - отмена хода, т.к. король попадает под шах
                this.x = tempX;
                return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);



            }


            // Эта часть кода сработает если игрок ошибся с траекторией хода ферзя					 
            return FalseAndMessage("Ферзь не может ходить по такой траектории!", methodMode);


        }




        internal override bool Attack(ObservableCollection<Figure> Figures, int number, string methodMode)
        {

            if (Figures[number].Type == "King") return FalseAndMessage("Нельзя атаковать короля!", methodMode);

            if (IsStepOn(Figures, Figures[number].x, Figures[number].y, methodMode) == true)

            {

                // Присваиваем текущие координаты фигуры временным переменным
                int tempY = this.y;
                int tempX = this.x;

                this.x = Figures[number].x; // Присваиваем новые координаты своей фигуре
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
                        this.x = tempX; // Возвращаем предыдущие координаты фигуры 
                        this.y = tempY;
                        return true;
                    }

                }

                if (listOfDangerFigures.Count == 1) // Если количество опасных фигур == 1 , шах королю!
                {

                    // Но если ферзь атакует эту фигуру (т.е. их координаты совпадают) - Атака успешна
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

                    this.x = tempX; // Возвращаем предыдущие координаты фигуры 
                    this.y = tempY;
                    return FalseAndMessage("Нельзя оставлять короля под шахом!", methodMode);

                }
                this.x = tempX; // Возвращаем предыдущие координаты фигуры 
                this.y = tempY;
                return FalseAndMessage("Атака этой фигуры не спасает от шаха другой!", methodMode);


            }

            // Эта часть кода сработает если игрок ошибся с траекторией хода ферзя		
            return FalseAndMessage("Ферзь не может атаковать по такой траектории!", methodMode);


        }







    }



}
