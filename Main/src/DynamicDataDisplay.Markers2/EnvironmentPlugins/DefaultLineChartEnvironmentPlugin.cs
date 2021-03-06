﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicDataDisplay.Markers.DataSources;
using System.Windows;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DynamicDataDisplay.Markers2
{
	/// <summary>
	/// Represents EnvironmentPlugin which enlarges Visible and Output rectangles in specified amount of times, default is 2.
	/// </summary>
	public sealed class DefaultLineChartEnvironmentPlugin : EnvironmentPlugin
	{
		private readonly double enlargeFactor = 2.0;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultLineChartEnvironmentPlugin"/> class with default enlargeFactor = 2.
		/// </summary>
		public DefaultLineChartEnvironmentPlugin() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultLineChartEnvironmentPlugin"/> class with the specified enlargeFactor.
		/// </summary>
		/// <param name="enlargeFactor">The enlarge factor.</param>
		public DefaultLineChartEnvironmentPlugin(double enlargeFactor)
		{
			Contract.Assert(!Double.IsNaN(enlargeFactor));
			Contract.Assert(enlargeFactor > 0);

			this.enlargeFactor = enlargeFactor;
		}

		/// <summary>
		/// Creates the environment.
		/// </summary>
		/// <param name="viewport">The viewport.</param>
		/// <returns></returns>
		public override DataSourceEnvironment CreateEnvironment(Viewport2D viewport)
		{
			Rect bigOutput = viewport.Output.ZoomOutFromCenter(enlargeFactor);
			DataRect bigVisible = viewport.Visible.ZoomOutFromCenter(enlargeFactor);
			CoordinateTransform transform = CoordinateTransform.FromRects(bigVisible, bigOutput);

			return new DataSourceEnvironment
			{
				Visible = bigVisible,
				RealVisible = viewport.Visible,
				Output = bigOutput,
				Transform = transform
			};
		}
	}
}
