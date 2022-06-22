
namespace BD
{
    partial class Ingredients
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ingredients));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboIngred = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvNehvat = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.comboTypeYchet = new System.Windows.Forms.ComboBox();
            this.comboPoryadoc = new System.Windows.Forms.ComboBox();
            this.comboTypeSort = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudKol = new System.Windows.Forms.NumericUpDown();
            this.nudVes = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.nudSrocHran = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboEdIzm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvJurnYchetIngred = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNehvat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrocHran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJurnYchetIngred)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.comboIngred);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.dgvNehvat);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.comboTypeYchet);
            this.splitContainer1.Panel1.Controls.Add(this.comboPoryadoc);
            this.splitContainer1.Panel1.Controls.Add(this.comboTypeSort);
            this.splitContainer1.Panel1.Controls.Add(this.buttonClear);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.nudKol);
            this.splitContainer1.Panel1.Controls.Add(this.nudVes);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            this.splitContainer1.Panel1.Controls.Add(this.nudSrocHran);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.comboEdIzm);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvJurnYchetIngred);
            this.splitContainer1.Size = new System.Drawing.Size(1035, 593);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 0;
            // 
            // comboIngred
            // 
            this.comboIngred.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboIngred.FormattingEnabled = true;
            this.comboIngred.Location = new System.Drawing.Point(3, 19);
            this.comboIngred.Name = "comboIngred";
            this.comboIngred.Size = new System.Drawing.Size(339, 27);
            this.comboIngred.TabIndex = 23;
            this.comboIngred.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 19);
            this.label7.TabIndex = 22;
            this.label7.Text = "Не хватает для блюд";
            // 
            // dgvNehvat
            // 
            this.dgvNehvat.AllowUserToAddRows = false;
            this.dgvNehvat.AllowUserToDeleteRows = false;
            this.dgvNehvat.AllowUserToResizeColumns = false;
            this.dgvNehvat.AllowUserToResizeRows = false;
            this.dgvNehvat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvNehvat.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe Print", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNehvat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNehvat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNehvat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column10,
            this.Column9});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe Print", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNehvat.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNehvat.EnableHeadersVisualStyles = false;
            this.dgvNehvat.Location = new System.Drawing.Point(3, 253);
            this.dgvNehvat.MultiSelect = false;
            this.dgvNehvat.Name = "dgvNehvat";
            this.dgvNehvat.ReadOnly = true;
            this.dgvNehvat.RowHeadersVisible = false;
            this.dgvNehvat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNehvat.Size = new System.Drawing.Size(339, 337);
            this.dgvNehvat.TabIndex = 21;
            this.dgvNehvat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.FillWeight = 30F;
            this.Column8.HeaderText = "Ингредиент";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.FillWeight = 20F;
            this.Column10.HeaderText = "Вес";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.FillWeight = 15F;
            this.Column9.HeaderText = "Ед. изм.";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(176, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 26);
            this.button2.TabIndex = 20;
            this.button2.Text = "Применить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboTypeYchet
            // 
            this.comboTypeYchet.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboTypeYchet.FormattingEnabled = true;
            this.comboTypeYchet.Items.AddRange(new object[] {
            "По приходу",
            "По списанию"});
            this.comboTypeYchet.Location = new System.Drawing.Point(3, 170);
            this.comboTypeYchet.Name = "comboTypeYchet";
            this.comboTypeYchet.Size = new System.Drawing.Size(167, 27);
            this.comboTypeYchet.TabIndex = 19;
            this.comboTypeYchet.Text = "Тип учета";
            this.comboTypeYchet.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboPoryadoc
            // 
            this.comboPoryadoc.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboPoryadoc.FormattingEnabled = true;
            this.comboPoryadoc.Items.AddRange(new object[] {
            "По возрастанию",
            "По убыванию"});
            this.comboPoryadoc.Location = new System.Drawing.Point(176, 138);
            this.comboPoryadoc.Name = "comboPoryadoc";
            this.comboPoryadoc.Size = new System.Drawing.Size(166, 27);
            this.comboPoryadoc.TabIndex = 18;
            this.comboPoryadoc.Text = "Порядок сортировки";
            this.comboPoryadoc.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboTypeSort
            // 
            this.comboTypeSort.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboTypeSort.FormattingEnabled = true;
            this.comboTypeSort.Items.AddRange(new object[] {
            "По наименованию",
            "По количеству",
            "По весу",
            "По единицам измерения",
            "По дате операции",
            "Срок хранения"});
            this.comboTypeSort.Location = new System.Drawing.Point(3, 138);
            this.comboTypeSort.Name = "comboTypeSort";
            this.comboTypeSort.Size = new System.Drawing.Size(167, 27);
            this.comboTypeSort.TabIndex = 17;
            this.comboTypeSort.Text = "Выберите тип сортировки";
            this.comboTypeSort.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClear.Location = new System.Drawing.Point(3, 202);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(339, 27);
            this.buttonClear.TabIndex = 14;
            this.buttonClear.Text = "Сбросить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Сортировка";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(101, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Кол-во";
            // 
            // nudKol
            // 
            this.nudKol.BackColor = System.Drawing.SystemColors.Window;
            this.nudKol.Enabled = false;
            this.nudKol.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudKol.Location = new System.Drawing.Point(91, 64);
            this.nudKol.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudKol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKol.Name = "nudKol";
            this.nudKol.Size = new System.Drawing.Size(79, 27);
            this.nudKol.TabIndex = 10;
            this.nudKol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudKol.ValueChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.nudKol.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // nudVes
            // 
            this.nudVes.Enabled = false;
            this.nudVes.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudVes.Location = new System.Drawing.Point(178, 64);
            this.nudVes.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudVes.Name = "nudVes";
            this.nudVes.Size = new System.Drawing.Size(71, 27);
            this.nudVes.TabIndex = 9;
            this.nudVes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVes.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(195, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Вес";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(3, 91);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(339, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // nudSrocHran
            // 
            this.nudSrocHran.Enabled = false;
            this.nudSrocHran.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudSrocHran.Location = new System.Drawing.Point(3, 64);
            this.nudSrocHran.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudSrocHran.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSrocHran.Name = "nudSrocHran";
            this.nudSrocHran.Size = new System.Drawing.Size(79, 27);
            this.nudSrocHran.TabIndex = 5;
            this.nudSrocHran.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSrocHran.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(0, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Срок хранения";
            // 
            // comboEdIzm
            // 
            this.comboEdIzm.DisplayMember = "0";
            this.comboEdIzm.Enabled = false;
            this.comboEdIzm.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboEdIzm.FormattingEnabled = true;
            this.comboEdIzm.Location = new System.Drawing.Point(256, 64);
            this.comboEdIzm.Name = "comboEdIzm";
            this.comboEdIzm.Size = new System.Drawing.Size(86, 27);
            this.comboEdIzm.TabIndex = 3;
            this.comboEdIzm.ValueMember = "0";
            this.comboEdIzm.SelectedIndexChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.comboEdIzm.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(270, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ед. изм.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование";
            // 
            // dgvJurnYchetIngred
            // 
            this.dgvJurnYchetIngred.AllowUserToAddRows = false;
            this.dgvJurnYchetIngred.AllowUserToDeleteRows = false;
            this.dgvJurnYchetIngred.AllowUserToResizeColumns = false;
            this.dgvJurnYchetIngred.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe Print", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJurnYchetIngred.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvJurnYchetIngred.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJurnYchetIngred.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe Print", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvJurnYchetIngred.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvJurnYchetIngred.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJurnYchetIngred.EnableHeadersVisualStyles = false;
            this.dgvJurnYchetIngred.Location = new System.Drawing.Point(0, 0);
            this.dgvJurnYchetIngred.MultiSelect = false;
            this.dgvJurnYchetIngred.Name = "dgvJurnYchetIngred";
            this.dgvJurnYchetIngred.ReadOnly = true;
            this.dgvJurnYchetIngred.RowHeadersVisible = false;
            this.dgvJurnYchetIngred.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJurnYchetIngred.Size = new System.Drawing.Size(686, 593);
            this.dgvJurnYchetIngred.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "Наименование";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 10F;
            this.Column2.HeaderText = "Кол-во";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 10F;
            this.Column3.HeaderText = "Вес";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 10F;
            this.Column4.HeaderText = "Ед. изм";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.FillWeight = 10F;
            this.Column5.HeaderText = "Тип учета";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.FillWeight = 20F;
            this.Column6.HeaderText = "Дата операции";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.FillWeight = 10F;
            this.Column7.HeaderText = "Пред. срок. хранения";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Ingredients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 593);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ingredients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ингредиенты";
            this.Load += new System.EventHandler(this.Ingred_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNehvat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrocHran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJurnYchetIngred)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudKol;
        private System.Windows.Forms.NumericUpDown nudVes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.NumericUpDown nudSrocHran;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboEdIzm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboTypeSort;
        private System.Windows.Forms.ComboBox comboPoryadoc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboTypeYchet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvNehvat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.ComboBox comboIngred;
        private System.Windows.Forms.DataGridView dgvJurnYchetIngred;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}