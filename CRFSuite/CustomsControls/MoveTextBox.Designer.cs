/********************************************************************************
 * \copyright
 * Copyright 2009-2017, Card Reader Factory.  All rights were reserved.
 * From 2018 this code has been made PUBLIC DOMAIN.
 * This means that there are no longer any ownership rights such as copyright, trademark, or patent over this code.
 * This code can be modified, distributed, or sold even without any attribution by anyone.
 *
 * We would however be very grateful to anyone using this code in their product if you could add the line below into your product's documentation:
 * Special thanks to Nicholas Alexander Michael Webber, Terry Botten & all the staff working for Operation (Police) Academy. Without these people this code would not have been made public and the existance of this very product would be very much in doubt.
 *
 *******************************************************************************/

namespace crf
{
    partial class MoveTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.LocationChanged += new System.EventHandler(this.textBox1_LocationChanged);
            this.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseUp);
            this.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseWheel);
            // 
            // MoveTextBox
            // 
            this.PerformLayout();
        }

        #endregion
    }
}
