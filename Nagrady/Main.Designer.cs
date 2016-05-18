namespace Nagrady
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.открытьСписокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокНаградToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.составитьОтчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.addRewardToEmpbtn = new System.Windows.Forms.Button();
            this.editbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(965, 213);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(14, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(471, 64);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить человека представленного к награде в список";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьСписокToolStripMenuItem,
            this.составитьОтчётToolStripMenuItem,
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem,
            this.авторыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(993, 36);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // открытьСписокToolStripMenuItem
            // 
            this.открытьСписокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem,
            this.списокНаградToolStripMenuItem});
            this.открытьСписокToolStripMenuItem.Name = "открытьСписокToolStripMenuItem";
            this.открытьСписокToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.открытьСписокToolStripMenuItem.Text = "Открыть список";
            // 
            // списокЛюдейПредставленныхКНаградамToolStripMenuItem
            // 
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Name = "списокЛюдейПредставленныхКНаградамToolStripMenuItem";
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Size = new System.Drawing.Size(479, 32);
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Text = "Список людей представленных к наградам";
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Click += new System.EventHandler(this.списокЛюдейПредставленныхКНаградамToolStripMenuItem_Click);
            // 
            // списокНаградToolStripMenuItem
            // 
            this.списокНаградToolStripMenuItem.Name = "списокНаградToolStripMenuItem";
            this.списокНаградToolStripMenuItem.Size = new System.Drawing.Size(479, 32);
            this.списокНаградToolStripMenuItem.Text = "Список наград";
            this.списокНаградToolStripMenuItem.Click += new System.EventHandler(this.списокНаградToolStripMenuItem_Click);
            // 
            // составитьОтчётToolStripMenuItem
            // 
            this.составитьОтчётToolStripMenuItem.Name = "составитьОтчётToolStripMenuItem";
            this.составитьОтчётToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.составитьОтчётToolStripMenuItem.Text = "Составить отчёт";
            this.составитьОтчётToolStripMenuItem.Click += new System.EventHandler(this.составитьОтчётToolStripMenuItem_Click);
            // 
            // проверитьПодключениеСбазойДанныхToolStripMenuItem
            // 
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Name = "проверитьПодключениеСбазойДанныхToolStripMenuItem";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Size = new System.Drawing.Size(301, 32);
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Text = "Проверить подключение с БД";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Click += new System.EventHandler(this.проверитьПодключениеСбазойДанныхToolStripMenuItem_Click);
            // 
            // авторыToolStripMenuItem
            // 
            this.авторыToolStripMenuItem.Name = "авторыToolStripMenuItem";
            this.авторыToolStripMenuItem.Size = new System.Drawing.Size(94, 32);
            this.авторыToolStripMenuItem.Text = "Авторы";
            this.авторыToolStripMenuItem.Click += new System.EventHandler(this.авторыToolStripMenuItem_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(519, 68);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 30);
            this.textBox2.TabIndex = 4;
            this.textBox2.Visible = false;
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.search.Location = new System.Drawing.Point(707, 61);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(176, 44);
            this.search.TabIndex = 5;
            this.search.Text = "Поиск";
            this.search.UseVisualStyleBackColor = true;
            this.search.Visible = false;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // addRewardToEmpbtn
            // 
            this.addRewardToEmpbtn.Enabled = false;
            this.addRewardToEmpbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addRewardToEmpbtn.Location = new System.Drawing.Point(508, 352);
            this.addRewardToEmpbtn.Name = "addRewardToEmpbtn";
            this.addRewardToEmpbtn.Size = new System.Drawing.Size(471, 60);
            this.addRewardToEmpbtn.TabIndex = 6;
            this.addRewardToEmpbtn.Text = "Добавить награду выбранному человеку из списка ";
            this.addRewardToEmpbtn.UseVisualStyleBackColor = true;
            this.addRewardToEmpbtn.Visible = false;
            this.addRewardToEmpbtn.Click += new System.EventHandler(this.addRewardToEmpbtn_Click);
            // 
            // editbtn
            // 
            this.editbtn.Enabled = false;
            this.editbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editbtn.Location = new System.Drawing.Point(14, 352);
            this.editbtn.Name = "editbtn";
            this.editbtn.Size = new System.Drawing.Size(471, 60);
            this.editbtn.TabIndex = 7;
            this.editbtn.Text = "Редактировать личные данные выбранного человека из списка ";
            this.editbtn.UseVisualStyleBackColor = true;
            this.editbtn.Visible = false;
            this.editbtn.Click += new System.EventHandler(this.editbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(115, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Введите фамилию для поиска по списку:";
            this.label1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(57, 488);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(256, 29);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Режим редактирования";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(508, 418);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(471, 64);
            this.button2.TabIndex = 10;
            this.button2.Text = "Удалить выбранного человека из списка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 529);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editbtn);
            this.Controls.Add(this.addRewardToEmpbtn);
            this.Controls.Add(this.search);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Наградная деятельность Министерства сельского хозяйства Астраханской области";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem открытьСписокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЛюдейПредставленныхКНаградамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокНаградToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem составитьОтчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem авторыToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ToolStripMenuItem проверитьПодключениеСбазойДанныхToolStripMenuItem;
        private System.Windows.Forms.Button addRewardToEmpbtn;
        private System.Windows.Forms.Button editbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
    }
}

