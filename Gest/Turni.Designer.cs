﻿namespace Bel3
{
    partial class Turni
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.panoramicaturni = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mattina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pomeriggio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modifica = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.panoramicaturni)).BeginInit();
            this.SuspendLayout();
            // 
            // panoramicaturni
            // 
            this.panoramicaturni.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.panoramicaturni.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.Mattina,
            this.Pomeriggio,
            this.Notte,
            this.Modifica});
            this.panoramicaturni.Location = new System.Drawing.Point(12, 12);
            this.panoramicaturni.Name = "panoramicaturni";
            this.panoramicaturni.Size = new System.Drawing.Size(753, 435);
            this.panoramicaturni.TabIndex = 0;
            this.panoramicaturni.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            // 
            // Mattina
            // 
            this.Mattina.HeaderText = "Mattina";
            this.Mattina.Name = "Mattina";
            // 
            // Pomeriggio
            // 
            this.Pomeriggio.HeaderText = "Pomeriggio";
            this.Pomeriggio.Name = "Pomeriggio";
            // 
            // Notte
            // 
            this.Notte.HeaderText = "Notte";
            this.Notte.Name = "Notte";
            // 
            // Modifica
            // 
            this.Modifica.HeaderText = "Modifica";
            this.Modifica.Name = "Modifica";
            this.Modifica.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Modifica.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(690, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Calc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 456);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(97, 20);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2013, 6, 6, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(177, 456);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(97, 20);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.Value = new System.DateTime(2013, 6, 6, 0, 0, 0, 0);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // Turni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 484);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panoramicaturni);
            this.Name = "Turni";
            this.Text = "Turni";
            this.Load += new System.EventHandler(this.Turni_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panoramicaturni)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView panoramicaturni;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCellEventHandler dataGridView1_CellContentClick;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mattina;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pomeriggio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notte;
        private System.Windows.Forms.DataGridViewButtonColumn Modifica;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}