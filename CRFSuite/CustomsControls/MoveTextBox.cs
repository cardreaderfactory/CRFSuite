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
    public partial class MoveTextBox : crf.CustomsControls.TextBoxAcceptDelKey
    {
        /**
         * point where the mouse left button was pressed.
         */
        private Point textBox1Point;

        /**
         * false if the user cannot move the control
         */
        private bool _canMove;

        /**
         * false if control cannot be moved used right mouse button
         * for now it is always set to false.
         */
        private readonly bool _canMoveRightButton = false;

        /**
         * font wide.
         */
        private float _fontWide;

        /**
         * number of chars the control is shift. positive, right shitf. negative, left shift.
         */
        private int _shift;

        /**
         * Initial left coordinate for the control.
         */
        private int _leftLocation;

        public MoveTextBox()
        {
            InitializeComponent();

            _canMove = true;
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                //fixed font, does not matter the character used.
                Graphics graph = this.CreateGraphics();
                graph.PageUnit = GraphicsUnit.Pixel;
                //not sure why this function is not returning same value for i and for w
                //and not sure why but it is better if I cast to an integer.
                _fontWide = (int)graph.MeasureString("abcdefghijklmnpqrstuvwxyz", value).Width / 25;
                base.Font = value;
            }
        }

        new public Point Location
        {
            get
            {
                return base.Location;
            }

            set
            {
                _shift = 0;
                _leftLocation = value.X;
                base.Location = value;
            }
        }

        public new int Left
        {
            get
            {
                return base.Left;
            }

            set
            {
                _shift = 0;
                _leftLocation = value;
                base.Left = value;
            }
        }

        private void textBox1_LocationChanged(object sender, EventArgs e)
        {
            if ((this.Parent == null) || (!this.Parent.Created))
                _leftLocation = Location.X;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((!_canMove) || (!_canMoveRightButton))
                return;

            if (e.Button == MouseButtons.Right)
            {
                textBox1Point = e.Location;

                Cursor.Current = Cursors.VSplit;
            }
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((!_canMove) || (!_canMoveRightButton))
                return;

            if (e.Button == MouseButtons.Right)
                Cursor.Current = Cursors.Default;
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if ((!_canMove) || (!_canMoveRightButton))
                return;

            Cursor.Current = Cursors.Cross;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if ((!_canMove) || (!_canMoveRightButton))
                return;

            Cursor.Current = Cursors.Default;
        }

        private void textBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!_canMove)
                return;

            if (e.Delta > 0)
                MoveToRight();
            else if (e.Delta < 0)
                MoveToLeft();
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((!_canMove) || (!_canMoveRightButton))
                return;

            if (e.Button == MouseButtons.Right)
            {
                int shift = textBox1Point.X - e.Location.X;

                //move left
                if (shift > _fontWide)
                {
                    //if (this.Right > (_fontWide * 2))
                    if ((-LeftInChars + 1) < this.Text.Length)
                    {
                        _shift--;

                        base.Left = (int)(_leftLocation + _shift * _fontWide) - this.GetPositionFromCharIndex(0).X;
                        textBox1Point.X -= (int)_fontWide;
                    }
                }
                else if ((-shift) > _fontWide)
                {
                    //move right
                    if ((base.Left + _fontWide * 2) < this.Parent.ClientSize.Width)
                    {
                        _shift++;

                        base.Left = (int)(_leftLocation + _shift * _fontWide);
                        textBox1Point.X += (int)_fontWide;
                    }
                }
            }
        }

        public void MoveToLeft()
        {
            //do not move control to left more than the original position.
            //if (((-LeftInChars()) + 1) <= this.Text.Length)
            if (LeftInChars > 0)
            {
                _shift--;

                base.Left = (int)(_leftLocation + _shift * _fontWide) - this.GetPositionFromCharIndex(0).X;
            }
        }

        public void MoveToRight()
        {
            if ((base.Left + _fontWide * 2) < this.Parent.ClientSize.Width)
            {
                _shift++;

                base.Left = (int)(_leftLocation + _shift * _fontWide);
            }
        }

        public bool CanMove
        {
            get
            {
                return _canMove;
            }

            set
            {
                _canMove = value;
            }
        }

        public int LeftInChars
        {
            get
            {
                //round to the nearest integer.
                return (int)Math.Round((((float)base.Left - _leftLocation) / _fontWide), 0, MidpointRounding.AwayFromZero);
            }

            set
            {
                _shift = value;
                base.Left = (int)(_fontWide * value + _leftLocation);
            }
        }
    }
}
