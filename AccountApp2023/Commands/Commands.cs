using System.Windows.Input;

namespace AccountWpf.MyCustomCommands
{
    public class Commands
    {
        private static RoutedUICommand _insert;
        private static RoutedUICommand _new;
        private static RoutedUICommand _delete;
        private static RoutedUICommand _save;
        private static RoutedUICommand _search;
        private static RoutedUICommand _modify;
        private static RoutedUICommand _refresh;

        static Commands()
        {
            var input_insert = new InputGestureCollection();
            input_insert.Add(new KeyGesture(Key.I, ModifierKeys.Control));

            var input_new = new InputGestureCollection();
            input_new.Add(new KeyGesture(Key.N, ModifierKeys.Control));

            var input_delete = new InputGestureCollection();
            input_delete.Add(new KeyGesture(Key.D, ModifierKeys.Control));

            var input_save = new InputGestureCollection();
            input_save.Add(new KeyGesture(Key.S, ModifierKeys.Control));

            var input_search = new InputGestureCollection();
            input_search.Add(new KeyGesture(Key.W, ModifierKeys.Control));

            var input_modify = new InputGestureCollection();
            input_modify.Add(new KeyGesture(Key.M, ModifierKeys.Control));

            var input_refresh = new InputGestureCollection();
            input_refresh.Add(new KeyGesture(Key.R, ModifierKeys.Control));

            Insert = new RoutedUICommand("Inser", "Insert", typeof(Commands), input_insert);
            New = new RoutedUICommand("New", "New", typeof(Commands), input_new);
            Delete = new RoutedUICommand("Delete", "Delete", typeof(Commands), input_delete);
            Save = new RoutedUICommand("Save", "Save", typeof(Commands), input_save);
            Search = new RoutedUICommand("Search", "Search", typeof(Commands), input_search);
            Modify = new RoutedUICommand("Modify", "Modify", typeof(Commands), input_modify);
            Refresh = new RoutedUICommand("Refresh", "Refresh", typeof(Commands), input_refresh);
        }
        public static RoutedUICommand Insert { get => _insert; set => _insert = value; }
        public static RoutedUICommand New { get => _new; set => _new = value; }
        public static RoutedUICommand Delete { get => _delete; set => _delete = value; }
        public static RoutedUICommand Save { get => _save; set => _save = value; }
        public static RoutedUICommand Search { get => _search; set => _search = value; }
        public static RoutedUICommand Modify { get => _modify; set => _modify = value; }
        public static RoutedUICommand Refresh { get => _refresh; set => _refresh = value; }
    }
}
