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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(812, 185);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить сотрудника в список";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьСписокToolStripMenuItem,
            this.составитьОтчётToolStripMenuItem,
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem,
            this.авторыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(851, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // открытьСписокToolStripMenuItem
            // 
            this.открытьСписокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem,
            this.списокНаградToolStripMenuItem});
            this.открытьСписокToolStripMenuItem.Name = "открытьСписокToolStripMenuItem";
            this.открытьСписокToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.открытьСписокToolStripMenuItem.Text = "Открыть список";
            // 
            // списокЛюдейПредставленныхКНаградамToolStripMenuItem
            // 
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Name = "списокЛюдейПредставленныхКНаградамToolStripMenuItem";
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem.Text = "Список людей представленных к наградам";
            // 
            // списокНаградToolStripMenuItem
            // 
            this.списокНаградToolStripMenuItem.Name = "списокНаградToolStripMenuItem";
            this.списокНаградToolStripMenuItem.Size = new System.Drawing.Size(311, 22);
            this.списокНаградToolStripMenuItem.Text = "Список наград";
            this.списокНаградToolStripMenuItem.Click += new System.EventHandler(this.списокНаградToolStripMenuItem_Click);
            // 
            // составитьОтчётToolStripMenuItem
            // 
            this.составитьОтчётToolStripMenuItem.Name = "составитьОтчётToolStripMenuItem";
            this.составитьОтчётToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.составитьОтчётToolStripMenuItem.Text = "Составить отчёт";
            this.составитьОтчётToolStripMenuItem.Click += new System.EventHandler(this.составитьОтчётToolStripMenuItem_Click);
            // 
            // проверитьПодключениеСбазойДанныхToolStripMenuItem
            // 
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Name = "проверитьПодключениеСбазойДанныхToolStripMenuItem";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Size = new System.Drawing.Size(185, 20);
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Text = "Проверить подключение с БД";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Click += new System.EventHandler(this.проверитьПодключениеСбазойДанныхToolStripMenuItem_Click);
            // 
            // авторыToolStripMenuItem
            // 
            this.авторыToolStripMenuItem.Name = "авторыToolStripMenuItem";
            this.авторыToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.авторыToolStripMenuItem.Text = "Авторы";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(35, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(157, 20);
            this.textBox2.TabIndex = 4;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(217, 44);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(102, 23);
            this.search.TabIndex = 5;
            this.search.Text = "Поиск";
            this.search.UseVisualStyleBackColor = true;
            // 
            // addRewardToEmpbtn
            // 
            this.addRewardToEmpbtn.Location = new System.Drawing.Point(217, 281);
            this.addRewardToEmpbtn.Name = "addRewardToEmpbtn";
            this.addRewardToEmpbtn.Size = new System.Drawing.Size(154, 37);
            this.addRewardToEmpbtn.TabIndex = 6;
            this.addRewardToEmpbtn.Text = "Добавить награду выбранному сотруднику";
            this.addRewardToEmpbtn.UseVisualStyleBackColor = true;
            this.addRewardToEmpbtn.Click += new System.EventHandler(this.addRewardToEmpbtn_Click);
            // 
            // editbtn
            // 
            this.editbtn.Location = new System.Drawing.Point(406, 281);
            this.editbtn.Name = "editbtn";
            this.editbtn.Size = new System.Drawing.Size(144, 37);
            this.editbtn.TabIndex = 7;
            this.editbtn.Text = "Редактировать личные данные";
            this.editbtn.UseVisualStyleBackColor = true;
            this.editbtn.Click += new System.EventHandler(this.editbtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 346);
            this.Controls.Add(this.editbtn);
            this.Controls.Add(this.addRewardToEmpbtn);
            this.Controls.Add(this.search);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Наградная деятельность Министерства с/х";
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
    }
}

