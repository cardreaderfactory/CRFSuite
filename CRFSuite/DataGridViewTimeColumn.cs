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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using crf.CustomsControls;

/**
 * Templete of this code is copied from http://msdn.microsoft.com/en-us/library/7tas5c80.aspx
 */
namespace crf
{
    public class DataGridViewTimeColumn : DataGridViewColumn
    {
        public DataGridViewTimeColumn() : base(new DataGridViewTimeCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }

            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(DataGridViewTimeCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewTimeCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewTimeCell : DataGridViewTextBoxCell
    {
        //private static readonly Color[] _colors = { Color.IndianRed, Color.BlueViolet };
        private static readonly Color[] _colors = { Color.FromArgb(253, 255, 199),
                                                    Color.FromArgb(149, 255, 229)
                                                  };

        public DataGridViewTimeCell() : base()
        {
        }

        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                      DataGridViewElementStates cellState, object value,
                                      object formattedValue, string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            CardDataGridView grid = (CardDataGridView)DataGridView;

            int cardIndex = rowIndex;

            IList<Card> _cards = grid.GetCardsFromGrid();

            if (_cards != null)
            {
                int? color = _cards[cardIndex].TimeGroupColor;
                if (color != null)
                {
                    cellStyle.BackColor = _colors[color.Value];
                }
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                       cellState, value, formattedValue, errorText, 
                       cellStyle, advancedBorderStyle, paintParts);
        }
    }
}
