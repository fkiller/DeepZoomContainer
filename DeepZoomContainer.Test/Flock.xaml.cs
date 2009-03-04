using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ROH.Web.Client.Silverlight
{
	public partial class Flock : UserControl
	{
		public Flock()
		{
			// Required to initialize variables
			InitializeComponent();
            Loaded += new RoutedEventHandler(Flock_Loaded);
		}

        void Flock_Loaded(object sender, RoutedEventArgs e)
        {
            FlockAnimation.Begin();
        }
	}
}