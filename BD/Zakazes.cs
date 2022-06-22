using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BD
{
    public partial class Zakazes : Form
    {
        public Zakazes(string connectionString, string Role, int User)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.Role = Role;
            this.User = User;
        }

        public string connectionString, Role;
        public int User, id_blud;

        int but = 0;
        string sql;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSostZakaz.Rows.Clear();
            int a = (int)dgvZakaz.CurrentRow.Cells[0].Value;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;
            con.Open();
            command = new SqlCommand(
                "select t2.Name, count(t2.Name), t1.Komment from Sost_zakaz t1 " +
                "join Blud_and_nap t2 on t1.ID_Blud_and_nap=t2.ID_Blud_and_nap " +
                "where t1.ID_Zakaz = " + a + " group by t2.Name, t1.Komment;", con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    dgvSostZakaz.Rows.Add(reader.GetString(0), reader.GetInt32(1), reader.IsDBNull(2) ? "Без комментариев" : reader.GetString(2));
                    i++;
                }
                reader.Close();
            }
            con.Close();
        }

        private void Zakaz_Load(object sender, EventArgs e)
        {
            UPDATE();
        }

        int ID_Zak
        {
            get{
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command; SqlDataReader reader;
                con.Open();
                command = new SqlCommand(
                    "Select top 100 ID_Zakaz from Zakaz where ID_User = " + User + " and Oplacheno = 0 order by ID_Zakaz desc;", con);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int nom = reader.GetInt32(0);
                    reader.Close();
                    con.Close();
                    return nom;
                }
                else return 0; }
        }

        void UPDATE()
        {
            dgvZakaz.Rows.Clear();
            dgvSostZakaz.Rows.Clear();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;

            if (dgvZakaz.RowCount == 0)
            {
                con.Open();
                command = new SqlCommand(
                    "EXECUTE delSostZak " + ID_Zak + "; " +
                    "delete from Zakaz where ID_Zakaz = " + ID_Zak + " and Date_zakaz is NULL", con);
                command.ExecuteReader();
                con.Close();
            }

            con.Open();
            switch (but)
            {
                case 0: default: { sql = "select ID_Zakaz, Sost_gotov, Ob_stoim, Oplacheno, Date_zakaz from Zakaz where ID_User = " + User + " ;"; break; }
                case 1: { sql = "select ID_Zakaz, Sost_gotov, Ob_stoim, Oplacheno, Date_zakaz from Zakaz where Sost_gotov = 0;"; break; }
            }
            command = new SqlCommand(sql, con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    dgvZakaz.Rows.Add(reader.GetInt32(0),reader.GetBoolean(1),reader.GetInt32(2),
                        reader.GetBoolean(3), reader.IsDBNull(4) ? "Заказ не был подтвержден" : reader.GetDateTime(4).ToString());
                    i++;
                }
                reader.Close();
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            but = button.TabIndex;
            UPDATE();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvZakaz.RowCount > 0)
            {
                int a = (int)dgvZakaz.CurrentRow.Cells[0].Value;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command;
                con.Open();
                command = new SqlCommand(
                    "update Zakaz set Sost_gotov = 1 where ID_Zakaz = " + a + " ;", con);
                command.ExecuteReader();
                con.Close();
                UPDATE();
            }
        }
        
    }
}
