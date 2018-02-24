using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectionConverterPlugin.Forms
{
    public partial class InputAxisDataForm : Form
    {
        string startText;

        double first_double;
        double second_double;
        double third_double;

        public InputAxisDataForm()
        {
            InitializeComponent();
            btn_ok.Enabled = false;
            startText = maskedtb_main.Text;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            ParseText(maskedtb_main.Text);
            firstDouble.Text = first_double.ToString();
            secondDouble.Text = second_double.ToString();
        }

        private void maskedtb_main_TextChanged(object sender, EventArgs e)
        {
            if (startText==maskedtb_main.Text)
                btn_ok.Enabled = false;
            else
                btn_ok.Enabled = true;
        }

        private void ParseText(string inPutText)
        {

            int index = inPutText.IndexOf('+');
            string firstNumber = inPutText.Substring(3, index-3);
            string secondNumber = inPutText.Substring(index+1, 5);

            first_double = ConvertStringToDouble(firstNumber);
            second_double = ConvertStringToDouble(secondNumber);
        }

        private double ConvertStringToDouble(string stringNumber)
        {
            return Convert.ToDouble(stringNumber);
        }
    }
}
