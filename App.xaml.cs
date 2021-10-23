using System;
using System.Windows;

namespace Chess
{
    // Этот класс управляет всеми окнами
    public partial class App : Application
    {

        BoardWindow boardWindow;
        ConnectWindow connectWindow;
        LocalNetConfWindow localNetConfWindow;

        // Метод закрывает все окна
        void StopProgramm(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        void StopProgramm()
        {
            Application.Current.Shutdown();
        }


        // Метод открывает окно ConnectWindow
        void ShowConnectWindow()
        {
            connectWindow = new ConnectWindow();
            connectWindow.Topmost = true; // Делаем окно поверх остальных

            connectWindow.Show();
        }


        // Метод закрывает окно ConnectWindow
        void CloseConnectWindow()
        {
            connectWindow.Close();
        }

        // Метод открывает окно LocalNetConfWindow
        void ShowLocalNetConfWindow()
        {
            localNetConfWindow = new LocalNetConfWindow();
            localNetConfWindow.Topmost = true; // Делаем окно поверх остальных
            localNetConfWindow.Show();
        }


        // Метод закрывает окно LocalNetConfWindow
        void CloseLocalNetConfWindow()
        {

            localNetConfWindow.Close();
        }





        // Kонструктор класса
        App()
        {

            Events.EventShowConnectWindow += ShowConnectWindow; // Подписка на события открытия\закрытия окна ConnectWindow
            Events.EventCloseConnectWindow += CloseConnectWindow;

            Events.EventShowLocalNetConfWindow += ShowLocalNetConfWindow; // Подписка на события открытия\закрытия окна LocalNetConfWindow
            Events.EventCloseLocalNetConfWindow += CloseLocalNetConfWindow;

            Events.Exit += StopProgramm; // Подписка на событие нажатия кнопки Exit

            boardWindow = new BoardWindow();

            // Если окно boardWindow закроется но останутся не закрытые диалоговые окна - тогда они тоже закроются
            boardWindow.Closed += StopProgramm;

            boardWindow.Show(); // Отображаем основное окно - шахматную доску

        }



    }

}