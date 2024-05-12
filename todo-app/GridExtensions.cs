namespace todo_app
{
    using System.Windows;
    using System.Windows.Controls;

    public class GridExtensions
    {
        /// <summary>
        /// Columns are resized to proportionally fill horizontal blank space.
        /// It is applied only on columns with the Width property set to "Auto".
        /// Minimum width of columns is defined by their content.
        /// </summary>
        public static readonly DependencyProperty HorizontalPropFillOfBlankSpaceProperty =
            DependencyProperty.RegisterAttached("HorizontalPropFillOfBlankSpace", typeof(bool), typeof(GridExtensions),
                new UIPropertyMetadata(false, OnHorizontalPropFillChanged));

        public static bool GetHorizontalPropFillOfBlankSpace(Grid grid)
        {
            return (bool)grid.GetValue(HorizontalPropFillOfBlankSpaceProperty);
        }

        public static void SetHorizontalPropFillOfBlankSpace(Grid grid, bool value)
        {
            grid.SetValue(HorizontalPropFillOfBlankSpaceProperty, value);
        }

        private static void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Grid grid))
                return;

            foreach (var cd in grid.ColumnDefinitions)
            {
                if (cd.Width.IsAuto && cd.ActualWidth != 0d)
                {
                    if (cd.MinWidth == 0d)
                        cd.MinWidth = cd.ActualWidth;
                    cd.Width = new GridLength(1d, GridUnitType.Star);
                }
            }
        }

        private static void OnHorizontalPropFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Grid grid))
                return;

            if ((bool)e.NewValue)
                grid.Loaded += Grid_Loaded;
            else
                grid.Loaded -= Grid_Loaded;
        }
    }
}