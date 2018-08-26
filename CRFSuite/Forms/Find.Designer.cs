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
    partial class Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Find));
            this.lblWhat = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkCase = new System.Windows.Forms.CheckBox();
            this.groupDirection = new System.Windows.Forms.GroupBox();
            this.radioDown = new System.Windows.Forms.RadioButton();
            this.radioUp = new System.Windows.Forms.RadioButton();
            this.groupDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWhat
            // 
            this.lblWhat.AutoSize = true;
            this.lblWhat.Location = new System.Drawing.Point(12, 18);
            this.lblWhat.Name = "lblWhat";
            this.lblWhat.Size = new System.Drawing.Size(56, 13);
            this.lblWhat.TabIndex = 0;
            this.lblWhat.Text = "Find what:";
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(80, 11);
            this.txtFind.MaxLength = 80;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(162, 20);
            this.txtFind.TabIndex = 0;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // btnNext
            // 
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnNext.Location = new System.Drawing.Point(254, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Find Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(254, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkCase
            // 
            this.checkCase.AutoSize = true;
            this.checkCase.Location = new System.Drawing.Point(15, 65);
            this.checkCase.Name = "checkCase";
            this.checkCase.Size = new System.Drawing.Size(82, 17);
            this.checkCase.TabIndex = 1;
            this.checkCase.Text = "Match case";
            this.checkCase.UseVisualStyleBackColor = true;
            this.checkCase.CheckedChanged += new System.EventHandler(this.checkCase_CheckedChanged);
            // 
            // groupDirection
            // 
            this.groupDirection.Controls.Add(this.radioDown);
            this.groupDirection.Controls.Add(this.radioUp);
            this.groupDirection.Location = new System.Drawing.Point(126, 37);
            this.groupDirection.Name = "groupDirection";
            this.groupDirection.Size = new System.Drawing.Size(116, 45);
            this.groupDirection.TabIndex = 2;
            this.groupDirection.TabStop = false;
            this.groupDirection.Text = "Direction";
            // 
            // radioDown
            // 
            this.radioDown.AutoSize = true;
            this.radioDown.Location = new System.Drawing.Point(51, 19);
            this.radioDown.Name = "radioDown";
            this.radioDown.Size = new System.Drawing.Size(53, 17);
            this.radioDown.TabIndex = 4;
            this.radioDown.TabStop = true;
            this.radioDown.Text = "Down";
            this.radioDown.UseVisualStyleBackColor = true;
            // 
            // radioUp
            // 
            this.radioUp.AutoSize = true;
            this.radioUp.Location = new System.Drawing.Point(6, 19);
            this.radioUp.Name = "radioUp";
            this.radioUp.Size = new System.Drawing.Size(39, 17);
            this.radioUp.TabIndex = 3;
            this.radioUp.TabStop = true;
            this.radioUp.Text = "Up";
            this.radioUp.UseVisualStyleBackColor = true;
            this.radioUp.CheckedChanged += new System.EventHandler(this.radioUp_CheckedChanged);
            // 
            // Find
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(340, 97);
            this.Controls.Add(this.groupDirection);
            this.Controls.Add(this.checkCase);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.lblWhat);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(Properties.Resources.CRFIcon));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Find";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.Load += new System.EventHandler(this.Find_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Find_FormClosed);
            this.groupDirection.ResumeLayout(false);
            this.groupDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWhat;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkCase;
        private System.Windows.Forms.GroupBox groupDirection;
        private System.Windows.Forms.RadioButton radioDown;
        private System.Windows.Forms.RadioButton radioUp;
    }
}