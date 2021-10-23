using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MessageBox = System.Windows.MessageBox;

namespace Chess
{

    static class Check
    {

        // Метод определяет есть ли шах королю по его текущим координатам
        // возвращает 0 - если шаха нет, 1 - шах есть и нужна проверка на мат,
        // 2 - шах есть, но можно его можно парировать
        public static int IsShahForKing(ObservableCollection<Figure> Figures, string activeColor)
        {

            // Зная цвет короля (activeColor), находим номер короля в коллекции по его имени	
            int numberOfKing = activeColor == "White" ? ChessModel.GetNumberFigure(Figures, "WhiteKing") : ChessModel.GetNumberFigure(Figures, "BlackKing");

            // Проверка всех фигур ПРОТИВНИКА в цикле
            for (int i = 0; i < Figures.Count; i++)
            {
                // Если фигура ПРОТИВНИКА (отличающаяся по цвету) может атаковать короля, тогда шах Королю           
                if (Figures[i].Color != Figures[numberOfKing].Color && Figures[i].IsStepOn(Figures, Figures[numberOfKing].x, Figures[numberOfKing].y, "Attack") == true)

                {
                    int numberOfDangerFigure = i; // Это номер фигуры которя ставит Королю шах

                    for (int i2 = 0; i2 < Figures.Count; i2++)
                    {


                        // Если есть СВОЯ фигура,кроме самого Короля, способная атаковать угрожающую шахом фигуру
                        // противника (под номером numberOfDangerFigure) - возвращаем true без проверки на мат
                        if (Figures[i2].Color == activeColor && i2 != numberOfKing && Figures[i2].Attack(Figures, numberOfDangerFigure, "Check") == true)
                        {
                            // MessageBox.Show("Убрать опасную фигуру может " + Figures[i2].Name);
                            return 2;//true;
                        }

                    }

                    // Проверка есть ли своя фигура способная ПРИКРЫТЬ Короля - встать на траектории шаха
                    // При этом, вражеская фигура не может быть Конь или Пешка - т.к. Конь прыгает через фигуры, а между Пешкой
                    // и Королем нет свободных клеток, если Пешка угрожает королю шахом
                    if (Figures[numberOfDangerFigure].Type != "Horse" || Figures[numberOfDangerFigure].Type != "Pawn")
                    {

                        // Если шах по горизонтали
                        if (Figures[numberOfDangerFigure].x != Figures[numberOfKing].x && Figures[numberOfDangerFigure].y == Figures[numberOfKing].y)
                        {

                            // Находим горизонтальный диапазон - количество клеток между королем и опасной фигурой угрожающей ему шахом
                            int horizontalRange = Math.Abs(Figures[numberOfKing].x - Figures[numberOfDangerFigure].x);
                            horizontalRange--;

                            // MessageBox.Show("гор.диапазон = " + horizontalRange);
                            if (horizontalRange > 0)
                            {
                                for (int i3 = 1; i3 <= horizontalRange; i3++)
                                {

                                    if (Figures[numberOfDangerFigure].x > Figures[numberOfKing].x)
                                    {

                                        //MessageBox.Show("х опасной " + Figures[numberOfDangerFigure].x + " > " + "х короля " + Figures[numberOfKing].x);


                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int f = Figures[numberOfKing].x + i3;

                                            if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по х = " + f + " " + Figures[i4].Name);
                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfKing].x + i3, Figures[numberOfKing].y, "Check") == true)
                                            {

                                                //  MessageBox.Show("Можно поставить " + Figures[i4].Name);


                                                return 2;//true;

                                            }
                                        }

                                    }

                                    if (Figures[numberOfDangerFigure].x < Figures[numberOfKing].x)
                                    {

                                        //MessageBox.Show("х опасной " + Figures[numberOfDangerFigure].x + " < " + "х короля " + Figures[numberOfKing].x);


                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int f = Figures[numberOfDangerFigure].x + i3;
                                            if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по х = " + f + " " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x + i3, Figures[numberOfKing].y, "Check") == true)
                                            {

                                                // MessageBox.Show("Можно поставить " + Figures[i4].Name);
                                                return 2;//true;

                                            }
                                        }

                                    }


                                }
                            }
                        }

                        // Если шах по вертикали
                        if (Figures[numberOfDangerFigure].x == Figures[numberOfKing].x && Figures[numberOfDangerFigure].y != Figures[numberOfKing].y)
                        {

                            // Находим вертикальный диапазон - количество клеток между королем и опасной фигурой угрожающей ему шахом
                            int verticalRange = Math.Abs(Figures[numberOfKing].y - Figures[numberOfDangerFigure].y);
                            verticalRange--;

                            // MessageBox.Show("верт.диапазон = " + verticalRange);
                            if (verticalRange > 0)
                            {

                                for (int i3 = 1; i3 <= verticalRange; i3++)
                                {

                                    if (Figures[numberOfDangerFigure].y > Figures[numberOfKing].y)
                                    {

                                        // MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " > " + "y короля " + Figures[numberOfKing].y);


                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int f = Figures[numberOfKing].y + i3;

                                            if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по y = " + f + " " + Figures[i4].Name);
                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfKing].x, Figures[numberOfKing].y + i3, "Check") == true)
                                            {

                                                //  MessageBox.Show("Можно поставить " + Figures[i4].Name);
                                                return 2;//true;

                                            }
                                        }

                                    }

                                    if (Figures[numberOfDangerFigure].y < Figures[numberOfKing].y)
                                    {

                                        // MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " < " + "y короля " + Figures[numberOfKing].y);


                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int f = Figures[numberOfDangerFigure].y + i3;
                                            if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по y = " + f + " " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x, Figures[numberOfDangerFigure].y + i3, "Check") == true)
                                            {

                                                // MessageBox.Show("Можно поставить " + Figures[i4].Name);
                                                return 2;//true;

                                            }
                                        }

                                    }


                                }
                            }
                        }

                        // Если шах по диагонали
                        if (Figures[numberOfKing].x - Figures[numberOfDangerFigure].x == Figures[numberOfKing].y - Figures[numberOfDangerFigure].y
                                   || Figures[numberOfDangerFigure].x - Figures[numberOfKing].x == Figures[numberOfKing].y - Figures[numberOfDangerFigure].y)
                        {

                            // Находим диагональный диапазон - количество клеток между королем и опасной фигурой угрожающей ему шахом по диагонали
                            int verticalRange = Math.Abs(Figures[numberOfKing].x - Figures[numberOfDangerFigure].x);
                            verticalRange--;

                            // MessageBox.Show("диагон.диапазон = " + verticalRange);

                            if (verticalRange > 0)
                            {

                                // Диагональ -x -y 
                                if (Figures[numberOfKing].x > Figures[numberOfDangerFigure].x && Figures[numberOfKing].y > Figures[numberOfDangerFigure].y)
                                {

                                    for (int i3 = 1; i3 <= verticalRange; i3++)
                                    {
                                        //MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " > " + "y короля " + Figures[numberOfKing].y);

                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int yt = Figures[numberOfDangerFigure].y + i3; //
                                            int xt = Figures[numberOfDangerFigure].x + i3; //

                                            if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по x = " + xt + " и y " + yt + " для " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x + i3, Figures[numberOfDangerFigure].y + i3, "Check") == true)
                                            {

                                                //  MessageBox.Show("На диагональ можно поставить " + Figures[i4].Name);
                                                return 2;// true;

                                            }

                                        }


                                    }



                                }

                                // Диагональ -x +y 
                                if (Figures[numberOfKing].x > Figures[numberOfDangerFigure].x && Figures[numberOfKing].y < Figures[numberOfDangerFigure].y)
                                {

                                    for (int i3 = 1; i3 <= verticalRange; i3++)
                                    {
                                        // MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " > " + "y короля " + Figures[numberOfKing].y);

                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int yt = Figures[numberOfDangerFigure].y - i3; //
                                            int xt = Figures[numberOfDangerFigure].x + i3; //

                                            //if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по x = " + xt + " и y " + yt + " для " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x + i3, Figures[numberOfDangerFigure].y - i3, "Check") == true)
                                            {

                                                // MessageBox.Show("На диагональ можно поставить " + Figures[i4].Name);
                                                return 2; //true;

                                            }

                                        }


                                    }




                                }

                                // Диагональ +x -y 
                                if (Figures[numberOfKing].x < Figures[numberOfDangerFigure].x && Figures[numberOfKing].y > Figures[numberOfDangerFigure].y)
                                {

                                    for (int i3 = 1; i3 <= verticalRange; i3++)
                                    {
                                        //MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " > " + "y короля " + Figures[numberOfKing].y);

                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int yt = Figures[numberOfDangerFigure].y + i3; //
                                            int xt = Figures[numberOfDangerFigure].x - i3; //

                                            //if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по x = " + xt + " и y " + yt + " для " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x - i3, Figures[numberOfDangerFigure].y + i3, "Check") == true)
                                            {

                                                //  MessageBox.Show("На диагональ можно поставить " + Figures[i4].Name);
                                                return 2; //true;

                                            }

                                        }


                                    }




                                }

                                // Диагональ +x +y 
                                if (Figures[numberOfKing].x < Figures[numberOfDangerFigure].x && Figures[numberOfKing].y < Figures[numberOfDangerFigure].y)
                                {

                                    for (int i3 = 1; i3 <= verticalRange; i3++)
                                    {
                                        // MessageBox.Show("y опасной " + Figures[numberOfDangerFigure].y + " > " + "y короля " + Figures[numberOfKing].y);

                                        // Проверка для всех своих фигур в цикле
                                        for (int i4 = 0; i4 < Figures.Count; i4++)
                                        {
                                            int yt = Figures[numberOfDangerFigure].y - i3; //
                                            int xt = Figures[numberOfDangerFigure].x - i3; //

                                            // if (Figures[i4].Color == Figures[numberOfKing].Color) MessageBox.Show("поиск по x = " + xt + " и y " + yt + " для " + Figures[i4].Name);

                                            if (Figures[i4].Color == Figures[numberOfKing].Color && Figures[i4].Step(Figures, Figures[numberOfDangerFigure].x - i3, Figures[numberOfDangerFigure].y - i3, "Check") == true)
                                            {

                                                //  MessageBox.Show("На диагональ можно поставить " + Figures[i4].Name);
                                                return 2; //true;

                                            }

                                        }


                                    }




                                }


                            }
                        }

                    }

                    return 1; //Шах есть
                }

            }

            return 0; // false; // 0  Шаха нет

        }



        // Метод определяет есть ли мат королю проверяя его на шах по 8ми позициям
        public static bool IsCheckMate(ObservableCollection<Figure> Figures, string activeColor)
        {

            int numberFigure;

            // Зная цвет короля (activeColor), находим номер короля в коллекции по его имени	
            int numberOfKing = activeColor == "White" ? ChessModel.GetNumberFigure(Figures, "WhiteKing") : ChessModel.GetNumberFigure(Figures, "BlackKing");

            int temp_x = Figures[numberOfKing].x; //Запомним текущие координаты короля
            int temp_y = Figures[numberOfKing].y;

            // У Короля 8 вариантов хода. Проверяем 8 клеток на возможность короля выйти из шаха

            // Клетка 1
            if (Figures[numberOfKing].x - 1 >= 0 && Figures[numberOfKing].y - 1 >= 0)
            {

                // MessageBox.Show("1 клетка x=" + (Figures[numberOfKing].x - 1).ToString() + " y=" + (Figures[numberOfKing].y - 1).ToString());

                // Проверка не стоит ли на клетке фигура 	

                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x - 1, Figures[numberOfKing].y - 1);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 1 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //MessageBox.Show("Клетка 1 занята вражеской фигурой");

                        Figures[numberOfKing].x--;
                        Figures[numberOfKing].y--;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Опасная фигура это " + listOfDangerFigures[0].Name);
                            // MessageBox.Show("Клетка 1 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    // MessageBox.Show("Клетка 1 пустая");

                    Figures[numberOfKing].x--;
                    Figures[numberOfKing].y--;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        // MessageBox.Show("Клетка 1 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    // MessageBox.Show("Клетка 1 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }


            // Клетка 2
            if (Figures[numberOfKing].y - 1 >= 0)
            {

                // MessageBox.Show("2 клетка x=" + Figures[numberOfKing].x + " y=" + (Figures[numberOfKing].y - 1));

                // Проверка не стоит ли на клетке фигура 

                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x, Figures[numberOfKing].y - 1);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 2 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //  MessageBox.Show("Клетка 2 занята вражеской фигурой");

                        Figures[numberOfKing].y--;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            //  MessageBox.Show("Клетка 2 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    // MessageBox.Show("Клетка 2 пустая");

                    Figures[numberOfKing].y--;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        // MessageBox.Show("Клетка 2 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    // MessageBox.Show("Клетка 2 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }

            }

            // Клетка 3
            if (Figures[numberOfKing].y - 1 >= 0 && Figures[numberOfKing].x + 1 <= 7)
            {

                //MessageBox.Show("3 клетка x=" + (Figures[numberOfKing].x + 1).ToString() + " y=" + (Figures[numberOfKing].y - 1).ToString());

                // Проверка не стоит ли на клетке фигура 			    
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x + 1, Figures[numberOfKing].y - 1);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 3 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        // MessageBox.Show("Клетка 3 занята вражеской фигурой");

                        Figures[numberOfKing].x++;
                        Figures[numberOfKing].y--;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Клетка 3 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    //MessageBox.Show("Клетка 3 пустая");

                    Figures[numberOfKing].x++;
                    Figures[numberOfKing].y--;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        // MessageBox.Show("Клетка 3 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    // MessageBox.Show("Клетка 3 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }


            // Клетка 4
            if (Figures[numberOfKing].x + 1 <= 7)
            {

                // MessageBox.Show("4 клетка x=" + (Figures[numberOfKing].x + 1) + " y=" + (Figures[numberOfKing].y));

                // Проверка не стоит ли на клетке фигура 
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x + 1, Figures[numberOfKing].y);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 4 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //  MessageBox.Show("Клетка 4 занята вражеской фигурой");

                        Figures[numberOfKing].x++;


                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Клетка 4 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    // MessageBox.Show("Клетка 4 пустая");

                    Figures[numberOfKing].x++;


                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        // MessageBox.Show("Клетка 4 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    // MessageBox.Show("Клетка 4 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }



            // Клетка 5
            if (Figures[numberOfKing].y + 1 <= 7 && Figures[numberOfKing].x + 1 <= 7)
            {

                // MessageBox.Show("5 клетка x=" + (Figures[numberOfKing].x + 1) + " y=" + (Figures[numberOfKing].y + 1));

                // Проверка не стоит ли на клетке фигура 
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x + 1, Figures[numberOfKing].y + 1);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 5 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //  MessageBox.Show("Клетка 5 занята вражеской фигурой");

                        Figures[numberOfKing].x++;
                        Figures[numberOfKing].y++;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        //   MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Клетка 5 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    //MessageBox.Show("Клетка 5 пустая");

                    Figures[numberOfKing].x++;
                    Figures[numberOfKing].y++;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        //  MessageBox.Show("Клетка 5 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    // MessageBox.Show("Клетка 5 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }


            // Клетка 6
            if (Figures[numberOfKing].y + 1 <= 7)
            {

                //  MessageBox.Show("6 клетка x=" + Figures[numberOfKing].x + " y=" + (Figures[numberOfKing].y + 1));

                // Проверка не стоит ли на клетке фигура 	
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x, Figures[numberOfKing].y + 1);

                if (numberFigure != 999)
                {

                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 6 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        // MessageBox.Show("Клетка 6 занята вражеской фигурой");


                        Figures[numberOfKing].y++;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Клетка 6 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    // MessageBox.Show("Клетка 6 пустая");


                    Figures[numberOfKing].y++;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        // MessageBox.Show("Клетка 6 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    //// MessageBox.Show("Клетка 6 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }


            // Клетка 7
            if (Figures[numberOfKing].x - 1 >= 0 && Figures[numberOfKing].y + 1 <= 7)
            {

                // MessageBox.Show("7 клетка x=" + (Figures[numberOfKing].x - 1) + " y=" + (Figures[numberOfKing].y + 1));

                // Проверка не стоит ли на клетке фигура 	
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x - 1, Figures[numberOfKing].y + 1);

                if (numberFigure != 999)

                {
                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 7 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //////// MessageBox.Show("Клетка 7 занята вражеской фигурой");

                        Figures[numberOfKing].x--;
                        Figures[numberOfKing].y++;

                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            ///  MessageBox.Show("Клетка 7 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

                }

                // Если клетка Пустая
                else

                {
                    //MessageBox.Show("Клетка 7 пустая");

                    Figures[numberOfKing].x--;
                    Figures[numberOfKing].y++;

                    if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                    {
                        //MessageBox.Show("Клетка 7 Можно ходить");
                        Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                        Figures[numberOfKing].y = temp_y;
                        return false;
                    }

                    //MessageBox.Show("Клетка 7 Нельзя ходить под шах!");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;

                }


            }



            // Клетка 8
            if (Figures[numberOfKing].x - 1 >= 0)
            {

                // MessageBox.Show("8 клетка x=" + (Figures[numberOfKing].x - 1) + " y=" + (Figures[numberOfKing].y));

                // Проверка не стоит ли на клетке фигура 		
                numberFigure = ChessModel.GetNumberFigure(Figures, Figures[numberOfKing].x - 1, Figures[numberOfKing].y);

                if (numberFigure != 999)


                    // Если это фигура своего цвета  
                    if (Figures[numberFigure].Color == activeColor) MessageBox.Show("Клетка 8 занята своей фигурой");

                    // Если это фигура противника  
                    else
                    {

                        //MessageBox.Show("Клетка 8 занята вражеской фигурой");

                        Figures[numberOfKing].x--;


                        List<Figure> listOfDangerFigures = ChessModel.ListOfDangerFigures(Figures, activeColor);

                        // MessageBox.Show("Количество опасных фигур " + listOfDangerFigures.Count);

                        // Если количество опасных фигур = 0
                        if (listOfDangerFigures.Count == 0)
                        {
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                            return false;
                        }

                        // Если количество опасных фигур >= 1
                        if (listOfDangerFigures.Count >= 1)
                        {
                            // MessageBox.Show("Клетка 8 Сюда нельзя");
                            Figures[numberOfKing].x = temp_x; // Возвращаем старые координаты
                            Figures[numberOfKing].y = temp_y;
                        }

                    }

            }

            // Если клетка Пустая
            else

            {
                //MessageBox.Show("Клетка 8 пустая");

                Figures[numberOfKing].x--;


                if (ChessModel.ListOfDangerFigures(Figures, activeColor).Count == 0)
                {
                    // MessageBox.Show("Клетка 8 Можно ходить");
                    Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                    Figures[numberOfKing].y = temp_y;
                    return false;
                }

                //MessageBox.Show("Клетка 8 Нельзя ходить под шах!");
                Figures[numberOfKing].x = temp_x; //Возвращаем старые координаты
                Figures[numberOfKing].y = temp_y;

            }


            return true;


        }












    }
}







