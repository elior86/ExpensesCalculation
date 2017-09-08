using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MasterInstallerCopy.Behaviors
{
    public class DragBehavior : Behavior<Border>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                Window mainWindow = Application.Current.MainWindow;
                var expender = mainWindow.FindName("expenderBtn");
                if (expender != null && expender is Expander)
                {
                    ((Expander)expender).IsExpanded = false;
                }
                Mouse.OverrideCursor = Cursors.SizeAll;
                mainWindow.DragMove();
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }
    }
}
