using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace TaskList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            osvjeziListu();
        }

        private void osvjeziListu()
        {
           Process[] procesi = Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains(filterTextBox.Text.ToUpper())).OrderBy(p => p.ProcessName).ToArray();
           popisProcesaDataGrid.ItemsSource = procesi;

        }

        private void osvjeziButton_Click(object sender, RoutedEventArgs e)
        {
            osvjeziListu();
        }

        private void popisProcesaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process p = popisProcesaDataGrid.SelectedValue as Process;
            PropertyInfo[] props = typeof(Process).GetProperties();

            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                string s;

                try
                {
                    s = prop.GetValue(p).ToString();
                    sb.AppendLine(prop.Name + ": " + s);

                }
                catch //(Exception ex)
                {
                  //  sb.AppendLine("Ne može! " + ex.Message);
                    
                }

            }
            procesTextBlock.Text = sb.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process p = popisProcesaDataGrid.SelectedValue as Process;

            try
            {
                if (!p.HasExited)
                {
                    p.Kill();
                    p = null;
                    osvjeziListu();

                }
            }
            catch
            { }

            osvjeziListu();
        }

        private void PokreniButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                string[] args = noviProcesTextBox.Text.Split(' ');
                if (args.Length > 1)
                {
                    Process.Start(args[0], args[1]);
                }
                    
                
                else
                {
                    Process.Start(args[0]);
                }
                   
                osvjeziListu();

            }

            catch (Exception ex)
            {
                statusLabel.Content = ex.Message;
                statusLabel.Visibility = Visibility.Visible;
            }

        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            osvjeziListu();
        }
    }
}
