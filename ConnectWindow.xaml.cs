
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace Chess
{



    public partial class ConnectWindow : Window
    {

        public ConnectWindowVM connectWindowVM;


        public ConnectWindow()
        {
            InitializeComponent();
            connectWindowVM = new ConnectWindowVM();
            DataContext = connectWindowVM;

        }

        //~ConnectWindow()
        //{
        //    MessageBox.Show("Connect window close");
        //}

    }
}