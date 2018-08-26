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
using System.IO;
using crf.Properties;
using crf.CustomsControls;
using crf.Forms;
using crf.Presentation;
using System.Diagnostics;
using TaskDialogDLL;
using crf.Algorithm;

namespace crf
{

    public partial class frmDecode : Form
    {
        static public string messageSaveFailed = "Please check if the file is being used by another program or if the destination write protected.";

        #region windows members and properties
        private CardFileFormat.Format _currentFileFormat = CardFileFormat.Format.NONE;
        private String _filename = "";
        private List<Card> _cards = null;
        public  Point PrePoint;
        private Keys _key;
        public bool recoveryMode = false;

        //private int _fontWidth;
        private readonly string _captionText = "Decode";
        public static bool isTextEdit = true;
        public string currentKey = "";
        public bool isCrfModified = false;
        private bool isTxtModified = false;
        private bool isSimpleDecodeFirst = false;
        private bool isStaring = false;
        private string priString = "";
        public static bool isKeyWorked = true;
        private bool _isEncrypted = true;
        private IWin32Window _parent;
        crf.Presentation.DecodePresenter _decodePresenter;
        public bool isAutoResize = false;
        public string deviceBuild = "";

        public Form DecodeForm { get { return this; } }
        public List<Card> Cards { get { return _cards; } }
        public string CurrentFileName
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;

                if ((_filename != null) && (_filename != ""))
                    this.Text = _captionText + " - " + _filename;
                else
                    this.Text = _captionText;
            }
        }

        public CardFileFormat.Format CurrentFileFormat
        {
            get
            {
                return _currentFileFormat;
            }

            set
            {
                _currentFileFormat = value;
            }
        }
        public SaveFileDialog SaveFileDialog { get { return saveFileDialog; } }
        public crf.CustomsControls.CardDataGridView CardsGrid { get { return dataGridCards; } }
        public DataGridView VariantsGrid { get { return dataGridVariants; } }
        public ToolStripTextBox MsGroupCards { get { return txtMsGroup; } }
        public ToolStripTextBox CommonSubstringChars { get { return txtCommonSubStringChars; } }
        public ToolStripComboBox BPCFormat { get { return toolStripComboFormat; } }
        public ToolStripTextBox AddNumber { get { return toolStripAddText; } }
        public ToolStripComboBox ShowChars { get { return toolStripShowCharsCombo; } }
        public ToolStrip FieldsTool { get { return toolStrip1; } }
        public ToolStrip SettingsTool { get { return toolStrip2; } }
        public ToolStrip DecodeTool { get { return toolStrip3; } }
        public SplitContainer SplitContain { get { return splitContainer1; } }
        public bool isEncrypted { get { return _isEncrypted; } }
        public RichTextBox CardsTextBox { get { return txtBox; } }
        public ToolStripStatusLabel StatusAdvancedDecode { get { return toolStripStatusAdvance; } }
        public ToolStripStatusLabel StatusSimpleDecode { get { return toolStripStatusSimple; } }
        public ToolStripMenuItem ViewShowToolbar { get { return viewToolbar1ToolStripMenuItem; } }
        public ToolStripMenuItem ViewSettingsToolbar { get { return viewSettingsToolbar; } }
        public ToolStripMenuItem ViewDecodeToolbar { get { return viewDecodeToolbar; } }
        #endregion

        #region Load File

        public frmDecode()
        {
            InitializeComponent();

            _decodePresenter = new Presentation.DecodePresenter(this);
            this.resizeColumnTimer.Tag = new List<int>();
            //CurrentFileName = "";
            autoResizeColumnsToolStripMenuItem.Checked = Properties.Settings.Default.autoResize;
        }

        public new void Show()
        {
            Show(null, CurrentFileName);
            //throw new ArgumentException("Parent needed.");
        }

        public void ShowDialog(string filename)
        {
            _cards = getCards(filename, null);

            if (_cards != null)
                base.ShowDialog();
        }

        public void Show(IWin32Window parent, string filename, string key)
        {
            _cards = getCards(filename, key);
            if (_cards != null)
            {
                _parent = parent;
                base.Show();
            }
        }

        public void Show(IWin32Window parent, string filename)
        {
            if (filename == null || !File.Exists(filename))
                return;

            if ( filename.Contains(".txt"))
            {
                try
                {
                    Process.Start("\"" + filename + "\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error opening file: " + ex.Message,
                                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            string key = getKey(filename);
            if (key == "")
                _isEncrypted = false;
            Show(parent, filename, key);
        }

        public new void Show(IWin32Window parent)
        {
            Show(parent, getFile());
        }

        private List<Card> getCards(string filename, string key)
        {
            List<Card> cards = null;

            if (filename == null || !File.Exists(filename))
                return null;

            if (key == null)
            {
                key = getKey(filename);
                if (key == null)
                    return null;
            }

            CardReader cardReader = null;

            try
            {
                cardReader = new CardReader(filename);

                //read cards
                cards = cardReader.Read(key, recoveryMode);
                currentKey = key;

                CurrentFileFormat = cardReader.FileFormat;
                CurrentFileName = filename;

                hideUnusedTracks();
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                MessageBox.Show("Cannot open file.\nEnsure that file is not being used by another application and use correct password.\n\n" + ex.Message,
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cards = null;
            }
            finally
            {
                if (cardReader != null)
                    cardReader.Close();
            }
            return cards;
        }

        /* returns "" if no key is needed */
        /* returns null if user aborted */
        string getKey(string filename)
        {
            string key = null;
            CrfReader crfStream;

            //ask for key if needed
            if (filename == null)
                return null;

            if (!CardReader.NeedPassword(filename))
                return "";

            try
            {
                crfStream = new CrfReader(filename);
            }
            catch
            {
                return null;
            }
            key = crfStream.tryKnownKeys();
            crfStream.Close();
                
                
            if (key == null)
            {
                EnterKey keyForm = new EnterKey(deviceBuild);
                if (keyForm.ShowDialog() != DialogResult.OK)
                    return null;

                key = keyForm.key.Text;
            }

            return key;
        }

        string getFile()
        {
            //OpenFileDialog o = new OpenFileDialog();
            //DialogResult ret;
            //openFileDialog1.AddExtension = true;
            openFileDialog.Title = "Select the file to decode";
            openFileDialog.Filter = "Supported Files (*." + Program.extension + ";*.txt)|*." + Program.extension + ";*.txt|" + Utils.getFilter(Program.extension) + "|" + Utils.getFilter("txt") + "|" + "All Files|*.*";
            //openFileDialog.AutoUpgradeEnabled = true;

            //ret = openFileDialog.ShowDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            else
                return null;
        }
        #endregion

        #region FileMenu Event
        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            string fileName = _decodePresenter.GetCurrentFullFileNoWithExtension();
            exportFileCsvToolStripMenuItem.Text = "Export as " + Path.GetFileNameWithoutExtension (fileName) + ".csv";
            exportFileTxtToolStripMenuItem.Text = "Export as " + Path.GetFileNameWithoutExtension (fileName) + ".txt";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmDecode decodeForm = new frmDecode();
            if (sender is ToolStripMenuItem)
                decodeForm.recoveryMode = (sender as ToolStripMenuItem).Text.ToLower().Contains("recovery");
            decodeForm.Show(decodeForm.Parent);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFileFormat != CardFileFormat.Format.CRF)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                _decodePresenter.saveCurrentFile(CurrentFileName, CardFileFormat.Format.CRF, false);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = Utils.getFilter(Program.extension);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filename = saveFileDialog.FileName;
                _decodePresenter.saveCurrentFile(_filename, CardFileFormat.Format.CRF, false);
            }
        }

        private void exportFileTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtBox.Visible == true)
            {
                saveText(_decodePresenter.GetCurrentFullFileNoWithExtension() + ".txt");
                return;
            }
            _decodePresenter.exportToFile(_decodePresenter.GetCurrentFullFileNoWithExtension() + ".txt", CardFileFormat.Format.TXT);
        }

        private void exportFileCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _decodePresenter.exportToFile(_decodePresenter.GetCurrentFullFileNoWithExtension() + ".csv", CardFileFormat.Format.CSV);
        }

        private void exportAsTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtBox.Visible == true)
            {
                saveText(null);
                return;
            }

            _decodePresenter.exportCurrentFile("", CardFileFormat.Format.TXT);
        }

        private void exportAsCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _decodePresenter.exportCurrentFile("", CardFileFormat.Format.CSV);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool saveText(string file)
        {
            if (file == null || file == "")
            {
                SaveFileDialog.FileName = file;
                SaveFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = SaveFileDialog.FileName;
                }
                else return true;

            }

            try
            {
                txtBox.SaveFile(file, RichTextBoxStreamType.PlainText);
            }
            catch
            {
                TaskDialog.MessageBox(this,
                                        "Export text file",
                                        "Exporting the text file has failed",
                                        messageSaveFailed,
                                        "",
                                        TaskDialogButtons.OK,
                                        SysIcons.Error);
                return false;
            }

            return openExportedTextFile(file);

        }

        public bool openExportedTextFile(string filename)
        {
            if (TaskDialog.MessageBox(this,
                        "Export text file",
                        "The file has been saved successfully.\nDo you want to open the saved file?",
                        "Warning: Do not use this file as '*." + Program.extension.ToUpper() + "' replacement!",
                        "The text files do not contain important informations (variants, encoding, colors etc). If you still need this information please make sure that you also save this document in CRF format.",
                        "",
                        "",
                        TaskDialogButtons.YesNo,
                        SysIcons.Question, 
                        SysIcons.Question) == DialogResult.Yes)
            {
                try
                {
                    Process.Start("\"" + filename + "\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error opening saved file: " + ex.Message,
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
 
        }


        private bool SaveTxtText(string file)
        {
            if (file == null || file == "")
            {
                SaveFileDialog.FileName = file;
                SaveFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = SaveFileDialog.FileName;
                }
                else return true;
            }
            txtBox.SaveFile(file, RichTextBoxStreamType.PlainText);
            return true;
        }
        #endregion

        #region Edit Menu Event
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(CardsTextBox.Visible == true)
            {
                CardsTextBox.Undo();
                return;
            }
            RichTextBox txtEditingControl = _decodePresenter.getEditingControl() as RichTextBox;
            if (txtEditingControl != null)
            {
                txtEditingControl.Undo();
            }
            else
            {
                _decodePresenter.gridUndo();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CardsTextBox.Visible == true)
            {
                CardsTextBox.Redo();
                return;
            }

            RichTextBox txtEditingControl = _decodePresenter.getEditingControl() as RichTextBox;
            if (txtEditingControl != null)
            {
                txtEditingControl.Redo();
            }
            else
            {
                _decodePresenter.gridRedo();
            }

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CardsTextBox.Visible == true)
            {
                CardsTextBox.Cut();
                return;
            }


            TextBoxBase txtEditingControl = _decodePresenter.getEditingControl();
            if (txtEditingControl != null)
            {
                if (!txtEditingControl.ReadOnly)
                    txtEditingControl.Cut();
            }
            else
            {
                _decodePresenter.gridCut();
            }
            isCrfModified = true;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CardsTextBox.Visible == true)
            {
                CardsTextBox.Copy();
                return;
            }


            TextBoxBase txtEditingControl = _decodePresenter.getEditingControl();
            if (txtEditingControl != null)
            {
                txtEditingControl.Copy();
            }
            else
            {
                _decodePresenter.gridCopy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CardsTextBox.Visible == true)
            {
                CardsTextBox.Paste();
                return;
            }

            TextBoxBase txtEditingControl = _decodePresenter.getEditingControl();
            if (txtEditingControl != null)
            {
                if (!txtEditingControl.ReadOnly)
                {
                    List<Card> clipbordCards = _decodePresenter.getCardsFromClipboard();
                    if (clipbordCards == null)
                        txtEditingControl.Paste();
                    else
                        MessageBox.Show(this, "Cannot paste a card to a track.", "Warning");
                }
                //CardsGrid.Refresh();
                gridRefresh();

            }
            else
            {
                _decodePresenter.gridPaste();
            }
            isCrfModified = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxBase txtEditingControl = _decodePresenter.getEditingControl();
            if (txtEditingControl != null)
            {
                if (!txtEditingControl.ReadOnly)
                    txtEditingControl.SelectedText = "";
            }
            else
            {
                _decodePresenter.gridDelete();
            }
            isCrfModified = true;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IList<Card> cardsInGrid = CardsGrid.GetCardsFromGrid();

            if (cardsInGrid.Count == 0)
            {
                MessageBox.Show(DecodeForm,
                                "Cannot search in an empty document.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _decodePresenter.FindInstance();
        }

        private void findNextStripMenuItem_Click(object sender, EventArgs e)
        {
            IList<Card> cardsInGrid = CardsGrid.GetCardsFromGrid();

            if (cardsInGrid.Count == 0)
            {
                MessageBox.Show(DecodeForm,
                                "Cannot search in an empty document.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindData.Instance.FindString == string.Empty)
            {
                findToolStripMenuItem_Click(null, null);
            }
            else
            {
                FindData.Instance.Next = true;
                _decodePresenter.FindInForm(FindData.Instance);
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CardsTextBox.Visible == true)
            {
                CardsTextBox.SelectAll();
                return;
            }

            TextBoxBase txtEditingControl = _decodePresenter.getEditingControl();

            //editing in a cell?
            if (txtEditingControl != null)
            {
                txtEditingControl.SelectAll();
            }
            else
            {
                CardsGrid.SelectAll();
            }
        }

        private void textEdidModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CardsGrid.ReadOnly = !CardsGrid.ReadOnly;
            isTextEdit = true;
            this.textEditToolStripMenuItem.Checked = true;
            editOnEnterToolStripMenuItem.Checked = false;
            editOnF2ToolStripMenuItem.Checked = false;
            CardsGrid.Columns[(int)cardGridColumn.COUNT_COLUMN].ReadOnly = true;
            CardsGrid.Columns[(int)cardGridColumn.DIRECTION_COLUMN].ReadOnly = true;
            CardsGrid.Columns[(int)cardGridColumn.TIME_COLUMN].ReadOnly = true;
            //CardsGrid.EndEdit();
        }

        private void editOnEnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEditOnEnter(true);
        }

        private void editOnF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setEditOnEnter(false);
        }
        private void setEditOnEnter(bool value)
        {
            editOnEnterToolStripMenuItem.Checked = value;
            editOnF2ToolStripMenuItem.Checked = !value;
            this.textEditToolStripMenuItem.Checked = false;
            isTextEdit = false;
            if (value)
            {
                this.dataGridCards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
                this.dataGridVariants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
                //this.dataGridCards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
                //this.dataGridVariants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                this.dataGridCards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
                this.dataGridVariants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            }
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            TextBoxBase textBox = _decodePresenter.getEditingControl();
            List<Card> clipboardCards = null;
            string clipboardText = null;

            try
            {
                clipboardCards = _decodePresenter.getCardsFromClipboard();
            }
            catch
            {
                clipboardCards = null;
            }
            try
            {
                clipboardText = (string)Clipboard.GetData("Text");
            }
            catch
            {
                clipboardText = null;
            }

            //undo-redo only available for the moment while editing a cell
            //implementing a feature for undo redo for grid.
            //add check logic here.
            //cut-copy-paste-delete
            if (textBox != null)
            {
                pasteToolStripMenuItem.Enabled = (clipboardText != null) && (clipboardText.Length > 0) && (!textBox.ReadOnly);
                cutToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = (textBox.SelectionLength > 0) && (!textBox.ReadOnly);
                copyToolStripMenuItem.Enabled = (textBox.SelectionLength > 0);
            }
            else
            {
                cutToolStripMenuItem.Enabled =
                        copyToolStripMenuItem.Enabled =
                                deleteToolStripMenuItem.Enabled = CardsGrid.SelectedCells.Count > 0;
                pasteToolStripMenuItem.Enabled = clipboardCards != null;
            }

            if (CardsTextBox.Visible == true)
            {
                pasteToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }

            //find and find next not available if editing a cell
            findNextStripMenuItem.Enabled = (textBox == null);
            findToolStripMenuItem.Enabled = (textBox == null);
        }
        #endregion

        #region View Menu Event
        private void viewToolbar1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewToolbar1ToolStripMenuItem.Checked = !viewToolbar1ToolStripMenuItem.Checked;
            toolStrip1.Visible = viewToolbar1ToolStripMenuItem.Checked;

        }

        private void viewToolbar2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewSettingsToolbar.Checked = !viewSettingsToolbar.Checked;
            toolStrip2.Visible = viewSettingsToolbar.Checked;

        }

        private void decodeToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewDecodeToolbar.Checked = !viewDecodeToolbar.Checked;
            toolStrip3.Visible = viewDecodeToolbar.Checked;

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBarToolStripMenuItem.Checked = !statusBarToolStripMenuItem.Checked;
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;

        }

        private void btnViewID_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showCount = btnViewID.Checked;
            CardsGrid.Columns[(int)cardGridColumn.COUNT_COLUMN].Visible = btnViewID.Checked;
            gridRefresh();// CardsGrid.Refresh();
        }

        private void btnViewTime_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showTime = btnViewTime.Checked;
            _decodePresenter.dataGridCards_ChangeVisible(cardGridColumn.TIME_COLUMN, btnViewTime.Checked);
        }

        private void btnViewTrack1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showTrack1 = btnViewTrack1.Checked;
            _decodePresenter.dataGridCards_ChangeVisible(cardGridColumn.TRACK1_COLUMN, btnViewTrack1.Checked);
        }

        private void btnViewTrack2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showTrack2 = btnViewTrack2.Checked;
            _decodePresenter.dataGridCards_ChangeVisible(cardGridColumn.TRACK2_COLUMN, btnViewTrack2.Checked);
        }

        private void btnViewTrack3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showTrack3 = btnViewTrack3.Checked;
            _decodePresenter.dataGridCards_ChangeVisible(cardGridColumn.TRACK3_COLUMN, btnViewTrack3.Checked);
        }

        private void btnViewDirection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showDirection = btnViewDirection.Checked;
            _decodePresenter.dataGridCards_ChangeVisible(cardGridColumn.DIRECTION_COLUMN, btnViewDirection.Checked);
        }
        #endregion

        #region windows controls event
        private void dataGridCards_SelectionChanged(object sender, EventArgs e)
        {
            if (CardsGrid.CurrentCell != null && CardsGrid.CurrentCell.Value != null)
            {
                priString = CardsGrid.CurrentCell.Value.ToString();
            }
            if (isTextEdit == true)
            {
                if (CardsGrid.SelectedCells.Count > 1)
                    CardsGrid.EditMode = DataGridViewEditMode.EditOnF2;
                else
                    textEditMode();                
            }
            
            Track track = null;
            int trackId = -1;

            if (CardsGrid.SelectedCells.Count == 1)
            {
                IList<Card> cards = CardsGrid.GetCardsFromGrid();

                if (cards != null)
                {
                    Card selectedCard = cards[CardsGrid.CellRowIndex(CardsGrid.SelectedCells[0])];

                    switch (CardsGrid.SelectedCells[0].ColumnIndex)
                    {
                        case (int)cardGridColumn.TRACK1_COLUMN:
                        case (int)cardGridColumn.TRACK2_COLUMN:
                        case (int)cardGridColumn.TRACK3_COLUMN:
                            trackId = CardsGrid.SelectedCells[0].ColumnIndex - (int)cardGridColumn.TRACK1_COLUMN;
                            break;
                    }

                    if (trackId != -1)
                        track = selectedCard.GetTrack(trackId);
                }
            }

            //set correct bpc checkbox and correct value in add textbox
            if (null != track)
            {
                //do not update anything if we have called this function
                int bpc = track.BitsPerChar;
                byte addByte = track.AddByte;
                _decodePresenter.SetBPCIndex(bpc, toolStripComboFormat_SelectedIndexChanged);
                _decodePresenter.SetAddNumber(addByte, toolStripAddText_TextChanged);
            }
            else
            {
                int bpc = (int)BpcSupported.Start - 1;
                int addNumber = -1;

                if (trackId != -1)
                {
                    bpc = VariantSettings.bpc[trackId];
                    addNumber = VariantSettings.add[trackId];
                }

                _decodePresenter.SetBPCIndex(bpc, toolStripComboFormat_SelectedIndexChanged);
                _decodePresenter.SetAddNumber(addNumber, toolStripAddText_TextChanged);
            }
            _decodePresenter.setVariantsGridDataSource(track);
        }

        private void dataGridCards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxBase txtEditingBox = (TextBoxBase)CardsGrid.EditingControl;
            if (txtEditingBox == null && isTextEdit==true)
            {
                textEditMode();
            }
        }

        private void textEditMode()
        {
            if (CardsGrid.CurrentCell != null)
            {
                CardsGrid.BeginEdit(false);
            }
            
            TextBoxBase txtEditingBox = (TextBoxBase)CardsGrid.EditingControl;
            
            if (txtEditingBox != null)
            {
                int length = txtEditingBox.Text.Length;
                txtEditingBox.SelectionLength = 0;

                if (_key != 0 && _key == Keys.Left && isKeyWorked==false)
                {
                    txtEditingBox.SelectionStart = txtEditingBox.TextLength;
                    _key = 0;
                    isKeyWorked = true;
                    return;
                }
                else if (_key != 0 && _key == Keys.Right && isKeyWorked == false)
                {
                    txtEditingBox.SelectionStart = 0;
                    _key = 0;
                    isKeyWorked = true;
                    return;
                }
                else if (_key != 0 && _key == Keys.Up && isKeyWorked == false)
                {
                    Point Pre = txtEditingBox.PointToClient(PrePoint);
                    List<int> listWight = new List<int>();
                    int k = 0;
                    if(Pre.X >= 0)
                        for (; k <= length; k++)
                        {
                            Point curser = txtEditingBox.GetPositionFromCharIndex(k);
                            int wigth = TextRenderer.MeasureText(txtEditingBox.Text.Substring(0, k), txtEditingBox.Font).Width;
                            if (Pre.X < curser.X)
                                break;
                        }
                    txtEditingBox.SelectionStart = k;
                    _key = 0;
                    isKeyWorked = true;
                    return;
                }
                                
                int width = txtEditingBox.PointToClient(PrePoint).X;
                if (width > txtEditingBox.GetPositionFromCharIndex(length - 1).X + 3)
                {
                    txtEditingBox.SelectionStart = length;
                    return;
                }
                List<int> listWidth = new List<int>();
                for (int i = 0; i <= length; i++)
                {
                    listWidth.Add(Math.Abs(txtEditingBox.GetPositionFromCharIndex(i).X - width));
                }
                int minWidth = listWidth[0];
                for (int j = 0; j < listWidth.Count; j++)
                {
                    if (listWidth[j] < minWidth)
                    {
                        minWidth = listWidth[j];
                    }
                }
                txtEditingBox.SelectionStart = listWidth.IndexOf(minWidth);
                isKeyWorked = true;
            }
        }

        private void dataGridCards_MouseDown(object sender, MouseEventArgs e)
        {
            PrePoint = new Point(e.X, e.Y);
            PrePoint = CardsGrid.PointToScreen(PrePoint);
            bool b = e.X < CardsGrid.Columns.GetColumnsWidth(DataGridViewElementStates.Displayed);
            if (CardsGrid.EditingControl != null)
                CardsGrid.EditingControl.Enabled = b;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys e)
        {
            if (e == Keys.Up || e == Keys.Down || e == Keys.Left || e == Keys.Right)
            {
                if (isTextEdit == true)
                {
                    DataGridViewKey(e);
                }
            }
            return base.ProcessCmdKey(ref msg, e);
        }
        private void DataGridViewKey(Keys e)
        {
            TextBoxBase txtEditingBox = (TextBoxBase)CardsGrid.EditingControl;
            if (txtEditingBox != null)
            {
                if (e == Keys.Left)
                {
                    if (txtEditingBox.SelectionStart == 0)
                    {
                        _key = Keys.Left;
                        isKeyWorked = false;
                        return;
                    }
                }
                else if (e == Keys.Right)
                {
                    if (txtEditingBox.SelectionStart == txtEditingBox.TextLength && CardsGrid.CurrentCell.ColumnIndex != (int)cardGridColumn.TRACK3_COLUMN)
                    {
                        _key = Keys.Right;
                        isKeyWorked = false;
                        return;
                    }
                }
                else if (e == Keys.Up || e == Keys.Down)
                {

                    //_fontWidth = TextRenderer.MeasureText(txtEditingBox.Text.Substring(0, txtEditingBox.SelectionStart), txtEditingBox.Font).Width;

                    _key = Keys.Up;
                    isKeyWorked = false;
                    return;
                }
            }
            else
            {
                _key = Keys.Right;
                return;
            }
        }

        private void dataGridVariants_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
                return;


            if ((VariantsGrid.SelectedRows.Count == 1) &&
                (CardsGrid.SelectedCells.Count == 1))
            {
                Track group = (Track)VariantsGrid.DataSource;
                Variant v;
                v = group.SetPreferredUserVariant(VariantsGrid.SelectedRows[0].Index);
                try
                {
                    Clipboard.SetDataObject(v.BinaryString, true);
                }
                catch(Exception) {};
                ListCardUtil.SetAlignment(CardsGrid.GetActualCardsFromGrid(), 0);
                gridRefresh();//CardsGrid.Refresh();

            }
        }

        private void dataGridVariants_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != (int)VariantGridColumn.Rate)
            {
                return;
            }
            _decodePresenter.changeSortTrack();
        }
        public void toolStripComboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = BPCFormat.SelectedIndex;

            for (int i = 0; i < 3; i++)
            {
                if (VariantSettings.bpc[i] == (selectedIndex + (int)BpcSupported.Start))
                {
                    _decodePresenter.SetAddNumber(VariantSettings.add[i], toolStripAddText_TextChanged);
                    break;
                }
            }
            if ((selectedIndex >= 0) && (selectedIndex <= (int)BpcSupported.Range))
                _decodePresenter.variantBuildSettingsChanged((byte)(selectedIndex + (int)BpcSupported.Start));
        }

        public void toolStripAddText_TextChanged(object sender, EventArgs e)
        {
            int bpc = -1;
            int selectedIndex = BPCFormat.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex <= (int)BpcSupported.Range))
                bpc = (selectedIndex + (int)BpcSupported.Start);

            if (bpc != -1)
                _decodePresenter.variantBuildSettingsChanged((byte)bpc);
        }

        private void toolStripShowCharsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Properties.Settings.Default.decodeMethod == ShowChars.SelectedIndex)
            //    return;

            int lastIndex = Properties.Settings.Default.decodeMethod;
            Properties.Settings.Default.decodeMethod = ShowChars.SelectedIndex;
            Variant._DecodeMethod = ShowChars.SelectedIndex;
            if (lastIndex == (int)DecodeSettings.ValidChars.LongestValidCRC || 
                ShowChars.SelectedIndex == (int)DecodeSettings.ValidChars.LongestValidCRC)
            {
                gridAutoResizeColumn(-1);
            }
            else
            {
                gridRefresh();//CardsGrid.Refresh();
            }
            VariantsGrid.Refresh();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            CardsGrid.Filter = txtFilter.Text;
        }

        private static int readIntFromToolStripTextBox(ToolStripTextBox t, int lastValue)
        {
            int r;
            try
            {
                r = (int)Convert.ToUInt32(t.Text);
            }
            catch
            {
                r = lastValue;
                t.Text = lastValue.ToString();
                MessageBox.Show("Only positive numbers are allowed in this field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return r;
        }

        private void txtMsGroup_TextChanged(object sender, EventArgs e)
        {
            if (txtMsGroup.Text == null || txtMsGroup.Text == "")
                return;

            DecodeSettings.groupSwipesInterval = readIntFromToolStripTextBox(txtMsGroup, DecodeSettings.groupSwipesInterval);
            ListCardUtil.SetGroupCards(CardsGrid.GetActualCardsFromGrid(), 0);
            ListCardUtil.SetAlignment(CardsGrid.GetActualCardsFromGrid(), 0);
            gridRefresh();//CardsGrid.Refresh();
        }

        private void txtCommonSubStringChars_TextChanged(object sender, EventArgs e)
        {
            if (txtCommonSubStringChars.Text == null || txtCommonSubStringChars.Text == "")
                return;
            Settings.Default.alignChars = readIntFromToolStripTextBox(txtCommonSubStringChars, Settings.Default.alignChars);
            ListCardUtil.SetGroupCards(CardsGrid.GetActualCardsFromGrid(), 0);
            ListCardUtil.SetAlignment(CardsGrid.GetActualCardsFromGrid(), 0);
            gridRefresh();//CardsGrid.Refresh();
        }
        #endregion

        #region Windows Load and Tools Menu Event

        private void deleteEmptyCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IList<Card> cards = CardsGrid.GetCardsFromGrid();

            if ((cards == null) || (cards.Count == 0))
            {
                MessageBox.Show(DecodeForm, "No empty cards found, document is empty.",
                                "Decode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                CharsDeleteCards deleteForm = new CharsDeleteCards(cards);
                if (deleteForm.ShowDialog(DecodeForm) == DialogResult.OK)
                {
                    List<Card> indexes = deleteForm.GoodCards;

                    if (deleteForm.emptyCards == 0)
                    {
                        MessageBox.Show(DecodeForm, "No empty cards found.", "Decode",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show(DecodeForm, deleteForm.emptyCards + " empty cards found. Are you sure you want to delete them?",
                                            "Decode", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            CardsGrid.SetCardsInGrid(indexes);
                        }
                    }
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_decodePresenter.settingsWindow == null)
                _decodePresenter.showDecodeSetting(false);
            else
                _decodePresenter.settingsWindow.BringToFront();
        }

        private void frmDecode_Load(object sender, EventArgs e)
        {
            //foreach (Control c in this.Controls)
            //    c.Visible = false;
            isStaring = true;
            _decodePresenter.setCardsGridDataSource(Cards);
            InitialDataGrid(e);
            txtMsGroup.Text = Forms.DecodeSettings.groupSwipesInterval.ToString();
            txtCommonSubStringChars.Text = Properties.Settings.Default.alignChars.ToString();
            ShowChars.SelectedIndex = (int)Properties.Settings.Default.decodeMethod;
            isStaring = false;
        }
        private void InitialDataGrid(EventArgs e)
        {
            if (btnViewTime.Checked != Settings.Default.showTime)
            {
                btnViewTime.Checked = Settings.Default.showTime;
                btnViewTime_Click(btnViewTime, e);
            }
            if (btnViewDirection.Checked != Settings.Default.showDirection)
            {
                btnViewDirection.Checked = Settings.Default.showDirection;
                btnViewDirection_Click(btnViewDirection, e);
            }
            if (btnViewTrack1.Checked != Settings.Default.showTrack1)
            {
                btnViewTrack1.Checked = Settings.Default.showTrack1;
                btnViewTrack1_Click(btnViewTrack1, e);
            }
            if (btnViewTrack2.Checked != Settings.Default.showTrack2)
            {
                btnViewTrack2.Checked = Settings.Default.showTrack2;
                btnViewTrack2_Click(btnViewTrack2, e);
            }
            if (btnViewTrack3.Checked != Settings.Default.showTrack3)
            {
                btnViewTrack3.Checked = Settings.Default.showTrack3;
                btnViewTrack3_Click(btnViewTrack3, e);
            }
        }
        #endregion

        private void frmDecode_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isCrfModified || isTxtModified)
            {
                string warning = "";
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                if (isCrfModified && isTxtModified)
                {
                    warning = fileName + "." + Program.extension + " and " + fileName + ".txt";
                }
                else if (isCrfModified && !isTxtModified)
                {
                    warning = fileName + "." + Program.extension;
                }
                else if (!isCrfModified && isTxtModified)
                {
                    warning = fileName + ".txt ";
                }
                DialogResult result;
                result = MessageBox.Show(DecodeForm, "The file " + warning + " has been modified. Do you want to save your changes?",
                                            "Warning ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (isCrfModified && isTxtModified)
                        {
                            if (!_decodePresenter.saveCurrentFile(CurrentFileName, CardFileFormat.Format.CRF, false) && !SaveTxtText(_decodePresenter.GetCurrentFullFileNoWithExtension() + ".txt"))
                                e.Cancel = true;
                        }
                        else if (isCrfModified && !isTxtModified)
                        {
                            if (!_decodePresenter.saveCurrentFile(CurrentFileName, CardFileFormat.Format.CRF, false))
                                e.Cancel = true;
                        }
                        else if (!isCrfModified && isTxtModified)
                        {
                            if (!SaveTxtText(_decodePresenter.GetCurrentFullFileNoWithExtension() + ".txt"))
                                e.Cancel = true;
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
            isCrfModified = false;
            isTxtModified = false;
            if (_decodePresenter != null && _decodePresenter.settingsWindow != null)
                _decodePresenter.settingsWindow.Close();
        }

        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            if (isSimpleDecodeFirst)
            {
                isTxtModified = true;
            }
            isSimpleDecodeFirst = true;
            Int32 lines = CardsTextBox.Lines.Length;
            Int32 textLength = CardsTextBox.Text.Length;
            StatusSimpleDecode.Text = "Lines: " + lines + " Characters: " + textLength;
        }

        public void UpdateTxtBox()
        {
            txtBox_VisibleChanged(txtBox, new EventArgs());
        }
        public void txtBox_VisibleChanged(object sender, EventArgs e)
        {
            RichTextBox txtBox = sender as RichTextBox;
            if (txtBox.Visible == true)
            {
                exportAsCsvToolStripMenuItem.Enabled = false;
                exportFileCsvToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                IList<Card> cards = CardsGrid.GetCardsFromGrid();
                ListCardUtil.SetGroupCards(CardsGrid.GetActualCardsFromGrid(), 0);
                ListCardUtil.SetAlignment(CardsGrid.GetActualCardsFromGrid(), 0);
                MemoryStream stream = new MemoryStream();
                CardWriter cardWriter = new CardWriter(stream);
                StreamReader reader = new StreamReader(stream);
                int fields = 0;
                for (int i = 0; i < cardWriter.Fields.Length; i++)
                {
                    //column order must be the same as fields order!!
                    if (CardsGrid.Columns[i].Visible)
                        fields |= cardWriter.Fields[i];
                }
                cardWriter.Write("", cards, fields);

                stream.Seek(0, SeekOrigin.Begin);
                txtBox.Text = reader.ReadToEnd();
            }
            else
            {
                exportAsCsvToolStripMenuItem.Enabled = true;
                exportFileCsvToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void showKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The current key is: " + currentKey + "\n\nIt has been copied to clipboard.", "Key");
            try
            {
                Clipboard.SetDataObject(currentKey, true); 
            }
            catch (Exception)
            {

            }
            
        }

        private void frmDecode_Shown(object sender, EventArgs e)
        {
            resizeColumnsNowToolStripMenuItem_Click(sender, e);
            if (Properties.Settings.Default.showDecodeSettings)
                _decodePresenter.showDecodeSetting(true);
        }

         private void dataGridCards_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CardsGrid.CurrentCell != null && CardsGrid.CurrentCell.Value!=null)
            {
                string cellString = CardsGrid.CurrentCell.Value.ToString();
                if (!cellString.Equals(priString))
                {
                    isCrfModified = true;
                    priString = cellString;
                }
            }
        }
         private void autoResizeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setResizeColumns(!autoResizeColumnsToolStripMenuItem.Checked);
        }

        private void setResizeColumns(bool value)
        {
            autoResizeColumnsToolStripMenuItem.Checked = value;
            if (Properties.Settings.Default.autoResize != value)
            {
                Properties.Settings.Default.autoResize = autoResizeColumnsToolStripMenuItem.Checked;
                //Properties.Settings.Default.Save();
            }
        }

        private void resizeColumnsNowToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CardsGrid.AutoResizeColumns();
#if DEBUG
            CardDataGridView.RESIZECOLUMNCOUNT += 3;
            CardsGrid.UpdateStatusStripTextWhenResizing();
#endif
        }

        public void ToggleMenu(bool show)
        {
            if (show)
            {
                viewToolStripMenuItem.Enabled = true;
                toolStrip1.Visible = ViewShowToolbar.Checked;
                toolStrip2.Visible = ViewSettingsToolbar.Checked;
                toolStrip3.Visible = ViewDecodeToolbar.Checked;
            }
            else
            {
                viewToolStripMenuItem.Enabled = false;
                toolStrip1.Visible = show;
                toolStrip2.Visible = show;
                toolStrip3.Visible = show;
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Stop();
            CardsGrid.Refresh();
        }
        public void gridRefresh()
        {
            if (isStaring || resizeColumnTimer.Enabled)
                return;

            refreshTimer.Tick -= new EventHandler(refreshTimer_Tick);
            refreshTimer.Stop();
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);
            refreshTimer.Interval = 100;
            refreshTimer.Start();
        }

        private void resizeColumnTimer_Tick(object sender, EventArgs e)
        {
            List<int> columns = (List<int>)((Timer)sender).Tag;
            foreach (int column in columns)
            {
                if (column == -1)
                {
                    CardsGrid.AutoResizeColumns();
                    break;
                }
                else
                {
                    CardsGrid.AutoResizeColumn(column);
                }
            }
            ((Timer)sender).Tag = new List<int>();
            ((Timer)sender).Stop();
        }

        public void gridAutoResizeColumn(int id)
        {
            if (!Properties.Settings.Default.autoResize)
                return;

            Timer t = resizeColumnTimer;
            t.Stop();
            t.Interval = 100;
            List<int> columns = (List<int>)(t.Tag);
            if (columns.FindIndex(delegate(int i) { return i == id; }) == -1)
            {
                columns.Add(id);
            }
            t.Start();
            refreshTimer.Stop();
            //_decodeForm.CardsGrid.AutoResizeColumn(id);
#if DEBUG
            CardDataGridView.RESIZECOLUMNCOUNT++;
            CardsGrid.UpdateStatusStripTextWhenResizing();
#endif
        }

        private void autoResizeToolStripButton_Click(object sender, EventArgs e)
        {
            toolStripStatusSimple.Text = "Resizing ... please wait";
            dataGridCards.AutoResizeColumns();
            toolStripStatusSimple.Text = "Resizing complete.";
        }

        private void hideUnusedTracks()
        {
            int t1, t2, t3;
            if (_cards == null)
                return;
          
            _decodePresenter.findTracksWithInfo(_cards, out t1, out t2, out t3);

            Settings.Default.showTrack1 = (t1 != 0);
            Settings.Default.showTrack2 = (t2 != 0);
            Settings.Default.showTrack3 = (t3 != 0);
        }

        private void mergeCards_Click(object sender, EventArgs e)
        {
            CardsGrid.MergeSelectedCards();            
        }
    }

}


