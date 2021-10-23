using System;
using System.IO;

namespace Chess
{

    static class ReadFile
    {

        // Метод читает файл сохраненной игры (save.cfg) и возвращает массив данных о фигурах
        public static string[] ReadGame()

        {

            StreamReader sr = new StreamReader("save.cfg");

            // Узнаем количество фигур. Читаем первую строку и переводим в int - это количество фигур 
            int countOfFigures = Int32.Parse(sr.ReadLine());

            // Необходимый массив равен 1 строка под количество фигур + 1 строка показывающая активного игрока + количество фигур * 3 строки (имя, координаты x, y)    	 
            string[] dataSavedGame = new string[3 + countOfFigures * 3];

            // Добавляем количество фигур в массив
            dataSavedGame[0] = countOfFigures.ToString();

            // Добавляем в массив какими фигурами играет игрок (цвет)
            dataSavedGame[1] = sr.ReadLine();

            // Добавляем в массив цвет активного игрока
            dataSavedGame[2] = sr.ReadLine();

            // Читаем построчно параметры всех фигур и записываем их в массив. 
            for (int x = 3; x <= countOfFigures * 3 + 2; x++)
            {
                dataSavedGame[x] = sr.ReadLine();
            }

            sr.Close();

            return dataSavedGame; // Возвращаем массив

        }

        public static string[] ReadConf()
        {


            // Если файл pathes.cfg будет не найден - создаем его
            //	if (!File.Exists("pathes.cfg")) {SavePathes( DriveInfo.GetDrives()[0].Name, "Dark");}


            // Читаем pathes.cfg
            StreamReader sr = new StreamReader("localnet.cfg");
            string[] path_from_file = new string[4];

            for (int x = 0; x <= 3; x++)
            {

                path_from_file[x] = sr.ReadLine();


            }

            sr.Close();

            return path_from_file;

            //Если директории из файла  pathes.cfg не существуют - ставим корень первого диска			
            //if (!Directory.Exists(path_from_file[0])) path_from_file[0] = DriveInfo.GetDrives()[0].Name;
            //if (!Directory.Exists(path_from_file[1])) path_from_file[1] = DriveInfo.GetDrives()[0].Name;

            //Pathes.LeftPath = path_from_file[0];
            //Pathes.RightPath = path_from_file[1];

            //Если параметр цвета темы в файле pathes.cfg изменен или не существует - ставим Light		
            //if (path_from_file[2] == "Light" || path_from_file[2] == "Dark" ) Pathes.Theme = path_from_file[2];
            //else Pathes.Theme = "Light";




        }



    }
}
