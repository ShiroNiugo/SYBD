using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    public partial class Admin : Form
    {
        public Admin(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        private string connectionString;

        public string salt
        {
            get
            {
                string sol = "";
                Random rnd = new Random();
                int len = rnd.Next(5, 10);
                for (int i = 0; i < len; i++)
                {
                    sol += Convert.ToString((char)rnd.Next(33, 126));
                }
                sol = sol.Replace('"','R');
                return sol;
            }
        }

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

        void ITEMS()
        {
            roleItems.Items.Clear();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;
            con.Open();
            command = new SqlCommand("select Name from Role order by ID_Role asc;", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                roleItems.Items.Add(reader[0]);
            }
            reader.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;
            if (addButton.Checked) //добаление
            {
                con.Open();
                string login = string.Empty, user = string.Empty, loginBD = string.Empty;
                command = new SqlCommand(
                    "SELECT name FROM master.sys.server_principals WHERE name = '"+textLogin.Text+ "'; ", con);
                reader = command.ExecuteReader();
                if (reader.HasRows) { reader.Read(); login = reader.GetString(0); reader.Close(); }
                con.Close();
                con.Open();
                command = new SqlCommand(
                    "SELECT name FROM sys.database_principals WHERE name = '" +textLogin.Text+"' ;", con);
                reader = command.ExecuteReader();
                if (reader.HasRows) { reader.Read();user = reader.GetString(0); reader.Close(); }
                con.Close();
                con.Open();
                command = new SqlCommand(
                    "select Login from [User] where Login = '" +textLogin.Text+"';", con);
                reader = command.ExecuteReader();
                if (reader.HasRows) { reader.Read(); loginBD = reader.GetString(0); reader.Close(); }
                con.Close();

                string sql = "";
                if (textLogin.Text != string.Empty && textPassword.Text != string.Empty && roleItems.Text != "Выберите роль")
                {
                    if (login == string.Empty && user == string.Empty && loginBD == string.Empty)
                    {
                        sql = "EXEC sp_addlogin " + textLogin.Text + ", " + textPassword.Text + ", 'MenuRestaurant' ;" +
                            "EXEC sp_adduser " + textLogin.Text + ", " + textLogin.Text + ", ";
                        switch (roleItems.SelectedIndex + 1)
                        {
                            case 1: { sql += "'db_owner' ; EXEC sp_addsrvrolemember '"+textLogin.Text+"', 'sysadmin';"; break; }
                            case 2: { sql += "'Povar' ; "; break; }
                            case 3: { sql += "'User' ; "; break; }
                        }
                        sql += "EXECUTE pol " + (roleItems.SelectedIndex + 1) + ", '" + textLogin.Text + "', '" + md5(md5(salt) + md5(textPassword.Text)) + "', '"+ salt + "';";// в СУБД
                        con.Open();
                        command = new SqlCommand(sql, con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    else { MessageBox.Show("Логин уже используются!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else { MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            if (editButton.Checked) //изменение
            {
                try
                {
                    if (checkLogin.Checked && textPassword.Text != string.Empty)// логин
                    {
                        con.Open();
                        command = new SqlCommand(
                            "update [User] set Login = '" + textNewLogin.Text + "' where Login = '" + textLogin.Text + "';" +
                            "ALTER USER " + textLogin.Text + " WITH NAME = " + textNewLogin.Text + ";" +
                            "ALTER LOGIN " + textLogin.Text + " WITH NAME = " + textNewLogin.Text + ";" +
                            "EXEC sp_change_users_login 'Auto_Fix', '" + textNewLogin.Text + "', NULL, '" + textPassword.Text + "';", con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    else if(checkLogin.Checked && textPassword.Text == string.Empty) MessageBox.Show("Заполните пароль!");

                    if (checkPassword.Checked && !checkLogin.Checked)// пароль
                    {
                        con.Open();
                        command = new SqlCommand(
                            "update [User] set Password = '" + md5(md5(salt) + md5(textPassword.Text)) + "', Salt = '" + salt + "' where Login = '" + textLogin.Text + "';" +
                            "ALTER LOGIN " + textLogin.Text + " WITH PASSWORD = '" + textPassword.Text + "'; ", con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    else if(checkPassword.Checked && checkLogin.Checked)
                    {
                        con.Open();
                        command = new SqlCommand(
                            "update [User] set Password = '" + textPassword.Text + "' where Login = '" + textNewLogin.Text + "';" +
                            "ALTER LOGIN " + textNewLogin.Text + " WITH PASSWORD = '" + textPassword.Text + "';", con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                }
                catch { MessageBox.Show("Ошибка изменения пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            if (deleteButton.Checked) //Удаление
            {
                try
                {
                    con.Open();
                    command = new SqlCommand(
                        "delete from [User] where Login= '" + textLogin.Text + "';" +
                        "EXEC sp_dropuser " + textLogin.Text + ";" +
                        "EXEC sp_droplogin " + textLogin.Text + ";", con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Ошибка удаления пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            UPDATE();
        }

        void UPDATE()
        {
            ITEMS();
            dgvUsers.Rows.Clear();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand command = new SqlCommand("select t2.Name, Login, Password from [User] t1 join Role t2 on t1.ID_Role = t2.ID_Role", con);
            SqlDataReader reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                dgvUsers.Rows.Add(reader[0], reader[1], reader[2]);
                i++;
            }
            reader.Close();
            con.Close();
            cler();
        }

        void cler()
        {
            foreach (TextBox textBox in Controls.OfType<TextBox>())
                textBox.Text = string.Empty;
            roleItems.Text = "Выберите роль";
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            UPDATE();
            addButton.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            int a = radioButton.TabIndex;
            if (a!=18) {
                foreach (CheckBox checkBox in Controls.OfType<CheckBox>())
                    checkBox.Enabled = false;
            }
            else {
                foreach (CheckBox checkBox in Controls.OfType<CheckBox>())
                    checkBox.Enabled = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.Value = new String('*', 10);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLogin.Checked) label4.Visible = textNewLogin.Visible = textNewLogin.Enabled = true;
            else { label4.Visible = textNewLogin.Visible = textNewLogin.Enabled = false; textNewLogin.Text = ""; }

            if (checkPassword.Checked) { label3.Text = "Новый пароль"; }
            else { label3.Text = "Пароль"; }
        }
    }
}