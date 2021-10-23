
using System.Windows;


namespace Chess
{



    public partial class BoardWindow : Window
    {

        public BoardWindow()
        {
            InitializeComponent();
            BoardWindowVM boardWindowVM = new BoardWindowVM();
            DataContext = boardWindowVM;

        }



    }
}