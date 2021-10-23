
using System.Windows;


namespace Chess
{



    public partial class LocalNetConfWindow : Window
    {

        public LocalNetConfWindow()
        {
            InitializeComponent();
            LocalNetConfWindowVM localNetConfWindowVM = new LocalNetConfWindowVM();
            DataContext = localNetConfWindowVM;

        }

        //~LocalNetConfWindow() 
        //{
        //    MessageBox.Show("local net conf window xaml cs close");

        //}

    }
}