﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DynamicDataDisplay.Markers.DataSources;
using DynamicDataDisplay.Markers.DataSources.DataSourceFactories;
using System.Diagnostics.Contracts;
using Microsoft.Research.DynamicDataDisplay.Common.Auxiliary;

namespace Microsoft.Research.DynamicDataDisplay.Markers2
{
	/// <summary>
	/// Represents a base class for creating marker or line charts.
	/// </summary>
	public abstract class PointChartBase : DependencyObject, IPlotterElement
	{
		private Plotter2D plotter = null;
		private EnvironmentPlugin environmentPlugin = new DefaultLineChartEnvironmentPlugin();
		private DataRect visibleWhileCreation;
		private Rect outputWhileCreation;
		protected const double rectanglesEps = 0.05;

		/// <summary>
		/// Initializes a new instance of the <see cref="PointChartBase"/> class.
		/// </summary>
		public PointChartBase()
		{
			Viewport2D.SetIsContentBoundsHost(this, true);
		}

		/// <summary>
		/// Gets the visible rectangle while creation of points to draw.
		/// </summary>
		/// <value>The visible while creation.</value>
		protected DataRect VisibleWhileCreation
		{
			get { return visibleWhileCreation; }
		}

		/// <summary>
		/// Gets the output rectangle while creation of points to draw.
		/// </summary>
		/// <value>The output while creation.</value>
		protected Rect OutputWhileCreation
		{
			get { return outputWhileCreation; }
		}

		#region Helpers

		/// <summary>
		/// Gets or sets the environment plugin.
		/// </summary>
		/// <value>The environment plugin.</value>
		public EnvironmentPlugin EnvironmentPlugin
		{
			get { return environmentPlugin; }
			set
			{
				Contract.Assert(value != null);

				environmentPlugin = value;
			}
		}

		/// <summary>
		/// Creates the environment.
		/// </summary>
		/// <returns></returns>
		protected DataSourceEnvironment CreateEnvironment()
		{
			Contract.Assert(plotter != null);

			Viewport2D viewport = plotter.Viewport;
			DataSourceEnvironment result = environmentPlugin.CreateEnvironment(viewport);

			visibleWhileCreation = result.Visible;
			outputWhileCreation = result.Output;

			return result;
		}

		#endregion

		#region ItemsSource

		/// <summary>
		/// Gets or sets the items source. This is a DependencyProperty.
		/// </summary>
		/// <value>The items source.</value>
		public object ItemsSource
		{
			get { return (object)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
		  "ItemsSource",
		  typeof(object),
		  typeof(PointChartBase),
		  new FrameworkPropertyMetadata(null, OnItemsSourceReplaced));

		private static void OnItemsSourceReplaced(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointChartBase owner = (PointChartBase)d;
			owner.OnItemsSourceReplacedCore(e.OldValue, e.NewValue);
		}

		protected virtual void OnItemsSourceReplacedCore(object oldValue, object newValue)
		{
			object itemsSource = newValue;

			if (itemsSource != null)
			{
				var store = DataSourceFactoryStore.Current;
				var dataSource = store.BuildDataSource(itemsSource);

				if (dataSource != null)
				{
					DataSource = dataSource;
				}
				else
				{
					throw new ArgumentException("Cannot create a DataSource of given ItemsSource. Look into a list of DataSource types to determine what data can be passed.");
				}
			}
			else
			{
				DataSource = null;
			}
		}

		#endregion

		#region DataSource

		/// <summary>
		/// Gets or sets the data source. This is a DependencyProperty.
		/// </summary>
		/// <value>The data source.</value>
		public PointDataSourceBase DataSource
		{
			get { return (PointDataSourceBase)GetValue(DataSourceProperty); }
			set { SetValue(DataSourceProperty, value); }
		}

		public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
		  "DataSource",
		  typeof(PointDataSourceBase),
		  typeof(PointChartBase),
		  new FrameworkPropertyMetadata(null, OnDataSourceReplaced));

		private static void OnDataSourceReplaced(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointChartBase owner = (PointChartBase)d;
			owner.OnDataSourceReplaced((PointDataSourceBase)e.OldValue, (PointDataSourceBase)e.NewValue);
		}

		protected virtual void OnDataSourceReplaced(PointDataSourceBase oldDataSource, PointDataSourceBase newDataSource) { }

		#endregion

		#region IPlotterElement Members

		public virtual void OnPlotterAttached(Plotter plotter)
		{
			this.plotter = (Plotter2D)plotter;
			this.plotter.Viewport.PropertyChanged += new EventHandler<ExtendedPropertyChangedEventArgs>(Viewport_PropertyChanged);
		}

		private void Viewport_PropertyChanged(object sender, ExtendedPropertyChangedEventArgs e)
		{
			OnViewportPropertyChanged(e);
		}

		protected virtual void OnViewportPropertyChanged(ExtendedPropertyChangedEventArgs e) { }

		public virtual void OnPlotterDetaching(Plotter plotter)
		{
			this.plotter.Viewport.PropertyChanged -= Viewport_PropertyChanged;
			this.plotter = null;
		}

		Plotter IPlotterElement.Plotter
		{
			get { return plotter; }
		}

		protected Plotter2D Plotter
		{
			get { return plotter; }
		}

		#endregion
	}
}