﻿using System;
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
	public partial class Clouds : UserControl
	{
		public Clouds()
		{
			// Required to initialize variables
			InitializeComponent();
            CloudsAnimation.Begin();
		}
	}
}