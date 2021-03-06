﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ElementsTemplate
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private readonly Random rnd = new Random();
		private const int count = 300;
		private readonly ObservableCollection<DataLine> data = new ObservableCollection<DataLine>();
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			AddData();
			AddData();

			DataContext = data;
		}

		private void AddData()
		{
			double phase = rnd.NextDouble();
			var newData = Enumerable.Range(0, count).Select(i => new Point(i, phase * Math.Sin(phase + i / 10.0))).ToList();


			data.Add(new DataLine(newData));
		}

		private void RemoveData()
		{
			if (data.Count == 0)
				return;

			data.RemoveAt(0);
		}

		private void addButton_Click(object sender, RoutedEventArgs e)
		{
			AddData();
		}

		private void removeButton_Click(object sender, RoutedEventArgs e)
		{
			RemoveData();
		}

	}
}
