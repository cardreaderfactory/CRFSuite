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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace crf
{
    public partial class MoveButtonTextBox : UserControl
    {
        /**
         * Event handler to know when text control is moved.
         */
        public event EventHandler TextMove = null;

        /**
         * Event handler to know when text control text is changed.
         */
        [Browsable(true)]
        public new event EventHandler TextChanged = null;

        public MoveButtonTextBox()
        {
            InitializeComponent();
        }

        public TextBox EditingControl
        {
            get
            {
                return moveTextBox;
            }
        }

        #region Text box properties

        public override string Text
        {
            get
            {
                return moveTextBox.Text;
            }

            set
            {
                moveTextBox.Text = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return moveTextBox.MaxLength;
            }

            set
            {
                moveTextBox.MaxLength = value;
            }
        }


        public Font TextFont
        {
            get
            {
                return moveTextBox.Font;
            }

            set
            {
                moveTextBox.Font = value;
            }
        }

        #endregion

        #region Label properties

        public string LabelText
        {
            get
            {
                return lblCard.Text;
            }

            set
            {
                lblCard.Text = value;
            }
        }

        public Font LabelFont
        {
            get
            {
                return lblCard.Font;
            }

            set
            {
                lblCard.Font = value;
            }
        }

        #endregion

        #region MoveTextBox properties
        
        public bool CanMove
        {
            get
            {
                return moveTextBox.CanMove;
            }

            set
            {
                moveTextBox.CanMove = value;
                hScrollBar.Visible = value;
            }
        }

        public int LeftInChars
        {
            get
            {
                return moveTextBox.LeftInChars;
            }

            set
            {
                moveTextBox.LeftInChars = value;
            }
        }
        
        #endregion

        #region Events

        private void moveTextBox_Move(object sender, EventArgs e)
        {
            if (null != TextMove)
            {
                TextMove(sender, e);
            }
        }

        private void moveTextBox_TextChanged(object sender, EventArgs e)
        {
            if (null != TextChanged)
                TextChanged(sender, e);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                moveTextBox.MoveToRight();
            else if (e.NewValue < e.OldValue)
                moveTextBox.MoveToLeft();
        }

        #endregion

        #region ToolStripMenu events

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //enable/disable correct menu entries.
            undoToolStripMenuItem.Enabled = moveTextBox.CanUndo;
            cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = moveTextBox.SelectionLength > 0;
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
            selectAllToolStripMenuItem.Enabled = moveTextBox.TextLength > 0;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moveTextBox.SelectAll();
        }

        #endregion
    }
}
