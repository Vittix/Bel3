namespace Bel3
{
    partial class TariffeExtra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TariffeExtra));
            this.button2 = new System.Windows.Forms.Button();
            this.AddRoom = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.iva = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prezzo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nome = new System.Windows.Forms.TextBox();
            this.tariffex = new System.Windows.Forms.ListBox();
            this.Modify = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.AddRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(69, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "Fatto";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddRoom
            // 
            this.AddRoom.Controls.Add(this.label2);
            this.AddRoom.Controls.Add(this.iva);
            this.AddRoom.Controls.Add(this.label1);
            this.AddRoom.Controls.Add(this.prezzo);
            this.AddRoom.Controls.Add(this.label3);
            this.AddRoom.Controls.Add(this.nome);
            this.AddRoom.Controls.Add(this.tariffex);
            this.AddRoom.Controls.Add(this.Modify);
            this.AddRoom.Controls.Add(this.Add);
            this.AddRoom.Controls.Add(this.Delete);
            this.AddRoom.Location = new System.Drawing.Point(13, 27);
            this.AddRoom.Name = "AddRoom";
            this.AddRoom.Size = new System.Drawing.Size(258, 178);
            this.AddRoom.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "Iva:";
            // 
            // iva
            // 
            this.iva.Location = new System.Drawing.Point(68, 62);
            this.iva.Name = "iva";
            this.iva.Size = new System.Drawing.Size(94, 20);
            this.iva.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Prezzo:";
            // 
            // prezzo
            // 
            this.prezzo.Location = new System.Drawing.Point(68, 36);
            this.prezzo.Name = "prezzo";
            this.prezzo.Size = new System.Drawing.Size(94, 20);
            this.prezzo.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Nome:";
            // 
            // nome
            // 
            this.nome.Location = new System.Drawing.Point(68, 10);
            this.nome.Name = "nome";
            this.nome.Size = new System.Drawing.Size(94, 20);
            this.nome.TabIndex = 19;
            // 
            // tariffex
            // 
            this.tariffex.FormattingEnabled = true;
            this.tariffex.Location = new System.Drawing.Point(168, 10);
            this.tariffex.Name = "tariffex";
            this.tariffex.Size = new System.Drawing.Size(87, 134);
            this.tariffex.TabIndex = 14;
            this.tariffex.SelectedIndexChanged += new System.EventHandler(this.ListaStanze_SelectedIndexChanged);
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
            // TariffeExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.AddRoom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TariffeExtra";
            this.Text = "TariffeExtra";
            this.Load += new System.EventHandler(this.TariffeExtra_Load);
            this.AddRoom.ResumeLayout(false);
            this.AddRoom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel AddRoom;
        private System.Windows.Forms.ListBox tariffex;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox iva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox prezzo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nome;
    }
}