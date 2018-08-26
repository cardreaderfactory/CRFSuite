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

namespace CRFSuite
{
    public partial class DeviceInfoDataGrid : UserControl
    {
        private const int NUMBER_OF_ROWS = 5;
        private const int NUMBER_OF_COLUMNS = 2;

        DeviceInfoDataGridRowCollection _rows;

        public DeviceInfoDataGrid()
        {
            InitializeComponent();

            Label[][] rows = new Label[NUMBER_OF_ROWS][];

            for (int i = 0; i < NUMBER_OF_ROWS; i++)
                rows[i] = new Label[NUMBER_OF_COLUMNS];

            rows[0][0] = lblName1;
            rows[0][1] = lblValue1;
            rows[1][0] = lblName2;
            rows[1][1] = lblValue2;
            rows[2][0] = lblName3;
            rows[2][1] = lblValue3;
            rows[3][0] = lblName4;
            rows[3][1] = lblValue4;
            rows[4][0] = lblName5;
            rows[4][1] = lblValue5;

            _rows = new DeviceInfoDataGridRowCollection(rows);
        }

        public override string Text
        {
            get
            {
                return lblGroupText.Text;
            }

            set
            {
                lblGroupText.Text = value;
            }
        }

        public DeviceInfoDataGridRowCollection Rows
        {
            get
            {
                return _rows;
            }
        }

        public DeviceInfoDataRow NewRow()
        {
            return new DeviceInfoDataRow(_rows.GetNextRow(), NUMBER_OF_COLUMNS);
        }
    }

    public class DeviceInfoDataRow
    {
        private Label[] _rowLabels;

        internal DeviceInfoDataRow(Label[] rowLabels, int columns)
        {
            if (columns != rowLabels.Length)
                throw new ArgumentException();

            _rowLabels = rowLabels;
        }

        public string this[int column]
        {
            get
            {
                if (column >= _rowLabels.Length)
                    throw new ArgumentException();

                return _rowLabels[column].Text;
            }

            set
            {
                if (column >= _rowLabels.Length)
                    throw new ArgumentException();

                _rowLabels[column].Text = value;
            }
        }
    }

    public class DeviceInfoDataGridRowCollection
    {
        private Label[][] _rows;
        private List<DeviceInfoDataRow> _dataRows;

        internal DeviceInfoDataGridRowCollection(Label[][] rows)
        {
            _rows = rows;

            _dataRows = new List<DeviceInfoDataRow>();
        }

        internal Label[] GetNextRow()
        {
            if (_dataRows.Count == _rows.Length)
                throw new ArgumentOutOfRangeException();

            return _rows[_dataRows.Count];
        }

        public int Count
        {
            get
            {
                return _dataRows.Count;
            }
        }

        public void Add(DeviceInfoDataRow row)
        {
            if (_dataRows.Count == _rows.Length)
            {
                throw new ArgumentException();
            }

            _rows[_dataRows.Count][0].Text = row[0];
            _rows[_dataRows.Count][1].Text = row[1];

            _rows[_dataRows.Count][0].Visible = true;
            _rows[_dataRows.Count][1].Visible = true;

            _dataRows.Add(row);
        }

        public void RemoveAt(int position)
        {
            if (position >= _dataRows.Count)
            {
                throw new ArgumentException();
            }

            _dataRows.RemoveAt(position);

            _rows[_dataRows.Count][0].Visible = false;
            _rows[_dataRows.Count][1].Visible = false;
        }

        public DeviceInfoDataRow this[int row]
        {
            get
            {
                if (row >= _dataRows.Count)
                    throw new ArgumentException();

                return _dataRows[row];
            }
        }
    }
}
