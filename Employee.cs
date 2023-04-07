using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner
{
    public class Employee
    {
        public int id;
        public string name;
        public string surname;
        public string login;
        public string email;
        public string password;
        public string date;
        public string phone;
        public string position;
        public Employee(int ID, string imie, string nazwisko, string Login, string Email, string Haslo, string Data, string nr, string Stanowisko)
        {
            id = ID;
            name = imie;
            surname = nazwisko;
            login = Login;
            email = Email;
            password = Haslo;
            date = Data;
            phone = nr;
            position = Stanowisko;
        }
    }
}
