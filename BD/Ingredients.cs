using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BD
{
	public partial class Ingredients : Form
	{
		public Ingredients(string connectionString)
		{
			InitializeComponent();
			this.connectionString = connectionString;
		}
		string connectionString;

        // Заполнить combobox
        private void ITEMS()
		{
			comboEdIzm.Items.Clear();
			comboIngred.Items.Clear();


			using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
			{
				foreach (Ingred ingred in basa.Ingred.OrderBy(r => r.Name))
					comboIngred.Items.Add(ingred.Name);
				foreach (Ed_izm ed_Izm in basa.Ed_izm.OrderBy(r => r.Name))
					comboEdIzm.Items.Add(ed_Izm.Name);
			}
		}

        private void UPDATE()
		{
            ITEMS();
            Spisanie();
            blockeAndUnblock();
			dgvJurnYchetIngred.Rows.Clear();
			dgvNehvat.Rows.Clear();

			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command; SqlDataReader reader;
			
			con.Open();
			command = new SqlCommand(
				@"select t2.Name, Kol, t1.Ves, t3.Name, t4.Name, Date_oper, t2.Pred_sroc_hran 
				from Jurn_ychet_ingred t1 
				join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred 
				join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm 
				join Type_ychet t4 on t1.ID_Type_ychet = t4.ID_Type_ychet 
				order by t4.Name, t2.Name ", con);
			reader = command.ExecuteReader();
			int i = 0;
            while (reader.Read())
            {
                dgvJurnYchetIngred.Rows.Add(reader[0], reader[1], reader[2], reader[3],
                                       reader[4], reader[5], reader[6]);
                i++;
            }
            con.Close();
			i = 0;
			con.Open();
			command = new SqlCommand(
			@"select ing.Name, sum(t1.Ves), ed.Name 
			from Sost_rez t1 
			join Ingred ing on ing.ID_Ingred = t1.ID_Ingred
			join Ed_izm ed on ed.ID_Ed_izm = ing.ID_Ed_izm 
			where t1.Ves >= all(select t2.Ves 
								from Jurn_ychet_ingred t2 
								where t2.ID_Type_ychet = 1 
								and t2.ID_Ingred = t1.ID_Ingred)
			group by ing.Name, ed.Name ;", con);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				dgvNehvat.Rows.Add(reader[0], reader[1], reader[2]);
				i++;
			}
			con.Close();

			con.Open();
			command = new SqlCommand(
			@"update t1 set t1.Kol -= (t1.Kol - CEILING(t1.Ves / t2.Ves)) 
			from Jurn_ychet_ingred t1 
			join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred 
			where t1.Kol > (t1.Ves / t2.Ves);", con);
			command.ExecuteNonQuery();
			con.Close();
		}

		void blockeAndUnblock()
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command; SqlDataReader reader;

			con.Open();
			command = new SqlCommand("select ID_Rezept from Rezepts", con);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				SqlConnection con1 = new SqlConnection(connectionString);
				SqlCommand command1; SqlDataReader reader1;
				con1.Open();
				command1 = new SqlCommand("EXECUTE ID_RezTop1 " + id + ";", con1);
				reader1 = command1.ExecuteReader();
				SqlConnection con2 = new SqlConnection(connectionString);
				SqlCommand command2;
				con2.Open();
				if (reader1.HasRows)
				{
					command2 = new SqlCommand("EXECUTE blocke " + id + ";", con2);
					command2.ExecuteReader();
					con2.Close();
				}
				else
				{
					command2 = new SqlCommand("EXECUTE unblocke " + id + ";", con2);
					command2.ExecuteReader();
					con2.Close();
				}
				reader1.Close();
				con1.Close();
			}
			reader.Close();
			con.Close();
		}


		private void Ingred_Load(object sender, EventArgs e)
		{
			UPDATE();
		}

		// Проверка наличия ингредиента
		private int ProvercaPoNameIng(string name)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command; SqlDataReader reader;

			con.Open();
			command = new SqlCommand("Select ID_Ingred from Ingred where Name like '" + name + "';", con);
			reader = command.ExecuteReader();
			int id;
			if (reader.HasRows)
			{
				reader.Read();
				id = reader.GetInt32(0);
				reader.Close();
				con.Close();
				return id;
			}
			else return 0;
		}

		// Добавление ингредиента
		private void button1_Click(object sender, EventArgs e)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command;
			if (comboIngred.Text != string.Empty && comboEdIzm.Text != string.Empty && nudSrocHran.Value != 0)
			{
				int id_ing = ProvercaPoNameIng(comboIngred.Text);
				if (id_ing == 0)
				{
					con.Open();
					command = new SqlCommand("insert into Ingred(Name, Ves, ID_Ed_izm, Pred_sroc_hran) " +
						"values('" + comboIngred.Text + "', " + (float)nudVes.Value +
						"," + (comboEdIzm.SelectedIndex + 1) + ", " + (int)nudSrocHran.Value + ");", con);
					command.ExecuteNonQuery();
					con.Close();
				}
				else
				{
					con.Open();
					command = new SqlCommand("insert into Jurn_ychet_ingred(ID_ingred, ID_Type_ychet, Date_oper, Kol, Ves) " +
						"values('" + id_ing + "', 1, GETDATE(), " + (int)nudKol.Value + ", " + (float)nudVes.Value + ");", con);
					command.ExecuteNonQuery();
					con.Close();
				}
			}
			else { 
				MessageBox.Show("Заполните все поля!","Предупреждение",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				foreach (NumericUpDown numericUpDown in Controls.OfType<NumericUpDown>())
					comboIngred.BackColor = comboEdIzm.BackColor = numericUpDown.BackColor = Color.Red;
			}
			UPDATE();
		}

		private void textBox1_Enter(object sender, EventArgs e)
		{
			comboIngred.BackColor = comboEdIzm.BackColor = Color.White;
			foreach (NumericUpDown numericUpDown in Controls.OfType<NumericUpDown>())
				numericUpDown.BackColor = Color.White;
		}

		private void Sort(string sql, string where, string sort, string asc)
		{
			dgvJurnYchetIngred.Rows.Clear();
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command;
			con.Open();
			command = new SqlCommand(sql + where + sort + asc, con);
			SqlDataReader reader = command.ExecuteReader();
			int i = 0;
			while (reader.Read())
			{
				dgvJurnYchetIngred.Rows.Add(reader[0], reader[1], reader[2], reader[3],
									   reader[4], reader[5], reader[6]);
				i++;
			}
			con.Close();
		}

		// Списание ингредиентов
		private void Spisanie()
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand command; SqlDataReader reader;

			con.Open();
			command = new SqlCommand(
				@"select t1.ID_Ingred, t1.Pred_sroc_hran, t2.Date_oper from Ingred t1 
				join Jurn_ychet_ingred t2 on t1.ID_Ingred = t2.ID_Ingred 
				order by ID_Ingred asc", con);
			reader = command.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				int pred = reader.GetInt32(1);
				string data = reader.GetDateTime(2).ToString();
					SqlConnection con1 = new SqlConnection(connectionString);
					SqlCommand command1;
					con1.Open();
					command1 = new SqlCommand("EXECUTE spisanie " + pred + ", " + id + ", '"+ data +"' ;", con1);
					command1.ExecuteNonQuery();
					con1.Close();
			}
			reader.Close();
			con.Close();
		}

		// Очистка сортировки
		private void button2_Click(object sender, EventArgs e)
		{
			comboTypeSort.SelectedIndex = 
			comboPoryadoc.SelectedIndex =
			comboTypeYchet.SelectedIndex = -1;
			comboTypeSort.Text = "Выберите тип сортировки";
			comboPoryadoc.Text = "Порядок сортировки";
			comboTypeYchet.Text = "Тип учета";
			UPDATE();
		}

		// Сортировка
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			int a = comboTypeSort.SelectedIndex + 1, 
				b = comboPoryadoc.SelectedIndex + 1,
				c = comboTypeYchet.SelectedIndex + 1;
			string sql =
				"select t2.Name, t1.Kol, t1.Ves, t3.Name, t4.Name, t1.Date_oper, t2.Pred_sroc_hran " +
				"from Jurn_ychet_ingred t1 " +
				"join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred " +
				"join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm " +
				"join Type_ychet t4 on t1.ID_Type_ychet = t4.ID_Type_ychet ", 
				   where = "", sort, asc;
			switch (a)
			{
				case 1: default: { sort = "order by t4.Name, t2.Name "; break; }
				case 2: { sort = "order by t4.Name, t1.Kol "; break; }
				case 3: { sort = "order by t4.Name, t1.Ves "; break; }
				case 4: { sort = "order by t4.Name, t3.ID_Ed_izm "; break; }
				case 5: { sort = "order by t4.Name, t1.Date_oper "; break; }
			}
			switch (b)
			{
				case 1: default: { asc = "asc;"; break; }
				case 2: { asc = "desc;"; break; }
			}
			switch (c)
			{
				case 1: { where = "where t4.Name = 'Приход' "; break; }
				case 2: { where = "where t4.Name = 'Списание' "; break; }
				default: { break; }
			}
			Sort(sql, where, sort, asc);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			buttonAdd.Text = "Добавить ингредиент";
			if (comboIngred.Text != string.Empty)
			{
				foreach (NumericUpDown numericUpDown in splitContainer1.Panel1.Controls.OfType<NumericUpDown>())
					comboEdIzm.Enabled = numericUpDown.Enabled = true;
				SqlConnection con = new SqlConnection(connectionString);
				SqlCommand command; SqlDataReader reader;
				con.Open();
				double ves = 1;
				int id_ed_izm = 1, pred = 1;
				int id_ing = ProvercaPoNameIng(comboIngred.Text);
				if (id_ing != 0)
				{
					buttonAdd.Text = "Заказать";
					command = new SqlCommand(
						"select Ves, ID_Ed_izm, Pred_sroc_hran from Ingred where ID_Ingred = " + id_ing + ";", con);
					reader = command.ExecuteReader();
					if (reader.HasRows)
					{
						reader.Read();
						ves = reader.GetInt32(0);
						id_ed_izm = reader.GetInt32(1);
						pred = reader.GetInt32(2);
						reader.Close();
					}
					con.Close();
				}
				nudSrocHran.Value = pred;
				if (id_ed_izm == 2 || id_ed_izm == 4)
				{
					nudVes.DecimalPlaces = 3;
					nudVes.Increment = 0.01M;
				}
				else { nudVes.DecimalPlaces = 0; }
				nudVes.Value = (decimal)ves;
				comboEdIzm.SelectedIndex = id_ed_izm - 1;
				nudVes.Value = (decimal)((int)nudKol.Value * ves);
			}
			else { 
				foreach (NumericUpDown numericUpDown in splitContainer1.Panel1.Controls.OfType<NumericUpDown>())
					comboEdIzm.Enabled = numericUpDown.Enabled = false;
			}
		}
		private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			comboIngred.Text = (string)dgvNehvat.CurrentRow.Cells[0].Value;
		}

	}
}