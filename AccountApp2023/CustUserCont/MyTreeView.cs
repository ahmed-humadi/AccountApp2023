using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountApp2023.CustUserCont
{
    public class MyTreeView : TreeView
    {
        static MyTreeView()
        {
        }
        #region ExpandingBehaviour (Attached DependencyProperty)

        public static readonly DependencyProperty RefreshingBehaviourProperty =
       DependencyProperty.RegisterAttached("RefreshingBehaviour", typeof(ICommand), typeof(MyTreeView),
               new PropertyMetadata(OnRefreshingBehaviourChanged));

        public static readonly DependencyProperty SelectingBehaviourProperty =
       DependencyProperty.RegisterAttached("SelectingBehaviour", typeof(ICommand), typeof(MyTreeView),
               new PropertyMetadata(OnSelectingBehaviourChanged));


        public static readonly DependencyProperty ExpandingBehaviourProperty =
        DependencyProperty.RegisterAttached("ExpandingBehaviour", typeof(ICommand), typeof(MyTreeView),
                new PropertyMetadata(OnExpandingBehaviourChanged));

        public static readonly DependencyProperty CollapsingBehaviourProperty =
       DependencyProperty.RegisterAttached("CollapsingBehaviour", typeof(ICommand), typeof(MyTreeView),
               new PropertyMetadata(OnCollapsingBehaviourChanged));

        public static void SetRefreshingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(RefreshingBehaviourProperty, value);
        }
        public static ICommand GetRefreshingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(RefreshingBehaviourProperty);
        }
        public static void SetSelectingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(SelectingBehaviourProperty, value);
        }
        public static ICommand GetSelectingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(SelectingBehaviourProperty);
        }
        public static void SetExpandingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(ExpandingBehaviourProperty, value);
        }
        public static ICommand GetExpandingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(ExpandingBehaviourProperty);
        }
        public static void SetCollapsingBehaviour(DependencyObject o, ICommand value)
        {
            o.SetValue(CollapsingBehaviourProperty, value);
        }
        public static ICommand GetCollapsingBehaviour(DependencyObject o)
        {
            return (ICommand)o.GetValue(CollapsingBehaviourProperty);
        }
        private static void OnExpandingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem tvi = d as TreeViewItem;
            if (tvi != null)
            {
                ICommand ic = e.NewValue as ICommand;
                if (ic != null)
                {
                    tvi.Expanded += (s, a) =>
                    {
                        if (ic.CanExecute(a))
                        {
                            ic.Execute(a);

                        }
                        a.Handled = true;
                    };
                }
            }
        }
        private static void OnCollapsingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem tvi = d as TreeViewItem;
            if (tvi != null)
            {
                ICommand ic = e.NewValue as ICommand;
                if (ic != null)
                {
                    tvi.Collapsed += (s, a) =>
                    {
                        if (ic.CanExecute(a))
                        {
                            ic.Execute(a);

                        }
                        a.Handled = true;
                    };
                }
            }
        }

        private static void OnSelectingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem tvi = d as TreeViewItem;
            if (tvi != null)
            {
                ICommand ic = e.NewValue as ICommand;
                if (ic != null)
                {
                    tvi.Selected += (s, a) =>
                    {
                        if (ic.CanExecute(a))
                        {
                            ic.Execute(a);

                        }
                        a.Handled = true;
                    };
                }
            }
        }

        private static void OnRefreshingBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem tvi = d as TreeViewItem;
            if (tvi != null)
            {
                ICommand ic = e.NewValue as ICommand;
                if (ic != null)
                { 
                    if (ic.CanExecute(e))
                    {
                        ic.Execute(e);
                    }
                }
            }
        }

        #endregion
    }
}

