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
using System.Windows.Shapes;

namespace newAsphere
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        static int sagBoxNumber=0;
        static Asphere thisLens;

        public ResultWindow()
        {
            InitializeComponent();
        }

        public void setBFS(double BFS)
        {
            //this.txtBFS.ContentStringFormat=
            this.txtBFS.Content = BFS.ToString();
        }

        public void setToolRadius(double maxtool)
        {
            this.txtMaxToolRadius.Content = maxtool.ToString();
        }

        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void displaySagTable(Asphere lens, double start, double end, double step)
        {
            double x = start;
            thisLens = lens;
            do
            {
                TextBox newTextBox1 = new TextBox()
                { Width = 50, Height=25 };
                TextBox newTextBox2 = new TextBox()
                { Width = 100, Height=25 };
                StackPanel newStackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Name="stackPanel"+sagBoxNumber.ToString() };

                newTextBox1.Text = x.ToString();
                newTextBox2.Text = (lens.getSag(x)).ToString();
                newStackPanel.Children.Add(newTextBox1);
                newStackPanel.Children.Add(newTextBox2);
                sagTableStackPanel.Children.Add(newStackPanel);
                x += step;
            } while (x <= end);

            this.displayLastRow();
            
        }

        private void displayLastRow()
        {
            TextBox nextPosition = new TextBox()
            { Width = 50, Height = 25, Name = "nextPosition" + sagBoxNumber.ToString() };
            nextPosition.AddHandler(TextBox.LostFocusEvent, new RoutedEventHandler(nextSagValue));
            TextBox nextSag = new TextBox()
            { Width = 100, Height = 25, Name = "nextSag" + sagBoxNumber.ToString() };

            StackPanel nextStackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            nextStackPanel.Children.Add(nextPosition);
            nextStackPanel.Children.Add(nextSag);
            sagTableStackPanel.Children.Add(nextStackPanel);
        }

        private void nextSagValue(object sender, RoutedEventArgs e)
        {
            double x;
            x=double.Parse((sender as TextBox).Text);

            MessageBox.Show((sender as TextBox).Name);
            string controlName;
            controlName = "nextSag" + sagBoxNumber.ToString();

            IInputElement focusedBox = FocusManager.GetFocusedElement(sagTableStackPanel);

            if (focusedBox == null)
            {
                MessageBox.Show("Null");
            }
        }
    }
}
