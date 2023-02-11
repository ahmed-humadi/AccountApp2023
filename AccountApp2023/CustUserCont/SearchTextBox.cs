using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    public class SearchTextBox : TextBox, ICommandSource
    {
        static SearchTextBox()
        {
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Button button = GetTemplateChild("PART_SearchButton") as Button;

            this.TextChanged += SearchTextBox_TextChanged;

            if(!(button is null))
                button.Click += Button_Click;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender as TextBox).IsKeyboardFocusWithin)
                e.Handled = true;
            else
            {
                // let this bobble event to go to the viewmodel to be handled
                // there .
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
                this.ExecuteCommandSource(this);
        }
        public event EventHandler SearchButtonClicked;

        protected void OnSearchButtonClicked()
        {
            SearchButtonClicked?.Invoke(this, new EventArgs());
        }

        public static readonly DependencyProperty CommandProperty =
                DependencyProperty.Register(
                        "Command",
                        typeof(ICommand),
                        typeof(SearchTextBox),
                        new FrameworkPropertyMetadata((ICommand)null,
                            new PropertyChangedCallback(OnCommandChanged)));
        public static readonly DependencyProperty CommandParameterProperty =
                DependencyProperty.Register(
                        "CommandParameter",
                        typeof(object),
                        typeof(SearchTextBox),
                        new FrameworkPropertyMetadata((object)null));
        public static readonly DependencyProperty CommandTargetProperty =
                DependencyProperty.Register(
                        "CommandTarget",
                        typeof(IInputElement),
                        typeof(SearchTextBox),
                        new FrameworkPropertyMetadata((IInputElement)null));
        public ICommand Command 
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }

        public bool CanExecute { get; private set; }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchTextBox b = (SearchTextBox)d;
            b.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
        }
        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                UnhookCommand(oldCommand);
            }
            if (newCommand != null)
            {
                HookCommand(newCommand);
            }
        }
        private void UnhookCommand(ICommand command)
        {
            CanExecuteChangedEventManager.RemoveHandler(command, OnCanExecuteChanged);
            UpdateCanExecute();
        }

        private void HookCommand(ICommand command)
        {
            CanExecuteChangedEventManager.AddHandler(command, OnCanExecuteChanged);
            UpdateCanExecute();
        }

        private void OnCanExecuteChanged(object sender, EventArgs e)
        {
            UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            if (Command != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    object parameter = this.CommandParameter;
                    IInputElement target = this.CommandTarget;

                    RoutedCommand routed = command as RoutedCommand;
                    if (routed != null)
                    {
                        if (target == null)
                        {
                            target = this as IInputElement;
                        }
                        CanExecute = routed.CanExecute(parameter, target);
                    }
                    else
                    {
                        CanExecute = command.CanExecute(parameter);
                    }
                }
                else
                {
                    CanExecute = true;
                }
            }
        }
        internal void ExecuteCommandSource(ICommandSource commandSource)
        {
            ICommand command = commandSource.Command;
            if (command != null)
            {
                object parameter = commandSource.CommandParameter;
                IInputElement target = commandSource.CommandTarget;

                RoutedCommand routed = command as RoutedCommand;
                if (routed != null)
                {
                    if (target == null)
                    {
                        target = commandSource as IInputElement;
                    }
                    if (routed.CanExecute(parameter, target))
                    {
                        routed.Execute(parameter, target);
                    }
                }
                else if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
    }
}
