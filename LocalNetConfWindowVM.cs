using MessageBox = System.Windows.MessageBox;

namespace Chess

{
    public class LocalNetConfWindowVM
    {

        public string MyIpAddr { set; get; }
        public string MyPort { set; get; }
        public string EnemyIpAddr { set; get; }
        public string EnemyPort { set; get; }

        // Команда обрабатывает нажатие кнопки Сохранить
        private RelayCommand pressSaveButton;
        public RelayCommand PressSaveButton
        {
            get
            {
                return pressSaveButton ??
                (pressSaveButton = new RelayCommand(obj =>
                {

                    SaveFile.SaveConf(MyIpAddr, MyPort, EnemyIpAddr, EnemyPort);

                    Events.CloseLocalNetConfWindow(); //Вызываем метод закрывающий окно

                }));
            }
        }



        public LocalNetConfWindowVM()
        {

            string[] path_from_file = ReadFile.ReadConf();

            MyIpAddr = path_from_file[0];
            MyPort = path_from_file[1];

            EnemyIpAddr = path_from_file[2];
            EnemyPort = path_from_file[3];

        }

        //~LocalNetConfWindowVM()
        //{
        //    MessageBox.Show("local net conf window vm close");
        //}



    }


}
