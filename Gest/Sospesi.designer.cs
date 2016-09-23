namespace Bel3
{
    partial class Sospesi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sospesi));
            this.PrezzoTot = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.IdPrenotazione = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Nome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prezzo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Iva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnoiva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantità = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // PrezzoTot
            // 
            this.PrezzoTot.AutoSize = true;
            this.PrezzoTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrezzoTot.Location = new System.Drawing.Point(12, 323);
            this.PrezzoTot.Name = "PrezzoTot";
            this.PrezzoTot.Size = new System.Drawing.Size(172, 29);
            this.PrezzoTot.TabIndex = 4;
            this.PrezzoTot.Text = "Prezzo totale:";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdPrenotazione,
            this.Nome,
            this.Prezzo,
            this.Iva,
            this.pnoiva,
            this.quantità,
            this.data});
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(489, 305);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // IdPrenotazione
            // 
            this.IdPrenotazione.Text = "Id Conto";
            this.IdPrenotazione.Width = 55;
            // 
            // Nome
            // 
            this.Nome.Text = "Nome";
            this.Nome.Width = 113;
            // 
            // Prezzo
            // 
            this.Prezzo.Text = "Prezzo + iva";
            this.Prezzo.Width = 78;
            // 
            // Iva
            // 
            this.Iva.Text = "Iva";
            this.Iva.Width = 31;
            // 
            // pnoiva
            // 
            this.pnoiva.Text = "Prezzo-iva";
            this.pnoiva.Width = 76;
            // 
            // quantità
            // 
            this.quantità.Text = "quantità";
            // 
            // data
            // 
            this.data.Text = "data";
            this.data.Width = 82;
            // 
            // Sospesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 406);
            this.Controls.Add(this.PrezzoTot);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sospesi";
            this.Text = "Sospesi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PrezzoTot;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader IdPrenotazione;
        private System.Windows.Forms.ColumnHeader Nome;
        private System.Windows.Forms.ColumnHeader Prezzo;
        private System.Windows.Forms.ColumnHeader Iva;
        private System.Windows.Forms.ColumnHeader pnoiva;
        private System.Windows.Forms.ColumnHeader quantità;
        private System.Windows.Forms.ColumnHeader data;
    }
}