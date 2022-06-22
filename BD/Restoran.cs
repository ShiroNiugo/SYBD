using System;
using System.Linq;
using System.Windows.Forms;

namespace BD
{
    public partial class Restoran : Form
    {
        public string Role;
        public int User;
        public string connectionString;

        public Restoran()
        {
            InitializeComponent();
        }

        private void рецептыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<MenuRezeptov>().Count() == 1)
            {
                Application.OpenForms.OfType<MenuRezeptov>().First().Dispose();
            }
            MenuRezeptov v = new MenuRezeptov(connectionString, Role, User);
            v.MdiParent = this;
            v.Show();
        }

        private void опрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовая работа\nЦель работы: автоматизировать работу движения " +
                            "\nи учета блюд, ингредиентов, напитков, \nденег, заказов и т.д. для удобства работы " +
                            "\nс информационной системой.\nВыполнил Капустин Д.Е.\nГруппа ЭИС-36", "О программе");
        }

        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Admin>().Count() == 1)
            {
                Application.OpenForms.OfType<Admin>().First().Dispose();
            }
            Admin v = new Admin(connectionString);
            v.MdiParent = this;
            v.Show();
        }

        private void отчетностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Otchets>().Count() == 1)
            {
                Application.OpenForms.OfType<Otchets>().First().Dispose();
            }
            Otchets v = new Otchets(connectionString);
            v.MdiParent = this;
            v.Show();
        }

        private void заказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Zakazes>().Count() == 1)
            {
                Application.OpenForms.OfType<Zakazes>().First().Dispose();
            }
            Zakazes v = new Zakazes(connectionString, Role, User);
            v.MdiParent = this;
            v.Update();
            switch (Role)
            {
                case "User":{v.Height = 450; break;}
                default: { break; }
            }
            v.Show();
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Ingredients>().Count() == 1)
            {
                Application.OpenForms.OfType<Ingredients>().First().Dispose();
            }
            Ingredients v = new Ingredients(connectionString);
            v.MdiParent = this;
            v.Show();
        }

        private void выходИзУчЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите выйти из учетной записи?", "Выход из учетной записи", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        Login login = new Login();
                        login.Show();
                        Dispose(); break; }
                case DialogResult.No: { break; }
            }
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите выйти из приложения?", "Выход из приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            switch (result)
            {
                case DialogResult.Yes: {Application.Exit(); break; }
                case DialogResult.No: { break; }
            }
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Admin>().Count() == 1)
            {
                Application.OpenForms.OfType<Admin>().First().Dispose();
            }
            Admin v = new Admin(connectionString);
            v.MdiParent = this;
            v.Show();
        }

        private void Restoran_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void менюРесторанаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Main>().Count() == 1)
            {
                Application.OpenForms.OfType<Main>().First().Dispose();
            }
            Main v = new Main(connectionString, Role, User);
            v.MdiParent = this;
            v.Show();
        }
    }
}
