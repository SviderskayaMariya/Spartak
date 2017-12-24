using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    class Player
    {
        public int key;
        public string name;
        public DateTime bdate;
        public int height;
        public int weight;
        public string nationality;
        public string role;
        public int number;
        public Player(int w, string q, DateTime a, int b, int c, string d, string e, int f)
        {
            key = w;
            name = q;
            bdate = a;
            height = b;
            weight = c;
            nationality = d;
            role = e;
            number = f;
        }
        public override string ToString()
        {
            return $"Игрок: {name.Trim()}, Дата рождения: {bdate.ToShortDateString()}, Рост: {height}, Вес: {weight}, Гражданство: {nationality}, Амплуа: {role}, Номер: {number}.";
        }
    }
}
