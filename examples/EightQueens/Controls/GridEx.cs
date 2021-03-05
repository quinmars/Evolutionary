using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EightQueens.Controls
{
    /// <summary>
    /// Taken from:
    /// https://rachel53461.wordpress.com/2011/09/17/wpf-grids-rowcolumn-count-properties/
    /// </summary>
    public class GridEx
    {
        /// <summary>
        /// Adds the specified number of Rows to RowDefinitions. 
        /// The height can be set by the RowHeight attached property.
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached(
                "RowCount", typeof(int), typeof(GridEx),
                new PropertyMetadata(-1, RowCountChanged));

        // Get
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        // Set
        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        // Change Event - Adds the Rows
        public static void RowCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            var height = GetRowHeight(obj);
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = height });
        }

        /// <summary>
        /// The row height to be used with the row count property.
        /// </summary>
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.RegisterAttached("RowHeight", typeof(GridLength), typeof(GridEx), new PropertyMetadata(new GridLength(1, GridUnitType.Star), RowHeightChanged));

        public static GridLength GetRowHeight(DependencyObject obj)
        {
            return (GridLength)obj.GetValue(RowHeightProperty);
        }

        public static void SetRowHeight(DependencyObject obj, GridLength value)
        {
            obj.SetValue(RowHeightProperty, value);
        }
        
        private static void RowHeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid))
                return;

            var height = (GridLength)e.NewValue;

            Grid grid = (Grid)obj;

            foreach (var row in grid.RowDefinitions)
            {
                row.Height = height;
            }
        }

        /// <summary>
        /// Adds the specified number of Columns to ColumnDefinitions. 
        /// Default Width is Auto
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnCount", typeof(int), typeof(GridEx),
                new PropertyMetadata(-1, ColumnCountChanged));

        // Get
        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        // Set
        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        // Change Event - Add the Columns
        public static void ColumnCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            var width = GetColumnWidth(obj);
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = width });
        }
        
        /// <summary>
        /// The column width to be used with the column count property.
        /// </summary>
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.RegisterAttached("ColumnWidth", typeof(GridLength), typeof(GridEx), new PropertyMetadata(new GridLength(1, GridUnitType.Star), ColumnWidthChanged));

        public static GridLength GetColumnWidth(DependencyObject obj)
        {
            return (GridLength)obj.GetValue(ColumnWidthProperty);
        }

        public static void SetColumnWidth(DependencyObject obj, GridLength value)
        {
            obj.SetValue(ColumnWidthProperty, value);
        }
        
        private static void ColumnWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid))
                return;

            var width = (GridLength)e.NewValue;

            Grid grid = (Grid)obj;

            foreach (var column in grid.ColumnDefinitions)
            {
                column.Width = width;
            }
        }
    }
}
