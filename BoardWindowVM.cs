using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Chess
{



    public class BoardWindowVM : INotifyPropertyChanged

    {

        public delegate void Deleg();


        ChessModel model;

        private int rotate_angle;
        public int RotateAngle
        {
            set
            {
                rotate_angle = value;
                OnPropertyChanged("RotateAngle");
            }

            get { return rotate_angle; }
        }


        private string digitOnRow2;
        public string DigitOnRow2

        {
            get { return digitOnRow2; }
            set { digitOnRow2 = value; OnPropertyChanged("DigitOnRow2"); }
        }

        private string digitOnRow3;
        public string DigitOnRow3

        {
            get { return digitOnRow3; }
            set { digitOnRow3 = value; OnPropertyChanged("DigitOnRow3"); }
        }


        private string digitOnRow4;
        public string DigitOnRow4

        {
            get { return digitOnRow4; }
            set { digitOnRow4 = value; OnPropertyChanged("DigitOnRow4"); }
        }

        private string digitOnRow5;
        public string DigitOnRow5

        {
            get { return digitOnRow5; }
            set { digitOnRow5 = value; OnPropertyChanged("DigitOnRow5"); }
        }

        private string digitOnRow6;
        public string DigitOnRow6

        {
            get { return digitOnRow6; }
            set { digitOnRow6 = value; OnPropertyChanged("DigitOnRow6"); }
        }

        private string digitOnRow7;
        public string DigitOnRow7

        {
            get { return digitOnRow7; }
            set { digitOnRow7 = value; OnPropertyChanged("DigitOnRow7"); }
        }

        private string digitOnRow8;
        public string DigitOnRow8

        {
            get { return digitOnRow8; }
            set { digitOnRow8 = value; OnPropertyChanged("DigitOnRow8"); }
        }

        private string digitOnRow9;
        public string DigitOnRow9

        {
            get { return digitOnRow9; }
            set { digitOnRow9 = value; OnPropertyChanged("DigitOnRow9"); }
        }

        private string charOnColumn1;
        public string CharOnColumn1
        {
            get { return charOnColumn1; }
            set { charOnColumn1 = value; OnPropertyChanged("CharOnColumn1"); }
        }

        private string charOnColumn2;
        public string CharOnColumn2
        {
            get { return charOnColumn2; }
            set { charOnColumn2 = value; OnPropertyChanged("CharOnColumn2"); }
        }

        private string charOnColumn3;
        public string CharOnColumn3
        {
            get { return charOnColumn3; }
            set { charOnColumn3 = value; OnPropertyChanged("CharOnColumn3"); }
        }

        private string charOnColumn4;
        public string CharOnColumn4
        {
            get { return charOnColumn4; }
            set { charOnColumn4 = value; OnPropertyChanged("CharOnColumn4"); }
        }

        private string charOnColumn5;
        public string CharOnColumn5
        {
            get { return charOnColumn5; }
            set { charOnColumn5 = value; OnPropertyChanged("CharOnColumn5"); }
        }

        private string charOnColumn6;
        public string CharOnColumn6
        {
            get { return charOnColumn6; }
            set { charOnColumn6 = value; OnPropertyChanged("CharOnColumn6"); }
        }

        private string charOnColumn7;
        public string CharOnColumn7
        {
            get { return charOnColumn7; }
            set { charOnColumn7 = value; OnPropertyChanged("CharOnColumn7"); }
        }

        private string charOnColumn8;
        public string CharOnColumn8
        {
            get { return charOnColumn8; }
            set { charOnColumn8 = value; OnPropertyChanged("CharOnColumn8"); }
        }




        public string SelectedFigureVisibility
        {
            get
            {
                // Это условие необходимо, чтобы не отображался  зеленый квадрат под выбранной фигурой
                // если Новая Игра из меню еще не запущена (т.е. model == null )            
                if (model == null) return "Hidden";

                // Если есть выбранная фигура - отображается зеленая подсветка
                if (model.SelectedFigure != null) return "Visible";

                else return "Hidden";

            }
        }
        public int SelectedFigure_X { get { return model.SelectedFigure.x; } }

        public int SelectedFigure_Y { get { return model.SelectedFigure.y; } }




        public BitmapFrame BlackPawn1
        {

            get
            {

                return model.bp1.Icon;

            }

        }


        public int BlackPawn1_X
        {

            get { return model.bp1.x; }


            set
            {

                model.bp1.x = value;
                // OnPropertyChanged("BlackPawn1_X");
            }


        }


        public int BlackPawn1_Y
        {

            get { return model.bp1.y; }


            set
            {

                model.bp1.y = value;
                // OnPropertyChanged("BlackPawn1_Y");
            }


        }



        public BitmapFrame BlackPawn2
        {

            get
            {

                return model.bp2.Icon;
            }

        }


        public int BlackPawn2_X
        {

            get
            {

                return model.bp2.x;
            }


            set
            {

                model.bp2.x = value;
                //OnPropertyChanged("BlackPawn2_X");
            }


        }

        public int BlackPawn2_Y
        {

            get
            {

                return model.bp2.y;
            }


            set
            {

                model.bp2.y = value;
                //OnPropertyChanged("BlackPawn2_Y");
            }


        }



        public BitmapFrame BlackPawn3
        {

            get
            {

                return model.bp3.Icon;
            }

        }


        public int BlackPawn3_X
        {

            get
            {

                return model.bp3.x;
            }


            set
            {

                model.bp3.x = value;
                // OnPropertyChanged("BlackPawn3_X");
            }


        }

        public int BlackPawn3_Y
        {

            get
            {

                return model.bp3.y;
            }


            set
            {

                model.bp3.y = value;
                //OnPropertyChanged("BlackPawn3_Y");
            }


        }



        public BitmapFrame BlackPawn4
        {

            get
            {

                return model.bp4.Icon;
            }

        }


        public int BlackPawn4_X
        {

            get
            {

                return model.bp4.x;
            }


            set
            {

                model.bp4.x = value;
                // OnPropertyChanged("BlackPawn4_X");
            }


        }

        public int BlackPawn4_Y
        {

            get
            {

                return model.bp4.y;
            }


            set
            {

                model.bp4.y = value;
                //OnPropertyChanged("BlackPawn4_Y");
            }


        }



        public BitmapFrame BlackPawn5
        {

            get
            {

                return model.bp5.Icon;
            }

        }


        public int BlackPawn5_X
        {

            get
            {

                return model.bp5.x;
            }


            set
            {

                model.bp5.x = value;
                //OnPropertyChanged("BlackPawn5_X");
            }


        }

        public int BlackPawn5_Y
        {

            get
            {

                return model.bp5.y;
            }


            set
            {

                model.bp5.y = value;
                // OnPropertyChanged("BlackPawn5_Y");
            }


        }



        public BitmapFrame BlackPawn6
        {

            get { return model.bp6.Icon; }

        }


        public int BlackPawn6_X
        {

            get
            {

                return model.bp6.x;
            }


            set
            {

                model.bp6.x = value;
                //OnPropertyChanged("BlackPawn6_X");
            }


        }

        public int BlackPawn6_Y
        {

            get
            {

                return model.bp6.y;
            }


            set
            {

                model.bp6.y = value;
                //OnPropertyChanged("BlackPawn6_Y");
            }


        }



        public BitmapFrame BlackPawn7
        {

            get
            {

                return model.bp7.Icon;
            }

        }


        public int BlackPawn7_X
        {

            get
            {

                return model.bp7.x;
            }


            set
            {

                model.bp7.x = value;
                // OnPropertyChanged("BlackPawn7_X");
            }


        }

        public int BlackPawn7_Y
        {

            get
            {

                return model.bp7.y;
            }


            set
            {

                model.bp7.y = value;
                // OnPropertyChanged("BlackPawn7_Y");
            }


        }



        public BitmapFrame BlackPawn8
        {

            get
            {

                return model.bp8.Icon;
            }

        }


        public int BlackPawn8_X
        {

            get
            {

                return model.bp8.x;
            }


            set
            {

                model.bp8.x = value;
                //OnPropertyChanged("BlackPawn8_X");
            }


        }

        public int BlackPawn8_Y
        {

            get { return model.bp8.y; }


            set
            {

                model.bp8.y = value;
                // OnPropertyChanged("BlackPawn8_Y");
            }


        }


        public BitmapFrame BlackHorse1
        {

            get
            {


                return model.bh1.Icon;
            }

        }


        public int BlackHorse1_X
        {

            get { return model.bh1.x; }


            set
            {

                model.bh1.x = value;
                // OnPropertyChanged("BlackHorse1_X");
            }

        }

        public int BlackHorse1_Y
        {

            get { return model.bh1.y; }

            set
            {

                model.bh1.y = value;
                //OnPropertyChanged("BlackHorse1_Y");
            }

        }


        public BitmapFrame BlackHorse2
        {

            get
            {
                return model.bh2.Icon;
            }

        }


        public int BlackHorse2_X
        {

            get { return model.bh2.x; }


            set
            {

                model.bh2.x = value;
                //OnPropertyChanged("BlackHorse2_X");
            }

        }

        public int BlackHorse2_Y
        {

            get { return model.bh2.y; }

            set
            {

                model.bh2.y = value;
                //OnPropertyChanged("BlackHorse2_Y");
            }

        }



        public BitmapFrame BlackRook1
        {

            get { return model.br1.Icon; }

        }


        public int BlackRook1_X
        {

            get { return model.br1.x; }


            set
            {

                model.br1.x = value;
                // OnPropertyChanged("BlackRook1_X");
            }

        }

        public int BlackRook1_Y
        {

            get { return model.br1.y; }

            set
            {

                model.br1.y = value;
                // OnPropertyChanged("BlackRook1_Y");
            }

        }


        public BitmapFrame BlackRook2
        {

            get { return model.br2.Icon; }

        }


        public int BlackRook2_X
        {

            get { return model.br2.x; }


            set
            {

                model.br2.x = value;
                //OnPropertyChanged("BlackRook2_X");
            }

        }

        public int BlackRook2_Y
        {

            get { return model.br2.y; }

            set
            {

                model.br2.y = value;
                //OnPropertyChanged("BlackRook2_Y");
            }

        }

        public BitmapFrame BlackBishop1
        {

            get { return model.bb1.Icon; }

        }


        public int BlackBishop1_X
        {

            get { return model.bb1.x; }


            set
            {

                model.bb1.x = value;
                // OnPropertyChanged("BlackBishop1_X");
            }

        }

        public int BlackBishop1_Y
        {

            get { return model.bb1.y; }

            set
            {

                model.bb1.y = value;
                //OnPropertyChanged("BlackBishop1_Y");
            }

        }




        public BitmapFrame BlackBishop2
        {

            get { return model.bb2.Icon; }

        }


        public int BlackBishop2_X
        {

            get { return model.bb2.x; }


            set
            {

                model.bb2.x = value;
                // OnPropertyChanged("BlackBishop2_X");
            }

        }

        public int BlackBishop2_Y
        {

            get { return model.bb2.y; }

            set
            {

                model.bb2.y = value;

            }

        }


        public BitmapFrame BlackQueen
        {

            get { return model.bq.Icon; }

        }


        public int BlackQueen_X
        {

            get { return model.bq.x; }


            set
            {

                model.bq.x = value;

            }

        }

        public int BlackQueen_Y
        {

            get { return model.bq.y; }

            set
            {

                model.bq.y = value;

            }

        }


        public BitmapFrame BlackKing
        {

            get { return model.bk.Icon; }

        }


        public int BlackKing_X
        {

            get { return model.bk.x; }


            set
            {

                model.bk.x = value;

            }

        }

        public int BlackKing_Y
        {

            get { return model.bk.y; }

            set
            {

                model.bk.y = value;

            }

        }

        //	//=====================================================	
        public BitmapFrame WhitePawn1
        {

            get { return model.wp1.Icon; }

        }


        public int WhitePawn1_X
        {

            get { return model.wp1.x; }

            set
            {
                model.wp1.x = value;

            }

        }

        public int WhitePawn1_Y
        {

            get { return model.wp1.y; }

            set
            {
                model.wp1.y = value;

            }


        }


        public BitmapFrame WhitePawn2
        {

            get { return model.wp2.Icon; }

        }


        public int WhitePawn2_X
        {

            get { return model.wp2.x; }

            set
            {
                model.wp2.x = value;

            }

        }

        public int WhitePawn2_Y
        {

            get { return model.wp2.y; }

            set
            {
                model.wp2.y = value;

            }


        }


        public BitmapFrame WhitePawn3
        {

            get { return model.wp3.Icon; }

        }


        public int WhitePawn3_X
        {

            get { return model.wp3.x; }

            set
            {
                model.wp3.x = value;

            }

        }

        public int WhitePawn3_Y
        {

            get { return model.wp3.y; }

            set
            {
                model.wp3.y = value;

            }


        }



        public BitmapFrame WhitePawn4
        {

            get { return model.wp4.Icon; }

        }


        public int WhitePawn4_X
        {

            get { return model.wp4.x; }

            set
            {
                model.wp4.x = value;

            }

        }

        public int WhitePawn4_Y
        {

            get { return model.wp4.y; }

            set
            {
                model.wp4.y = value;

            }


        }



        public BitmapFrame WhitePawn5
        {

            get { return model.wp5.Icon; }

        }


        public int WhitePawn5_X
        {

            get { return model.wp5.x; }

            set
            {
                model.wp5.x = value;

            }

        }

        public int WhitePawn5_Y
        {

            get { return model.wp5.y; }

            set
            {
                model.wp5.y = value;

            }


        }


        public BitmapFrame WhitePawn6
        {

            get { return model.wp6.Icon; }

        }


        public int WhitePawn6_X
        {

            get { return model.wp6.x; }

            set
            {
                model.wp6.x = value;

            }

        }

        public int WhitePawn6_Y
        {

            get { return model.wp6.y; }

            set
            {
                model.wp6.y = value;

            }


        }


        public BitmapFrame WhitePawn7
        {

            get { return model.wp7.Icon; }

        }


        public int WhitePawn7_X
        {

            get { return model.wp7.x; }

            set
            {
                model.wp7.x = value;

            }

        }

        public int WhitePawn7_Y
        {

            get { return model.wp7.y; }

            set
            {
                model.wp7.y = value;

            }


        }


        public BitmapFrame WhitePawn8
        {

            get { return model.wp8.Icon; }

        }


        public int WhitePawn8_X
        {

            get { return model.wp8.x; }

            set
            {
                model.wp8.x = value;

            }

        }

        public int WhitePawn8_Y
        {

            get { return model.wp8.y; }

            set
            {
                model.wp8.y = value;

            }


        }

        public BitmapFrame WhiteHorse1
        {

            get { return model.wh1.Icon; }

        }


        public int WhiteHorse1_X
        {

            get { return model.wh1.x; }


            set
            {

                model.wh1.x = value;

            }

        }

        public int WhiteHorse1_Y
        {

            get { return model.wh1.y; }

            set
            {

                model.wh1.y = value;

            }

        }


        public BitmapFrame WhiteHorse2
        {

            get { return model.wh2.Icon; }

        }


        public int WhiteHorse2_X
        {

            get { return model.wh2.x; }


            set
            {

                model.wh2.x = value;

            }

        }

        public int WhiteHorse2_Y
        {

            get { return model.wh2.y; }

            set
            {

                model.wh2.y = value;

            }

        }


        public BitmapFrame WhiteRook1
        {

            get { return model.wr1.Icon; }

        }


        public int WhiteRook1_X
        {

            get { return model.wr1.x; }


            set
            {

                model.wr1.x = value;

            }

        }

        public int WhiteRook1_Y
        {

            get { return model.wr1.y; }

            set
            {

                model.wr1.y = value;

            }

        }



        public BitmapFrame WhiteRook2
        {

            get { return model.wr2.Icon; }

        }


        public int WhiteRook2_X
        {

            get { return model.wr2.x; }


            set
            {

                model.wr2.x = value;

            }

        }

        public int WhiteRook2_Y
        {

            get { return model.wr2.y; }

            set
            {

                model.wr2.y = value;

            }

        }


        public BitmapFrame WhiteBishop1
        {

            get { return model.wb1.Icon; }

        }


        public int WhiteBishop1_X
        {

            get { return model.wb1.x; }


            set
            {

                model.wb1.x = value;

            }

        }

        public int WhiteBishop1_Y
        {

            get { return model.wb1.y; }

            set
            {

                model.wb1.y = value;

            }

        }



        public BitmapFrame WhiteBishop2
        {

            get { return model.wb2.Icon; }

        }


        public int WhiteBishop2_X
        {

            get { return model.wb2.x; }


            set
            {

                model.wb2.x = value;

            }

        }

        public int WhiteBishop2_Y
        {

            get { return model.wb2.y; }

            set
            {

                model.wb2.y = value;

            }

        }

        public BitmapFrame WhiteQueen
        {

            get { return model.wq.Icon; }

        }


        public int WhiteQueen_X
        {

            get { return model.wq.x; }


            set
            {

                model.wq.x = value;

            }

        }

        public int WhiteQueen_Y
        {

            get { return model.wq.y; }

            set
            {

                model.wq.y = value;

            }

        }


        public BitmapFrame WhiteKing
        {

            get
            {

                return model.wk.Icon;
            }

        }


        public int WhiteKing_X
        {

            get { return model.wk.x; }


            set
            {

                model.wk.x = value;

            }

        }

        public int WhiteKing_Y
        {

            get { return model.wk.y; }

            set
            {

                model.wk.y = value;

            }

        }


        public string InformationMessageProperty
        {

            get { return InformationMessage.message; }

        }


        public string StepStatus
        {

            get
            {

                if (model.activeColor == "End Game")
                {
                    StepStatusColor = "White";
                    return "Игра окончена";
                }

                if (model.activeColor == "Nothing")
                {
                    StepStatusColor = "Red";
                    return "Противник думает...";
                }

                else
                {
                    StepStatusColor = "LightGreen";
                    return "Ваш ход...";
                }
            }

        }

        private string stepStatusColor;
        public string StepStatusColor
        {

            set
            {

                stepStatusColor = value;
                OnPropertyChanged("StepStatusColor");

            }

            get { return stepStatusColor; }
        }


        // Команда обрабатывает клик по клетке доски
        private RelayCommand ct;
        public RelayCommand ClickOnDesk
        {
            get
            {
                return ct ??
                (ct = new RelayCommand(obj =>
                {

                    //Получаем координаты клетки					                       	
                    string[] words = obj.ToString().Split(new char[] { '_' });

                    // Передаем координаты клетки в модель, при условии, что
                    // Новая Игра запущена (т.е. model!=null)	
                    if (model != null) model.Click(int.Parse(words[1]), int.Parse(words[2]));



                }));
            }
        }


        // Команда обрабатывает клик по фигуре
        private RelayCommand cf;
        public RelayCommand ClickOnFigure
        {
            get
            {
                return cf ??
                (cf = new RelayCommand(obj =>
                {

                    if (model != null) model.Click(obj.ToString()); //Передаем имя фигуры в модель	

                }));
            }
        }


        // Команда обрабатывает нажатие кнопки Новая Игра
        private RelayCommand newGame;
        public RelayCommand NewGame
        {
            get
            {
                return newGame ??
                (newGame = new RelayCommand(obj =>
                {
                    // Метод ShowConnectWindow() создает событие EventShowConnectWindow()
                    // на которое подписан класс App.xaml.cs - и он открывает окно connectWindow
                    Events.ShowConnectWindow();

                }));
            }
        }

        // Команда обрабатывает нажатие кнопки Настройки Сети
        private RelayCommand localNetConf;
        public RelayCommand LocalNetConf
        {
            get
            {
                return localNetConf ??
                (localNetConf = new RelayCommand(obj =>
                {
                    // Метод ShowLocalNetConfWindow() создает событие EventShowLocalNetConfWindow()
                    // на которое подписан класс App.xaml.cs - и он открывает окно localNetConfWindow                    
                    Events.ShowLocalNetConfWindow();

                }));
            }
        }


        // Команда обрабатывает нажатие кнопки Сохранить игру
        private RelayCommand saveGame;
        public RelayCommand SaveGame
        {
            get
            {
                return saveGame ??
                (saveGame = new RelayCommand(obj =>
                {
                    SaveFile.SaveGame(model.Figures, model.selectedColor, model.activeColor);
                    model.server.Send("Save!");

                }));
            }
        }

        // Команда обрабатывает нажатие кнопки Загрузить игру
        private RelayCommand loadGame;
        public RelayCommand LoadGame
        {
            get
            {
                return loadGame ??
                (loadGame = new RelayCommand(obj =>
                {


                    StartGame("load");


                }));
            }
        }



        // Команда обрабатывает нажатие кнопки Выход
        private RelayCommand exit;
        public RelayCommand Exit
        {
            get
            {
                return exit ??
                (exit = new RelayCommand(obj =>
                {
                    // Метод ExitGame создает событие Exit на которое подписан класс App.xaml.cs - и он закрывает все окна                   
                    Events.ExitGame();

                }));
            }
        }


        void ReShowLightSelectedFigure()
        {

            OnPropertyChanged("SelectedFigureVisibility"); //Обновляем значения 	
            OnPropertyChanged("SelectedFigure_X");
            OnPropertyChanged("SelectedFigure_Y");

        }


        void ReShowMessage()
        {

            OnPropertyChanged("InformationMessageProperty");

        }





        // Метод перерисовывает фигуры на доске
        void ReShowFigures()
        {

            // Перерисовываем фигуры на доске
            for (int x = 0; x < model.Figures.Count; x++)
            {

                OnPropertyChanged(model.Figures[x].PropNameForX); //Обновляем значения
                OnPropertyChanged(model.Figures[x].PropNameForY);
                if (model.Figures[x].Icon == null) OnPropertyChanged(model.Figures[x].Name); //Обновляем изображение по новым координатам

            }

            OnPropertyChanged("StepStatus");
            model.RemoveFigure(); //Удаляем фигуру из коллекции в модели

        }



        void EndGame()
        {

            model.FigureYesNoSelected -= ReShowLightSelectedFigure; //Подписываемся на событие выбра фигуры

            model.EventNextStep -= ReShowFigures; //Подписываемся на событие завершения хода при котором вызывается метод перерисовывающий фигуры

            model.EventEndModelGame -= EndGame; //Подписываемся на событие окончания игры 

            InformationMessage.NewInformationMessage -= ReShowMessage; //Подписываемся на событие

            //Events.EventStartNewGame -= StartNewGame;
            model = null;

        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

        }




        // Метод начинает игру (новую или загружает сохраненную игру если colorOrLoad == "load" )
        void StartGame(string colorOrLoad)
        {

            model = new ChessModel(colorOrLoad);

            model.FigureYesNoSelected += ReShowLightSelectedFigure; //Подписываемся на событие выбра фигуры

            model.EventNextStep += ReShowFigures; //Подписываемся на событие завершения хода при котором вызывается метод перерисовывающий фигуры

            model.EventEndModelGame += EndGame; //Подписываемся на событие окончания игры

            InformationMessage.NewInformationMessage += ReShowMessage; //Подписываемся на событие


            if (model.selectedColor == "White")
            {
                DigitOnRow2 = "8";
                DigitOnRow3 = "7";
                DigitOnRow4 = "6";
                DigitOnRow5 = "5";
                DigitOnRow6 = "4";
                DigitOnRow7 = "3";
                DigitOnRow8 = "2";
                DigitOnRow9 = "1";

                CharOnColumn1 = "A";
                CharOnColumn2 = "B";
                CharOnColumn3 = "C";
                CharOnColumn4 = "D";
                CharOnColumn5 = "E";
                CharOnColumn6 = "F";
                CharOnColumn7 = "G";
                CharOnColumn8 = "H";
            }

            if (model.selectedColor == "Black")
            {
                DigitOnRow2 = "1";
                DigitOnRow3 = "2";
                DigitOnRow4 = "3";
                DigitOnRow5 = "4";
                DigitOnRow6 = "5";
                DigitOnRow7 = "6";
                DigitOnRow8 = "7";
                DigitOnRow9 = "8";

                CharOnColumn1 = "H";
                CharOnColumn2 = "G";
                CharOnColumn3 = "F";
                CharOnColumn4 = "E";
                CharOnColumn5 = "D";
                CharOnColumn6 = "C";
                CharOnColumn7 = "B";
                CharOnColumn8 = "A";
            }


            RotateAngle = model.selectedColor == "White" ? Int32.Parse("0") : Int32.Parse("180");


            OnPropertyChanged("BlackPawn1");
            OnPropertyChanged("BlackPawn2");
            OnPropertyChanged("BlackPawn3");
            OnPropertyChanged("BlackPawn4");
            OnPropertyChanged("BlackPawn5");
            OnPropertyChanged("BlackPawn6");
            OnPropertyChanged("BlackPawn7");
            OnPropertyChanged("BlackPawn8");
            OnPropertyChanged("BlackRook1");
            OnPropertyChanged("BlackRook2");
            OnPropertyChanged("BlackBishop1");
            OnPropertyChanged("BlackBishop2");
            OnPropertyChanged("BlackHorse1");
            OnPropertyChanged("BlackHorse2");
            OnPropertyChanged("BlackQueen");
            OnPropertyChanged("BlackKing");

            OnPropertyChanged("WhitePawn1");
            OnPropertyChanged("WhitePawn2");
            OnPropertyChanged("WhitePawn3");
            OnPropertyChanged("WhitePawn4");
            OnPropertyChanged("WhitePawn5");
            OnPropertyChanged("WhitePawn6");
            OnPropertyChanged("WhitePawn7");
            OnPropertyChanged("WhitePawn8");
            OnPropertyChanged("WhiteRook1");
            OnPropertyChanged("WhiteRook2");
            OnPropertyChanged("WhiteBishop1");
            OnPropertyChanged("WhiteBishop2");
            OnPropertyChanged("WhiteHorse1");
            OnPropertyChanged("WhiteHorse2");
            OnPropertyChanged("WhiteQueen");
            OnPropertyChanged("WhiteKing");

            ReShowFigures(); // Перерисовываем фигуры на доске   

            OnPropertyChanged("SelectedFigureVisibility"); //Обновляем значения 

        }

        // Конструктор
        public BoardWindowVM()
        {

            // Подписка на событие начала новой игры
            Events.EventStartNewGame += StartGame;

        }




    }
}
