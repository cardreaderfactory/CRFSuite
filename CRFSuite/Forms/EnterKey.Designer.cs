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
    partial class EnterKey
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
            this.lblKey = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.showKey = new System.Windows.Forms.CheckBox();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.rememberKey = new System.Windows.Forms.CheckBox();
            this.cmdRequestKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(13, 15);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(195, 13);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "Enter key (32 characters long, 0..9 A..F)";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(15, 40);
            this.key.MaxLength = 32;
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(236, 20);
            this.key.TabIndex = 0;
            this.key.UseSystemPasswordChar = true;
            //this.key.Validating += new System.ComponentModel.CancelEventHandler(this.validatingKey);
            // 
            // showKey
            // 
            this.showKey.AutoSize = true;
            this.showKey.Location = new System.Drawing.Point(15, 66);
            this.showKey.Name = "showKey";
            this.showKey.Size = new System.Drawing.Size(73, 17);
            this.showKey.TabIndex = 1;
            this.showKey.Text = "Show key";
            this.showKey.UseVisualStyleBackColor = true;
            this.showKey.CheckedChanged += new System.EventHandler(this.checkShow_CheckedChanged);
            // 
            // cmdAccept
            // 
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Location = new System.Drawing.Point(15, 91);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(101, 23);
            this.cmdAccept.TabIndex = 2;
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(148, 91);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(101, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // rememberKey
            // 
            this.rememberKey.AutoSize = true;
            this.rememberKey.Location = new System.Drawing.Point(156, 66);
            this.rememberKey.Margin = new System.Windows.Forms.Padding(2);
            this.rememberKey.Name = "rememberKey";
            this.rememberKey.Size = new System.Drawing.Size(97, 17);
            this.rememberKey.TabIndex = 5;
            this.rememberKey.Text = "&Remember key";
            this.rememberKey.UseVisualStyleBackColor = true;
            this.rememberKey.CheckedChanged += new System.EventHandler(this.rememberKey_CheckedChanged);
            // 
            // cmdRequestKey
            // 
            this.cmdRequestKey.Location = new System.Drawing.Point(214, 10);
            this.cmdRequestKey.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRequestKey.Name = "cmdRequestKey";
            this.cmdRequestKey.Size = new System.Drawing.Size(36, 23);
            this.cmdRequestKey.TabIndex = 6;
            this.cmdRequestKey.Text = "?";
            this.cmdRequestKey.UseVisualStyleBackColor = true;
            this.cmdRequestKey.Click += new System.EventHandler(this.cmdRequestKey_Click);
            // 
            // EnterKey
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(262, 123);
            this.Controls.Add(this.cmdRequestKey);
            this.Controls.Add(this.rememberKey);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.showKey);
            this.Controls.Add(this.key);
            this.Controls.Add(this.lblKey);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterKey";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Key";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnterKey_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.CheckBox showKey;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.TextBox key;
        private System.Windows.Forms.CheckBox rememberKey;
        private System.Windows.Forms.Button cmdRequestKey;
    }
}