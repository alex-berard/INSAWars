﻿using System;
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

namespace UI
{
    /// <summary>
    /// Defines a very simple window displaying an error message.
    /// </summary>
    public partial class LoadErrorWindow : Window
    {
        public LoadErrorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
