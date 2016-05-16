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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.открытьСписокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокЛюдейПредставленныхКНаградамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокНаградToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.составитьОтчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётОНагражденныхЛюдяхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётОНаграднойДеятельностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.button1.Location = new System.Drawing.Point(82, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(181, 290);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьСписокToolStripMenuItem,
            this.составитьОтчётToolStripMenuItem,
            this.авторыToolStripMenuItem,
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
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
            this.составитьОтчётToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчётОНагражденныхЛюдяхToolStripMenuItem,
            this.отчётОНаграднойДеятельностиToolStripMenuItem});
            this.составитьОтчётToolStripMenuItem.Name = "составитьОтчётToolStripMenuItem";
            this.составитьОтчётToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.составитьОтчётToolStripMenuItem.Text = "Составить отчёт";
            // 
            // отчётОНагражденныхЛюдяхToolStripMenuItem
            // 
            this.отчётОНагражденныхЛюдяхToolStripMenuItem.Name = "отчётОНагражденныхЛюдяхToolStripMenuItem";
            this.отчётОНагражденныхЛюдяхToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.отчётОНагражденныхЛюдяхToolStripMenuItem.Text = "Отчёт о награжденных людях";
            // 
            // отчётОНаграднойДеятельностиToolStripMenuItem
            // 
            this.отчётОНаграднойДеятельностиToolStripMenuItem.Name = "отчётОНаграднойДеятельностиToolStripMenuItem";
            this.отчётОНаграднойДеятельностиToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.отчётОНаграднойДеятельностиToolStripMenuItem.Text = "Отчёт о наградной деятельности";
            this.отчётОНаграднойДеятельностиToolStripMenuItem.Click += new System.EventHandler(this.отчётОНаграднойДеятельностиToolStripMenuItem_Click);
            // 
            // авторыToolStripMenuItem
            // 
            this.авторыToolStripMenuItem.Name = "авторыToolStripMenuItem";
            this.авторыToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.авторыToolStripMenuItem.Text = "Авторы";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(53, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(237, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // проверитьПодключениеСбазойДанныхToolStripMenuItem
            // 
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Name = "проверитьПодключениеСбазойДанныхToolStripMenuItem";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Size = new System.Drawing.Size(185, 20);
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Text = "Проверить подключение с БД";
            this.проверитьПодключениеСбазойДанныхToolStripMenuItem.Click += new System.EventHandler(this.проверитьПодключениеСбазойДанныхToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 337);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem открытьСписокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокЛюдейПредставленныхКНаградамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокНаградToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem составитьОтчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётОНагражденныхЛюдяхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётОНаграднойДеятельностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem авторыToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem проверитьПодключениеСбазойДанныхToolStripMenuItem;
    }
}

