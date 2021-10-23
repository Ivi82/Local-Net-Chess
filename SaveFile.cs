
using System.Collections.ObjectModel;
using System.IO;

namespace Chess
{

    static class SaveFile
    {


        public static void SaveConf(string myIp, string myPort, string enemyIpAddr, string enemyPort)

        {

            StreamWriter sw = new StreamWriter("localnet.cfg", false);
            sw.WriteLine(myIp);
            sw.WriteLine(myPort);
            sw.WriteLine(enemyIpAddr);
            sw.WriteLine(enemyPort);
            sw.Close();
        }

        public static void SaveGame(ObservableCollection<Figure> Figures, string selectedColor, string activeColor)

        {

            StreamWriter sw = new StreamWriter("save.cfg", false);

            sw.WriteLine(Figures.Count);
            sw.WriteLine(selectedColor);
            sw.WriteLine(activeColor);

            for (int x = 0; x < Figures.Count; x++)

            {
                sw.WriteLine(Figures[x].Name);
                sw.WriteLine(Figures[x].x);
                sw.WriteLine(Figures[x].y);

            }


            sw.Close();
        }

    }
}
