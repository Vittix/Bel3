namespace Bel3
{
    partial class Prenotazione
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prenotazione));
            this.cNome = new System.Windows.Forms.TextBox();
            this.cCognome = new System.Windows.Forms.TextBox();
            this.codicefiscale = new System.Windows.Forms.TextBox();
            this.telefoni = new System.Windows.Forms.TextBox();
            this.arrivo = new System.Windows.Forms.DateTimePicker();
            this.partenza = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.libere = new System.Windows.Forms.ListView();
            this.Stanze = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.ragsociale = new System.Windows.Forms.TextBox();
            this.Ragionesociale = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.forfait = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.operatore = new System.Windows.Forms.ComboBox();
            this.tipostanza = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.arrangiamento = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tipologia = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.da = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.anticipo = new System.Windows.Forms.TextBox();
            this.pagata = new System.Windows.Forms.CheckBox();
            this.iva = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tarforfait = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.riferimento = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Giorni = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cNome
            // 
            this.cNome.Location = new System.Drawing.Point(111, 15);
            this.cNome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cNome.Name = "cNome";
            this.cNome.Size = new System.Drawing.Size(255, 22);
            this.cNome.TabIndex = 0;
            this.cNome.TextChanged += new System.EventHandler(this.cNome_TextChanged);
            // 
            // cCognome
            // 
            this.cCognome.Location = new System.Drawing.Point(479, 15);
            this.cCognome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cCognome.Name = "cCognome";
            this.cCognome.Size = new System.Drawing.Size(297, 22);
            this.cCognome.TabIndex = 1;
            this.cCognome.TextChanged += new System.EventHandler(this.cCognome_TextChanged);
            // 
            // codicefiscale
            // 
            this.codicefiscale.Location = new System.Drawing.Point(231, 82);
            this.codicefiscale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.codicefiscale.Name = "codicefiscale";
            this.codicefiscale.Size = new System.Drawing.Size(545, 22);
            this.codicefiscale.TabIndex = 3;
            // 
            // telefoni
            // 
            this.telefoni.Location = new System.Drawing.Point(97, 114);
            this.telefoni.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.telefoni.Name = "telefoni";
            this.telefoni.Size = new System.Drawing.Size(679, 22);
            this.telefoni.TabIndex = 4;
            this.telefoni.TextChanged += new System.EventHandler(this.telefoni_TextChanged);
            // 
            // arrivo
            // 
            this.arrivo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.arrivo.Location = new System.Drawing.Point(181, 146);
            this.arrivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.arrivo.Name = "arrivo";
            this.arrivo.Size = new System.Drawing.Size(132, 22);
            this.arrivo.TabIndex = 5;
            this.arrivo.Value = new System.DateTime(2011, 7, 1, 0, 0, 0, 0);
            this.arrivo.ValueChanged += new System.EventHandler(this.From_ValueChanged);
            // 
            // partenza
            // 
            this.partenza.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.partenza.Location = new System.Drawing.Point(181, 178);
            this.partenza.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.partenza.Name = "partenza";
            this.partenza.Size = new System.Drawing.Size(132, 22);
            this.partenza.TabIndex = 6;
            this.partenza.Value = new System.DateTime(2011, 7, 1, 0, 0, 0, 0);
            this.partenza.ValueChanged += new System.EventHandler(this.To_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nome:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(375, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cognome:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label6.Location = new System.Drawing.Point(19, 82);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 22);
            this.label6.TabIndex = 16;
            this.label6.Text = "Codice Fiscale o P. IVA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label7.Location = new System.Drawing.Point(15, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 22);
            this.label7.TabIndex = 17;
            this.label7.Text = "Telefoni:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label10.Location = new System.Drawing.Point(19, 146);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 22);
            this.label10.TabIndex = 20;
            this.label10.Text = "Data di arrivo:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label11.Location = new System.Drawing.Point(19, 178);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 22);
            this.label11.TabIndex = 21;
            this.label11.Text = "Data di partenza:";
            // 
            // libere
            // 
            this.libere.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Stanze});
            this.libere.HideSelection = false;
            this.libere.Location = new System.Drawing.Point(405, 178);
            this.libere.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.libere.Name = "libere";
            this.libere.Size = new System.Drawing.Size(371, 179);
            this.libere.TabIndex = 7;
            this.libere.UseCompatibleStateImageBehavior = false;
            this.libere.View = System.Windows.Forms.View.Details;
            this.libere.SelectedIndexChanged += new System.EventHandler(this.libere_SelectedIndexChanged);
            // 
            // Stanze
            // 
            this.Stanze.Text = "Camera";
            this.Stanze.Width = 65;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 462);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 28);
            this.button1.TabIndex = 17;
            this.button1.Text = "Prenota";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label12.Location = new System.Drawing.Point(401, 154);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(161, 22);
            this.label12.TabIndex = 24;
            this.label12.Text = "Seleziona Camera:";
            // 
            // ragsociale
            // 
            this.ragsociale.Location = new System.Drawing.Point(168, 54);
            this.ragsociale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ragsociale.Name = "ragsociale";
            this.ragsociale.Size = new System.Drawing.Size(608, 22);
            this.ragsociale.TabIndex = 2;
            this.ragsociale.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Ragionesociale
            // 
            this.Ragionesociale.AutoSize = true;
            this.Ragionesociale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Ragionesociale.Location = new System.Drawing.Point(19, 54);
            this.Ragionesociale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Ragionesociale.Name = "Ragionesociale";
            this.Ragionesociale.Size = new System.Drawing.Size(143, 22);
            this.Ragionesociale.TabIndex = 26;
            this.Ragionesociale.Text = "Ragione sociale:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(168, 462);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 28);
            this.button2.TabIndex = 18;
            this.button2.Text = "Prenota Esistente";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // forfait
            // 
            this.forfait.AutoSize = true;
            this.forfait.Location = new System.Drawing.Point(313, 369);
            this.forfait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.forfait.Name = "forfait";
            this.forfait.Size = new System.Drawing.Size(70, 21);
            this.forfait.TabIndex = 16;
            this.forfait.Text = "Forfait";
            this.forfait.UseVisualStyleBackColor = true;
            this.forfait.CheckedChanged += new System.EventHandler(this.forfait_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(19, 240);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 22);
            this.label3.TabIndex = 32;
            this.label3.Text = "Operatore:";
            // 
            // operatore
            // 
            this.operatore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operatore.FormattingEnabled = true;
            this.operatore.Location = new System.Drawing.Point(129, 240);
            this.operatore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatore.Name = "operatore";
            this.operatore.Size = new System.Drawing.Size(184, 24);
            this.operatore.TabIndex = 9;
            this.operatore.SelectedIndexChanged += new System.EventHandler(this.operatore_SelectedIndexChanged);
            // 
            // tipostanza
            // 
            this.tipostanza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipostanza.FormattingEnabled = true;
            this.tipostanza.Location = new System.Drawing.Point(129, 299);
            this.tipostanza.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tipostanza.Name = "tipostanza";
            this.tipostanza.Size = new System.Drawing.Size(184, 24);
            this.tipostanza.TabIndex = 11;
            this.tipostanza.SelectedIndexChanged += new System.EventHandler(this.tipostanza_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(19, 299);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 22);
            this.label4.TabIndex = 34;
            this.label4.Text = "Tipo stanza:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // arrangiamento
            // 
            this.arrangiamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arrangiamento.FormattingEnabled = true;
            this.arrangiamento.Location = new System.Drawing.Point(157, 332);
            this.arrangiamento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.arrangiamento.Name = "arrangiamento";
            this.arrangiamento.Size = new System.Drawing.Size(156, 24);
            this.arrangiamento.TabIndex = 12;
            this.arrangiamento.SelectedIndexChanged += new System.EventHandler(this.arrangiamento_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label5.Location = new System.Drawing.Point(19, 332);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 22);
            this.label5.TabIndex = 36;
            this.label5.Text = "Arrangiamento:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // tipologia
            // 
            this.tipologia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipologia.FormattingEnabled = true;
            this.tipologia.Location = new System.Drawing.Point(129, 270);
            this.tipologia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tipologia.Name = "tipologia";
            this.tipologia.Size = new System.Drawing.Size(184, 24);
            this.tipologia.TabIndex = 10;
            this.tipologia.SelectedIndexChanged += new System.EventHandler(this.tipologia_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label8.Location = new System.Drawing.Point(19, 270);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 22);
            this.label8.TabIndex = 38;
            this.label8.Text = "Tipologia:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // da
            // 
            this.da.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.da.FormattingEnabled = true;
            this.da.Location = new System.Drawing.Point(129, 209);
            this.da.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.da.Name = "da";
            this.da.Size = new System.Drawing.Size(184, 24);
            this.da.TabIndex = 8;
            this.da.SelectedIndexChanged += new System.EventHandler(this.da_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label9.Location = new System.Drawing.Point(19, 209);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 22);
            this.label9.TabIndex = 40;
            this.label9.Text = "Da:";
            // 
            // anticipo
            // 
            this.anticipo.Enabled = false;
            this.anticipo.Location = new System.Drawing.Point(97, 396);
            this.anticipo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.anticipo.Name = "anticipo";
            this.anticipo.Size = new System.Drawing.Size(171, 22);
            this.anticipo.TabIndex = 13;
            this.anticipo.Text = "0";
            this.anticipo.Visible = false;
            // 
            // pagata
            // 
            this.pagata.AutoSize = true;
            this.pagata.Location = new System.Drawing.Point(313, 393);
            this.pagata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pagata.Name = "pagata";
            this.pagata.Size = new System.Drawing.Size(75, 21);
            this.pagata.TabIndex = 15;
            this.pagata.Text = "Pagata";
            this.pagata.UseVisualStyleBackColor = true;
            this.pagata.CheckedChanged += new System.EventHandler(this.pagata_CheckedChanged);
            // 
            // iva
            // 
            this.iva.Location = new System.Drawing.Point(313, 421);
            this.iva.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iva.Name = "iva";
            this.iva.Size = new System.Drawing.Size(57, 22);
            this.iva.TabIndex = 44;
            this.iva.Text = "10";
            this.iva.TextChanged += new System.EventHandler(this.iva_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label15.Location = new System.Drawing.Point(275, 422);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 22);
            this.label15.TabIndex = 45;
            this.label15.Text = "Iva:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // tarforfait
            // 
            this.tarforfait.Location = new System.Drawing.Point(97, 423);
            this.tarforfait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tarforfait.Name = "tarforfait";
            this.tarforfait.Size = new System.Drawing.Size(171, 22);
            this.tarforfait.TabIndex = 46;
            this.tarforfait.TextChanged += new System.EventHandler(this.tarforfait_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label16.Location = new System.Drawing.Point(28, 425);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 22);
            this.label16.TabIndex = 47;
            this.label16.Text = "Tariffa:";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(327, 462);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 28);
            this.button3.TabIndex = 48;
            this.button3.Text = "Modifica Prenotazione";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // riferimento
            // 
            this.riferimento.Location = new System.Drawing.Point(97, 368);
            this.riferimento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.riferimento.Name = "riferimento";
            this.riferimento.Size = new System.Drawing.Size(171, 22);
            this.riferimento.TabIndex = 49;
            this.riferimento.TextChanged += new System.EventHandler(this.riferimento_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label13.Location = new System.Drawing.Point(49, 369);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 22);
            this.label13.TabIndex = 50;
            this.label13.Text = "Rif. :";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(405, 372);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(371, 82);
            this.richTextBox1.TabIndex = 51;
            this.richTextBox1.Text = "";
            // 
            // Giorni
            // 
            this.Giorni.AutoSize = true;
            this.Giorni.Location = new System.Drawing.Point(323, 187);
            this.Giorni.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Giorni.Name = "Giorni";
            this.Giorni.Size = new System.Drawing.Size(50, 17);
            this.Giorni.TabIndex = 52;
            this.Giorni.Text = "Giorni:";
            this.Giorni.Click += new System.EventHandler(this.Giorni_Click);
            // 
            // Prenotazione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 498);
            this.Controls.Add(this.Giorni);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.riferimento);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tarforfait);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.iva);
            this.Controls.Add(this.pagata);
            this.Controls.Add(this.anticipo);
            this.Controls.Add(this.da);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tipologia);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.arrangiamento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tipostanza);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.operatore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.forfait);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Ragionesociale);
            this.Controls.Add(this.ragsociale);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.libere);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.partenza);
            this.Controls.Add(this.arrivo);
            this.Controls.Add(this.telefoni);
            this.Controls.Add(this.codicefiscale);
            this.Controls.Add(this.cCognome);
            this.Controls.Add(this.cNome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Prenotazione";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prenotazione";
            this.Load += new System.EventHandler(this.Prenotazione_Load);
            this.Shown += new System.EventHandler(this.mouseoverme);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cNome;
        private System.Windows.Forms.TextBox cCognome;
        private System.Windows.Forms.TextBox codicefiscale;
        private System.Windows.Forms.TextBox telefoni;
        private System.Windows.Forms.DateTimePicker arrivo;
        private System.Windows.Forms.DateTimePicker partenza;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView libere;
        private System.Windows.Forms.ColumnHeader Stanze;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ragsociale;
        private System.Windows.Forms.Label Ragionesociale;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox forfait;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox operatore;
        public System.Windows.Forms.ComboBox tipostanza;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox arrangiamento;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox tipologia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox da;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox anticipo;
        public System.Windows.Forms.CheckBox pagata;
        public System.Windows.Forms.TextBox iva;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox tarforfait;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox riferimento;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label Giorni;
    }
}