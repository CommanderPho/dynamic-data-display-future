﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DynamicDataDisplay.Markers2;
using System.Windows.Media;

namespace Microsoft.Research.DynamicDataDisplay
{
	/// <summary>
	/// Contains useful extension methods of LineChartBase class.
	/// </summary>
	public static class LineChartExtensions
	{
		/// <summary>
		/// Sets the stroke of line chart.
		/// </summary>
		/// <param name="chart">The chart.</param>
		/// <param name="stroke">The stroke.</param>
		/// <returns></returns>
		public static T WithStroke<T>(this T chart, Brush stroke) where T : LineChartBase
		{
			if (chart == null)
				throw new ArgumentNullException("chart");

			chart.Stroke = stroke;

			return chart;
		}

		/// <summary>
		/// Sets the stroke thickness of line chart.
		/// </summary>
		/// <param name="chart">The chart.</param>
		/// <param name="strokeThickness">The stroke thickness.</param>
		/// <returns></returns>
		public static T WithStrokeThickness<T>(this T chart, double strokeThickness) where T : LineChartBase
		{
			if (chart == null)
				throw new ArgumentNullException("chart");

			chart.StrokeThickness = strokeThickness;

			return chart;
		}

		/// <summary>
		/// Sets the stroke dash array of line chart.
		/// </summary>
		/// <param name="chart">The chart.</param>
		/// <param name="pattern">The pattern.</param>
		/// <returns></returns>
		public static T WithStrokeDashArray<T>(this T chart, IEnumerable<double> pattern) where T : LineChartBase
		{
			if (chart == null)
				throw new ArgumentNullException("chart");

			chart.StrokeDashArray = new DoubleCollection(pattern);

			return chart;
		}
	}
}
