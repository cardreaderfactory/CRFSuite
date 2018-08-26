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
    partial class frmDecode
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridCards = new crf.CustomsControls.CardDataGridView();
            this.ReaderCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DirectionAsImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.timeDataGridViewTextBoxColumn = new crf.DataGridViewTimeColumn();
            this.track1DataGridViewTextBoxColumn = new crf.DataGridViewTrackColumn();
            this.track2DataGridViewTextBoxColumn = new crf.DataGridViewTrackColumn();
            this.track3DataGridViewTextBoxColumn = new crf.DataGridViewTrackColumn();
            this.iCardBindingSource = new crf.CustomsControls.CardBindingSource(this.components);
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.dataGridVariants = new System.Windows.Forms.DataGridView();
            this.Rate = new crf.DataGridViewProgressColumn();
            this.DirectionImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Solution = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.binaryStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iVariantBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusSimple = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusAdvance = new System.Windows.Forms.ToolStripStatusLabel();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.cardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripShowCharsCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripFormatLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboFormat = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripAddLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripAddText = new crf.CustomsControls.ToolStripTextBoxAcceptDelKey();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoveryOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportFileTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFileCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textEdidModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editOnEnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editOnF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolbar1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSettingsToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDecodeToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.viewIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTrack1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTrack2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTrack3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.autoResizeColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeColumnsNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEmptyCardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnViewID = new System.Windows.Forms.ToolStripButton();
            this.btnViewDirection = new System.Windows.Forms.ToolStripButton();
            this.btnViewTime = new System.Windows.Forms.ToolStripButton();
            this.btnViewTrack1 = new System.Windows.Forms.ToolStripButton();
            this.btnViewTrack2 = new System.Windows.Forms.ToolStripButton();
            this.btnViewTrack3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.autoResizeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewProgressColumn1 = new crf.DataGridViewProgressColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.lblCombine = new System.Windows.Forms.ToolStripLabel();
            this.txtMsGroup = new crf.CustomsControls.ToolStripTextBoxAcceptDelKey();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtCommonSubStringChars = new crf.CustomsControls.ToolStripTextBoxAcceptDelKey();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFilter = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new crf.CustomsControls.ToolStripTextBoxAcceptDelKey();
            this.cardBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.resizeColumnTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iCardBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iVariantBindingSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource1)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.AutoUpgradeEnabled = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.dataGridCards);
            this.splitContainer1.Panel1.Controls.Add(this.txtBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.dataGridVariants);
            this.splitContainer1.Size = new System.Drawing.Size(694, 213);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 8;
            // 
            // dataGridCards
            // 
            this.dataGridCards.AllowUserToAddRows = false;
            this.dataGridCards.AllowUserToDeleteRows = false;
            this.dataGridCards.AutoGenerateColumns = false;
            this.dataGridCards.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReaderCard,
            this.cardNumberDataGridViewTextBoxColumn,
            this.DirectionAsImage,
            this.timeDataGridViewTextBoxColumn,
            this.track1DataGridViewTextBoxColumn,
            this.track2DataGridViewTextBoxColumn,
            this.track3DataGridViewTextBoxColumn});
            this.dataGridCards.DataSource = this.iCardBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridCards.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridCards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridCards.Filter = "";
            this.dataGridCards.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dataGridCards.Location = new System.Drawing.Point(0, 0);
            this.dataGridCards.Name = "dataGridCards";
            this.dataGridCards.RowHeadersVisible = false;
            this.dataGridCards.RowTemplate.Height = 24;
            this.dataGridCards.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridCards.Size = new System.Drawing.Size(694, 180);
            this.dataGridCards.TabIndex = 1;
            this.dataGridCards.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridCards_CellEndEdit);
            this.dataGridCards.SelectionChanged += new System.EventHandler(this.dataGridCards_SelectionChanged);
            this.dataGridCards.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridCards_MouseDown);
            // 
            // ReaderCard
            // 
            this.ReaderCard.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ReaderCard.DataPropertyName = "ReaderCard";
            this.ReaderCard.HeaderText = "ReaderCard";
            this.ReaderCard.Name = "ReaderCard";
            this.ReaderCard.ReadOnly = true;
            this.ReaderCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReaderCard.Visible = false;
            // 
            // cardNumberDataGridViewTextBoxColumn
            // 
            this.cardNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cardNumberDataGridViewTextBoxColumn.DataPropertyName = "CardNumber";
            this.cardNumberDataGridViewTextBoxColumn.FillWeight = 15F;
            this.cardNumberDataGridViewTextBoxColumn.HeaderText = "Card";
            this.cardNumberDataGridViewTextBoxColumn.MinimumWidth = 40;
            this.cardNumberDataGridViewTextBoxColumn.Name = "cardNumberDataGridViewTextBoxColumn";
            this.cardNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.cardNumberDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cardNumberDataGridViewTextBoxColumn.Width = 54;
            // 
            // DirectionAsImage
            // 
            this.DirectionAsImage.DataPropertyName = "DirectionAsImage";
            this.DirectionAsImage.FillWeight = 20F;
            this.DirectionAsImage.HeaderText = "Direction";
            this.DirectionAsImage.MinimumWidth = 60;
            this.DirectionAsImage.Name = "DirectionAsImage";
            this.DirectionAsImage.ReadOnly = true;
            this.DirectionAsImage.Width = 60;
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.FillWeight = 60F;
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            this.timeDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.timeDataGridViewTextBoxColumn.Width = 36;
            // 
            // track1DataGridViewTextBoxColumn
            // 
            this.track1DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.track1DataGridViewTextBoxColumn.DataPropertyName = "Track1";
            this.track1DataGridViewTextBoxColumn.FillWeight = 110F;
            this.track1DataGridViewTextBoxColumn.HeaderText = "Track1";
            this.track1DataGridViewTextBoxColumn.MinimumWidth = 50;
            this.track1DataGridViewTextBoxColumn.Name = "track1DataGridViewTextBoxColumn";
            this.track1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.track1DataGridViewTextBoxColumn.Width = 300;
            // 
            // track2DataGridViewTextBoxColumn
            // 
            this.track2DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.track2DataGridViewTextBoxColumn.DataPropertyName = "Track2";
            this.track2DataGridViewTextBoxColumn.FillWeight = 200F;
            this.track2DataGridViewTextBoxColumn.HeaderText = "Track2";
            this.track2DataGridViewTextBoxColumn.MinimumWidth = 50;
            this.track2DataGridViewTextBoxColumn.Name = "track2DataGridViewTextBoxColumn";
            this.track2DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.track2DataGridViewTextBoxColumn.Width = 300;
            // 
            // track3DataGridViewTextBoxColumn
            // 
            this.track3DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.track3DataGridViewTextBoxColumn.DataPropertyName = "Track3";
            this.track3DataGridViewTextBoxColumn.FillWeight = 75F;
            this.track3DataGridViewTextBoxColumn.HeaderText = "Track3";
            this.track3DataGridViewTextBoxColumn.MinimumWidth = 50;
            this.track3DataGridViewTextBoxColumn.Name = "track3DataGridViewTextBoxColumn";
            this.track3DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.track3DataGridViewTextBoxColumn.Width = 300;
            // 
            // iCardBindingSource
            // 
            this.iCardBindingSource.DataSource = typeof(crf.ICard);
            // 
            // txtBox
            // 
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(694, 180);
            this.txtBox.TabIndex = 2;
            this.txtBox.Text = "";
            this.txtBox.Visible = false;
            this.txtBox.WordWrap = false;
            this.txtBox.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
            this.txtBox.VisibleChanged += new System.EventHandler(this.txtBox_VisibleChanged);
            // 
            // dataGridVariants
            // 
            this.dataGridVariants.AllowUserToAddRows = false;
            this.dataGridVariants.AllowUserToDeleteRows = false;
            this.dataGridVariants.AutoGenerateColumns = false;
            this.dataGridVariants.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridVariants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVariants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rate,
            this.DirectionImage,
            this.Solution,
            this.binaryStringDataGridViewTextBoxColumn});
            this.dataGridVariants.DataSource = this.iVariantBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridVariants.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridVariants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridVariants.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dataGridVariants.Location = new System.Drawing.Point(0, 0);
            this.dataGridVariants.Name = "dataGridVariants";
            this.dataGridVariants.RowHeadersVisible = false;
            this.dataGridVariants.RowTemplate.Height = 24;
            this.dataGridVariants.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridVariants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridVariants.Size = new System.Drawing.Size(694, 30);
            this.dataGridVariants.TabIndex = 2;
            this.dataGridVariants.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridVariants_CellClick);
            this.dataGridVariants.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridVariants_ColumnHeaderMouseClick);
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.FillWeight = 15.45403F;
            this.Rate.HeaderText = "Rate";
            this.Rate.MinimumWidth = 50;
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Rate.Width = 50;
            // 
            // DirectionImage
            // 
            this.DirectionImage.DataPropertyName = "DirectionAsImage";
            this.DirectionImage.FillWeight = 30.45685F;
            this.DirectionImage.HeaderText = "Direction";
            this.DirectionImage.MinimumWidth = 60;
            this.DirectionImage.Name = "DirectionImage";
            this.DirectionImage.ReadOnly = true;
            this.DirectionImage.Width = 60;
            // 
            // Solution
            // 
            this.Solution.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Solution.DataPropertyName = "ToStringWithParity";
            this.Solution.HeaderText = "Solution";
            this.Solution.Name = "Solution";
            this.Solution.ReadOnly = true;
            this.Solution.Width = 70;
            // 
            // binaryStringDataGridViewTextBoxColumn
            // 
            this.binaryStringDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.binaryStringDataGridViewTextBoxColumn.DataPropertyName = "BinaryString";
            this.binaryStringDataGridViewTextBoxColumn.HeaderText = "Binary";
            this.binaryStringDataGridViewTextBoxColumn.Name = "binaryStringDataGridViewTextBoxColumn";
            this.binaryStringDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iVariantBindingSource
            // 
            this.iVariantBindingSource.DataSource = typeof(crf.IVariant);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusSimple,
            this.toolStripStatusAdvance});
            this.statusStrip.Location = new System.Drawing.Point(0, 311);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(694, 22);
            this.statusStrip.TabIndex = 9;
            // 
            // toolStripStatusSimple
            // 
            this.toolStripStatusSimple.Name = "toolStripStatusSimple";
            this.toolStripStatusSimple.Size = new System.Drawing.Size(120, 17);
            this.toolStripStatusSimple.Text = "toolStripStatusSimple";
            this.toolStripStatusSimple.Visible = false;
            // 
            // toolStripStatusAdvance
            // 
            this.toolStripStatusAdvance.Name = "toolStripStatusAdvance";
            this.toolStripStatusAdvance.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusAdvance.Text = "toolStripStatusAdvance";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripShowCharsCombo,
            this.toolStripSeparator10,
            this.toolStripFormatLabel,
            this.toolStripComboFormat,
            this.toolStripAddLabel,
            this.toolStripAddText});
            this.toolStrip2.Location = new System.Drawing.Point(3, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(560, 25);
            this.toolStrip2.TabIndex = 18;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Image = global::crf.Properties.Resources.text_marked;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(111, 22);
            this.toolStripLabel3.Text = "Decode Method:";
            // 
            // toolStripShowCharsCombo
            // 
            this.toolStripShowCharsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripShowCharsCombo.DropDownWidth = 230;
            this.toolStripShowCharsCombo.Items.AddRange(new object[] {
            "Show everything (including the errors)",
            "Show only the valid data, hide the errors",
            "Show the best guess"});
            this.toolStripShowCharsCombo.MaxDropDownItems = 3;
            this.toolStripShowCharsCombo.Name = "toolStripShowCharsCombo";
            this.toolStripShowCharsCombo.Size = new System.Drawing.Size(230, 25);
            this.toolStripShowCharsCombo.ToolTipText = "Data to display";
            this.toolStripShowCharsCombo.SelectedIndexChanged += new System.EventHandler(this.toolStripShowCharsCombo_SelectedIndexChanged);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripFormatLabel
            // 
            this.toolStripFormatLabel.Image = global::crf.Properties.Resources.transform;
            this.toolStripFormatLabel.Name = "toolStripFormatLabel";
            this.toolStripFormatLabel.Size = new System.Drawing.Size(64, 22);
            this.toolStripFormatLabel.Text = "Format:";
            // 
            // toolStripComboFormat
            // 
            this.toolStripComboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboFormat.DropDownWidth = 20;
            this.toolStripComboFormat.Items.AddRange(new object[] {
            "4 bits",
            "5 bits",
            "6 bits",
            "7 bits",
            "8 bits",
            "9 bits"});
            this.toolStripComboFormat.MaxDropDownItems = 6;
            this.toolStripComboFormat.Name = "toolStripComboFormat";
            this.toolStripComboFormat.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboFormat.ToolTipText = "Bits per byte (including the parity bit) used by your decoding format";
            this.toolStripComboFormat.SelectedIndexChanged += new System.EventHandler(this.toolStripComboFormat_SelectedIndexChanged);
            // 
            // toolStripAddLabel
            // 
            this.toolStripAddLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAddLabel.Image = global::crf.Properties.Resources.add;
            this.toolStripAddLabel.Name = "toolStripAddLabel";
            this.toolStripAddLabel.Size = new System.Drawing.Size(16, 22);
            this.toolStripAddLabel.Text = "Add (hex):";
            // 
            // toolStripAddText
            // 
            this.toolStripAddText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.toolStripAddText.MaxLength = 2;
            this.toolStripAddText.Name = "toolStripAddText";
            this.toolStripAddText.Size = new System.Drawing.Size(40, 25);
            this.toolStripAddText.ToolTipText = "The ASCII code of the first character in the dictionary used by your decoding for" +
    "mat. The value is expected in hexadecimal (base 16).";
            this.toolStripAddText.TextChanged += new System.EventHandler(this.toolStripAddText_TextChanged);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(172, 6);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 175);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(694, 23);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.recoveryOpenToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator6,
            this.exportFileTxtToolStripMenuItem,
            this.exportFileCsvToolStripMenuItem,
            this.exportAsTxtToolStripMenuItem,
            this.exportAsCsvToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // recoveryOpenToolStripMenuItem
            // 
            this.recoveryOpenToolStripMenuItem.Name = "recoveryOpenToolStripMenuItem";
            this.recoveryOpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.recoveryOpenToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.recoveryOpenToolStripMenuItem.Text = "Open (recovery mode)...";
            this.recoveryOpenToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.saveAsToolStripMenuItem.Text = "Save &as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(274, 6);
            // 
            // exportFileTxtToolStripMenuItem
            // 
            this.exportFileTxtToolStripMenuItem.Name = "exportFileTxtToolStripMenuItem";
            this.exportFileTxtToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exportFileTxtToolStripMenuItem.Text = "fileTxt";
            this.exportFileTxtToolStripMenuItem.Click += new System.EventHandler(this.exportFileTxtToolStripMenuItem_Click);
            // 
            // exportFileCsvToolStripMenuItem
            // 
            this.exportFileCsvToolStripMenuItem.Name = "exportFileCsvToolStripMenuItem";
            this.exportFileCsvToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exportFileCsvToolStripMenuItem.Text = "fileCsv";
            this.exportFileCsvToolStripMenuItem.Click += new System.EventHandler(this.exportFileCsvToolStripMenuItem_Click);
            // 
            // exportAsTxtToolStripMenuItem
            // 
            this.exportAsTxtToolStripMenuItem.Name = "exportAsTxtToolStripMenuItem";
            this.exportAsTxtToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exportAsTxtToolStripMenuItem.Text = "Export as Text...";
            this.exportAsTxtToolStripMenuItem.Click += new System.EventHandler(this.exportAsTxtToolStripMenuItem_Click);
            // 
            // exportAsCsvToolStripMenuItem
            // 
            this.exportAsCsvToolStripMenuItem.Name = "exportAsCsvToolStripMenuItem";
            this.exportAsCsvToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exportAsCsvToolStripMenuItem.Text = "Export as CSV...";
            this.exportAsCsvToolStripMenuItem.Click += new System.EventHandler(this.exportAsCsvToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(274, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator2,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.findToolStripMenuItem,
            this.findNextStripMenuItem,
            this.replaceToolStripMenuItem,
            this.goToToolStripMenuItem,
            this.toolStripSeparator3,
            this.selectAllToolStripMenuItem,
            this.textEdidModeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 19);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.redoToolStripMenuItem.Text = "R&edo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.findToolStripMenuItem.Text = "&Find ...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findNextStripMenuItem
            // 
            this.findNextStripMenuItem.Name = "findNextStripMenuItem";
            this.findNextStripMenuItem.ShortcutKeyDisplayString = "F3";
            this.findNextStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.findNextStripMenuItem.Text = "Find &Next";
            this.findNextStripMenuItem.Click += new System.EventHandler(this.findNextStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.replaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.replaceToolStripMenuItem.Text = "&Replace ...";
            this.replaceToolStripMenuItem.Visible = false;
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+G";
            this.goToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.goToToolStripMenuItem.Text = "&GoTo";
            this.goToToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(165, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // textEdidModeToolStripMenuItem
            // 
            this.textEdidModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editOnEnterToolStripMenuItem,
            this.editOnF2ToolStripMenuItem,
            this.textEditToolStripMenuItem});
            this.textEdidModeToolStripMenuItem.Name = "textEdidModeToolStripMenuItem";
            this.textEdidModeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.textEdidModeToolStripMenuItem.Text = "&EditMode";
            // 
            // editOnEnterToolStripMenuItem
            // 
            this.editOnEnterToolStripMenuItem.Name = "editOnEnterToolStripMenuItem";
            this.editOnEnterToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editOnEnterToolStripMenuItem.Text = "Edit on Enter";
            this.editOnEnterToolStripMenuItem.Click += new System.EventHandler(this.editOnEnterToolStripMenuItem_Click);
            // 
            // editOnF2ToolStripMenuItem
            // 
            this.editOnF2ToolStripMenuItem.Name = "editOnF2ToolStripMenuItem";
            this.editOnF2ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editOnF2ToolStripMenuItem.Text = "Edit on F2";
            this.editOnF2ToolStripMenuItem.Click += new System.EventHandler(this.editOnF2ToolStripMenuItem_Click);
            // 
            // textEditToolStripMenuItem
            // 
            this.textEditToolStripMenuItem.Checked = true;
            this.textEditToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.textEditToolStripMenuItem.Name = "textEditToolStripMenuItem";
            this.textEditToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.textEditToolStripMenuItem.Text = "Text Edit";
            this.textEditToolStripMenuItem.Click += new System.EventHandler(this.textEdidModeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolbar1ToolStripMenuItem,
            this.viewSettingsToolbar,
            this.viewDecodeToolbar,
            this.statusBarToolStripMenuItem,
            this.toolStripSeparator8,
            this.viewIDToolStripMenuItem,
            this.viewTimeToolStripMenuItem,
            this.viewTrack1ToolStripMenuItem,
            this.viewTrack2ToolStripMenuItem,
            this.viewTrack3ToolStripMenuItem,
            this.viewDirectionToolStripMenuItem,
            this.toolStripSeparator7,
            this.autoResizeColumnsToolStripMenuItem,
            this.resizeColumnsNowToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // viewToolbar1ToolStripMenuItem
            // 
            this.viewToolbar1ToolStripMenuItem.Checked = true;
            this.viewToolbar1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolbar1ToolStripMenuItem.Name = "viewToolbar1ToolStripMenuItem";
            this.viewToolbar1ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewToolbar1ToolStripMenuItem.Text = "&Fields Toolbar";
            this.viewToolbar1ToolStripMenuItem.Click += new System.EventHandler(this.viewToolbar1ToolStripMenuItem_Click);
            // 
            // viewSettingsToolbar
            // 
            this.viewSettingsToolbar.Checked = true;
            this.viewSettingsToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewSettingsToolbar.Name = "viewSettingsToolbar";
            this.viewSettingsToolbar.Size = new System.Drawing.Size(221, 22);
            this.viewSettingsToolbar.Text = "Quick &Settings Toolbar";
            this.viewSettingsToolbar.Click += new System.EventHandler(this.viewToolbar2ToolStripMenuItem_Click);
            // 
            // viewDecodeToolbar
            // 
            this.viewDecodeToolbar.Checked = true;
            this.viewDecodeToolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewDecodeToolbar.Name = "viewDecodeToolbar";
            this.viewDecodeToolbar.Size = new System.Drawing.Size(221, 22);
            this.viewDecodeToolbar.Text = "&Decode Toolbar";
            this.viewDecodeToolbar.Click += new System.EventHandler(this.decodeToolbarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(218, 6);
            // 
            // viewIDToolStripMenuItem
            // 
            this.viewIDToolStripMenuItem.Name = "viewIDToolStripMenuItem";
            this.viewIDToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewIDToolStripMenuItem.Text = "&ID";
            this.viewIDToolStripMenuItem.Visible = false;
            // 
            // viewTimeToolStripMenuItem
            // 
            this.viewTimeToolStripMenuItem.Name = "viewTimeToolStripMenuItem";
            this.viewTimeToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewTimeToolStripMenuItem.Text = "&Time";
            this.viewTimeToolStripMenuItem.Visible = false;
            // 
            // viewTrack1ToolStripMenuItem
            // 
            this.viewTrack1ToolStripMenuItem.Name = "viewTrack1ToolStripMenuItem";
            this.viewTrack1ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewTrack1ToolStripMenuItem.Text = "Track&1";
            this.viewTrack1ToolStripMenuItem.Visible = false;
            // 
            // viewTrack2ToolStripMenuItem
            // 
            this.viewTrack2ToolStripMenuItem.Name = "viewTrack2ToolStripMenuItem";
            this.viewTrack2ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewTrack2ToolStripMenuItem.Text = "Track&2";
            this.viewTrack2ToolStripMenuItem.Visible = false;
            // 
            // viewTrack3ToolStripMenuItem
            // 
            this.viewTrack3ToolStripMenuItem.Name = "viewTrack3ToolStripMenuItem";
            this.viewTrack3ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewTrack3ToolStripMenuItem.Text = "Track&3";
            this.viewTrack3ToolStripMenuItem.Visible = false;
            // 
            // viewDirectionToolStripMenuItem
            // 
            this.viewDirectionToolStripMenuItem.Name = "viewDirectionToolStripMenuItem";
            this.viewDirectionToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.viewDirectionToolStripMenuItem.Text = "&Direction";
            this.viewDirectionToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(218, 6);
            this.toolStripSeparator7.Visible = false;
            // 
            // autoResizeColumnsToolStripMenuItem
            // 
            this.autoResizeColumnsToolStripMenuItem.Checked = true;
            this.autoResizeColumnsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoResizeColumnsToolStripMenuItem.Name = "autoResizeColumnsToolStripMenuItem";
            this.autoResizeColumnsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.autoResizeColumnsToolStripMenuItem.Text = "Auto r&esize columns";
            this.autoResizeColumnsToolStripMenuItem.Click += new System.EventHandler(this.autoResizeColumnsToolStripMenuItem_Click);
            // 
            // resizeColumnsNowToolStripMenuItem
            // 
            this.resizeColumnsNowToolStripMenuItem.Name = "resizeColumnsNowToolStripMenuItem";
            this.resizeColumnsNowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.resizeColumnsNowToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.resizeColumnsNowToolStripMenuItem.Text = "Re&size columns now";
            this.resizeColumnsNowToolStripMenuItem.Click += new System.EventHandler(this.resizeColumnsNowToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEmptyCardsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.showKeyToolStripMenuItem,
            this.toolStripSeparator11,
            this.settingsToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 19);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // deleteEmptyCardsToolStripMenuItem
            // 
            this.deleteEmptyCardsToolStripMenuItem.Name = "deleteEmptyCardsToolStripMenuItem";
            this.deleteEmptyCardsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.deleteEmptyCardsToolStripMenuItem.Text = "&Delete empty cards";
            this.deleteEmptyCardsToolStripMenuItem.Click += new System.EventHandler(this.deleteEmptyCardsToolStripMenuItem_Click);
            // 
            // showKeyToolStripMenuItem
            // 
            this.showKeyToolStripMenuItem.Name = "showKeyToolStripMenuItem";
            this.showKeyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.showKeyToolStripMenuItem.Text = "Show key";
            this.showKeyToolStripMenuItem.Click += new System.EventHandler(this.showKeyToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.settingsToolStripMenuItem1.Text = "&Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.btnViewID,
            this.btnViewDirection,
            this.btnViewTime,
            this.btnViewTrack1,
            this.btnViewTrack2,
            this.btnViewTrack3,
            this.toolStripSeparator13,
            this.toolStripButton3,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(448, 25);
            this.toolStrip1.TabIndex = 20;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel1.Image = global::crf.Properties.Resources.column_preferences;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel1.Text = "Show";
            // 
            // btnViewID
            // 
            this.btnViewID.Checked = true;
            this.btnViewID.CheckOnClick = true;
            this.btnViewID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewID.Name = "btnViewID";
            this.btnViewID.Size = new System.Drawing.Size(23, 22);
            this.btnViewID.Text = "ID";
            this.btnViewID.ToolTipText = "Show/Hide ID";
            this.btnViewID.Visible = false;
            this.btnViewID.Click += new System.EventHandler(this.btnViewID_Click);
            // 
            // btnViewDirection
            // 
            this.btnViewDirection.Checked = true;
            this.btnViewDirection.CheckOnClick = true;
            this.btnViewDirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewDirection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewDirection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewDirection.Name = "btnViewDirection";
            this.btnViewDirection.Size = new System.Drawing.Size(59, 22);
            this.btnViewDirection.Text = "Direction";
            this.btnViewDirection.ToolTipText = "Show/Hide Direction";
            this.btnViewDirection.Click += new System.EventHandler(this.btnViewDirection_Click);
            // 
            // btnViewTime
            // 
            this.btnViewTime.Checked = true;
            this.btnViewTime.CheckOnClick = true;
            this.btnViewTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewTime.Name = "btnViewTime";
            this.btnViewTime.Size = new System.Drawing.Size(38, 22);
            this.btnViewTime.Text = "Time";
            this.btnViewTime.ToolTipText = "Show/Hide Time";
            this.btnViewTime.Click += new System.EventHandler(this.btnViewTime_Click);
            // 
            // btnViewTrack1
            // 
            this.btnViewTrack1.Checked = true;
            this.btnViewTrack1.CheckOnClick = true;
            this.btnViewTrack1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewTrack1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewTrack1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewTrack1.Name = "btnViewTrack1";
            this.btnViewTrack1.Size = new System.Drawing.Size(46, 22);
            this.btnViewTrack1.Text = "Track1";
            this.btnViewTrack1.ToolTipText = "Show/Hide Track1";
            this.btnViewTrack1.Click += new System.EventHandler(this.btnViewTrack1_Click);
            // 
            // btnViewTrack2
            // 
            this.btnViewTrack2.Checked = true;
            this.btnViewTrack2.CheckOnClick = true;
            this.btnViewTrack2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewTrack2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewTrack2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewTrack2.Name = "btnViewTrack2";
            this.btnViewTrack2.Size = new System.Drawing.Size(46, 22);
            this.btnViewTrack2.Text = "Track2";
            this.btnViewTrack2.ToolTipText = "Show/Hide Track2";
            this.btnViewTrack2.Click += new System.EventHandler(this.btnViewTrack2_Click);
            // 
            // btnViewTrack3
            // 
            this.btnViewTrack3.Checked = true;
            this.btnViewTrack3.CheckOnClick = true;
            this.btnViewTrack3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnViewTrack3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnViewTrack3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewTrack3.Name = "btnViewTrack3";
            this.btnViewTrack3.Size = new System.Drawing.Size(46, 22);
            this.btnViewTrack3.Text = "Track3";
            this.btnViewTrack3.ToolTipText = "Show/Hide Track3";
            this.btnViewTrack3.Click += new System.EventHandler(this.btnViewTrack3_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::crf.Properties.Resources.gear;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton3.Text = "Settings";
            this.toolStripButton3.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::crf.Properties.Resources.left_right;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(110, 22);
            this.toolStripButton2.Text = "Resize Columns";
            this.toolStripButton2.Click += new System.EventHandler(this.resizeColumnsNowToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton1.Text = "Settings";
            this.toolStripButton1.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // autoResizeToolStripButton
            // 
            this.autoResizeToolStripButton.Image = global::crf.Properties.Resources.left_right;
            this.autoResizeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoResizeToolStripButton.Name = "autoResizeToolStripButton";
            this.autoResizeToolStripButton.Size = new System.Drawing.Size(110, 22);
            this.autoResizeToolStripButton.Text = "Resize Columns";
            this.autoResizeToolStripButton.Click += new System.EventHandler(this.autoResizeToolStripButton_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "DirectionAsImage";
            this.dataGridViewImageColumn1.FillWeight = 18.07095F;
            this.dataGridViewImageColumn1.HeaderText = "Direction";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 101;
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.DataPropertyName = "Rate";
            this.dataGridViewProgressColumn1.HeaderText = "Rate";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            this.dataGridViewProgressColumn1.Width = 101;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCombine,
            this.txtMsGroup,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.txtCommonSubStringChars,
            this.toolStripSeparator9,
            this.lblFilter,
            this.txtFilter});
            this.toolStrip3.Location = new System.Drawing.Point(3, 50);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(440, 25);
            this.toolStrip3.TabIndex = 21;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // lblCombine
            // 
            this.lblCombine.Image = global::crf.Properties.Resources.selection;
            this.lblCombine.Name = "lblCombine";
            this.lblCombine.Size = new System.Drawing.Size(118, 22);
            this.lblCombine.Text = "Grouping interval:";
            // 
            // txtMsGroup
            // 
            this.txtMsGroup.MaxLength = 7;
            this.txtMsGroup.Name = "txtMsGroup";
            this.txtMsGroup.Size = new System.Drawing.Size(70, 25);
            this.txtMsGroup.ToolTipText = "Cards with a time difference smaller than this number will be coloured";
            this.txtMsGroup.TextChanged += new System.EventHandler(this.txtMsGroup_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = global::crf.Properties.Resources.notebook;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(54, 22);
            this.toolStripLabel2.Text = "Align:";
            // 
            // txtCommonSubStringChars
            // 
            this.txtCommonSubStringChars.MaxLength = 2;
            this.txtCommonSubStringChars.Name = "txtCommonSubStringChars";
            this.txtCommonSubStringChars.Size = new System.Drawing.Size(40, 25);
            this.txtCommonSubStringChars.ToolTipText = "Number of minimum common chars to align cards";
            this.txtCommonSubStringChars.TextChanged += new System.EventHandler(this.txtCommonSubStringChars_TextChanged);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // lblFilter
            // 
            this.lblFilter.Image = global::crf.Properties.Resources.find;
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(52, 22);
            this.lblFilter.Text = "Filter:";
            // 
            // txtFilter
            // 
            this.txtFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(76, 25);
            this.txtFilter.ToolTipText = "Show only entries that contain this text";
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cardBindingSource1
            // 
            this.cardBindingSource1.DataSource = typeof(crf.Card);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // resizeColumnTimer
            // 
            this.resizeColumnTimer.Interval = 300;
            this.resizeColumnTimer.Tick += new System.EventHandler(this.resizeColumnTimer_Tick);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(694, 213);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 23);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(694, 288);
            this.toolStripContainer1.TabIndex = 22;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip3);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.toolStripMenuItem1.Text = "Merge cards";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.mergeCards_Click);
            // 
            // frmDecode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 333);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.Icon = global::crf.Properties.Resources.CRFIcon;
            this.Name = "frmDecode";
            this.Text = "Decode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDecode_FormClosing);
            this.Load += new System.EventHandler(this.frmDecode_Load);
            this.Shown += new System.EventHandler(this.frmDecode_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iCardBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVariants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iVariantBindingSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardBindingSource1)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private crf.CustomsControls.CardDataGridView dataGridCards;
        private System.Windows.Forms.DataGridView dataGridVariants;
        private System.Windows.Forms.StatusStrip statusStrip;
        private crf.CustomsControls.CardBindingSource iCardBindingSource;
        private System.Windows.Forms.BindingSource iVariantBindingSource;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.BindingSource cardBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripShowCharsCombo;
        private System.Windows.Forms.ToolStripLabel toolStripFormatLabel;
        private System.Windows.Forms.ToolStripComboBox toolStripComboFormat;
        private System.Windows.Forms.ToolStripLabel toolStripAddLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exportFileTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportFileCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAsCsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNextStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolbar1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem viewTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTrack1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTrack2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTrack3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDirectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnViewTime;
        private System.Windows.Forms.ToolStripButton btnViewTrack1;
        private System.Windows.Forms.ToolStripButton btnViewTrack2;
        private System.Windows.Forms.ToolStripButton btnViewTrack3;
        private System.Windows.Forms.ToolStripButton btnViewDirection;
        private System.Windows.Forms.ToolStripMenuItem viewSettingsToolbar;
        private System.Windows.Forms.ToolStripMenuItem deleteEmptyCardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSimple;
        private crf.CustomsControls.ToolStripTextBoxAcceptDelKey toolStripAddText;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel lblCombine;
        private CustomsControls.ToolStripTextBoxAcceptDelKey txtMsGroup;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private CustomsControls.ToolStripTextBoxAcceptDelKey txtCommonSubStringChars;
        private System.Windows.Forms.ToolStripLabel lblFilter;
        private CustomsControls.ToolStripTextBoxAcceptDelKey txtFilter;
        private System.Windows.Forms.ToolStripButton btnViewID;
        private System.Windows.Forms.ToolStripMenuItem viewIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textEdidModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDecodeToolbar;
        private System.Windows.Forms.ToolStripMenuItem editOnEnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editOnF2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textEditToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusAdvance;
        private System.Windows.Forms.ToolStripMenuItem showKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoResizeColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeColumnsNowToolStripMenuItem;
        private System.Windows.Forms.BindingSource cardBindingSource1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        public System.Windows.Forms.Timer resizeColumnTimer;
        public System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton autoResizeToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReaderCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn DirectionAsImage;
        private DataGridViewTimeColumn timeDataGridViewTextBoxColumn;
        private DataGridViewTrackColumn track1DataGridViewTextBoxColumn;
        private DataGridViewTrackColumn track2DataGridViewTextBoxColumn;
        private DataGridViewTrackColumn track3DataGridViewTextBoxColumn;
//        private System.Windows.Forms.DataGridViewTextBoxColumn asStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem recoveryOpenToolStripMenuItem;
        private DataGridViewProgressColumn Rate;
        private System.Windows.Forms.DataGridViewImageColumn DirectionImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Solution;
        private System.Windows.Forms.DataGridViewTextBoxColumn binaryStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

    }
}