namespace Bel3.Gest
{
    partial class PrenotazioneMultipla
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
            this.label16 = new System.Windows.Forms.Label();
            this.tarforfait = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.iva = new System.Windows.Forms.TextBox();
            this.pagata = new System.Windows.Forms.CheckBox();
            this.tipologia = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.arrangiamento = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tipostanza = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.forfait = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label16.Location = new System.Drawing.Point(17, 111);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 22);
            this.label16.TabIndex = 59;
            this.label16.Text = "Tariffa:";
            // 
            // tarforfait
            // 
            this.tarforfait.Location = new System.Drawing.Point(86, 109);
            this.tarforfait.Margin = new System.Windows.Forms.Padding(4);
            this.tarforfait.Name = "tarforfait";
            this.tarforfait.Size = new System.Drawing.Size(171, 22);
            this.tarforfait.TabIndex = 58;
            this.tarforfait.TextChanged += new System.EventHandler(this.tarforfait_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label15.Location = new System.Drawing.Point(264, 108);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 22);
            this.label15.TabIndex = 57;
            this.label15.Text = "Iva:";
            // 
            // iva
            // 
            this.iva.Location = new System.Drawing.Point(302, 107);
            this.iva.Margin = new System.Windows.Forms.Padding(4);
            this.iva.Name = "iva";
            this.iva.Size = new System.Drawing.Size(57, 22);
            this.iva.TabIndex = 56;
            this.iva.Text = "10";
            this.iva.TextChanged += new System.EventHandler(this.iva_TextChanged);
            // 
            // pagata
            // 
            this.pagata.AutoSize = true;
            this.pagata.Location = new System.Drawing.Point(313, 45);
            this.pagata.Margin = new System.Windows.Forms.Padding(4);
            this.pagata.Name = "pagata";
            this.pagata.Size = new System.Drawing.Size(75, 21);
            this.pagata.TabIndex = 51;
            this.pagata.Text = "Pagata";
            this.pagata.UseVisualStyleBackColor = true;
            this.pagata.CheckedChanged += new System.EventHandler(this.pagata_CheckedChanged);
            // 
            // tipologia
            // 
            this.tipologia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipologia.FormattingEnabled = true;
            this.tipologia.Location = new System.Drawing.Point(121, 13);
            this.tipologia.Margin = new System.Windows.Forms.Padding(4);
            this.tipologia.Name = "tipologia";
            this.tipologia.Size = new System.Drawing.Size(184, 24);
            this.tipologia.TabIndex = 48;
            this.tipologia.SelectedIndexChanged += new System.EventHandler(this.tipologia_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label8.Location = new System.Drawing.Point(11, 13);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 22);
            this.label8.TabIndex = 55;
            this.label8.Text = "Tipologia:";
            // 
            // arrangiamento
            // 
            this.arrangiamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arrangiamento.FormattingEnabled = true;
            this.arrangiamento.Location = new System.Drawing.Point(149, 75);
            this.arrangiamento.Margin = new System.Windows.Forms.Padding(4);
            this.arrangiamento.Name = "arrangiamento";
            this.arrangiamento.Size = new System.Drawing.Size(156, 24);
            this.arrangiamento.TabIndex = 50;
            this.arrangiamento.SelectedIndexChanged += new System.EventHandler(this.arrangiamento_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label5.Location = new System.Drawing.Point(11, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 22);
            this.label5.TabIndex = 54;
            this.label5.Text = "Arrangiamento:";
            // 
            // tipostanza
            // 
            this.tipostanza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipostanza.FormattingEnabled = true;
            this.tipostanza.Location = new System.Drawing.Point(121, 42);
            this.tipostanza.Margin = new System.Windows.Forms.Padding(4);
            this.tipostanza.Name = "tipostanza";
            this.tipostanza.Size = new System.Drawing.Size(184, 24);
            this.tipostanza.TabIndex = 49;
            this.tipostanza.SelectedIndexChanged += new System.EventHandler(this.tipostanza_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(11, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 22);
            this.label4.TabIndex = 53;
            this.label4.Text = "Tipo stanza:";
            // 
            // forfait
            // 
            this.forfait.AutoSize = true;
            this.forfait.Location = new System.Drawing.Point(313, 16);
            this.forfait.Margin = new System.Windows.Forms.Padding(4);
            this.forfait.Name = "forfait";
            this.forfait.Size = new System.Drawing.Size(70, 21);
            this.forfait.TabIndex = 52;
            this.forfait.Text = "Forfait";
            this.forfait.UseVisualStyleBackColor = true;
            this.forfait.CheckedChanged += new System.EventHandler(this.forfait_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 43);
            this.button1.TabIndex = 60;
            this.button1.Text = "continua";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PrenotazioneMultipla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 193);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tarforfait);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.iva);
            this.Controls.Add(this.pagata);
            this.Controls.Add(this.tipologia);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.arrangiamento);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tipostanza);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.forfait);
            this.Name = "PrenotazioneMultipla";
            this.Text = "PrenotazioneMultipla";
            this.Load += new System.EventHandler(this.PrenotazioneMultipla_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tarforfait;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox iva;
        private System.Windows.Forms.CheckBox pagata;
        private System.Windows.Forms.ComboBox tipologia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox arrangiamento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox tipostanza;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox forfait;
        private System.Windows.Forms.Button button1;
    }
}