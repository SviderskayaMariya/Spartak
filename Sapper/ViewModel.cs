using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Sapper
{
    class Player
    {
        public string name;
        public DateTime bdate;
        public int height;
        public int weight;
        public string nationality;
        public string role;
        public int number;
        public Player(string q, DateTime a, int b, int c, string d, string e, int f)
        {
            name = q;
            bdate = a;
            height = b;
            weight = c;
            nationality = d;
            role = e;
            number = f;
        }
        public string InfoPlayer()
        {
            return $"Игрок: {name.Trim()},     Дата рождения: {bdate.ToShortDateString()},     Рост: {height},     Вес: {weight},     Гражданство: {nationality},     Амплуа: {role},     Номер: {number}.";
        }
    }
    internal class ViewModel : INotifyPropertyChanged
    {
        private string _info;
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }
        public Command LoadPlayer { get; set; } = new Command();
        public ViewModel()
        {
            LoadPlayer.Function = GetPlayer;
        }
        private void GetPlayer(object obj)
        {
            if (obj == null || !(obj is string)) return;
            Player player = null;
            using (SqlConnection connect = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = Спартак; Integrated Security = True"))
            {
                SqlCommand load = new SqlCommand($"select [name], birthday, height, weight, nationality, role, number from spartak where [key] = {(string)obj}", connect);
                SqlDataReader reader;
                try
                {
                    connect.Open();
                    reader = load.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(load);
                    connect.Close();
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    DataRow r = table.Rows[0];
                    player = new Player((string)r.ItemArray[0], (DateTime)r.ItemArray[1], (int)r.ItemArray[2], (int)r.ItemArray[3], (string)r.ItemArray[4], (string)r.ItemArray[5], (int)r.ItemArray[6]);
                }
                catch
                {
                    MessageBox.Show("Нет данных об игроке");
                    return;
                }
            }
            if (player == null) return;
            Info = player.InfoPlayer();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    internal class Command : ICommand
    {
        public Action<object> Function { get; set; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Function(parameter);
        }
    }
}