using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPA_Mech.Utils
{
    public abstract class DialogWindow : Window
    {


        protected DialogWindow(DialogVmBase viewModel)
        {
            WindowStyle = WindowStyle.ToolWindow;
            SizeToContent = SizeToContent.Height;

            DataContext = viewModel;
            Title = viewModel.WindowTitle;

            viewModel.Closed += ViewModel_Closed;
        }



        private void ViewModel_Closed(object sender, EventArgs eventArgs)
        {
            Close();
        }
    }
}