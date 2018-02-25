using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CivilToolsGUI.CustomControls
{
    public partial class RegExedTextBox : UserControl
    {
        Regex _regEx;
        string _value;
        string _valueTextBoxTemp;
        bool _matchOnKeyInput;

        public RegExedTextBox()
        {
            this.Enabled = false;

            ValueChanged += (o, e) => { };

            InitializeComponent();
        }
        public void SetRegExp(Regex regEx)
        {
            _regEx = regEx;
            this.Enabled = true;
        }

        public bool MatchOnKeyInput
        {
            get
            {
                return _matchOnKeyInput;
            }
            set
            {
                _matchOnKeyInput = value;
            }
        }

        private void tb_TextBox_TextChanged(object sender, EventArgs e)
        {
            if(_matchOnKeyInput)
            {
                MatchInput();
            }
        }

        private void MatchInput()
        {
            string inputTemp = tb_TextBox.Text;

            MatchCollection mcs = _regEx.Matches(tb_TextBox.Text);

            if (mcs.Count == 1 && mcs[0].Length == inputTemp.Length)
            {
                _valueTextBoxTemp = inputTemp;
            }
            else
            {
                tb_TextBox.Text = _valueTextBoxTemp;
            }
        }

        private void tb_TextBox_Leave(object sender, EventArgs e)
        {
            MatchInput();

            Value = _valueTextBoxTemp;
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _valueTextBoxTemp = _value;

                tb_TextBox.Text = _value;
                ValueChanged(this, null);
            }
        }

        public event EventHandler ValueChanged;
    }
}
