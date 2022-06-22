using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace BD
{
    public partial class Main : Form
    {
        private string connectionString;
        private int id_blud;
        private string Role;
        int User;

        public Main(string connectionString, string Role, int User)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.Role = Role;
            this.User = User;
            UPDATE();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            NUL();
            if (dgvSost_zakaza.RowCount > 0)
            {
                buttonValide.Enabled = false;
            }else buttonValide.Enabled = true;
            UPDATE();
        }

        private void dgvSostZakaz_AutoSizeRowsModeChanged(object sender, DataGridViewAutoSizeModeEventArgs e)
        {
            dgvSost_zakaza.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
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

        private int BlockeAndUnblock2(int id_blud)
        {
            SqlConnection con1 = new SqlConnection(connectionString);
            SqlCommand command1; SqlDataReader reader1;
            con1.Open();
            command1 = new SqlCommand("EXECUTE BlockAndUnblock2 " + id_blud + " ;", con1);
            reader1 = command1.ExecuteReader();
            int r = 0;
            if (reader1.HasRows)
            {
                reader1.Read();
                r = reader1.GetInt32(0);
                reader1.Close();
            }
            con1.Close();
            return r;
        }

        // Кнопка заказать
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvSost_zakaza.RowCount > 0)
            {
                int id_zak = ID_Zak;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command; SqlDataReader reader;
                con.Open();
                command = new SqlCommand("EXECUTE ID_IngKol " + id_zak + ";", con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id_ingred = reader.GetInt32(0);
                    double summaVesaZak = reader.GetDouble(1);

                    SqlConnection con1 = new SqlConnection(connectionString);
                    SqlCommand command1; SqlDataReader reader1;
                    con1.Open();
                    command1 = new SqlCommand(
                        "select ID_Ingred, Ves from Jurn_ychet_ingred " +
                        "where ID_Type_ychet = 1 and ID_Ingred = " + id_ingred + " order by ID_Ingred asc", con1);
                    reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        double summaVesaJurn = reader1.GetDouble(1);
                        string sql;
                        summaVesaZak = (summaVesaJurn >= summaVesaZak) ? summaVesaZak : (summaVesaJurn < summaVesaZak) ? summaVesaZak : 0;
                        SqlConnection con2 = new SqlConnection(connectionString);
                        SqlCommand command2;
                        if (summaVesaJurn <= summaVesaZak)
                        {
                            summaVesaZak -= summaVesaJurn;
                            sql = "EXECUTE UpIngJurn " + id_ingred + ",'" + summaVesaJurn + "';";
                        }
                        else
                        {
                            sql = "EXECUTE UpIngZak " + id_ingred + ",'" + summaVesaZak + "';";
                        }
                        con2.Open();
                        command2 = new SqlCommand(sql, con2);
                        command2.ExecuteReader();
                        con2.Close();
                    }
                    reader1.Close();
                    con1.Close();
                }
                reader.Close();
                con.Close();

                if (label5.Text == "Дата заказа: Заказ не был подтвержден")
                {
                    con.Open();
                    command = new SqlCommand("EXECUTE UpZakData "+User+", "+id_zak+";", con);
                    command.ExecuteReader();
                    con.Close();
                }
                buttonValide.Enabled = false;
            }
            UPDATE();
        }

        // Кнопка обновить
        private void button2_Click(object sender, EventArgs e)
        {
            UPDATE();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Oplata();
            buttonValide.Enabled = true;
            UPDATE();
        }

        // Узнает кол-во категории
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
                command1 = new SqlCommand("EXECUTE spisanie " + pred + ", " + id + ", '" + data + "' ;", con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            reader.Close();
            con.Close();
        }

        // Составление меню
        private void UPDATE()
        {
            Spisanie();
            Zakaz();
            blockeAndUnblock();
            splitContainer1.Panel2.Controls.Clear();
            int t = 0, kol = 0;
            string sql, sql2;
            kol = Count;

            SqlConnection con2 = new SqlConnection(connectionString);
            for (int c = 1; c <= kol; c++)
            {
                sql2 = @"EXECUTE kolCateg "+c+";";
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
                                tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
                                sql = "EXECUTE bludoAndNap " + c +";";
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
                                            Size = new Size(panel.Width - 50, 100)
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
                                            Size = new Size(140, 50),
                                            Location = new Point(0, label3.Location.Y + label3.Height + 5),
                                            Text = "Выбрать",
                                            Font = new Font("Segoe Print", 12, FontStyle.Regular),
                                            Name = reader[3].ToString()
                                        };
                                        tableLayout.Controls.Add(panel, col, row);
                                        panel.Controls.Add(picture);
                                        panel.Controls.Add(panel1);
                                        panel1.Controls.Add(label2);
                                        panel.Controls.Add(label3);
                                        panel.Controls.Add(button);
                                        
                                        if (buttonValide.Enabled && !buttonPay.Enabled)
                                        { button.Enabled = true; button.Click += Butclick; }
                                        else { button.Enabled = false; }

                                        switch (Role)
                                        {
                                            case "db_owner":
                                            case "Povar":
                                                {
                                                    Button button2 = new Button
                                                    {
                                                        Size = new Size(140, 50),
                                                        Location = new Point(button.Location.X + button.Width + 10, button.Location.Y),
                                                        Text = "Рецепт",
                                                        Font = new Font("Segoe Print", 12, FontStyle.Regular),
                                                        Name = reader[3].ToString()
                                                    };
                                                    panel.Controls.Add(button2);
                                                    button2.Click += Butclick2;
                                                    break;
                                                }
                                            default: break;
                                        }
                                        asd++;
                                    }
                                }
                                reader.Close();
                                con.Close();
                            }
                        }
                    }
                    tableLayout.Size = new Size(splitContainer1.Panel2.Width-20, tableLayout.RowCount * (int)tableLayout.RowStyles[0].Height);
                   
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

        // Узнать id заказа через user и оплату
        private int ID_Zak
        {
            get
            {
                int nom = 0;
                using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
                {
                    var i = basa.Zakaz
                        .Where(u => u.ID_User == User && u.Oplacheno == false)
                        .Select(t => new { id = t.ID_Zakaz});
                    foreach (var o in i) nom = o.id;
                }
                return nom;
            }
        }

        // Добавление блюда в заказ
        private void Butclick(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            id_blud = Convert.ToInt32(bt.Name);

            int id = BlockeAndUnblock2(id_blud);
            if (id == 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command; SqlDataReader reader;
                int NomZak = ID_Zak;

                if (NomZak == 0)
                {
                    con.Open();
                    command = new SqlCommand("EXECUTE addZakaz "+User+";", con);
                    command.ExecuteReader();
                    con.Close();
                    NomZak = ID_Zak;
                }

                con.Open();
                command = new SqlCommand("EXECUTE addSostZakaz "+NomZak+", "+ id_blud + ";", con);
                command.ExecuteNonQuery();
                con.Close();

                con.Open();
                command = new SqlCommand(
                    "Select Cena from Blud_and_nap where ID_Blud_and_nap = " + id_blud + " ;", con);
                reader = command.ExecuteReader();
                reader.Read();
                int sum = reader.GetInt32(0);
                reader.Close();
                con.Close();

                con.Open();
                command = new SqlCommand(
                    "Update Zakaz set Ob_stoim += " + sum + " where ID_Zakaz = " + NomZak + " ;", con);
                command.ExecuteNonQuery();
                con.Close();

                con.Open();
                command = new SqlCommand(
                    "Select Ob_stoim from Zakaz where ID_User = " + User + " and Oplacheno = 0;", con);
                reader = command.ExecuteReader();
                reader.Read();
                int sumM = reader.GetInt32(0);
                reader.Close();
                con.Close();

                label1.Text += NomZak;
                label3.Text = "Общая стоимость заказа: " + sumM;
                Zakaz();
            }
            else
            {
                MessageBox.Show("Извините больше нельзя взять это блюдо", "Извинение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UPDATE();
            }
        }

        // Посмотреть рецепт
        private void Butclick2(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            id_blud = Convert.ToInt32(bt.Name);

            if (Application.OpenForms.OfType<MenuRezeptov>().Count() == 1)
                Application.OpenForms.OfType<MenuRezeptov>().First().Close();
            MenuRezeptov rezepts = new MenuRezeptov(connectionString, Role, User)
            {
                Size = new Size(400, 680),
                StartPosition = FormStartPosition.CenterScreen,
                Visible = true
            };
            foreach (TextBox text in rezepts.splitContainer1.Panel1.Controls.OfType<TextBox>())
            {
                text.ReadOnly = true;
                text.BackColor = Color.White;
            }
            foreach (ComboBox combo in rezepts.splitContainer1.Panel1.Controls.OfType<ComboBox>())
                combo.Enabled = false;
            foreach (NumericUpDown numeric in rezepts.splitContainer1.Panel1.Controls.OfType<NumericUpDown>())
                numeric.Enabled = false;
            foreach (Button button in rezepts.splitContainer1.Panel1.Controls.OfType<Button>())
                button.Enabled = false;
            rezepts.MdiParent = this.MdiParent;
            rezepts.rtbSposPrig.ReadOnly = true;

            Button button1 = new Button
            {
                Text = "Закрыть",
                Font = new Font("Segoe Print", 16, FontStyle.Regular),
                Size = new Size(rezepts.buttonClear.Width, 45),
                Location = new Point(rezepts.buttonClear.Location.X, rezepts.buttonClear.Location.Y + rezepts.buttonClear.Height + 5)
            };
            button1.Click += rezepts.butrez;
            rezepts.splitContainer1.Panel1.Controls.Add(button1);
            rezepts.splitContainer1.Dock = DockStyle.Fill;
            rezepts.ID_Blud = id_blud;
            rezepts.butclick(sender, e);
            rezepts.Show();
        }

        private void Sost_gotov()
        {
            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var gotovo = basa.Zakaz.Where(t => t.ID_User == User && t.Oplacheno == false)
                    .Select(t => t.Sost_gotov);
                foreach (var gt in gotovo)
                {
                    if (gt == true) { buttonPay.Enabled = true; }
                    else { buttonPay.Enabled = false; }
                }
            }
        }

        // Узнать о текущем заказе
        private void Zakaz()
        {
            dgvSost_zakaza.Rows.Clear();

            using (MenuRestaurantEntities basa = new MenuRestaurantEntities())
            {
                var s = basa.Zakaz.Where(t => t.ID_User == User && t.Oplacheno == false)
                    .Select(t => new
                    {
                        NomZak = t.ID_Zakaz, Got = t.Sost_gotov, sum = t.Ob_stoim, Opl = t.Oplacheno, Dat = t.Date_zakaz
                    });
                foreach (var ss in s)
                {
                    label1.Text = "Номер заказа: " + ss.NomZak;
                    string got = (ss.Got == false) ? "готовиться" : "приготовлено";
                    label2.Text = "Состояние готовности: " + got;
                    label3.Text = "Общая стоимость: " + ss.sum;
                    string opl = (ss.Opl == false && ss.Got == false) ? "заказ еще не готов" : "официант скоро будет";
                    label4.Text = "Оплачено: " + opl;
                    string dat = (ss.Dat == null) ? "Заказ не был подтвержден" : ss.Dat.ToString();
                    label5.Text = "Дата заказа: " + dat;
                }
            }

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand command; SqlDataReader reader;
            con.Open();
            command = new SqlCommand("EXECUTE BludKolKom "+User+";", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                dgvSost_zakaza.Rows.Add(reader[0], reader[1], reader[2]);
            }
            reader.Close();
            con.Close();
            Sost_gotov();
        }

        //Оплата заказа
        private void Oplata()
        {
            SqlConnection con1 = new SqlConnection(connectionString);
            SqlCommand command1;

            con1.Open();
            command1 = new SqlCommand("EXECUTE UpZakOplata "+User+";", con1);
            command1.ExecuteNonQuery();
            con1.Close();
            buttonPay.Enabled = false;
        }

        //Удаление заказа если не он не был подтвержден
        private void NUL()
        {
            if (dgvSost_zakaza.RowCount == 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command;
                con.Open();
                command = new SqlCommand(
                    "EXECUTE delSostZak " + ID_Zak + ";" +
                    "delete from Zakaz where ID_Zakaz = " + ID_Zak + " and Date_zakaz is NULL", con);
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        // Убрать блюдо из заказа
        private void dataGridView1_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (buttonValide.Enabled == true)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command; SqlDataReader reader;

                int id = ID_Zak;
                if (e.ColumnIndex == 3)
                {
                    string name = dgvSost_zakaza.CurrentRow.Cells[0].Value.ToString();
                    con.Open();
                    command = new SqlCommand("EXECUTE IdSostZakTop1 '"+name+"', " + ID_Zak + "; ", con);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = reader.GetInt32(0);
                        reader.Close();
                    }
                    con.Close();

                    con.Open();
                    command = new SqlCommand(
                        "delete from Sost_zakaz where ID_Sost_zakaz = " + id + "; ", con);
                    command.ExecuteReader();
                    con.Close();

                    id = ID_Zak;

                    con.Open();
                    command = new SqlCommand("EXECUTE ZakObStoim '"+name+"', " + id + "; ", con);
                    command.ExecuteReader();
                    con.Close();

                    dgvSost_zakaza.Rows.RemoveAt(e.RowIndex);
                }
            }
            else { MessageBox.Show("Невозможно убрать", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            Zakaz();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            NUL();
        }

        private void dgvSost_zakaza_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSost_zakaza.Columns[0].Index)
            {
                var cell = dgvSost_zakaza.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = dgvSost_zakaza.Rows[e.RowIndex].Cells[2].Value.ToString();
                cell.Style.WrapMode = DataGridViewTriState.True;
            }
        }

        //Узнать id блюда по имени +
        int ID_BLUD
        {
            get
            {
                int id = 0;
                using (MenuRestaurantEntities basa = new MenuRestaurantEntities()) 
                {
                    var s = basa.Blud_and_nap.Where(t => t.Name == (string)dgvSost_zakaza.CurrentRow.Cells[0].Value)
                        .Select(t=>t.ID_Blud_and_nap);
                    foreach(var r in s) id = r;
                }
                return id;
            }
        }

        //Добавить комментарий
        public void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Application.OpenForms.OfType<Form1>().Count() == 1)
                Application.OpenForms.OfType<Form1>().First().Close();
            Form1 form = new Form1
            {
                Size = new Size(300, 275),
                Text = "Пожелания для блюда",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill
            };
            Label label = new Label
            {
                Text = "Комментарий",
                Location = new Point(0, 10),
                Size = new Size(panel.Width, 20)
            };
            RichTextBox richTextBox = new RichTextBox
            {
                Location = new Point(5, label.Location.Y + label.Height + 5),
                Size = new Size(form.Width - 25, 150),
            };
            Button button = new Button
            {
                Text = "Добавить комментарий",
                Location = new Point(richTextBox.Location.X+55, richTextBox.Location.Y + richTextBox.Height + 5),
                Size = new Size(220, 45)
            };
            Label label1 = new Label
            {
                Text = "Кол-во",
                Location = new Point(richTextBox.Location.X, richTextBox.Location.Y + richTextBox.Height + 5),
                Size = new Size(50, 20)
            };
            NumericUpDown numericUpDown = new NumericUpDown
            {
                Minimum = 1,
                Maximum = (int)dgvSost_zakaza.CurrentRow.Cells[1].Value,
                Location = new Point(richTextBox.Location.X, label1.Location.Y + label1.Height),
                Width = 50
            };

            form.Controls.Add(panel);
            panel.Controls.Add(label);
            panel.Controls.Add(richTextBox);
            panel.Controls.Add(button);
            panel.Controls.Add(label1);
            panel.Controls.Add(numericUpDown);
            var komment = (dgvSost_zakaza.CurrentRow.Cells[2].Value != DBNull.Value) ? (string)dgvSost_zakaza.CurrentRow.Cells[2].Value : string.Empty;
            
            richTextBox.Text = komment;

            form.MdiParent = this.MdiParent;
            form.Show();
            button.Click += registrationButton_Click;

            void registrationButton_Click(object sender1, EventArgs e1)
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand command;
                con.Open();
                command = new SqlCommand(
                    "EXECUTE UpKomSostZak "+(int)numericUpDown.Value +", " + 
                    richTextBox.Text+ "," +User+ ", " +ID_Zak+ ", " +ID_BLUD+";", con);
                command.ExecuteReader();
                con.Close();
                form.Close();
                Zakaz();
            }
        }
    }

    public partial class Form1 : Form { }
}