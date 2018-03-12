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

namespace SectionConverterPlugin.CustomControls
{
    public partial class RegExedTextBox : UserControl
    {
        Regex _regEx;
        string _value;
        string _valueTextBoxTemp;
        bool _matchOnKeyInput;
        bool _silent;
        bool _reverted;

        public RegExedTextBox()
        {
            this.Enabled = false;

            ValueChanged += (o, e) => { };
            _matchOnKeyInput = false;
            _silent = true;
            _reverted = false;

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
            _reverted = true;

            MatchCollection mcs = _regEx.Matches(tb_TextBox.Text);

            if (mcs.Count == 1 && mcs[0].Length == inputTemp.Length)
            {
                _valueTextBoxTemp = inputTemp;
                _reverted = false;
            }
            else
            {
                if (!_silent)
                {
                    MessageBox.Show("Invalid input: " + inputTemp);
                }

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
                var prevValue = _value;

                _value = value;
                _valueTextBoxTemp = _value;

                tb_TextBox.Text = _value;

                ValueChanged(this, null);
            }
        }

        public bool Silent { get => _silent; set => _silent = value; }
        public bool Reverted { get => _reverted;}

        public event EventHandler ValueChanged;

        public void SelectAll()
        {
            tb_TextBox.SelectAll();
        }
    }
}
