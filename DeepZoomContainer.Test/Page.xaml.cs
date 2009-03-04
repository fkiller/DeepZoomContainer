using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ROH.Web.Client.Silverlight
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            dzcTest.MultiScaleImage.ImageOpenSucceeded += new RoutedEventHandler(MultiScaleImage_ImageOpenSucceeded);
            txtZoom.KeyDown += new KeyEventHandler(txtZoom_KeyDown);
            txtOX.KeyDown += new KeyEventHandler(txtO_KeyDown);
            txtOY.KeyDown += new KeyEventHandler(txtO_KeyDown);
            txtBoundaryLeft.KeyDown += new KeyEventHandler(txtBoundary_KeyDown);
            txtBoundaryTop.KeyDown += new KeyEventHandler(txtBoundary_KeyDown);
            txtBoundaryRight.KeyDown += new KeyEventHandler(txtBoundary_KeyDown);
            txtBoundaryBottom.KeyDown += new KeyEventHandler(txtBoundary_KeyDown);
            chkLayer1Visible.Click += new RoutedEventHandler(chkBox_Click);
            chkMouseDrag.Click += new RoutedEventHandler(chkBox_Click);
            chkUseSpring.Click += new RoutedEventHandler(chkBox_Click);
            chkWheelZoom.Click += new RoutedEventHandler(chkBox_Click);
            chkUseScrollLimit.Checked += new RoutedEventHandler(chkUseScrollLimit_Checked);
        }

        void chkUseScrollLimit_Checked(object sender, RoutedEventArgs e)
        {
            dzcTest.ScrollLimit = null;
        }

        void MultiScaleImage_ImageOpenSucceeded(object sender, RoutedEventArgs e)
        {
            Random rndValue = new Random();
            for (int i = 0; i < 20; i++)
            {
                Cars carTest = new Cars(rndValue.NextDouble() * 360.0, rndValue.NextDouble() * 60.0, rndValue.NextDouble()*0.03 + 0.15);
                dzcTest.Add(rndValue.NextDouble() * 0.1 + 0.1, rndValue.NextDouble() * 0.1 + 0.3, carTest);
            }
            dzcTest.AddPin(0.4, 0.5, Colors.DarkGray, 'D').Scale = 1.7;
            dzcTest.AddPin(0.1, 0.8, Colors.Blue, 'A').Scale = 0.7;
            dzcTest.AddPin(0.2, 0.7, Colors.Brown, 'B').Scale = 0.7;
            dzcTest.AddPin(0.3, 0.6, Colors.Cyan, 'C').Scale = 0.7;
            dzcTest.AddPin(0.5, 0.3, Colors.Gray, 'E').Scale = 0.7;
            dzcTest.AddPin(0.6, 0.2, Colors.Green, 'F').Scale = 0.7;
            dzcTest.AddPin(0.2, 0.4, Colors.Green, ' ').Scale = 0.7;
            dzcTest.AddPin(0.1, 0.3, Colors.Green, char.MinValue).Scale = 0.7;
            dzcTest.AddPin(0.7, 0.4, Colors.Green, char.MinValue).Scale = 0.7;
            dzcTest.AddPin(0.4, 0.6, Colors.Green, char.MinValue).Scale = 0.7;
            dzcTest.AddPin(0.8, 0.7, Colors.Green, ' ').Scale = 3.7;
            dzcTest.Add(0.0, 0.0, new ROH.Web.Client.Silverlight.Clouds());
            dzcTest.Add(0.0, 0.0, new Flock());
            dzcTest.ScrollLimit = new Thickness(Convert.ToDouble(txtBoundaryLeft.Text),
                    Convert.ToDouble(txtBoundaryTop.Text),
                    Convert.ToDouble(txtBoundaryRight.Text),
                    Convert.ToDouble(txtBoundaryBottom.Text));
            dzcTest.MultiScaleImage.ViewportWidth = 0.1;
            dzcTest.MultiScaleImage.ViewportOrigin = new Point(0.45, 0.2);
        }

        void txtZoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dzcTest.MultiScaleImage.ViewportWidth = Convert.ToDouble(txtZoom.Text);
            }
        }

        void txtO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dzcTest.MultiScaleImage.ViewportOrigin = new Point(Convert.ToDouble(txtOX.Text), Convert.ToDouble(txtOY.Text));
            }
        }

        void chkBox_Click(object sender, RoutedEventArgs e)
        {
            if (chkLayer1Visible.IsChecked.Value)
                dzcTest.LayerVisibility = Visibility.Visible;
            else
                dzcTest.LayerVisibility = Visibility.Collapsed;

            dzcTest.MouseDrag = chkMouseDrag.IsChecked.Value;
            dzcTest.WheelZoom = chkWheelZoom.IsChecked.Value;
            dzcTest.MultiScaleImage.UseSprings = chkUseSpring.IsChecked.Value;
        }

        void txtBoundary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                dzcTest.ScrollLimit = new Thickness(Convert.ToDouble(txtBoundaryLeft.Text),
                    Convert.ToDouble(txtBoundaryTop.Text),
                    Convert.ToDouble(txtBoundaryRight.Text),
                    Convert.ToDouble(txtBoundaryBottom.Text));
                chkUseScrollLimit.IsChecked = false;
            }
        }
    }
}
