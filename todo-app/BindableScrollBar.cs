﻿namespace todo_app
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    /// <summary>
    /// An extended scrollbar that can be bound to an external scrollviewer.
    /// </summary>
    public class BindableScrollBar : ScrollBar
    {
        // Using a DependencyProperty as the backing store for BoundScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundScrollViewerProperty =
            DependencyProperty.Register("BoundScrollViewer", typeof(ScrollViewer), typeof(BindableScrollBar),
                new FrameworkPropertyMetadata(null, OnBoundScrollViewerPropertyChanged));


        /// <summary>
        /// Initializes a new instance of the <see cref="BindableScrollBar" /> class.
        /// </summary>
        /// <param name="scrollViewer"> The scroll viewer. </param>
        /// <param name="o"> The o. </param>
        public BindableScrollBar(ScrollViewer scrollViewer, Orientation o)
        {
            Orientation = o;
            BoundScrollViewer = scrollViewer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableScrollBar" /> class.
        /// </summary>
        public BindableScrollBar()
        {
        }

        public ScrollViewer BoundScrollViewer
        {
            get => (ScrollViewer)GetValue(BoundScrollViewerProperty);
            set => SetValue(BoundScrollViewerProperty, value);
        }

        private static void OnBoundScrollViewerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindableScrollBar sender &&
                e.NewValue != null)
                sender.UpdateBindings();
        }

        private void BoundScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    Value = e.HorizontalOffset;
                    break;
                case Orientation.Vertical:
                    Value = e.VerticalOffset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Orientation));
            }
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    BoundScrollViewer.ScrollToHorizontalOffset(e.NewValue);
                    break;
                case Orientation.Vertical:
                    BoundScrollViewer.ScrollToVerticalOffset(e.NewValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Orientation));
            }
        }

        private void UpdateBindings()
        {
            AddHandler(ScrollEvent, new ScrollEventHandler(OnScroll));
            BoundScrollViewer.AddHandler(ScrollViewer.ScrollChangedEvent,
                new ScrollChangedEventHandler(BoundScrollChanged));
            Minimum = 0;
            if (Orientation == Orientation.Horizontal)
            {
                SetBinding(MaximumProperty,
                    new Binding("ScrollableWidth") { Source = BoundScrollViewer, Mode = BindingMode.OneWay });
                SetBinding(ViewportSizeProperty,
                    new Binding("ViewportWidth") { Source = BoundScrollViewer, Mode = BindingMode.OneWay });
            }
            else
            {
                SetBinding(MaximumProperty,
                    new Binding("ScrollableHeight") { Source = BoundScrollViewer, Mode = BindingMode.OneWay });
                SetBinding(ViewportSizeProperty,
                    new Binding("ViewportHeight") { Source = BoundScrollViewer, Mode = BindingMode.OneWay });
            }

            LargeChange = 242;
            SmallChange = 16;
        }
    }
}