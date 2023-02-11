using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AccountApp2023.CustUserCont
{
    public class GridViewColumnBehaviour
    {
        public static readonly DependencyProperty CollapseableColumnProperty =
        DependencyProperty.RegisterAttached("CollapseableColumn", typeof(bool), typeof(GridViewColumnBehaviour),
           new UIPropertyMetadata(false, OnCollapseableColumnChanged));
        public static bool GetCollapseableColumn(DependencyObject d)
        {
            return (bool)d.GetValue(CollapseableColumnProperty);
        }

        public static void SetCollapseableColumn(DependencyObject d, bool value)
        {
            d.SetValue(CollapseableColumnProperty, value);
        }

        public static readonly DependencyProperty FixableColumnWidthProperty =
        DependencyProperty.RegisterAttached("FixableColumnWidth", typeof(bool), typeof(GridViewColumnBehaviour),
           new UIPropertyMetadata(false, OnFixableColumnWidthChanged));
        public static bool GetFixableColumnWidth(DependencyObject d)
        {
            return (bool)d.GetValue(FixableColumnWidthProperty);
        }

        public static void SetFixableColumnWidth(DependencyObject d, bool value)
        {
            d.SetValue(FixableColumnWidthProperty, value);
        }
        private static void OnFixableColumnWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var header = sender as GridViewColumnHeader;
            if (header == null)
                return;
        }
        private static void OnCollapseableColumnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var header = sender as GridViewColumnHeader;
            if (header == null)
                return;

            header.IsVisibleChanged += AdjustWidth;
        }

        private static void AdjustWidth(object sender, DependencyPropertyChangedEventArgs e)
        {
            var header = sender as GridViewColumnHeader;
            if (header == null)
                return;

            header.Column.Width = header.Visibility == Visibility.Collapsed ? 0 : 200;
        }
    }
}
