using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BD
{
    public partial class MenuRezeptov : Form
    {
        public MenuRezeptov(string connectionString, string Role, int User)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.Role = Role;
            this.User = User;
        }
        public string sql, connectionString, Role;
        public int User, ID_Rez, ID_Blud = 0;

        // Заполнить combobos
        void ITEMS()
        {
            comboIngred.Items.Clear();
            comboEdIzm.Items.Clear();
            comboCateg.Items.Clear();

            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                foreach (Ingred ingred in basa.Ingred.OrderBy(r => r.Name))
                    comboIngred.Items.Add(ingred.Name);
                foreach (Ed_izm ed_Izm in basa.Ed_izm.OrderBy(r => r.Name))
                    comboEdIzm.Items.Add(ed_Izm.Name);
                foreach (Categ categ in basa.Categ.OrderBy(r => r.Name))
                    comboCateg.Items.Add(categ.Name);
            }
        }

        // Узнать кол-во блюд в меню
        private int Count
        {
            get
            {
                using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
                {
                    return basa.Categ.Count();
                }
            }
        }

        // Заполение формы при загрузке
        private void Rezepts_Load(object sender, EventArgs e)
        {
            UPDATE();
            ITEMS();
            comboEdIzm.SelectedIndex = 0;
        }

        // Заполнение таблицы с ингредиентами
        private void button1_Click(object sender, EventArgs e)
        {
            if (textName.Text == string.Empty && textCena.Text == string.Empty && textPyt.Text == string.Empty && comboCateg.Text == string.Empty)
                MessageBox.Show("Поля на создание блюда не заполнены!");
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command; SqlDataReader reader;
                dgvSostRezept.Rows.Clear();
                con.Open();
                command = new SqlCommand(
                    "Select ID_Ingred from Ingred " +
                    "where Name like '" + comboIngred.SelectedItem + "';", con);
                reader = command.ExecuteReader();
                reader.Read();
                int id_ing = reader.GetInt32(0);
                reader.Close();
                con.Close();
                con.Open();
                command = new SqlCommand(
                    "Select ID_Ingred from Sost_rez " +
                    "where ID_Ingred = " + id_ing + " " +
                    "and ID_Rezept = " + ID_Rez + ";", con);
                reader = command.ExecuteReader();
                int id_ing_rez = 0;
                if (reader.HasRows)
                {
                    reader.Read();
                    id_ing_rez = reader.GetInt32(0);
                    reader.Close();
                }
                con.Close();
                 
                con.Open();
                if (id_ing != id_ing_rez) {
                    sql = "insert into Sost_rez(ID_Rezept, ID_Ingred, Kol, Ves) " +
                        "values(" + ID_Rez + ", " + id_ing + ", " + (int)nudKol.Value + ", replace('" + nudVes.Value + "', ',','.') );";
                }
                else
                {
                    sql = "update Sost_rez set Kol = "+ (int)nudKol.Value + ", Ves = replace('" + nudVes.Value + "', ',','.') " +
                        "where ID_Rezept = " + ID_Rez + " and ID_Ingred = " + id_ing + " ;";
                }
                command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();

                con.Open();
                command = new SqlCommand(
                "EXECUTE IngRez " + ID_Rez + ";", con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dgvSostRezept.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
                }
                reader.Close();
                con.Close();
            }
        }

        //загрузка картинки +-
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog() { 
                Filter = "JPG Image (.jpg)|*.jpg|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
                textPyt.Text = Path.Combine(Application.StartupPath.Replace(@"\bin\Debug", @"\"), "Resources") + @"\" + open.SafeFileName;

            }
            //SaveFileDialog save = new SaveFileDialog();
            Bitmap bmp = new Bitmap(open.FileName);
            bmp.Save(textPyt.Text);
            
        }

        //Очистка полей +
        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox text in splitContainer1.Panel1.Controls.OfType<TextBox>())
                text.Clear();
            foreach (ComboBox combo in splitContainer1.Panel1.Controls.OfType<ComboBox>())
                combo.SelectedIndex = -1;
            foreach (NumericUpDown numeric in splitContainer1.Panel1.Controls.OfType<NumericUpDown>())
                numeric.Value = 1;
            dgvSostRezept.Rows.Clear();
            rtbSposPrig.Clear();
            pictureBox1.Image = null; ID_Rez = ID_Blud = 0;
            buttonAddRez.Text = "Добавить рецепт";
        }

        //Узнать id рецепта по id блюда +
        int Rez(int id)
        {
            int id_rez = 0;
            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var s = basa.Rezepts.Where(t => t.ID_Blud_and_nap == id)
                    .Select(t => t.ID_Rezept);
                foreach (var r in s) id_rez = r;
            }
            return id_rez;
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

        // Проверка наличия ингредиента
        private int ProvercaPoNameIng(string name)
        {
            int id = 0;
            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var s = basa.Ingred.Where(t => t.Name == name)
                    .Select(t => t.ID_Ingred);
                foreach (var r in s) id = r;
            }
            return id;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            int id_ing = ProvercaPoNameIng((string)comboIngred.SelectedItem);
            double ves = 1;
            int id_ed_izm = 1;
            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var s = basa.Ingred.Where(t => t.ID_Ingred == id_ing)
                    .Select(t => new { ves = t.Ves, id_ed_izm = t.ID_Ed_izm });
                foreach (var r in s) { ves = r.ves; id_ed_izm = r.id_ed_izm; }
                comboEdIzm.SelectedIndex = id_ed_izm - 1;
                if (comboEdIzm.SelectedIndex == 1 || comboEdIzm.SelectedIndex == 3)
                {
                    nudVes.DecimalPlaces = 3;
                    nudVes.Increment = 0.01M;
                }
                else { nudVes.DecimalPlaces = 0; }
                nudVes.Value = (decimal)((int)nudKol.Value * ves);
            }
        }

        //Удаление из таблицы ингредиентов блюда +
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (buttonAddEdit.Enabled == true)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command;

                var i = 0;
                string o = dgvSostRezept.Rows[e.RowIndex].Cells[i].Value.ToString();

                if (e.ColumnIndex == 4)
                {
                    con.Open();
                    command = new SqlCommand("EXECUTE DelIdRez " + ID_Rez + ", " + o + ";", con);
                    command.ExecuteNonQuery();
                    con.Close();
                    dgvSostRezept.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        //Узнать id блюда по имени +
        int ID_BLUD(string name)
        {

            int id = 0;
            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var s = basa.Blud_and_nap.Where(t => t.Name == name)
                    .Select(t => t.ID_Blud_and_nap);
                foreach (var r in s) id = r;
            }
            return id;
        }

        //Добавление/Обновление блюда +
        private void button2_Click(object sender, EventArgs e)
        {

            string ResPath = Path.Combine(Application.StartupPath.Replace(@"\bin\Debug", @"\"), "Resources")+@"\";
            if (rtbSposPrig.Text == string.Empty && textName.Text == string.Empty && textCena.Text == string.Empty && textPyt.Text == string.Empty && comboCateg.Text == string.Empty)
            { 
                MessageBox.Show("Поля на создание блюда не заполнены!");
                buttonAddUpdate.Enabled = false;
            }
            else
            {
                buttonAddUpdate.Enabled = true;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command;
                if (ID_Blud > 0)
                {
                    con.Open();
                    command = new SqlCommand(
                        "update Blud_and_nap set ID_Categ =" + (comboCateg.SelectedIndex + 1) + ", Foto = '" + textPyt.Text+"', Name = '"+textName.Text+"', Cena = "+ Convert.ToInt32(textCena.Text) +
                        " where ID_Blud_and_nap = " + ID_Blud + " ;", con);
                    command.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    command = new SqlCommand(
                        "update Rezepts set Spos_prig = '" + rtbSposPrig.Text + "' where ID_Blud_and_nap = " + ID_Blud + " ;", con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    con.Open();
                    command = new SqlCommand(
                        "insert into Blud_and_nap(ID_Categ, Foto, Name, Cena, block) " + 
                        "values( " + (comboCateg.SelectedIndex + 1) + ", '" + textPyt.Text + "', '" + textName.Text + "', " + Convert.ToInt32(textCena.Text) + ", 0) ;", con);
                    command.ExecuteNonQuery();
                    con.Close();

                    int id = ID_BLUD(textName.Text);

                    con.Open();
                    command = new SqlCommand(
                        "insert into Rezepts(ID_Blud_and_nap, Spos_prig) "
                        + "values( " + id + ", '" + rtbSposPrig.Text + "') ;", con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            UPDATE();
        }

        //Заполнение меню выбора блюд
        void UPDATE()
        {
            blockeAndUnblock();
            splitContainer1.Panel2.Controls.Clear();
            int t = 0, kol = 0;
            string sql, sql2;
            kol = Count;

            SqlConnection con2 = new SqlConnection(connectionString);
            for (int c = 1; c <= kol; c++)
            {
                sql2 = "EXECUTE kolCateg2 " + c + "; ";
                con2.Open();
                SqlCommand table2 = new SqlCommand(sql2, con2);
                SqlDataReader reader2 = table2.ExecuteReader();

                while (reader2.Read())
                {
                    Label label1 = new Label
                    {
                        Text = reader2[0].ToString(),
                        Font = new Font("Segoe Print", 16, FontStyle.Regular),
                        Size = new Size(splitContainer1.Panel2.Width, 30)
                    };
                    TableLayoutPanel tableLayout = new TableLayoutPanel
                    {
                        RowCount = (reader2.GetInt32(1) / 2) + (reader2.GetInt32(1) % 2),
                        ColumnCount = 2,
                        CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble
                    };

                    int qwe = reader2.GetInt32(1), asd = 1;

                    SqlConnection con = new SqlConnection(connectionString);
                    for (int col = 0; col < tableLayout.ColumnCount; col++)
                    {
                        tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                        for (int row = 0; row < tableLayout.RowCount; row++)
                        {
                            if (col == 0)
                            {
                                tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
                                sql = "EXECUTE bludAndNap2 " + c + "; ";
                                con.Open();
                                SqlCommand table = new SqlCommand(sql, con);
                                SqlDataReader reader = table.ExecuteReader();
                                while (reader.Read())
                                {
                                    Panel panel = new Panel
                                    {
                                        Dock = DockStyle.Fill
                                    };
                                    if (qwe >= asd)
                                    {
                                        PictureBox picture = new PictureBox
                                        {
                                            Location = new Point(0, 0),
                                            Size = new Size(150, 100),
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Image = Image.FromFile(reader[1].ToString()),
                                            Padding = new Padding(5),
                                            BorderStyle = BorderStyle.Fixed3D
                                        };
                                        Panel panel1 = new Panel
                                        {
                                            Location = new Point(picture.Width, 5),
                                            Size = new Size(150, 100)
                                        };
                                        Label label2 = new Label
                                        {
                                            Dock = DockStyle.Fill,
                                            Text = "" + reader[0].ToString(),
                                            Font = new Font("Segoe Print", 12, FontStyle.Regular)
                                        };
                                        Label label3 = new Label
                                        {
                                            Location = new Point(0, picture.Location.Y + picture.Height + 10),
                                            Text = "Цена: " + reader[2].ToString(),
                                            Font = new Font("Segoe Print", 12, FontStyle.Regular)
                                        };
                                        Button button = new Button
                                        {
                                            Size = new Size(150, 50),
                                            Location = new Point(0, label3.Location.Y + label3.Height + 5),
                                            Text = "Выбрать",
                                            Font = new Font("Segoe Print", 12, FontStyle.Regular),
                                            Name = reader[3].ToString()
                                        };
                                        button.Click += butclick;
                                        tableLayout.Controls.Add(panel, col, row);
                                        panel.Controls.Add(picture);
                                        panel.Controls.Add(panel1);
                                        panel1.Controls.Add(label2);
                                        panel.Controls.Add(label3);
                                        panel.Controls.Add(button);
                                        asd++;
                                    }
                                }
                                reader.Close();
                                con.Close();
                            }
                        }
                    }
                    tableLayout.Size = new Size(splitContainer1.Panel2.Width, tableLayout.RowCount * (int)tableLayout.RowStyles[0].Height);

                    label1.Location = new Point(0, t + 5);
                    splitContainer1.Panel2.Controls.Add(label1);
                    tableLayout.Location = new Point(0, label1.Location.Y + label1.Height + 5);
                    splitContainer1.Panel2.Controls.Add(tableLayout);
                    t = tableLayout.Location.Y + tableLayout.Height + 20;
                }
                reader2.Close();
                con2.Close();
            }
        }

        //Выбор блюда +
        public void butclick(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            ID_Blud = Convert.ToInt32(bt.Name);
            ID_Rez = Rez(ID_Blud);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;

            dgvSostRezept.Rows.Clear();
            con.Open();
            command = new SqlCommand("EXECUTE RezOcn " + ID_Blud + "; ", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                textName.Text = reader.GetString(0);
                comboCateg.SelectedIndex = reader.GetInt32(1)-1;
                textCena.Text = reader.GetInt32(2).ToString();
                textPyt.Text = reader.GetString(3);
                pictureBox1.Image = Image.FromFile(reader.GetString(3));
                rtbSposPrig.Text = reader.GetString(4);
            }
            reader.Close();
            con.Close();

            con.Open();
            command = new SqlCommand("EXECUTE RezIng " + ID_Blud + "; ", con);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    dgvSostRezept.Rows.Add(reader[0].ToString(), reader[1], 
                        reader[2], reader[3].ToString());
                    i++;
                }
                reader.Close();
            }
            con.Close();
            buttonAddRez.Text = "Обновить рецепт";
        }

        // Закрытие формы
        public void butrez(object sender, EventArgs e)
        {
            Close();
        }
        

    }
}
