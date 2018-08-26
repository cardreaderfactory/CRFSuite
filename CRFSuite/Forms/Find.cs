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

namespace crf
{
    public partial class Find : Form
    {
        /**
         * Only 1 find form can be created, so Find form is a singleton. Singleton is needed since it is
         * a modalless form, so user can press find option as many times as he/she wants.
         * Find instance will be created only when find form is on the screen.
         * FindData is defined in a separate singleton because once it is created it will be kept in memory
         * until the application is closed, so find option are remembered after closing a find dialog.
         * Singleton is released once the form is closed.
         */
        private static Find _instance = null;

        /**< parent form. this is the form use as parent and the form where search will be done */
        private ISearchablePresenter _presenter;

        /**< used to know if search is the first or next */
        private bool _firstSearch;

        public static Find Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Find();

                return _instance;
            }
        }

        public new void Show()
        {
            throw new Exception("Error!!! Parent must be specified as this is a modalless dialog so it is always on top of the parent.");
        }

        public new void Show(IWin32Window parent)
        {
            throw new Exception("Error!!! Parent must be a searchable form in order to search on it.");
        }

        /**
         * Parent form can only be a form where a search can be perfom.
         */
        public void Show(ISearchablePresenter presenter)
        {
            if (this.Created && _presenter == presenter)
                Focus();
            else
            {
                this.Visible = false;
                base.Show(presenter.Form());
                _presenter = presenter;
            }

            _firstSearch = true;
        }

        /**
         * Protected constructor. Singleton will build itself
         */
        protected Find()
        {
            InitializeComponent();
        }

        private void Find_Load(object sender, EventArgs e)
        {
            if (FindData.Instance.FindString == string.Empty)
                btnNext.Enabled = false;
            txtFind.Text = FindData.Instance.FindString;
            checkCase.Checked = FindData.Instance.MatchCase;
            radioDown.Checked = FindData.Instance.DirectionDown;
            radioUp.Checked = !FindData.Instance.DirectionDown;
        }

        private void checkCase_CheckedChanged(object sender, EventArgs e)
        {
            FindData.Instance.MatchCase = checkCase.Checked;
        }

        private void radioUp_CheckedChanged(object sender, EventArgs e)
        {
            FindData.Instance.DirectionDown = !radioUp.Checked;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FindData.Instance.FindString = txtFind.Text;

            FindData.Instance.Next = !(_firstSearch || txtFind.Modified);
            _firstSearch = false;
            txtFind.Modified = false;

            _presenter.FindInForm(FindData.Instance);
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = (txtFind.Text.Length != 0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Find_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }
    }

    public interface ISearchablePresenter
    {
        Form Form();
        void FindInForm(FindData findData);
    }

    public class FindData
    {
        /**< string to look for */
        private string _findString;

        /**< true if comparison must be done with match */
        private bool _matchCase;

        /**< true if direction is down. false if direction is up */
        private bool _down;

        /**< true if next item must be found. false if search must start in the selected item. */
        private bool _next;

        /**< singleton instance */
        private static FindData _instance = null;

        /**
         * start searching values. needed to know when we have looked in the whole document.
         */
        private int _startCardRow;
        private int _startCardColumn;
        private int _startVariantRow;

        /**< private constructor so instance can only be created by the instance function */
        private FindData()
        {
            //default search values
            _findString = string.Empty;
            _matchCase = false;
            _down = true;
            _next = false;

            _startCardRow = _startCardColumn = _startVariantRow = -1;
        }

        public static FindData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FindData();

                return _instance;
            }
        }

        public string FindString
        {
            get
            {
                return _findString;
            }

            set
            {
                _findString = value;
                _next = false;
            }
        }

        public bool MatchCase
        {
            get
            {
                return _matchCase;
            }

            set
            {
                _matchCase = value;
            }
        }

        public bool DirectionDown
        {
            get
            {
                return _down;
            }

            set
            {
                _down = value;
            }
        }

        public bool Next
        {
            get
            {
                return _next;
            }

            set
            {
                _next = value;
            }
        }

        public void GetStartPoint(out int startCardRow, out int startCardColumn, out int startVariantRow)
        {
            startCardRow = _startCardRow;
            startCardColumn = _startCardColumn;
            startVariantRow = _startVariantRow;
        }

        public void SetStartPoint(int startCardRow, int startCardColumn, int startVariantRow)
        {
            _startCardRow = startCardRow;
            _startCardColumn = startCardColumn;
            _startVariantRow = startVariantRow;
        }
    }
}
