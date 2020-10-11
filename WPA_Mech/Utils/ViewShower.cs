using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPA_Mech.Controls;

namespace WPA_Mech.Utils
{
    public class ViewShower
    {
        public static void Show(int p_viewIndex, object p_dataContext, bool p_isModal, Action<bool?> p_closeAction)
        {
            UserControl control = null;
            switch (p_viewIndex)
            {
                case 0:
                    control = new RedactNoWorkList();
                    break;
                case 1:
                    control = new NoWorkList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("p_viewIndex", "Такого индекса View не существует");
            }
            if (control != null)
            {
                Window wnd = new Window();
                wnd.SizeToContent = SizeToContent.WidthAndHeight;
                control.DataContext = p_dataContext;
                StackPanel sp = new StackPanel();
                sp.Children.Add(control);
                Button applyButton = new Button();
                // applyButton.Content = "Внести изменения";
                applyButton.Click += (s, e) => { if (p_isModal) wnd.DialogResult = true; else wnd.Close(); };
                StackPanel buttonPanel = new StackPanel();
                buttonPanel.Orientation = Orientation.Horizontal;
                buttonPanel.Children.Add(applyButton);
                sp.Children.Add(buttonPanel);
                wnd.Content = sp;
                wnd.Closed += (s, e) => p_closeAction(wnd.DialogResult);
                if (p_isModal)
                {
                    Button cancelButton = new Button();
                    // cancelButton.Content = "Отмена";
                    cancelButton.Click += (s, e) => wnd.DialogResult = false;
                    buttonPanel.Children.Add(cancelButton);
                    wnd.ShowDialog();
                }
                else
                {
                    wnd.Show();
                }
            }
        }
    }
}