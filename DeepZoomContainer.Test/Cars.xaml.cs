using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using PathAnimation;

namespace ROH.Web.Client.Silverlight
{
	public partial class Cars : UserControl
	{
        DoubleAnimationUsingPath animX;
        DoubleAnimationUsingPath animY;
        DoubleAnimationUsingPath animAngle;
        Double Angle;
        Double Delay;
        Double Scale;

		public Cars(Double angle, Double delay, Double scale)
		{
			// Required to initialize variables
			InitializeComponent();
            this.Angle = angle;
            this.Delay = delay;
            this.Scale = scale;
            this.Loaded += new RoutedEventHandler(Cars_Loaded);
		}

        void Cars_Loaded(object sender, RoutedEventArgs e)
        {
            TimeSpan begin = TimeSpan.FromSeconds(-1*this.Delay);
            TimeSpan dur = TimeSpan.FromSeconds(60);
            String pathGeo = this.Resources["PathGeometry"] as String;
            StringToPathGeometryConverter converter = new StringToPathGeometryConverter();
            PathGeometry selectedGeo = converter.Convert(pathGeo);
            
            TransformGroup transFinal = new TransformGroup();
            transFinal.Children.Add(new RotateTransform() { CenterX = this.Width / 2, CenterY = this.Height / 2, Angle = this.Angle });
            transFinal.Children.Add(new ScaleTransform() { CenterX = this.Width / 2, CenterY = this.Height / 2, ScaleX = this.Scale, ScaleY = this.Scale });
            selectedGeo.Transform = transFinal;

            if (animX != null)
                animX.Stop();

            animX = new DoubleAnimationUsingPath();
            animX.BeginTime = begin;
            animX.Duration = dur;
            animX.PathGeometry = selectedGeo;
            animX.Source = PathAnimationSource.X;
            animX.Target = imgCar;
            animX.TargetProperty = new PropertyPath("(Canvas.Left)");
            animX.Tolerance = 1;

            if (animY != null)
                animY.Stop();

            animY = new DoubleAnimationUsingPath();
            animY.BeginTime = begin;
            animY.Duration = dur;
            animY.PathGeometry = selectedGeo;
            animY.Source = PathAnimationSource.Y;
            animY.Target = imgCar;
            animY.TargetProperty = new PropertyPath("(Canvas.Top)");
            animY.Tolerance = 1;

            // Added by Won Dong
            if (animAngle != null)
                animAngle.Stop();

            animAngle = new DoubleAnimationUsingPath();
            animAngle.BeginTime = begin;
            animAngle.Duration = dur;
            animAngle.PathGeometry = selectedGeo;
            animAngle.Source = PathAnimationSource.Angle;
            animAngle.Target = imgCar;
            animAngle.TargetProperty = new PropertyPath("(Canvas.RenderTransform).(RotateTransform.Angle)");
            animAngle.Tolerance = 1;

            animX.RepeatBehavior = RepeatBehavior.Forever;
            animY.RepeatBehavior = RepeatBehavior.Forever;
            animAngle.RepeatBehavior = RepeatBehavior.Forever;

            animX.Begin();
            animY.Begin();
            animAngle.Begin();
        }
	}
}