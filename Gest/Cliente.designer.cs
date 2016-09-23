namespace Bel3
{
    partial class ModificaCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModificaCliente));
            this.cNome = new System.Windows.Forms.TextBox();
            this.cCognome = new System.Windows.Forms.TextBox();
            this.ragsociale = new System.Windows.Forms.TextBox();
            this.codicefiscale = new System.Windows.Forms.TextBox();
            this.telefoni = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Ragionesociale = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cNome
            // 
            this.cNome.Location = new System.Drawing.Point(173, 11);
            this.cNome.Name = "cNome";
            this.cNome.Size = new System.Drawing.Size(295, 20);
            this.cNome.TabIndex = 0;
            // 
            // cCognome
            // 
            this.cCognome.Location = new System.Drawing.Point(173, 37);
            this.cCognome.Name = "cCognome";
            this.cCognome.Size = new System.Drawing.Size(295, 20);
            this.cCognome.TabIndex = 1;
            // 
            // ragsociale
            // 
            this.ragsociale.Location = new System.Drawing.Point(173, 60);
            this.ragsociale.Name = "ragsociale";
            this.ragsociale.Size = new System.Drawing.Size(295, 20);
            this.ragsociale.TabIndex = 2;
            this.ragsociale.TextChanged += new System.EventHandler(this.ragsociale_TextChanged);
            // 
            // codicefiscale
            // 
            this.codicefiscale.Location = new System.Drawing.Point(173, 86);
            this.codicefiscale.Name = "codicefiscale";
            this.codicefiscale.Size = new System.Drawing.Size(295, 20);
            this.codicefiscale.TabIndex = 3;
            // 
            // telefoni
            // 
            this.telefoni.Location = new System.Drawing.Point(78, 112);
            this.telefoni.Name = "telefoni";
            this.telefoni.Size = new System.Drawing.Size(390, 20);
            this.telefoni.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(10, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cognome:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label6.Location = new System.Drawing.Point(10, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Codice Fiscale o P. IVA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label7.Location = new System.Drawing.Point(10, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Telefoni:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Inserisci";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Ragionesociale
            // 
            this.Ragionesociale.AutoSize = true;
            this.Ragionesociale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Ragionesociale.Location = new System.Drawing.Point(10, 60);
            this.Ragionesociale.Name = "Ragionesociale";
            this.Ragionesociale.Size = new System.Drawing.Size(113, 17);
            this.Ragionesociale.TabIndex = 26;
            this.Ragionesociale.Text = "Ragione sociale:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(252, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "Modifica";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ModificaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 168);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Ragionesociale);
            this.Controls.Add(this.ragsociale);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.telefoni);
            this.Controls.Add(this.codicefiscale);
            this.Controls.Add(this.cCognome);
            this.Controls.Add(this.cNome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModificaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modifica Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cNome;
        private System.Windows.Forms.TextBox cCognome;
        private System.Windows.Forms.Label Ragionesociale;
        private System.Windows.Forms.TextBox codicefiscale;
        private System.Windows.Forms.TextBox telefoni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ragsociale;
        private System.Windows.Forms.Button button2;
    }
}