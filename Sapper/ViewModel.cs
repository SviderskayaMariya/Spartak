using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sapper
{
<<<<<<< HEAD
=======
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
        //REVIEW: Почему бы не сделать это в перегрузке ToString()
        public string InfoPlayer()
        {
            return $"Игрок: {name.Trim()},     Дата рождения: {bdate.ToShortDateString()},     Рост: {height},     Вес: {weight},     Гражданство: {nationality},     Амплуа: {role},     Номер: {number}.";
        }
    }
    //REVIEW: Класс - в отдельный файл, не надо мешать
>>>>>>> dcfeacf43e6ad6cada3bc2b74f03e7fa7407c400
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
        private ImageSource _photo;
        public ImageSource Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }
        private List<string> _playersList;
        public List<string> PlayersList
        {
            get { return _playersList; }
            set
            {
                _playersList = value;
                OnPropertyChanged("PlayersList");
            }
        }
        public Command LoadPlayer { get; set; } = new Command();
        public ViewModel()
        {
            LoadPlayer.Function = GetPlayer;
            List<string> load = GetPlayersList();
            if (load != null) PlayersList = new List<string>(load);
        }
        private void GetPlayer(object obj)
        {
            if (obj == null || !(obj is string)) return;
            Player player = null;
<<<<<<< HEAD
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.sqlexpress))
=======
            //REVIEW: строку подключения - в настройки
            using (SqlConnection connect = new SqlConnection("Data Source =.\\SQLEXPRESS; Initial Catalog = Спартак; Integrated Security = True"))
>>>>>>> dcfeacf43e6ad6cada3bc2b74f03e7fa7407c400
            {
                SqlCommand load = new SqlCommand($"select [key], [name], birthday, height, weight, nationality, role, number from spartak where [name] like \'%{(string)obj}%\'", connect);
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
                    player = new Player((int)r.ItemArray[0], (string)r.ItemArray[1], (DateTime)r.ItemArray[2], (int)r.ItemArray[3], (int)r.ItemArray[4], (string)r.ItemArray[5], (string)r.ItemArray[6], (int)r.ItemArray[7]);
                }
                catch
                {
                    MessageBox.Show("Не загружается игрок");
                    return;
                }
                try
                {
                    Photo = new BitmapImage(new Uri($"Images/{player.key}.jpg", UriKind.Relative));
                }
                catch
                {
                    MessageBox.Show("Невозможно загрузить фото игрока");
                }
            }
            if (player == null) return;
            Info = player.ToString();
        }
        private List<string> GetPlayersList()
        {
            List<string> l = new List<string>();
            using (SqlConnection connect = new SqlConnection(Properties.Settings.Default.sqlexpress))
            {
                SqlCommand load = new SqlCommand("select [name] from spartak", connect);
                SqlDataReader reader;
                try
                {
                    connect.Open();
                    reader = load.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(load);
                    connect.Close();
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                        l.Add(((string)row.ItemArray[0]).Trim());
                }
                catch
                {
                    MessageBox.Show("Невозможно загрузить список игроков");
                    return null;
                }
                return l;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
<<<<<<< HEAD
=======
    //REVIEW: В отдельный файл
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
>>>>>>> dcfeacf43e6ad6cada3bc2b74f03e7fa7407c400
}