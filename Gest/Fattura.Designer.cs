namespace Bel3
{
    partial class Fattura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fattura));
            this.Intestazione = new System.Windows.Forms.RichTextBox();
            this.progressivo = new System.Windows.Forms.TextBox();
            this.emissione = new System.Windows.Forms.DateTimePicker();
            this.codicefiscale = new System.Windows.Forms.TextBox();
            this.arrivo = new System.Windows.Forms.DateTimePicker();
            this.partenza = new System.Windows.Forms.DateTimePicker();
            this.durata = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.cIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imponibile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imposta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DataT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescrizioneT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IvaT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrezzoT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Intestazione
            // 
            this.Intestazione.Location = new System.Drawing.Point(276, 12);
            this.Intestazione.Name = "Intestazione";
            this.Intestazione.Size = new System.Drawing.Size(227, 76);
            this.Intestazione.TabIndex = 0;
            this.Intestazione.Text = "";
            // 
            // progressivo
            // 
            this.progressivo.Enabled = false;
            this.progressivo.Location = new System.Drawing.Point(86, 12);
            this.progressivo.Name = "progressivo";
            this.progressivo.Size = new System.Drawing.Size(90, 20);
            this.progressivo.TabIndex = 1;
            // 
            // emissione
            // 
            this.emissione.Enabled = false;
            this.emissione.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.emissione.Location = new System.Drawing.Point(86, 38);
            this.emissione.Name = "emissione";
            this.emissione.Size = new System.Drawing.Size(90, 20);
            this.emissione.TabIndex = 2;
            // 
            // codicefiscale
            // 
            this.codicefiscale.Location = new System.Drawing.Point(347, 94);
            this.codicefiscale.Name = "codicefiscale";
            this.codicefiscale.Size = new System.Drawing.Size(156, 20);
            this.codicefiscale.TabIndex = 3;
            // 
            // arrivo
            // 
            this.arrivo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.arrivo.Location = new System.Drawing.Point(62, 68);
            this.arrivo.Name = "arrivo";
            this.arrivo.Size = new System.Drawing.Size(114, 20);
            this.arrivo.TabIndex = 4;
            this.arrivo.ValueChanged += new System.EventHandler(this.arrivo_ValueChanged);
            // 
            // partenza
            // 
            this.partenza.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.partenza.Location = new System.Drawing.Point(77, 94);
            this.partenza.Name = "partenza";
            this.partenza.Size = new System.Drawing.Size(99, 20);
            this.partenza.TabIndex = 5;
            this.partenza.ValueChanged += new System.EventHandler(this.partenza_ValueChanged);
            // 
            // durata
            // 
            this.durata.AutoSize = true;
            this.durata.Location = new System.Drawing.Point(15, 127);
            this.durata.Name = "durata";
            this.durata.Size = new System.Drawing.Size(37, 13);
            this.durata.TabIndex = 7;
            this.durata.Text = "Giorni:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Progressivo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Emissione:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Arrivo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Partenza:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Intestazione:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Codice fiscale o Partita iva:";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cIva,
            this.imponibile,
            this.imposta,
            this.columnHeader1});
            this.listView2.LabelEdit = true;
            this.listView2.Location = new System.Drawing.Point(13, 307);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(492, 72);
            this.listView2.TabIndex = 14;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // cIva
            // 
            this.cIva.Text = "Iva";
            // 
            // imponibile
            // 
            this.imponibile.Text = "Imponibile";
            // 
            // imposta
            // 
            this.imposta.Text = "Imposta";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Totale";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(347, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Stampa Definitiva";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(183, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Stampa Prova";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(18, 409);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Pagata";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataT,
            this.DescrizioneT,
            this.IvaT,
            this.PrezzoT});
            this.dataGridView1.Location = new System.Drawing.Point(13, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(490, 148);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.prova);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.prova2);
            // 
            // DataT
            // 
            this.DataT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataT.FillWeight = 60F;
            this.DataT.HeaderText = "Data";
            this.DataT.Name = "DataT";
            // 
            // DescrizioneT
            // 
            this.DescrizioneT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescrizioneT.HeaderText = "Descrizione";
            this.DescrizioneT.Name = "DescrizioneT";
            // 
            // IvaT
            // 
            this.IvaT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IvaT.HeaderText = "Iva";
            this.IvaT.Name = "IvaT";
            // 
            // PrezzoT
            // 
            this.PrezzoT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PrezzoT.HeaderText = "Prezzo";
            this.PrezzoT.Name = "PrezzoT";
            // 
            // Fattura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 444);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.durata);
            this.Controls.Add(this.partenza);
            this.Controls.Add(this.arrivo);
            this.Controls.Add(this.codicefiscale);
            this.Controls.Add(this.emissione);
            this.Controls.Add(this.progressivo);
            this.Controls.Add(this.Intestazione);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fattura";
            this.Text = "Fattura";
            this.Load += new System.EventHandler(this.Fattura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Intestazione;
        private System.Windows.Forms.TextBox progressivo;
        private System.Windows.Forms.DateTimePicker emissione;
        private System.Windows.Forms.TextBox codicefiscale;
        private System.Windows.Forms.DateTimePicker arrivo;
        private System.Windows.Forms.DateTimePicker partenza;
        private System.Windows.Forms.Label durata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader cIva;
        private System.Windows.Forms.ColumnHeader imponibile;
        private System.Windows.Forms.ColumnHeader imposta;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescrizioneT;
        private System.Windows.Forms.DataGridViewTextBoxColumn IvaT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrezzoT;
        private System.Windows.Forms.ListView listView2;

    }
}