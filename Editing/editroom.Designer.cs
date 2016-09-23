namespace Bel3
{
    partial class editroom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editroom));
            this.label9 = new System.Windows.Forms.Label();
            this.ListaStanze = new System.Windows.Forms.ListBox();
            this.Modify = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.AddRoom = new System.Windows.Forms.Panel();
            this.a = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.da = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.roomName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.AddRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.da)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(189, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Lista Stanze";
            // 
            // ListaStanze
            // 
            this.ListaStanze.FormattingEnabled = true;
            this.ListaStanze.Location = new System.Drawing.Point(139, 3);
            this.ListaStanze.Name = "ListaStanze";
            this.ListaStanze.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListaStanze.Size = new System.Drawing.Size(169, 147);
            this.ListaStanze.TabIndex = 14;
            this.ListaStanze.SelectedIndexChanged += new System.EventHandler(this.ListaStanze_SelectedIndexChanged_1);
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(102, 152);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(75, 23);
            this.Modify.TabIndex = 8;
            this.Modify.Text = "Modifica";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.Modify_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Aggiungi Stanza";
            // 
            // AddRoom
            // 
            this.AddRoom.Controls.Add(this.a);
            this.AddRoom.Controls.Add(this.label1);
            this.AddRoom.Controls.Add(this.ListaStanze);
            this.AddRoom.Controls.Add(this.Modify);
            this.AddRoom.Controls.Add(this.da);
            this.AddRoom.Controls.Add(this.label6);
            this.AddRoom.Controls.Add(this.label3);
            this.AddRoom.Controls.Add(this.Add);
            this.AddRoom.Controls.Add(this.Delete);
            this.AddRoom.Controls.Add(this.roomName);
            this.AddRoom.Location = new System.Drawing.Point(9, 26);
            this.AddRoom.Name = "AddRoom";
            this.AddRoom.Size = new System.Drawing.Size(312, 178);
            this.AddRoom.TabIndex = 13;
            // 
            // a
            // 
            this.a.Location = new System.Drawing.Point(71, 59);
            this.a.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(42, 20);
            this.a.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "a:";
            // 
            // da
            // 
            this.da.Location = new System.Drawing.Point(71, 33);
            this.da.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.da.Name = "da";
            this.da.Size = new System.Drawing.Size(42, 20);
            this.da.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(18, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "da:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(18, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nome:";
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(21, 152);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 4;
            this.Add.Text = "Aggiungi";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click_1);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(183, 152);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 11;
            this.Delete.Text = "Rimuovi";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // roomName
            // 
            this.roomName.Location = new System.Drawing.Point(71, 10);
            this.roomName.Name = "roomName";
            this.roomName.Size = new System.Drawing.Size(63, 20);
            this.roomName.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 212);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Fatto";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // editroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 247);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AddRoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "editroom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "editroom";
            this.Load += new System.EventHandler(this.editroom_Load);
            this.AddRoom.ResumeLayout(false);
            this.AddRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.da)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox ListaStanze;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel AddRoom;
        private System.Windows.Forms.NumericUpDown da;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.TextBox roomName;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown a;
        private System.Windows.Forms.Label label1;
    }
}