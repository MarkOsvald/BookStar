using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using Dashboard.Tables;

namespace Dashboard
{
    public class SqlClient
    {
        public static List<Books> LoadBooks()
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                var output = cnn.Query<Books>("select * from Books", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Books> LoadUserBooks(string username)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                var output = cnn.Query<Books>("select * from Books where Username like '" + username + "'", new DynamicParameters());
                return output.ToList();
            }
        }

        public static Books LoadBook(string book, string author, string rating, string year)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                var output = cnn.Query<Books>("select * from Books where Book like '" + book + "' and Year like '" + year + "' and Rating like '"
                    + rating + "' and Author like '" + author + "'");
                return output.First();
            }
        }

        public static void DeleteBookWithId(string book, string year, string rating, string author)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                cnn.Execute("delete from Books where Book like '" + book + "' and Year like '" + year + "' and Rating like '" 
                    + rating + "' and Author like '" + author + "'");
            }
        }

        public static void SaveBook(Books book)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                cnn.Execute("insert into Books (Author,Book,Year,Review,Username,Rating) values (@Author,@Book,@Year,@Review,@Username,@Rating)", book);
            }
        }
        public static void SaveUser(Users user)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                cnn.Execute("insert into Users (Username,Password) values (@Username,@Password)", user);
            }
        }
        public static void ChangePassword(Users user)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                cnn.Execute("update Users set Password = @Password where Username = @Username", user);
            }
        }

        public static bool LoginUser(Users user)
        {
            using (IDbConnection cnn = new SQLiteConnection("Data Source=./DemoDB.db;Version=3;"))
            {
                var output = cnn.Query<Users>("select * from Users where Username like '" + user.Username  + "' and Password like '" + user.Password + "'");
                return output.Any();
            }
        }

    }
}
