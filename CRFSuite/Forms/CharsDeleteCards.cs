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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using crf.Presentation;

namespace crf.Forms
{
    public partial class CharsDeleteCards : Form
    {
        bool _closeError = false;
        IList<Card> _cards;
        List<Card> _indexes = new List<Card>();
        public int emptyCards = 0;


        public CharsDeleteCards(IList<Card> cards)
        {
            InitializeComponent();

            _cards = cards;
            textBox1.Text = Properties.Settings.Default.emptyCardChars.ToString();
        }

        public List<Card> GoodCards { get { return _indexes; } }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                
                Properties.Settings.Default.emptyCardChars = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                _closeError = true;
            }

            if (_closeError)
                MessageBox.Show(this, "Incorrect number. Please enter a correct number.", "Decode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void CharsDeleteCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_closeError)
            {
                e.Cancel = true;
                _closeError = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int maxNumber = 0;
            try
            {
                maxNumber = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                lblStimation.Text = "";
                return;
            }

            _indexes.Clear();
            emptyCards = 0;

            for (int i = 0; i < _cards.Count; i++)
            {
                Card card = _cards[i];

                if (card.Empty(maxNumber))
                {
                    emptyCards++;
                }
                else
                {
                    _indexes.Add(card);
                }
            }

            lblStimation.Text = emptyCards + " card";

            if (emptyCards != 1)
            {
                lblStimation.Text += "s";
            }

            lblStimation.Text += " found.";
        }
    }
}
