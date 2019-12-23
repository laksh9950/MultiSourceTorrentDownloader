﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MultiSourceTorrentDownloader.Behaviors
{
    public class PersistanceBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += WindowLoaded;
            AssociatedObject.Closing += WindowClosing;
        }


        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= WindowLoaded;
            AssociatedObject.Closing -= WindowClosing;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (SettingsExist())
            {
                AssociatedObject.SizeToContent = SizeToContent.Manual;
                AssociatedObject.Width = Properties.Settings.Default.WindowWidth;
                AssociatedObject.Height = Properties.Settings.Default.WindowHeight;
            }
            else
                AssociatedObject.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private bool SettingsExist() => Properties.Settings.Default.WindowWidth != 0 && Properties.Settings.Default.WindowHeight != 0;

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowWidth = AssociatedObject.Width;
            Properties.Settings.Default.WindowHeight = AssociatedObject.Height;

            Properties.Settings.Default.Save();
        }
    }
}