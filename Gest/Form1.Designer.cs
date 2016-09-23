namespace Bel3
{
    partial class Telefonate
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
            this.TelefonateTex = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TelefonateTex
            // 
            this.TelefonateTex.Location = new System.Drawing.Point(12, 12);
            this.TelefonateTex.Name = "TelefonateTex";
            this.TelefonateTex.Size = new System.Drawing.Size(625, 411);
            this.TelefonateTex.TabIndex = 0;
            this.TelefonateTex.Text = "";
            // 
            // Telefonate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 456);
            this.Controls.Add(this.TelefonateTex);
            this.Name = "Telefonate";
            this.Text = "Telefonate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox TelefonateTex;


    }
}