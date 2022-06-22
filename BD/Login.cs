using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace BD
{
    public partial class Login : Form{

        public Login(){
            InitializeComponent();
        }

        public string Role;
        public string connectionString;

        static string md5(string text)
        {
            return md5(Encoding.UTF8.GetBytes(text));
        }

        static string md5(byte[] bytes)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(bytes);
                var sb = new StringBuilder(hashBytes.Length * 2);
                for (int i = 0; i < hashBytes.Length; i++) sb.AppendFormat("{0:x2}", hashBytes[i]);
                return sb.ToString();
            }
        }

        public void button1_Click(object sender, EventArgs e) {
            if (textLogin.Text != string.Empty || textPassword.Text != string.Empty)
            {
                connectionString = @"Data Source=SHIRONIUGO\SQLEXPRESS;Initial Catalog=MenuRestaurant;User Id = " + textLogin.Text + "; Password = " + textPassword.Text + "; ";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand sql; SqlDataReader reader;
                try
                {
                    con.Open();
                    sql = new SqlCommand("exec sp_helpuser '" + textLogin.Text + "';", con);
                    reader = sql.ExecuteReader();
                    reader.Read();
                    Role = reader.GetString(1);
                    reader.Close();
                    con.Close();
                    using (MenuRestaurantEntities basa = new MenuRestaurantEntities()) 
                    {
                        var vhod = basa.User
                                    .Where(c => c.Login == textLogin.Text)
                                    .Select(c => new { User = c.ID_User, Sail = c.Salt, Password = c.Password });
                        foreach(var login in vhod)
                        {
                            if (md5(md5(login.Sail) + md5(textPassword.Text)) == login.Password)
                            {
                                Restoran restoran = new Restoran()
                                {
                                    Width = 1200,
                                    Height = 800,
                                    StartPosition = FormStartPosition.CenterScreen
                                };
                                restoran.User = login.User;
                                restoran.Role = Role;
                                restoran.connectionString = connectionString;
                                restoran.menuStrip1.Update();
                                switch (Role)
                                {
                                    case "db_owner":
                                        {
                                            restoran.администрированиеToolStripMenuItem.Visible = true;
                                            restoran.рецептыToolStripMenuItem.Visible = true;
                                            restoran.ингредентыToolStripMenuItem.Visible = true; break;
                                        }
                                    case "Povar":
                                        {
                                            restoran.рецептыToolStripMenuItem.Visible = true;
                                            restoran.ингредентыToolStripMenuItem.Visible = true; break;
                                        }
                                    default: { break; }
                                }
                                restoran.Show();
                                Hide();
                            }
                            else MessageBox.Show("Ошибка логина или пароля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        } 
    }
}