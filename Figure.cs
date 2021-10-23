
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Chess
{

    class Figure : IFalseAndMessage // internal - доступен из любого места текущей сборки. недоступен для др.программ и сборок 
    {
        internal BitmapFrame Icon { get; set; }
        internal int x { get; set; }
        internal int y { get; set; }
        internal string Type { get; set; }
        internal string PropNameForX;
        internal string PropNameForY;
        internal string Color;
        internal string Name;



        internal virtual bool Attack(ObservableCollection<Figure> Figures, int number, string methodMode)
        {
            return false;
        }

        internal virtual bool IsStepOn(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {
            return false;
        }

        internal virtual bool Step(ObservableCollection<Figure> Figures, int x, int y, string methodMode)
        {
            return false;
        }

        public bool FalseAndMessage(string message, string methodMode)
        {
            if (methodMode == "Active") InformationMessage.Set(message);
            return false;
        }



    }





}
