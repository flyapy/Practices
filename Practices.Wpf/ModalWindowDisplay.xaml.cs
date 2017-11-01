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
using System.Diagnostics;

namespace Practices.Wpf
{
    /// <summary>
    /// Interaction logic for ModalWindowDisplay.xaml
    /// </summary>
    public partial class ModalWindowDisplay : Window
    {
        public ModalWindowDisplay()
        {
            InitializeComponent();
            this.txtShowModalWindow.KeyUp += TxtShowModalWindow_KeyUp;
            this.btnShowModalWindow.Click += BtnShowModalWindow_Click;
        }

        private void BtnShowModalWindow_Click(object sender, RoutedEventArgs e)
        {
            //new MessageWindow().ShowDialog();
            //ShowMessage("You hit me");
            new SubWindow().Show();
        }

        private void TxtShowModalWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ShowMessage("You hit me!You hit me!You hit me!You hit me!You hit me!You hit me!You hit me!You hit me!You hit me!You hit me!");
                //MessageWindow window = new MessageWindow();
                //window.Owner = this;
                //window.ShowDialog();
            }
        }

        private void ShowMessage(string msg)
        {
            ////MessageBox.Show(msg);
            //MessageWindow window = new MessageWindow(msg);
            ////window.Owner = this;
            //window.Owner = App.Current.MainWindow; //目前的场景有效
            //window.ShowDialog();
            MessageHelper.Show(msg);
        }
    }
}
