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
using System.ComponentModel;

using crf;
using crf.CustomsControls;
using crf.Algorithm;

/**
 * Templete of this code is copied from http://msdn.microsoft.com/en-us/library/7tas5c80.aspx
 */
namespace crf
{
    public class DataGridViewTrackColumn : DataGridViewColumn
    {
        public DataGridViewTrackColumn()
            : base(new DataGridViewTrackCell())
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
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(DataGridViewTrackCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewTrackCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewTrackCell : DataGridViewImageCell /*DataGridViewTextBoxCell*/
    {
        //font size will be calculated only once.
        static Size fontSize = new Size(-1, -1);

        public DataGridViewTrackCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            if (this.RowIndex < 0)
                return;

            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewTrackEditingControl ctl = DataGridView.EditingControl as DataGridViewTrackEditingControl;
            ctl.Font = dataGridViewCellStyle.Font;
            ctl.Width = GetSize(this.RowIndex).Width;
            ctl.SelectionBackColor = dataGridViewCellStyle.BackColor;
            ctl.ParityText = (StringWithParity)this.Value;
            CardDataGridView grid = (CardDataGridView)DataGridView;
            ctl.variant = grid.GetVariant(rowIndex, this.ColumnIndex);
            ctl.originalLength = ctl.Text.Length;
            if (ctl.variant != null)
            {
                bool ali = grid.GetVariant(rowIndex, this.ColumnIndex).Alignment == null;
                int alignment = ali ? 0 : (int)grid.GetVariant(rowIndex, this.ColumnIndex).Alignment;
                ctl.Margin = new Padding(alignment * CalculateFontSize(ctl.Font).Width + 3, 3, 3, 3);
                ctl.SelectionAlignment = HorizontalAlignment.Left;
                ctl.Modified = false;
                if (ali)
                {
                    if (ctl.variant.HasSS)
                        ctl.SelectionAlignment = HorizontalAlignment.Left;
                    else if (ctl.variant.HasES)
                        ctl.SelectionAlignment = HorizontalAlignment.Right;
                    else
                        ctl.SelectionAlignment = HorizontalAlignment.Center;
                }

            }
            else
            {
                ctl.Margin = new Padding(3, 3, 3, 3);
                ctl.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        public override void DetachEditingControl()
        {
            if (DataGridView == null)
                return;

            DataGridViewTrackEditingControl ctl = DataGridView.EditingControl as DataGridViewTrackEditingControl;

            if (ctl != null && ctl.Modified)
            {
                StringWithParity text = ctl.ParityText;

                CardDataGridView grid = (CardDataGridView)DataGridView;
                Card card = grid.GetCard(this.RowIndex);

                if (card != null)
                    card.SetTrackString(this.ColumnIndex - (int)Presentation.cardGridColumn.TRACK1_COLUMN, text);
                //maybe this will slow the programm, but it is needed.
                // otherwise we will get alignment bugs once content changed.
                ListCardUtil.SetAlignment(grid.GetActualCardsFromGrid(), 0);
                if (  crf.Properties.Settings.Default.autoResize && 
                     (ctl.Text.Length > ctl.originalLength || ctl.Text.Length < ctl.originalLength - 10)
                   )
                {
                    grid.AutoResizeColumn(this.ColumnIndex);
#if DEBUG
                    CardDataGridView.RESIZECOLUMNCOUNT++;
                    grid.UpdateStatusStripTextWhenResizing();
#endif
                }
            }

            base.DetachEditingControl();
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that DataGridViewTrackCell uses.
                return typeof(DataGridViewTrackEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that Track contains.
                return typeof(StringWithParity);
            }

            set
            {
                base.ValueType = value;
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // empty string.
                return null;
            }
        }

        protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                      DataGridViewElementStates cellState, object value,
                                      object formattedValue, string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            if (value == null || DataGridView == null)
                return;

            CardDataGridView grid = (CardDataGridView)DataGridView;

            Variant variant = grid.GetVariant(rowIndex, this.ColumnIndex);

            //group might be empty.
            int? alignment = null;
            if (null != variant)
            {
                //issue 5: A card with SS and ES should not be aligned by the previous card.
                //                if (variant.HasSS && variant.HasES)
                //                {
                //                    cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //                }
                //                else 
                if (variant.Alignment != null)
                {
                    alignment = variant.Alignment.Value;
                }
                else
                {
                    if (variant.HasSS)
                        cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    else if (variant.HasES)
                        cellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    else
                        cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            /*
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                       cellState, value.ToString(), formattedValue, errorText, 
                       cellStyle, advancedBorderStyle, paintParts);
            */

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                       cellState, null, null, errorText,
                       cellStyle, advancedBorderStyle, paintParts);

            Image img = GetRtfImage(rowIndex, value, base.Selected, cellStyle, alignment);

            if (alignment == null)
                alignment = 0;

            if (fontSize.Width == -1)
                fontSize = CalculateFontSize(cellStyle.Font);

            if (img != null)
                graphics.DrawImage(img,
                                   cellBounds.Left + _editingControl.Margin.Left + fontSize.Width * alignment.Value,
                                   cellBounds.Top + _editingControl.Margin.Top);
        }
        #region Functions needed to use RichTextCellView

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle,
                                                    TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter,
                                                    DataGridViewDataErrorContexts context)
        {
            return value;
        }

        public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip,
                                                    DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded,
                                                    bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            base.PositionEditingControl(setLocation, setSize, cellBounds, cellClip, cellStyle,
                                        singleVerticalBorderAdded, singleHorizontalBorderAdded,
                                        isFirstDisplayedColumn, isFirstDisplayedRow);

            /** not sure why I have to do this manually in order to center control correctly */
            if (setLocation && DataGridView != null)
            {
                DataGridView.EditingControl.Left += DataGridView.EditingControl.Margin.Left;
                DataGridView.EditingControl.Top += DataGridView.EditingControl.Margin.Top;
            }
        }

        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return new Size(0, 0);

            CardDataGridView grid = (CardDataGridView)DataGridView;
            Variant variant = grid.GetVariant(rowIndex, this.ColumnIndex);
            StringWithParity value = (StringWithParity)GetValue(rowIndex);
            Size size = new Size(0, 0);

            if (fontSize.Width == -1)
                fontSize = CalculateFontSize(cellStyle.Font);

            if ((value != null) && (variant != null))
            {
                string sValue = value.ToString();

                int alignment = 0;
                if (variant.Alignment != null)
                    alignment = variant.Alignment.Value;

                size.Width = fontSize.Width * (sValue.Length + alignment);
                size.Height = fontSize.Height;
            }

            size.Width += _editingControl.Margin.Left + _editingControl.Margin.Right + 1;
            size.Height += _editingControl.Margin.Bottom + _editingControl.Margin.Top + 1;

            return size;
        }

        private Size CalculateFontSize(Font font)
        {
            Size size = TextRenderer.MeasureText("a", font);
            size.Width = size.Width / 2;

            return size;
        }

        //private static readonly RichTextBox _editingControl = new RichTextBox();
        private static readonly DataGridViewTrackEditingControl _editingControl = new DataGridViewTrackEditingControl();

        // Images for selected and normal states.
        private Image GetRtfImage(int rowIndex, object value, bool selected, DataGridViewCellStyle cellStyle, int? leftAlignment)
        {
            Size cellSize = GetSize(rowIndex);

            if (cellSize.Width < 1 || cellSize.Height < 1)
                return null;

            //RichTextBox ctl = _editingControl;
            DataGridViewTrackEditingControl ctl = _editingControl;
            if (ctl == null)
                return null;
            //ctl.ApplyCellStyleToEditingControl(cellStyle);
            ctl.Size = GetSize(rowIndex);
            ctl.Font = cellStyle.Font;
            ctl.ParityText = (StringWithParity)value;
            //ctl.Text = ((CRCString)value).ToString();

            if (leftAlignment == null)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                        ctl.SelectionAlignment = HorizontalAlignment.Left;
                        break;

                    case DataGridViewContentAlignment.MiddleRight:
                        ctl.SelectionAlignment = HorizontalAlignment.Right;
                        break;

                    default:
                        ctl.SelectionAlignment = HorizontalAlignment.Center;
                        break;
                }

                leftAlignment = 0;
            }

            // Print the content of RichTextBox to an image.
            Size imgSize = new Size(cellSize.Width - _editingControl.Margin.Left - _editingControl.Margin.Right + 1 - leftAlignment.Value * fontSize.Width,
                                    cellSize.Height - _editingControl.Margin.Bottom - _editingControl.Margin.Top + 1);
            //it might happen if alignment is big.
            if (imgSize.Width <= 0)
                imgSize.Width = 1;
            Image rtfImg = null;

            if (selected)
            {
                // Selected cell state
                ctl.BackColor = DataGridView.DefaultCellStyle.SelectionBackColor;
                ctl.ForeColor = DataGridView.DefaultCellStyle.SelectionForeColor;

                // Print image
                rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);

                // Restore RichTextBox
                ctl.BackColor = DataGridView.DefaultCellStyle.BackColor;
                ctl.ForeColor = DataGridView.DefaultCellStyle.ForeColor;
            }
            else
            {
                rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);
            }

            return rtfImg;
        }

        #region Handlers of edit events, copied from DataGridViewTextBoxCell

        private byte flagsState;

        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            base.OnEnter(rowIndex, throughMouseClick);

            if ((base.DataGridView != null) && throughMouseClick)
            {
                this.flagsState = (byte)(this.flagsState | 1);
            }
        }

        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            base.OnLeave(rowIndex, throughMouseClick);

            if (base.DataGridView != null)
            {
                this.flagsState = (byte)(this.flagsState & -2);
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (base.DataGridView != null)
            {
                Point currentCellAddress = base.DataGridView.CurrentCellAddress;

                if (((currentCellAddress.X == e.ColumnIndex) &&
                    (currentCellAddress.Y == e.RowIndex)) &&
                    (e.Button == MouseButtons.Left))
                {
                    if ((this.flagsState & 1) != 0)
                    {
                        this.flagsState = (byte)(this.flagsState & -2);
                    }
                    else if (base.DataGridView.EditMode !=
                              DataGridViewEditMode.EditProgrammatically)
                    {
                        base.DataGridView.BeginEdit(false);
                    }
                }
            }
        }

        #endregion

        #endregion
    }

    public class DataGridViewTrackEditingControl : RichTextBox, IDataGridViewEditingControl //DataGridViewTextBoxEditingControl
    {
        /**
         * Bug 15. <del> is not working in editing text boxes. Solution found on:
         * http://www.syncfusion.com/FAQ/WindowsForms/FAQ_c94c.aspx
         */
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_CHAR = 0x102;
        private const int WM_MOUSEWHEEL = 0x020A;
        public Variant variant = null;
        public int originalLength = 0;


        public DataGridViewTrackEditingControl()
            : base()
        {
            base.MaxLength = 200;
            base.Multiline = false;
            
        }
        private int MaxCharValidValue(int add, int bpc)
        {
            return add + (1 << (bpc - 1));
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            
            Point p = this.GetPositionFromCharIndex(SelectionStart - 1);
            ((frmDecode)this._grid.FindForm()).PrePoint = this.PointToScreen(p);

            base.OnKeyPress(e);
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {
            }
            base.DefWndProc(ref m);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL)
            {   
                //SendMessage(richTextBox1.Handle, 0x020A, (int)m.WParam, (int)m.LParam);
            }
            else
                base.WndProc(ref m);
        }

        //this guy can only process key input...
        public override bool PreProcessMessage(ref Message msg)
        {
            Keys keyCode = (Keys)(int)msg.WParam & Keys.KeyCode;
            
            if ((msg.Msg == WM_KEYDOWN || msg.Msg == WM_KEYUP)
                 && keyCode == Keys.Delete)
            {
                return false;
            }
            else if (msg.Msg == WM_CHAR)
            {
                //do we need to translate to upper case?
                if (((char)keyCode >= 'a') && ((char)keyCode <= 'z'))
                {
                    keyCode = (Keys)((char)keyCode - ('a' - 'A'));

                    //high bytes of WParam are missed. I do not think this is a problem
                    //since we are translating only letters.
                    msg.WParam = (IntPtr)(int)keyCode;
                }

                // how get the cell row and col?
                // (new System.Collections.ArrayList.ArrayListDebugView(this.EditingControlDataGridView.SelectedCells.items)).Items[0]
                //    this.EditingControlDataGridView.SelectedCells.GetEnumerator

                //filter chars that cannot be encoded depending on user settings.
                //do not filter control chars.
                if ((keyCode >= Keys.Space) && (variant != null) &&
                    (((int)keyCode < variant.add) ||
                     ((int)keyCode > MaxCharValidValue(variant.add, variant.Bpc))))
                {
                    return true;
                }
            }

            return base.PreProcessMessage(ref msg);
        }

        public StringWithParity ParityText
        {
            get
            {
                UTF8Encoding UTF8 = new UTF8Encoding();

                return new StringWithParity(UTF8.GetBytes(base.Text), GetTextCRC());
            }

            set
            {
                string valueString = value.ToString();
                base.Text = valueString;
                if (StringWithParity.IsNullOrEmpty(value))
                    return;

                bool[] parityCheck = (bool[])value;

                int i = 0;
                while ((i < valueString.Length) && (i < parityCheck.Length))
                {
                    int length = 0;
                    bool crcValue = parityCheck[i];

                    do
                    {
                        i++;
                        length++;
                    }
                    while ((i < valueString.Length) && (i < parityCheck.Length) && (crcValue == parityCheck[i]));

                    base.Select(i - length, length);

                    if (!crcValue)
                    {
                        base.SelectionFont = new Font(base.Font, FontStyle.Underline);
                        base.SelectionColor = Color.Green;
                    }
                    else
                    {
                        base.SelectionFont = new Font(base.Font, FontStyle.Regular);
                        base.SelectionColor = Color.Black;

                        //if ((value.StopByte() + 1) == i)
                        //{
                        //    base.SelectionColor = Color.Black;
                        //}
                        //else
                        //{
                        //    base.SelectionColor = Color.DimGray;
                        //}
                    }
                }

                this.Modified = false;
            }
        }

        public bool[] GetTextCRC()
        {
            if (base.Text.Length == 0)
                return null;

            bool[] retValue = new bool[base.Text.Length];

            for (int i = 0; i < retValue.Length; i++)
            {
                base.Select(i, 1);
                retValue[i] = (base.SelectionColor != Color.Green);
            }

            return retValue;
        }

        #region IDataGridViewEditingControl interface implementation

        private object _formattedValue = null;
        private int _rowIndex = 0;
        private DataGridView _grid = null;

        public DataGridView EditingControlDataGridView { get { return _grid; } set { _grid = value; } }

        public object EditingControlFormattedValue { get { return _formattedValue; } set { _formattedValue = value; } }

        public int EditingControlRowIndex { get { return _rowIndex; } set { _rowIndex = value; } }

        public bool EditingControlValueChanged { get { return this.Modified; } set { this.Modified = value; } }

        public Cursor EditingPanelCursor { get { return base.Cursor; } }

        public bool RepositionEditingControlOnValueChange { get { return true; } }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            //base.Font = dataGridViewCellStyle.Font;
            base.ForeColor = dataGridViewCellStyle.ForeColor;
            base.BorderStyle = BorderStyle.None;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (this.variant != null && keyData == Keys.Left || keyData == Keys.Right)
            {
                Point p = this.GetPositionFromCharIndex(SelectionStart - 1);
                p.X += (keyData == Keys.Left? -1 : 1) * TextRenderer.MeasureText(" ", this.Font).Width / 2;

                if (SelectionStart == 0)
                    p.X -= TextRenderer.MeasureText(" ", this.Font).Width/2;
                ((frmDecode)this._grid.FindForm()).PrePoint = this.PointToScreen(p);
            }

            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Return:
                    // The code for 'Enter' is copied from 
                    // DataGridViewTextBoxEditingControl,
                    // Shift + Enter = NewLine
                    if ((((keyData & (Keys.Alt | Keys.Control | Keys.Shift))
                         == Keys.Shift) && this.Multiline))
                    {
                        return true;
                    }
                    break;

                case Keys.Left:
                    if (this.SelectedText == this.Text)
                    {
                        SelectionStart = 0;
                        return true;
                    }
                    if (SelectionStart != 0)
                        return true;
                    break;
                case Keys.Right:
                  if (SelectionStart != Text.Length)
                        return true;
                    break;

                case Keys.Up:
                case Keys.Down:
                    return false;

                case Keys.Home:
                case Keys.End:
                    return true;
            }

            return !dataGridViewWantsInputKey;
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return _formattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            //not sure why this function is called with selectAll to false but we want to select all
            //and text editing control selects all text.
            //if (selectAll)
            this.SelectAll();
        }

        #endregion
    }
}
