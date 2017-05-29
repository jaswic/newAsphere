using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace newAsphere
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Reset();
        }

        private void Reset()
        {
            txtRadius.Text = string.Empty;
            txtDiameter.Text = string.Empty;
            txtConic.Text = string.Empty;
            txtA1.Text = "0";
            txtA2.Text = "0";
            txtA3.Text = "0";
            txtA4.Text = "0";
            txtA5.Text = "0";
            txtA6.Text = "0";
            txtA7.Text = "0";
            txtA8.Text = "0";
            txtA9.Text = "0";
            txtA10.Text = "0";
            txtA11.Text = "0";
            txtA12.Text = "0";
            txtA13.Text = "0";
            txtA14.Text = "0";
            txtA15.Text = "0";
            txtA16.Text = "0";
            txtA17.Text = "0";
            txtA18.Text = "0";
            txtA19.Text = "0";
            txtA20.Text = "0";

            if (radEven.IsChecked == true)
            {
                txtA1.IsEnabled = false;
                txtA3.IsEnabled = false;
                txtA5.IsEnabled = false;
                txtA7.IsEnabled = false;
                txtA9.IsEnabled = false;
                txtA11.IsEnabled = false;
                txtA13.IsEnabled = false;
                txtA15.IsEnabled = false;
                txtA17.IsEnabled = false;
                txtA19.IsEnabled = false;
            }

            startValue.Text = "0";
            endValue.Text = "0";
            stepValue.Text = "0";

            txtRadius.Focus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void radFull_Checked(object sender, RoutedEventArgs e)
        {
            txtA1.IsEnabled = true;
            txtA3.IsEnabled = true;
            txtA5.IsEnabled = true;
            txtA7.IsEnabled = true;
            txtA9.IsEnabled = true;
            txtA11.IsEnabled = true;
            txtA13.IsEnabled = true;
            txtA15.IsEnabled = true;
            txtA17.IsEnabled = true;
            txtA19.IsEnabled = true;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Asphere lens;
            double[] A = new double[21];
            TextBox textBox = new TextBox();

            //initialize values that are never used
            A[0] = 0;

            //get array of terms
            /*for (int i=1; i<=20; i++)
            {
                textBox.Name = "txtA" + i;
                value = textBox.Text;
                if (value == "")
                {
                    A[i] = 0;
                }
                else
                {
                    A[i] = double.Parse(value);
                }
            }*/
            if (radEven.IsChecked == true)
            {
                A[1] = 0;
                A[3] = 0;
                A[5] = 0;
                A[7] = 0;
                A[9] = 0;
                A[11] = 0;
                A[13] = 0;
                A[15] = 0;
                A[17] = 0;
                A[19] = 0;
            }
            else
            {
                A[1] = double.Parse(txtA1.Text);
                A[3] = double.Parse(txtA3.Text);
                A[5] = double.Parse(txtA5.Text);
                A[7] = double.Parse(txtA7.Text);
                A[9] = double.Parse(txtA9.Text);
                A[11] = double.Parse(txtA11.Text);
                A[13] = double.Parse(txtA13.Text);
                A[15] = double.Parse(txtA15.Text);
                A[17] = double.Parse(txtA17.Text);
                A[19] = double.Parse(txtA19.Text);
            }

            A[2] = double.Parse(txtA2.Text);
            A[4] = double.Parse(txtA4.Text);
            A[6] = double.Parse(txtA6.Text);
            A[8] = double.Parse(txtA8.Text);
            A[10] = double.Parse(txtA10.Text);
            A[12] = double.Parse(txtA12.Text);
            A[14] = double.Parse(txtA14.Text);
            A[16] = double.Parse(txtA16.Text);
            A[18] = double.Parse(txtA18.Text);
            A[20] = double.Parse(txtA20.Text);

            lens = new Asphere(double.Parse(txtRadius.Text), double.Parse(txtConic.Text), A, double.Parse(txtDiameter.Text));

            ResultWindow results = new ResultWindow();
            results.Show();

            results.setToolRadius(lens.MaxTool);
            results.setBFS(lens.BFS);
            results.displaySagTable(lens, double.Parse(startValue.Text), double.Parse(endValue.Text), double.Parse(stepValue.Text));
        }
        private void txtDiameter_LostFocus(object sender, RoutedEventArgs e)
        {
            double diameter;
            diameter = double.Parse(txtDiameter.Text);

            endValue.Text = (diameter / 2).ToString();

            if (diameter <= 5)
            {
                stepValue.Text = "0.1";
            }
            else
            {
                stepValue.Text = "0.5";
            }
        }
    }
}
