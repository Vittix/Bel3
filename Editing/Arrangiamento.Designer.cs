namespace Bel3
{
    partial class Arrangiamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Arrangiamento));
            this.button2 = new System.Windows.Forms.Button();
            this.AddRoom = new System.Windows.Forms.Panel();
            this.ListaStanze = new System.Windows.Forms.ListBox();
            this.Modify = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.roomName = new System.Windows.Forms.TextBox();
            this.AddRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(69, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Fatto";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddRoom
            // 
            this.AddRoom.Controls.Add(this.ListaStanze);
            this.AddRoom.Controls.Add(this.Modify);
            this.AddRoom.Controls.Add(this.label3);
            this.AddRoom.Controls.Add(this.Add);
            this.AddRoom.Controls.Add(this.Delete);
            this.AddRoom.Controls.Add(this.roomName);
            this.AddRoom.Location = new System.Drawing.Point(13, 27);
            this.AddRoom.Name = "AddRoom";
            this.AddRoom.Size = new System.Drawing.Size(258, 178);
            this.AddRoom.TabIndex = 19;
            // 
            // ListaStanze
            // 
            this.ListaStanze.FormattingEnabled = true;
            this.ListaStanze.Location = new System.Drawing.Point(168, 10);
            this.ListaStanze.Name = "ListaStanze";
            this.ListaStanze.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListaStanze.Size = new System.Drawing.Size(87, 134);
            this.ListaStanze.TabIndex = 14;
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(87, 152);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(75, 23);
            this.Modify.TabIndex = 8;
            this.Modify.Text = "Modifica";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.Modify_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nome:";
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(6, 152);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 4;
            this.Add.Text = "Aggiungi";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(168, 152);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 11;
            this.Delete.Text = "Rimuovi";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // roomName
            // 
            this.roomName.Location = new System.Drawing.Point(56, 10);
            this.roomName.Name = "roomName";
            this.roomName.Size = new System.Drawing.Size(106, 20);
            this.roomName.TabIndex = 0;
            // 
            // Arrangiamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.AddRoom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Arrangiamento";
            this.Text = "Arrangiamento";
            this.Load += new System.EventHandler(this.Arrangiamento_Load);
            this.AddRoom.ResumeLayout(false);
            this.AddRoom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel AddRoom;
        private System.Windows.Forms.ListBox ListaStanze;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.TextBox roomName;
    }
}