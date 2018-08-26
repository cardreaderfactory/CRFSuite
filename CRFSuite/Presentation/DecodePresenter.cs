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
using System.Diagnostics;
using System.IO;
using Serializable;
using crf.Properties;
using System.Drawing;
using crf.Forms;
using crf.CustomsControls;
using TaskDialogDLL;
using crf.Algorithm;

namespace crf.Presentation
{
    public class Operation
    {
        string OpName;
        List<Card> Content;

        public Operation(string op, List<Card> con)
        {
            OpName = op;
            Content = con;
        }
        public void AddToOperationList(List<Operation> list, ref int index)
        {
            if (index < list.Count - 1)
            {
                list.RemoveRange(index + 1, list.Count - index - 1);
            }
            list.Add(this);
            index++;
        }
        public void Undo(CustomsControls.CardDataGridView CardsGrid)
        {
            switch (OpName)
            {
                case "Cut":
                case "Delete":
                    int firstRow;
                    if (!CardsGrid.GetFirstSelectedRow(out firstRow))
                        firstRow = 0;
                    CardsGrid.AddCards(firstRow, Content);
                    break;
                case "Paste":
                    CardsGrid.DeleteCards(Content);
                    break;
            }
        }
        public void Redo(CustomsControls.CardDataGridView CardsGrid)
        {
            switch (OpName)
            {
                case "Cut":
                case "Delete":
                    CardsGrid.DeleteCards(Content);
                    break;
                case "Paste":
                    int firstRow;
                    if (!CardsGrid.GetFirstSelectedRow(out firstRow))
                        firstRow = 0;
                    CardsGrid.AddCards(firstRow, Content);
                    break;
            }
        }
    }

    public enum cardGridColumn
    {
        READER_COLUMN,
        COUNT_COLUMN,
        DIRECTION_COLUMN,
        TIME_COLUMN,
        TRACK1_COLUMN,
        TRACK2_COLUMN,
        TRACK3_COLUMN

    }
    public enum VariantGridColumn
    {
        Rate,
        Direction,
        AsString,
        Binary
    }
    public enum ComboxSelect
    {
        GroupSwipesUnits,
        ShowAdvanceDecode,
        TimeFormat,
        DecodeMethod,
        TrackBPC,
    }



    class DecodePresenter : ISearchablePresenter
    {
        private readonly string CLIPBOARD_CARDS_NAME = "Cards";
        private const int CARD_READER_COLUMN = 1;
        private const int CARD_DIRECTION_COLUMN = 2;
        private const int CARD_TIME_COLUMN = 3;
        private const int CARD_TRACK1_COLUMN = 4;
        private const int CARD_TRACK2_COLUMN = 5;
        private const int CARD_TRACK3_COLUMN = 6;

        private List<Operation> _ops = new List<Operation>();
        private int _opsIndex = -1;

        private frmDecode _decodeForm;
        public DecodeSettings settingsWindow;
        private List<double> timeDiff;
        public DecodePresenter(frmDecode form)
        {
            _decodeForm = form;
        }
        public Form Form()
        {
            return _decodeForm.DecodeForm;
        }

        #region File Menu Method
        public bool saveCurrentFile(string filename, CardFileFormat.Format format, bool export)
        {
            int fields = 0;

            CardWriter cardWriter = null;

            try
            {
                cardWriter = new CardWriter(filename, format);

                for (int i = 0; i < cardWriter.Fields.Length; i++)
                {
                    //column order must be the same as fields order!!
                    if (_decodeForm.CardsGrid.Columns[i].Visible)
                        fields |= cardWriter.Fields[i];
                }

                cardWriter.Write(_decodeForm.currentKey, _decodeForm.CardsGrid.GetCardsFromGrid(), fields);

                if (!export)
                {
                    _decodeForm.CurrentFileFormat = cardWriter.FileFormat;
                    _decodeForm.CurrentFileName = filename;
                }
                else
                {
                    _decodeForm.openExportedTextFile(filename);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                TaskDialog.MessageBox(_decodeForm.DecodeForm,
                            "Error",
                            "Cannot save file: " + filename,
                            frmDecode.messageSaveFailed,
                            "",
                            TaskDialogButtons.OK,
                            SysIcons.Error
                            );
                return false;
            }
            finally
            {
                if (cardWriter != null)
                    cardWriter.Close();
            }
            _decodeForm.isCrfModified = false;
            return true;
        }
        public void exportToFile(string file, CardFileFormat.Format format)
        {
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                if (MessageBox.Show(_decodeForm.DecodeForm, "File already exists. Do you want to replace it?", "Warning",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    exportCurrentFile(fileInfo.Name, format);
                    return;
                }
            }

            saveCurrentFile(file, format, true);
        }

        public void exportCurrentFile(string file, CardFileFormat.Format format)
        {
            string filter;

            switch (format)
            {
                case CardFileFormat.Format.TXT:
                    filter = "Text Files (*.txt)|*.txt";
                    break;

                case CardFileFormat.Format.CSV:
                    filter = "CSV Files (*.csv)|*.csv";
                    break;

                case CardFileFormat.Format.NONE:
                    filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";
                    break;

                default:
                    throw new ArgumentException("Not supported export format.");
            }

            _decodeForm.SaveFileDialog.FileName = file;
            _decodeForm.SaveFileDialog.Filter = filter;
            //saveFileDialog.AutoUpgradeEnabled = true;

            if (_decodeForm.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = _decodeForm.SaveFileDialog.FileName;

                //file will be saved depending on file extension
                saveCurrentFile(filename, format, true);
            }
        }
        public string GetCurrentFullFileNoWithExtension()
        {
            FileInfo fileInfo = new FileInfo(_decodeForm.CurrentFileName);

            return _decodeForm.CurrentFileName.Substring(0, _decodeForm.CurrentFileName.Length - fileInfo.Extension.Length);
        }
        #endregion
        #region
        public TextBoxBase getEditingControl()
        {
            TextBoxBase txtEditingBox = getEditingControlFromGrid(_decodeForm.CardsGrid);

            if (txtEditingBox == null)
                txtEditingBox = getEditingControlFromGrid(_decodeForm.VariantsGrid);

            return txtEditingBox;
        }
        private TextBoxBase getEditingControlFromGrid(DataGridView grid)
        {
            TextBoxBase txtEditingBox = null;

            if (grid.EditingControl != null)
            {
                try
                {
                    txtEditingBox = (TextBoxBase)grid.EditingControl;
                }
                catch
                {
                }
            }

            return txtEditingBox;
        }
        public void gridUndo()
        {
            if (isUndoAccess)
            {
                Operation op = _ops[_opsIndex--];
                op.Undo(_decodeForm.CardsGrid);
            }
        }
        public void gridRedo()
        {
            if (isRedoAccess)
            {
                Operation op = _ops[++_opsIndex];
                op.Redo(_decodeForm.CardsGrid);
            }
        }
        public bool isUndoAccess { get { return (_opsIndex > -1) && (_opsIndex < _ops.Count); } }
        public bool isRedoAccess { get { return (_opsIndex > -2) && (_opsIndex < _ops.Count - 1); } }

        public void gridCut()
        {
            List<Card> cutCards = null;
            if (_decodeForm.CardsGrid.GetSelectedCards(out cutCards))
            {
                setCardsInClipboard(cutCards);
                _decodeForm.CardsGrid.DeleteCards(cutCards);
                Operation op = new Operation("Cut", cutCards);
                op.AddToOperationList(_ops, ref _opsIndex);
            }
        }
        public void gridCopy()//no undo or redo.
        {
            List<Card> copyCards = null;
            if (_decodeForm.CardsGrid.GetSelectedCards(out copyCards))
            {
                setCardsInClipboard(copyCards);
            }
        }
        public void gridDelete()
        {
            List<Card> selectedCards = null;
            if (_decodeForm.CardsGrid.GetSelectedCards(out selectedCards))
            {
                _decodeForm.CardsGrid.DeleteCards(selectedCards);
                Operation op = new Operation("Delete", selectedCards);
                op.AddToOperationList(_ops, ref _opsIndex);
            }

        }
        public void gridPaste()
        {
            List<Card> clipboardCards = null;

            try
            {
                clipboardCards = getCardsFromClipboard();
            }
            catch
            {
                clipboardCards = null;
            }

            if (null != clipboardCards)
            {
                if (clipboardCards.Count > 0)
                {
                    int firstRow;
                    if (!_decodeForm.CardsGrid.GetFirstSelectedRow(out firstRow))
                        firstRow = 0;

                    //do not delete selection!! user might not have a way to unselect all cards.
                    _decodeForm.CardsGrid.AddCards(firstRow, clipboardCards);
                    Operation op = new Operation("Paste", clipboardCards);
                    op.AddToOperationList(_ops, ref _opsIndex);
                }
            }
        }

        private void setCardsInClipboard(List<Card> cards)
        {
            if (cards != null && cards.Count != 0)
            {
                timeDiff = new List<double>();
                for (int j = 0; j < cards.Count; j++)
                {
                    timeDiff.Add(cards[j].timeDiff);
                }
            }
            DataObject data = new DataObject();

            //cards as a list of cards
            List<a> ListA = cards.ConvertAll<a>(delegate(Card card) { return (a)card; });
            data.SetData(CLIPBOARD_CARDS_NAME, ListA);

            //cards as text
            int fields = 0;

            MemoryStream stream = new MemoryStream();
            CardWriter cardWriter = new CardWriter(stream, CardFileFormat.Format.TXT);
            StreamReader reader = new StreamReader(stream);

            for (int i = 0; i < cardWriter.Fields.Length; i++)
            {
                //column order must be the same as fields order!!
                if (_decodeForm.CardsGrid.Columns[i].Visible)
                    fields |= cardWriter.Fields[i];
            }

            cardWriter.Write(null, cards, fields);

            stream.Seek(0, SeekOrigin.Begin);   // reset file pointer

            data.SetText(reader.ReadToEnd());

            //set data on clipboard
            try
            {
                Clipboard.SetDataObject(data);
            }
            catch (Exception)
            {

            }
        }
        public List<Card> getCardsFromClipboard()
        {
            List<Card> clipboardCards = null;
            List<a> ListA = (List<a>)Clipboard.GetData(CLIPBOARD_CARDS_NAME);

            if (ListA != null)
                clipboardCards = ListA.ConvertAll<Card>(delegate(a card) { return Card.BuildFromA(card); });
            if (timeDiff != null && timeDiff.Count != 0&&clipboardCards!=null&&clipboardCards.Count ==timeDiff.Count)
            {
                for (int i = 0; i < timeDiff.Count; i++)
                {
                    clipboardCards[i].timeDiff = timeDiff[i];
                }
            }
            return clipboardCards;
        }
        public void FindInForm(FindData findData)
        {
            if (_decodeForm.CardsTextBox.Visible != true)
            {
                IList<Card> cards = _decodeForm.CardsGrid.GetCardsFromGrid();

                //do nothing if no cards in document
                if (cards.Count == 0)
                {
                    MessageBox.Show(_decodeForm.DecodeForm,
                                    "Cannot search in an empty document.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //start looking in the first selected cell. if no cell selected start in 0 cell.
                int variantStartRow = 0;
                if (_decodeForm.VariantsGrid.SelectedCells.Count > 0)
                {
                    variantStartRow = _decodeForm.VariantsGrid.SelectedCells[0].RowIndex;
                }

                int cardStartRow = 0;
                int cardStartColumn = CARD_READER_COLUMN;
                if (_decodeForm.CardsGrid.SelectedCells.Count > 0)
                {
                    cardStartRow = _decodeForm.CardsGrid.CellRowIndex(_decodeForm.CardsGrid.SelectedCells[0]);
                    cardStartColumn = _decodeForm.CardsGrid.SelectedCells[0].ColumnIndex;

                    //move to next if needed
                    if (findData.Next)
                    {
                        //move to next/previous card
                        nextCellToFindInGrid(_decodeForm.CardsGrid, ref cardStartRow, ref cardStartColumn,
                                             ref variantStartRow, findData.DirectionDown);
                    }
                    else
                    {
                        //save this point as startpoint.
                        findData.SetStartPoint(cardStartRow, cardStartColumn, variantStartRow);
                    }
                }

                StringComparison compareMethod = StringComparison.CurrentCultureIgnoreCase;
                if (findData.MatchCase)
                    compareMethod = StringComparison.CurrentCulture;

                int cardRow = cardStartRow;
                int cardColumn = cardStartColumn;
                int variantRow = variantStartRow;

                //get starting point to know when search is finished.
                findData.GetStartPoint(out cardStartRow, out cardStartColumn, out variantStartRow);
                do
                {
                    if (FindInField(cardRow, cardColumn, variantRow, findData.FindString, compareMethod))
                    {
                        if (findData.Next && (cardRow == cardStartRow) && (cardColumn == cardStartColumn) && (variantRow == variantStartRow))
                            MessageBox.Show(_decodeForm.DecodeForm,
                                            "Find reached the starting point of the search.",
                                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //move to next/previous card/variant
                    nextCellToFindInGrid(_decodeForm.CardsGrid, ref cardRow, ref cardColumn, ref variantRow, findData.DirectionDown);
                }
                while ((cardRow != cardStartRow) || (cardColumn != cardStartColumn) || (variantRow != variantStartRow));

                if (findData.Next)
                    MessageBox.Show(_decodeForm.DecodeForm,
                                    "The search has reached the end of the document.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(_decodeForm.DecodeForm,
                                    "Cannot find \"" + findData.FindString + "\".", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _decodeForm.CardsTextBox.SelectionStart = _decodeForm.CardsTextBox.Find(findData.FindString, _decodeForm.CardsTextBox.SelectionStart + 1, RichTextBoxFinds.None);
                _decodeForm.CardsTextBox.SelectionLength = findData.FindString.Length;
                _decodeForm.CardsTextBox.Focus();

            }
        }
        public void FindInstance()
        {
            Find.Instance.Show(this);
        }
        private bool FindInField(int cardRow,
                                 int cardColumn,
                                 int variantRow,
                                 string findString,
                                 StringComparison compareMethod)
        {
            IList<Card> cards = _decodeForm.CardsGrid.GetCardsFromGrid();
            Card card = cards[cardRow];

            switch (cardColumn)
            {
                case CARD_READER_COLUMN:
                    if (card.ReaderCard.IndexOf(findString, 0, compareMethod) != -1)
                    {
                        selectCellInGrid(_decodeForm.CardsGrid, cardRow, cardColumn);
                        return true;
                    }
                    break;

                case CARD_TIME_COLUMN:
                    if (card.Time.IndexOf(findString, 0, compareMethod) != -1)
                    {
                        selectCellInGrid(_decodeForm.CardsGrid, cardRow, cardColumn);
                        return true;
                    }
                    break;

                case CARD_TRACK1_COLUMN:
                case CARD_TRACK2_COLUMN:
                case CARD_TRACK3_COLUMN:
                    Track group = card.GetTrack(cardColumn - CARD_TRACK1_COLUMN);
                    if (group == null)
                        break;

                    //if group is not sorted sort it. it will be sorted when selected
                    if (group.CurrentSort() == Serializable.SortOrder.None)
                    {
                        group.Sort(Serializable.SortOrder.Descending);
                    }

                    if (variantRow >= group.Count)
                        return false;
                    Variant variant = (Variant)group[variantRow];
                    if (variant.ToString().IndexOf(findString, 0, compareMethod) != -1)
                    {
                        selectCellInGrid(_decodeForm.CardsGrid, cardRow, cardColumn);

                        _decodeForm.VariantsGrid.DataSource = group;

                        selectCellInGrid(_decodeForm.VariantsGrid, variantRow, 0);

                        return true;
                    }
                    break;
            }

            return false;
        }
        private void selectCellInGrid(DataGridView grid, int row, int column)
        {
            if ((column < grid.Columns.Count) && (row < grid.Rows.Count))
            {

                grid.CurrentCell = grid.Rows[row].Cells[column];

                //select cell
                grid.Rows[row].Cells[column].Selected = true;

                //make selected cell visible
                if (!grid.Rows[row].Cells[column].Displayed)
                    grid.FirstDisplayedScrollingRowIndex = row;

                //refresh
                //grid.Refresh();
            }
        }
        private void nextCellToFindInGrid(DataGridView grid, ref int row, ref int column, ref int variantRow, bool directionDown)
        {
            IList<Card> cards = _decodeForm.CardsGrid.GetCardsFromGrid();
            Card card = cards[row];
            Track group = null;
            int maxVariants = 0;

            switch (column)
            {
                case CARD_TRACK1_COLUMN:
                case CARD_TRACK2_COLUMN:
                case CARD_TRACK3_COLUMN:
                    group = card.GetTrack(column - CARD_TRACK1_COLUMN);
                    if (group != null)
                        maxVariants = group.Count;
                    break;
            }

            if (directionDown)
            {
                if (maxVariants != 0)
                {
                    variantRow++;
                    if (variantRow < maxVariants)
                        return;
                }

                column++;
                variantRow = 0;
                if (column <= CARD_TRACK3_COLUMN)
                    return;

                column = CARD_READER_COLUMN;
                row++;
                if (row == grid.Rows.Count)
                    row = 0;
            }
            else
            {
                if (maxVariants != 0)
                {
                    variantRow--;
                    if (variantRow >= 0)
                        return;
                }

                column--;
                if (column >= CARD_READER_COLUMN)
                {
                    if (column >= CARD_TRACK1_COLUMN)
                    {
                        group = card.GetTrack(column - CARD_TRACK1_COLUMN);
                        if (group != null)
                            variantRow = group.Count - 1;
                        else
                            variantRow = 0;
                    }
                    else
                        variantRow = 0;
                    return;
                }

                column = CARD_TRACK3_COLUMN;
                row--;
                if (row < 0)
                {
                    row = cards.Count - 1;
                }
                card = cards[row];
                group = card.GetTrack(column - CARD_TRACK1_COLUMN);
                if (group != null)
                    variantRow = group.Count - 1;
                else
                    variantRow = 0;
            }
        }
        #endregion
        public void setCardsGridDataSource(List<Card> cards)
        {
            //_decodeForm.CardsGrid.SetCardsInGrid(null);
            if (cards != null)
                _decodeForm.CardsGrid.StatusStrip = _decodeForm.StatusAdvancedDecode;
                _decodeForm.CardsGrid.SetCardsInGrid(cards);
            setShowDecode();
        }
        
        private void setShowDecode()
        {
            if (Settings.Default.showAdvancedDecoder)
            {
                _decodeForm.CardsGrid.Visible = true;
                _decodeForm.CardsTextBox.Visible = false;
                _decodeForm.StatusAdvancedDecode.Visible = true;
                _decodeForm.StatusSimpleDecode.Visible = false;
                _decodeForm.SplitContain.Panel2Collapsed = false;
                _decodeForm.ToggleMenu(true);
            }
            else
            {
                _decodeForm.CardsGrid.Visible = false;
                _decodeForm.CardsTextBox.Visible = true;
                _decodeForm.StatusAdvancedDecode.Visible = false;
                _decodeForm.StatusSimpleDecode.Visible = true;
                _decodeForm.SplitContain.Panel2Collapsed = true;
                _decodeForm.ToggleMenu(false);

            }
        }

        public void showDecodeSetting(bool firstTime)
        {
            IList<Card> cards = _decodeForm.CardsGrid.GetCardsFromGrid();
            setVariantsGridDataSource(null);
            _decodeForm.CardsGrid.CurrentCell = null;
            if ((cards == null) || (cards.Count == 0))
            {
                    MessageBox.Show(_decodeForm.DecodeForm,
                                    "No cards in file. Check that you have written correct password.", "Decode",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!firstTime)
            {
                string advancedDecoderWarning = "By changing any setting in this window, you will loose any changes that you have made manually. Please make sure that you've saved your data before continuing.";
                if (!Settings.Default.showAdvancedDecoder && Settings.Default.showDecoderSwitchWarning)
                {
                    DialogResult r = TaskDialog.MessageBox(_decodeForm.DecodeForm, 
                        "Warning", 
                        "Are you sure that you want to continue?", 
                        advancedDecoderWarning, 
                        "Don't show this message again for this document",
                        TaskDialogButtons.YesNo,
                        SysIcons.Warning);
                    Settings.Default.showDecoderSwitchWarning = !TaskDialog.VerificationChecked;
                    if ( r == DialogResult.No)
                        return;
                }
            }

            int t1, t2, t3;
            findTracksWithInfo(cards, out t1, out t2, out t3);
            settingsWindow = new DecodeSettings(t1, t2, t3, cards.Count);
            settingsWindow.applyChange += applyChange;
            settingsWindow.Show(_decodeForm);
        }



        public void changeSortTrack()
        {
            Track track = (Track)_decodeForm.VariantsGrid.DataSource;

            if ((track != null) && (track.Count > 0))
            {
                if (Serializable.SortOrder.Descending == track.CurrentSort())
                    track.Sort(Serializable.SortOrder.Ascending);
                else//ascending or none
                    track.Sort(Serializable.SortOrder.Descending);

                _decodeForm.VariantsGrid.Refresh();

                if (Serializable.SortOrder.Descending == track.CurrentSort())
                    _decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                else
                    _decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;

                //select de preferred variant.
                selectCellInGrid(_decodeForm.VariantsGrid, track.GetPreferredVariantIndex(), 0);
            }
        }

        public void findTracksWithInfo(IList<Card> cards, out int track1, out int track2, out int track3)
        {
            track1 = 0;
            track2 = 0;
            track3 = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];

                if (!card.TrackEmpty(0, 0))
                    track1++;
                if (!card.TrackEmpty(1, 0))
                    track2++;
                if (!card.TrackEmpty(2, 0))
                    track3++;
            }
        }

        public void SetBPCIndex(int bpc, EventHandler handler)
        {
            _decodeForm.BPCFormat.SelectedIndexChanged -= handler;
            _decodeForm.BPCFormat.SelectedIndex = bpc - (int)BpcSupported.Start;
            //UserSettings.Instance.Bpc = bpc - 4;
            _decodeForm.BPCFormat.SelectedIndexChanged += handler;
        }
        public void SetAddNumber(int number, EventHandler handler)
        {
            _decodeForm.AddNumber.TextChanged -= handler;
            if (number < 0)
                _decodeForm.AddNumber.Text = string.Empty;
            else
                _decodeForm.AddNumber.Text = Convert.ToString(number, 16);
            //UserSettings.Instance.Add = number;
            _decodeForm.AddNumber.TextChanged += handler;
        }
        public void variantBuildSettingsChanged(byte bpc)
        {
            Track group = (Track)_decodeForm.VariantsGrid.DataSource;
            if (group == null)
                return;

            byte add;

            try
            {
                add = Convert.ToByte(_decodeForm.AddNumber.Text, 16);
            }
            catch
            {
                _decodeForm.AddNumber.Text = group.AddByte.ToString("x");
                return;
            }

            //all variants in the group have the same bpc and track
            if ((group.BitsPerChar != bpc) || (group.AddByte != add))
            {
                group.ChangeGroupSettings(bpc, add);
                _decodeForm.VariantsGrid.DataSource = null;
                setVariantsGridDataSource(group);
                _decodeForm.CardsGrid.Refresh();
            }
        }

        public void setVariantsGridDataSource(Track track)
        {
            if (track == null)
            {
                _decodeForm.VariantsGrid.DataSource = null;
                _decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                return;
            }

            //keep user sort
            Serializable.SortOrder sortOrder = Serializable.SortOrder.Descending;
            if (_decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Ascending)
                sortOrder = Serializable.SortOrder.Ascending;

            if (sortOrder != track.CurrentSort())
                track.Sort(sortOrder);

            _decodeForm.VariantsGrid.DataSource = track;
            if (sortOrder == Serializable.SortOrder.Descending)
                _decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
            else
                _decodeForm.VariantsGrid.Columns[(int)VariantGridColumn.Rate].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;

            //select de preferred variant.
            selectCellInGrid(_decodeForm.VariantsGrid, track.GetPreferredVariantIndex(), 0);
            _decodeForm.UpdateTxtBox();
        }

        public void applyChange(DecodeSettings.Change changeType, object value)
        {
            switch (changeType)
            {
                case DecodeSettings.Change.DecoderType:
                    setShowDecode();
                    break;

                case DecodeSettings.Change.ShowDirection:
                    dataGridCards_ChangeVisible(cardGridColumn.DIRECTION_COLUMN, (bool)value);
                    break;

                case DecodeSettings.Change.ShowTime:
                    dataGridCards_ChangeVisible(cardGridColumn.TIME_COLUMN, (bool)value);
                    break;

                case DecodeSettings.Change.ShowTrack:
                    dataGridCards_ChangeVisible(cardGridColumn.TRACK1_COLUMN + (int)value, settingsWindow.getShowTrack((int)value));
                    break;

                
                case DecodeSettings.Change.TimeFormat:
                    decodeSettingComboxSelectChanged(ComboxSelect.TimeFormat, (int)value);
                    break;

                case DecodeSettings.Change.DecodeMethod:
                    decodeSettingComboxSelectChanged(ComboxSelect.DecodeMethod, (int)value);
                    break;

                case DecodeSettings.Change.TrackBPC:
                    decodeSettingComboxSelectChanged(ComboxSelect.TrackBPC, (int)value);
                    break;

                case DecodeSettings.Change.AlignChars:
                    _decodeForm.CommonSubstringChars.Text = (string)value;
                    ListCardUtil.SetGroupCards(_decodeForm.CardsGrid.GetActualCardsFromGrid (),0);
                    ListCardUtil.SetAlignment(_decodeForm.CardsGrid.GetActualCardsFromGrid (),0);
                   _decodeForm.gridRefresh();// _decodeForm.CardsGrid.Refresh();
                    _decodeForm.UpdateTxtBox();
                    break;

                case DecodeSettings.Change.GroupSwipes:
                    _decodeForm.MsGroupCards.Text = ((int)value).ToString();
                    ListCardUtil.SetGroupCards(_decodeForm.CardsGrid.GetActualCardsFromGrid (),0);
                    ListCardUtil.SetAlignment(_decodeForm.CardsGrid.GetActualCardsFromGrid (),0);
                    _decodeForm.gridRefresh();//_decodeForm.CardsGrid.Refresh();
                    _decodeForm.UpdateTxtBox();
                    break;

                case DecodeSettings.Change.SettingsClosed:
                    settingsWindow = null;
                    break;

            }
        }

        public void dataGridCards_ChangeVisible(cardGridColumn viewItem, bool checkState)
        {
            _decodeForm.CardsGrid.Columns[(int)viewItem].Visible = checkState;
            ToolStripButton stripButton = (ToolStripButton)_decodeForm.FieldsTool.Items[(int)viewItem];
            stripButton.Checked = checkState;
            _decodeForm.UpdateTxtBox();
        }

        public void decodeSettingComboxSelectChanged(ComboxSelect comboxSelect, int index)
        {
            IList<Card> cards = _decodeForm.CardsGrid.GetCardsFromGrid();
            if (cards == null || cards.Count == 0)
                return;

            switch (comboxSelect)
            {
                case ComboxSelect.TimeFormat:
                    Settings.Default.showTimeFormat = index;
                    _decodeForm.gridAutoResizeColumn((int)cardGridColumn.TIME_COLUMN);
                    break;
                case ComboxSelect.DecodeMethod:
                    _decodeForm.ShowChars.SelectedIndex = index;
                    break;
                case ComboxSelect.TrackBPC:
                    for (int i = 0; i < cards.Count; i++)
                    {
                        Track group = cards[i].GetTrack(index);
                        if (group != null)
                        {
                            group.ChangeGroupSettings(settingsWindow.getBPC(index), settingsWindow.getStart(index));
                        }
                    }
                    _decodeForm.gridAutoResizeColumn((int)cardGridColumn.TRACK1_COLUMN + index);

                    break;
            }
            _decodeForm.gridRefresh();//_decodeForm.CardsGrid.Refresh();
            _decodeForm.VariantsGrid.Refresh();
            _decodeForm.UpdateTxtBox();
        }

    }
}
