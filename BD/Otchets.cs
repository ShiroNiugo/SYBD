using Microsoft.Office.Interop.Excel;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using Button = System.Windows.Forms.Button;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Linq;

namespace BD
{
    public partial class Otchets : Form
    {
        public Otchets(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        SaveFileDialog SaveFileDialog = new SaveFileDialog {
            Filter = "Excel File (*.xlsx)|*.xls;*.xlsx;*.xlsm |PDF File (*.pdf)|*.pdf"
        };
        DataSet MyDataSet = new DataSet();
        SqlDataAdapter adapter; SqlCommand command;

        _Application app;
        public string connectionString;
        public int id_blud;
        int nom;
        string nameList;

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите восстановить БД?","Восстановление БД",MessageBoxButtons.YesNo , MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand command = new SqlCommand(
                    @" USE [master]
                    ALTER DATABASE [MenuRestaurant] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                    RESTORE DATABASE [MenuRestaurant] FROM  DISK = N'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\MenuRestaurant.bak' WITH  FILE = 6,  NOUNLOAD,  STATS = 5
                    ALTER DATABASE [MenuRestaurant] SET MULTI_USER
                    ", con);
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите сделать резервную копию БД?", "Создание резервной копии БД", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand command = new SqlCommand(
                    @"backup database MenuRestaurant 
                to disk = 'D:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\MenuRestaurant.bak';", con);
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount > 0 && dataGridView1.RowCount > 0)
            {
                app = new Application();
                _Workbook workbook = app.Workbooks.Add(Type.Missing);
                _Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.ActiveSheet;
                worksheet.Name = nameList;
                for (int i = 0; i < MyDataSet.Tables.Count; i++)
                {
                    switch (nom)
                    {
                        case 5:
                            {
                                worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                worksheet.Cells[1, 3] = MyDataSet.Tables[i].Columns[2].ColumnName;
                                break;
                            }
                        case 6:
                        case 8:
                            {
                                worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                worksheet.Cells[1, 3] = MyDataSet.Tables[i].Columns[2].ColumnName;
                                worksheet.Cells[1, 4] = MyDataSet.Tables[i].Columns[3].ColumnName;
                                break;
                            }
                        case 7:
                        case 9:
                            {
                                worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                worksheet.Cells[1, 3] = MyDataSet.Tables[i].Columns[2].ColumnName;
                                worksheet.Cells[1, 4] = MyDataSet.Tables[i].Columns[3].ColumnName;
                                worksheet.Cells[1, 5] = MyDataSet.Tables[i].Columns[4].ColumnName;
                                break;
                            }
                    }
                }
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[2, i] = dataGridView1[i - 1, 0].Value;
                    worksheet.Columns[i].ColumnWidth = 20;
                    worksheet.Cells[1, i].Interior.Color = Color.LightGray;
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        worksheet.Cells[i + 2, j + 1] = dataGridView1[j, i].Value;
                char a = 'A';
                string exRange = "A1:" + Convert.ToChar(a + (dataGridView1.Columns.Count - 1)) + (dataGridView1.RowCount + 1);
                worksheet.get_Range(exRange).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                worksheet.Columns.AutoFit();
                worksheet.get_Range(exRange).WrapText = true;
                worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }
            else MessageBox.Show("Выберите отчетность для печати", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void clearTab()
        {
            dataGridView1.DataSource = null;
            MyDataSet.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            nom = button.TabIndex;
            nameList = button.Text; 
            clearTab();
            SqlConnection con = new SqlConnection(connectionString);
            SaveFileDialog.FileName = string.Empty;
            con.Open();
            string sql = string.Empty;
            switch (nom)
            {
                case 5:
                    {
                        sql = "select t2.Name as 'Наименование', sum(t1.Ves) as 'Общий вес', t3.Name as 'Ед. изм.' " +
                            "from Jurn_ychet_ingred t1 " +
                            "join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred " +
                            "join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm " +
                            "where t1.ID_Type_ychet = 1 " +
                            "group by t2.Name, t3.Name order by t2.Name";
                        break;
                    }
                case 6:
                    {
                        sql = "select t4.Login as 'Пользователь', t3.Name as 'Блюдо или напиток', " +
                            "   t1.Date_zakaz as 'Ед. изм.', t2.Komment as 'Комментарий' " +
                            "from Zakaz t1 " +
                            "join Sost_zakaz t2 on t1.ID_Zakaz= t2.ID_Zakaz " +
                            "join Blud_and_nap t3 on t2.ID_Blud_and_nap = t3.ID_Blud_and_nap " +
                            "join [User] t4 on t1.ID_User=t4.ID_User " +
                            "where t1.Oplacheno = 1 order by t1.Date_zakaz";
                        break;
                    }
                case 7:
                    {
                        sql = "select t2.Name as 'Наименование',t1.Kol as 'Кол-во', " +
                                "sum(t1.Ves) as 'Общий вес', t3.Name as 'Ед. изм.', t1.Date_oper as 'Дата' " +
                            "from Jurn_ychet_ingred t1 " +
                            "join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred " +
                            "join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm " +
                            "where t1.ID_Type_ychet = 1 " +
                            "group by t2.Name, t1.Kol, t3.Name, t1.Date_oper " +
                            "order by t2.Name";
                        break;
                    }
                case 8:
                    {
                        sql = "select t2.Name as 'Наименование', count(t2.Name) as 'Кол-во', " +
                                "sum(t1.Ves) as 'Общий вес', t3.Name as 'Ед. изм.' " +
                            "from Jurn_ychet_ingred t1 " +
                            "join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred " +
                            "join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm " +
                            "where t1.ID_Type_ychet = 1 " +
                            "group by t2.Name, t3.Name " +
                            "order by t2.Name";
                        break;
                    }
                case 9:
                    {
                        sql = @"select t2.Name as 'Наименование',t1.Kol as 'Кол-во', 
                                sum(t1.Ves) as 'Общий вес', t3.Name as 'Ед. изм.', t1.Date_oper as 'Дата' 
                            from Jurn_ychet_ingred t1 
                            join Ingred t2 on t1.ID_Ingred = t2.ID_Ingred 
                            join Ed_izm t3 on t2.ID_Ed_izm = t3.ID_Ed_izm 
                            where t1.ID_Type_ychet = 2 
                            group by t2.Name, t1.Kol, t3.Name,t1.Date_oper 
                            order by t2.Name";
                        break;
                    }
                default: {clearTab(); break;}
            }
            if (sql != string.Empty) {
                command = new SqlCommand(sql, con);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(MyDataSet, nameList);
                dataGridView1.DataSource = MyDataSet.Tables[0];
            }
            con.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount > 0 && dataGridView1.RowCount > 0)
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveFileDialog.FilterIndex == 1)
                    {
                        app = new Application();
                        _Workbook workbook = app.Workbooks.Add(Type.Missing);
                        _Worksheet worksheet = null;
                        app.Visible = true;
                        worksheet = workbook.ActiveSheet;
                        worksheet.Name = nameList;
                        for (int i = 0; i < MyDataSet.Tables.Count; i++)
                        {
                            switch (nom)
                            {
                                case 5:
                                    {
                                        worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                        worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                        worksheet.Cells[1, 3]= MyDataSet.Tables[i].Columns[2].ColumnName;
                                        break;
                                    }
                                case 6:
                                case 8:
                                    {
                                        worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                        worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                        worksheet.Cells[1, 3] = MyDataSet.Tables[i].Columns[2].ColumnName;
                                        worksheet.Cells[1, 4] = MyDataSet.Tables[i].Columns[3].ColumnName;
                                        break;
                                    }
                                case 7:
                                case 9:
                                    {
                                        worksheet.Cells[1, 1] = MyDataSet.Tables[i].Columns[0].ColumnName;
                                        worksheet.Cells[1, 2] = MyDataSet.Tables[i].Columns[1].ColumnName;
                                        worksheet.Cells[1, 3] = MyDataSet.Tables[i].Columns[2].ColumnName;
                                        worksheet.Cells[1, 4] = MyDataSet.Tables[i].Columns[3].ColumnName;
                                        worksheet.Cells[1, 5] = MyDataSet.Tables[i].Columns[4].ColumnName;
                                        break;
                                    }
                            }
                        }
                        for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                        {
                            worksheet.Cells[2, i] = dataGridView1[i - 1, 0].Value;
                            worksheet.Columns[i].ColumnWidth = 20;
                            worksheet.Cells[1, i].Interior.Color = Color.LightGray;
                        }
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                worksheet.Cells[i + 2, j + 1] = dataGridView1[j, i].Value;

                        char a = 'A';
                        string exRange = "A1:" + Convert.ToChar(a + (dataGridView1.Columns.Count - 1)) + (dataGridView1.RowCount + 1);
                        worksheet.get_Range(exRange).Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        worksheet.Columns.AutoFit();
                        workbook.SaveAs(SaveFileDialog.FileName,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                            XlSaveAsAccessMode.xlExclusive,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        app.Quit();
                    }
                    else
                    {
                        Document doc = new Document();
                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(SaveFileDialog.FileName, FileMode.Create));
                        }
                        catch { MessageBox.Show("Данный файл уже используется","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        doc.Open();
                        BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                        for (int i = 0; i < MyDataSet.Tables.Count; i++)
                        {
                            PdfPTable table = new PdfPTable(MyDataSet.Tables[i].Columns.Count);
                            PdfPCell cell = new PdfPCell();

                            cell.Colspan = MyDataSet.Tables[i].Columns.Count;
                            cell.HorizontalAlignment = 1;
                            cell.Border = 0;
                            table.AddCell(cell);

                            for (int j = 0; j < MyDataSet.Tables[i].Columns.Count; j++)
                            {
                                cell = new PdfPCell(new Phrase(new Phrase(MyDataSet.Tables[i].Columns[j].ColumnName, font)));
                                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                table.AddCell(cell);
                            }

                            for (int j = 0; j < MyDataSet.Tables[i].Rows.Count; j++)
                            {
                                for (int k = 0; k < MyDataSet.Tables[i].Columns.Count; k++)
                                {
                                    table.AddCell(new Phrase(MyDataSet.Tables[i].Rows[j][k].ToString(), font));
                                }
                            }
                            doc.Add(table);
                        }
                        doc.Close();
                    }
                }
            }
            else MessageBox.Show("Выберите отчетность","Предупреждение",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
