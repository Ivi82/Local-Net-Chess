namespace Chess
{
    public delegate void Delegate();
    public delegate void ParamDelegate(string x);

    static public class Events
    {

        static public event Delegate EventShowConnectWindow;
        static public event Delegate EventCloseConnectWindow;
        static public event Delegate EventShowLocalNetConfWindow;
        static public event Delegate EventCloseLocalNetConfWindow;
        static public event Delegate Exit;
        static public event ParamDelegate EventStartNewGame;

        public static void ShowConnectWindow()
        {
            EventShowConnectWindow();
        }

        public static void ShowLocalNetConfWindow()
        {
            EventShowLocalNetConfWindow();
        }

        public static void CloseLocalNetConfWindow()
        {

            EventCloseLocalNetConfWindow();
        }



        public static void ExitGame()
        {
            Exit();
        }

        public static void StartNewGame(string x)
        {

            EventCloseConnectWindow();
            EventStartNewGame(x);
        }




    }
}
