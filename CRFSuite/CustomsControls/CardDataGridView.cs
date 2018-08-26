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
using System.Data;
using System.ComponentModel;
using System.Drawing;
using crf.Presentation;
using crf.Algorithm;

namespace crf.CustomsControls
{
    public class CardDataGridView : System.Windows.Forms.DataGridView
    {
        //filter applied
        private string _filter = string.Empty;

        private ToolStripStatusLabel _status = null;

        //all cards in document. It is null when filter is null, so dataSource has all cards.
        //When filter is not null DataSource has filtered cards and _filteredCards has all
        //cards loaded from document.
        private BindingList<ICard> _filteredCards = null;

        //* Status strip where to write number of cards in document. */
        public ToolStripStatusLabel StatusStrip { set { _status = value; } }
#if DEBUG
        public static long RESIZECOLUMNCOUNT = 0;
#endif
        /**
         * First row with column numbers.
         */
        public CardDataGridView()
        {
        }

        private int GetMaxWidth(List<string> list, System.Drawing.Font font)
        {
            int maxWidth = 0;
            for (int i = 0; i < list.Count; i++)
            {
                int width = TextRenderer.MeasureText(list[i], font).Width;
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }
            return maxWidth;
        }
        private void ShowCardsInDocument()
        {
            if (_status != null)
            {
                IList<Card> cards = GetCardsFromGrid();
                if (cards != null)
                    _status.Text = cards.Count + " card" + ((cards.Count != 1) ? "s " : " ") + " in document.";
                else
                    _status.Text = string.Empty;
            }
        }

        public string Filter
        {
            get { return _filter; }

            set
            {
                CardBindingSource iCardBindingSource = (CardBindingSource)DataSource;

                _filter = (value != null) ? value : string.Empty;

                if (_filter.Length == 0)
                {
                    if (_filteredCards != null)
                    {
                        ListCardUtil.SetGroupCards(_filteredCards, 0);
                        ListCardUtil.SetAlignment(_filteredCards, 0);

                        iCardBindingSource.DataSource = _filteredCards;
                        _filteredCards = null;
                    }
                }
                else
                {

                    if (_filteredCards == null)
                        _filteredCards = (BindingList<ICard>)iCardBindingSource.DataSource;

                    BindingList<ICard> newFilter = new BindingList<ICard>();
                    newFilter.AllowNew = true;
                    for (int i = 0; i < _filteredCards.Count; i++)
                    {
                        Card card = (Card)_filteredCards[i];
                        if (card.Contains(_filter))
                            newFilter.Add(card);
                    }

                    ListCardUtil.SetGroupCards(newFilter, 0);
                    ListCardUtil.SetAlignment(newFilter, 0);

                    iCardBindingSource.DataSource = newFilter;
                }

                ShowCardsInDocument();
            }
        }

        public void RemoveFilter()
        {
            Filter = string.Empty;
        }

        public int CellRowIndex(DataGridViewCell cell)
        {
            return cell.RowIndex;
        }


        /**
         * Returns all selected cells. Cells in first row is not included if first row
         * is used as columns numbers.
         */
        public new IList<DataGridViewCell> SelectedCells
        {
            get
            {
                DataGridViewSelectedCellCollection selCels = base.SelectedCells;
                IList<DataGridViewCell> selCelsList = new List<DataGridViewCell>();

                foreach (DataGridViewCell cell in selCels)
                {
                    selCelsList.Add(cell);
                }

                return selCelsList;
            }
        }

        /**
         * Returns first selected cell index row or -1 if no cell is selected.
         */
        public bool GetFirstSelectedRow(out int firstSelectedRow)
        {
            firstSelectedRow = -1;

            if (SelectedCells.Count == 0)
                return false;
            else
            {
                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (firstSelectedRow == -1)
                        firstSelectedRow = CellRowIndex(cell);
                    else if (firstSelectedRow > CellRowIndex(cell))
                        firstSelectedRow = CellRowIndex(cell);
                }

                return true;
            }
        }

        public void MergeSelectedCards()
        {
            if (SelectedCells.Count <= 1)
                return;

            CardBindingSource iCardBindingSource = (CardBindingSource)this.DataSource;
            if (iCardBindingSource == null)
                return;

            //return all cards but the first one that is the column numbers.
            BindingList<ICard> bindingCards = iCardBindingSource.DataSource as BindingList<ICard>;
            if (bindingCards == null)
                return;

            if (base.SelectedCells == null)
                return;

            DataGridViewSelectedCellCollection selCels = base.SelectedCells;

            Card c = null;
            Card c2 = null;

//            bindingCards.

            foreach (DataGridViewCell cell in base.SelectedCells)
            {
                //int idx = -1;
                //if (cell.OwningRow != null && 
                //    cell.OwningRow.Cells.Count > 0 && 
                //    Int32.TryParse(cell.OwningRow.Cells[0].Value.ToString(), out idx))
                //{
                //    if (idx >= 0 && idx < bindingCards.Count)
                //    {
                //        if (c == null)
                //            c = (Card)bindingCards[idx];
                //        else
                //            c2 = (Card)bindingCards[idx];
                //    }
                //}

                if (c == null)
                    c = (Card)bindingCards[cell.RowIndex];
                else
                    c2 = (Card)bindingCards[cell.RowIndex];

                if (c != null && c2 != null)
                {
                    c.merge(c2);
                    bindingCards.Remove(c2);
                    c2 = null;
                }
            }
 

        }


        public bool GetSelectedCards(out List<Card> selectedCards)
        {
            if (SelectedCells.Count == 0)
            {
                selectedCards = null;
                return false;
            }
            else
            {
                //get all selected rows
                List<int> indexes = new List<int>();

                foreach (DataGridViewCell cell in SelectedCells)
                {
                    if (!indexes.Contains(CellRowIndex(cell)))
                        indexes.Add(CellRowIndex(cell));
                }

                //sort list by index
                indexes.Sort();

                //copy selected rows cards to a new array
                selectedCards = new List<Card>(indexes.Count);
                IList<Card> cards = GetCardsFromGrid();
                for (int i = 0; i < indexes.Count; i++)
                {
                    selectedCards.Add(cards[indexes[i]]);
                }

                return true;
            }
        }

        public void DeleteSelectedCards()
        {
            //delete selected objects
        }

        public void DeleteCards(List<Card> cardsToDelete)
        {
            IList<ICard> cards = GetActualCardsFromGrid();

            if (cards != null)
            {
                foreach (Card card in cardsToDelete)
                {
                    //int index = cards.IndexOf(card);

                    //if (index >= 0)
                    cards.Remove(card);
                        //this.Rows.RemoveAt(index);

                    if (_filteredCards != null)
                        _filteredCards.Remove(card);
                }

                ListCardUtil.SetGroupCards(cards, 0);
                ListCardUtil.SetAlignment(cards, 0);

                ShowCardsInDocument();
            }
        }

        public void AddCard(int index, Card card)
        {
            BindingList<ICard> cards = GetActualCardsFromGrid();

            if (index < 0)
                index = 0;

            if ((cards != null) && (card != null))
            {
                //do we need to insert it on filtered cards?
                if (_filteredCards != null)
                {
                    _filteredCards.Insert(_filteredCards.IndexOf(cards[index]), card);
                }

                cards.Insert(index, card);

                ListCardUtil.SetGroupCards(cards, 0);
                ListCardUtil.SetAlignment(cards, 0);

                ShowCardsInDocument();
            }
        }

        public void AddCards(int index, IList<Card> cardsToInsert)
        {
            BindingList<ICard> cards = GetActualCardsFromGrid();

            if ((cards != null) && (cardsToInsert.Count >= 0))
            {
                //do we need to insert it on filtered cards?
                if (_filteredCards != null)
                {
                    int indexAux = _filteredCards.IndexOf(cards[index]);
                    foreach (Card card in cardsToInsert)
                    {
                        _filteredCards.Insert(indexAux++, card);
                    }
                }

                foreach (Card card in cardsToInsert)
                {
                    cards.Insert(index++, card);
                }

                ListCardUtil.SetGroupCards(cards, 0);
                ListCardUtil.SetAlignment(cards, 0);

                ShowCardsInDocument();
            }
        }

        public BindingList<ICard> GetActualCardsFromGrid()
        {
            CardBindingSource iCardBindingSource = (CardBindingSource)this.DataSource;
            BindingList<ICard> bindingCards = iCardBindingSource.DataSource as BindingList<ICard>;
            if (bindingCards == null)
                return null;

            return bindingCards;
        }

        public IList<Card> GetCardsFromGrid()
        {
            CardBindingSource iCardBindingSource = (CardBindingSource)this.DataSource;
            if (iCardBindingSource == null)
                return null;

            //return all cards but the first one that is the column numbers.
            BindingList<ICard> bindingCards = iCardBindingSource.DataSource as BindingList<ICard>;
            if (bindingCards == null)
                return null;

            List<Card> cards = new List<Card>();
            for (int i = 0; i < bindingCards.Count; i++)
                cards.Add((Card)bindingCards[i]);

            return cards;
        }

        public void SetCardsInGrid(IList<Card> cards)
        {
            CardBindingSource iCardBindingSource = (CardBindingSource)this.DataSource;
            BindingList<ICard> newCards = null;
            if (cards != null)
            {
                newCards = new BindingList<ICard>();
                foreach (Card card in cards)
                {
                    newCards.Add(card);
                }
                newCards.AllowNew = true;

                //ListCardUtil.SetGroupCards(newCards, 0);
                //ListCardUtil.SetAlignment(newCards, 0);
            }

            iCardBindingSource.DataSource = newCards;
            ShowCardsInDocument();
        }

        public Card GetCard(int rowIndex)
        {
            BindingList<ICard> _cards = GetActualCardsFromGrid();
            Card card = null;

            if (_cards != null)
                card = (Card)_cards[rowIndex];

            return card;
        }

        public Track GetTrack(int rowIndex, int columnIndex)
        {
            Track group = null;

            Card card = GetCard(rowIndex);

            if (card != null)
                group = card.GetTrack(columnIndex - (int)Presentation.cardGridColumn.TRACK1_COLUMN);

            return group;
        }

        public Variant GetVariant(int rowIndex, int columnIndex)
        {
            Variant variant = null;

            Track group = GetTrack(rowIndex, columnIndex);

            if (null != group)
                variant = group.GetPreferredVariant();

            return variant;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
#if DEBUG
        public void UpdateStatusStripTextWhenResizing()
        {
//            string txt = "Resizing a column.. (Columns resized: " + RESIZECOLUMNCOUNT + ", Cells read: " + Variant.AsStringCount + ")";
//            this._status.Text = txt;
        }
#endif
    }

    public class CardBindingSource : BindingSource
    {
        public CardBindingSource(IContainer container)
            : base(container)
        {
        }

        public CardBindingSource(object dataSource, string dataMember)
            : base(dataSource, dataMember)
        {
        }

        /*
        private List<Card> _cards = null;

        public new object DataSource 
        { 
            get 
            {
                DataTable table = base.DataSource as DataTable;
                List<Card> cards = new List<Card>();

                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] values = row.ItemArray;
                        cards.Add((Card)values[values.Length - 1]);
                    }
                }

                return cards;
            } 
            
            set 
            {
                if (value is List<Card>)
                {
                    _cards = (List<Card>)value;
                    base.DataSource = CardBindingSource.ToDataTable(_cards);
                }
                else
                    base.DataSource = value;
            }
        }

        protected static DataTable ToDataTable(List<Card> cards)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ICard));

            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            table.Columns.Add("CardPointer", typeof(ICard));

            object[] values = new object[props.Count + 1];
            foreach (Card card in cards)
            {
                for (int i = 0; i < (values.Length - 1); i++)
                {
                    values[i] = props[i].GetValue(card);
                }
                values[values.Length - 1] = card;

                table.Rows.Add(values);
            }

            return table;
        }
        */
    }
}
