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

/**
 * Copied from http://social.msdn.microsoft.com/forums/en-US/winformsdatacontrols/thread/769ca9d6-1e9d-4d76-8c23-db535b2f19c2/
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

using crf.CustomsControls;

namespace crf
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;

        static DataGridViewProgressCell()
        {
            emptyImage = ResourcesLoader.LoadImage(ResourcesLoader.ImageID.Empty);
        }

        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }

        // Method required to make the Progress Cell consistent with the default Image Cell.
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                                                    int rowIndex, 
                                                    ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter,
                                                    TypeConverter formattedValueTypeConverter,
                                                    DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g, 
                                      System.Drawing.Rectangle clipBounds, 
                                      System.Drawing.Rectangle cellBounds, 
                                      int rowIndex, DataGridViewElementStates cellState, 
                                      object value, object formattedValue, string errorText, 
                                      DataGridViewCellStyle cellStyle, 
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle, 
                                      DataGridViewPaintParts paintParts)
        {
            CardDataGridView grid = DataGridView as CardDataGridView;

            int cardIndex = rowIndex;

            IDataGridViewProgressColumnValue columnValue = (IDataGridViewProgressColumnValue)this.DataGridView.DataSource;

            int progressVal = value!=null ?(int)value:0;
            // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
            float percentage = ((float)progressVal / (float)columnValue.MaximumValue);
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);


            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds,
                       rowIndex, cellState, value, formattedValue, errorText,
                       cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            if (percentage > 0.0)
            {
                // Draw the progress bar and the text
                g.FillRectangle(new SolidBrush(Color.FromArgb(163, 189, 242)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                g.DrawString(progressVal.ToString() + "(" + ((int)(percentage * 100)).ToString() + "%)", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
            }
            else
            {
                // draw the text
                if (this.DataGridView.CurrentRow.Index == rowIndex)
                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
                else
                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
            }
        }
    }
}
