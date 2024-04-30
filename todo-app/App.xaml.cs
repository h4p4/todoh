namespace todo_app
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TreeViewItem), UIElement.PreviewMouseRightButtonDownEvent,
                new RoutedEventHandler(TreeViewItem_PreviewMouseRightButtonDownEvent));

            base.OnStartup(e);
        }

        private void TreeViewItem_PreviewMouseRightButtonDownEvent(object sender, RoutedEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = true;
        }
    }
}