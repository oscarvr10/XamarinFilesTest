using System;
using Xamarin.Forms;

namespace XamarinFilesTest.Views.CustomControls
{
	public class RadialProgressBar : View
	{
		public static readonly BindableProperty IndeterminateProperty =
			BindableProperty.Create(
				nameof(Indeterminate),
				typeof(bool),
				typeof(RadialProgressBar),
				default(bool));

		public bool Indeterminate
		{
			get { return (bool)GetValue(IndeterminateProperty); }
			set { SetValue(IndeterminateProperty, value); }
		}


		public static readonly BindableProperty ProgressProperty =
			BindableProperty.Create(nameof(Progress),
				typeof(int),
				typeof(RadialProgressBar),
				0);

		/// <summary>
		/// Gets or sets the current progress
		/// </summary>
		/// <value>The progress.</value>
		public int Progress
		{
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}

		public static readonly BindableProperty MaxProperty =
			BindableProperty.Create(nameof(Max),
				typeof(int),
				typeof(RadialProgressBar),
				100);

		/// <summary>
		/// Gets or sets the max value
		/// </summary>
		/// <value>The max.</value>
		public float Max
		{
			get { return (int)GetValue(MaxProperty); }
			set { SetValue(MaxProperty, value); }
		}


		public static readonly BindableProperty ProgressBackgroundColorProperty =
			BindableProperty.Create(nameof(ProgressBackgroundColor),
				typeof(Color),
				typeof(RadialProgressBar),
				Color.White);

		/// <summary>
		/// Gets or sets the ProgressBackgroundColorProperty
		/// </summary>
		/// <value>The color of the ProgressBackgroundColorProperty.</value>
		public Color ProgressBackgroundColor
		{
			get { return (Color)GetValue(ProgressBackgroundColorProperty); }
			set { SetValue(ProgressBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty ProgressColorProperty =
			BindableProperty.Create(nameof(ProgressColor),
				typeof(Color),
				typeof(RadialProgressBar),
				Color.Red);

		/// <summary>
		/// Gets or sets the progress color
		/// </summary>
		/// <value>The color of the progress.</value>
		public Color ProgressColor
		{
			get { return (Color)GetValue(ProgressColorProperty); }
			set { SetValue(ProgressColorProperty, value); }
		}


		public static readonly BindableProperty IndeterminateSpeedProperty =
			BindableProperty.Create(nameof(IndeterminateSpeed),
				typeof(int),
				typeof(RadialProgressBar),
				100);

		public int IndeterminateSpeed
		{
			get { return (int)GetValue(IndeterminateSpeedProperty); }
			set { SetValue(IndeterminateSpeedProperty, value); }
		}
	}
}
