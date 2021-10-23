namespace Chess

{
    public class ConnectWindowVM
    {

        public delegate void Deleg(int a);

        Connect connect;

        public int CheckStatusWhite { set; get; }
        public int CheckStatusBlack { set; get; }

        // Команда обрабатывает нажатие кнопки Ok
        private RelayCommand pressOkButton;
        public RelayCommand PressOkButton
        {
            get
            {
                return pressOkButton ??
                (pressOkButton = new RelayCommand(obj =>
                {

                    connect.Ok(CheckStatusWhite.ToString() + " " + CheckStatusBlack.ToString());

                }));
            }
        }



        public ConnectWindowVM()
        {

            connect = new Connect();

        }





    }


}
