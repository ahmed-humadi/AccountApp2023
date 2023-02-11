using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace FrameWorkLib
{
    public class StoryboardManager
    {
        public static DependencyProperty IDProperty =
            DependencyProperty.RegisterAttached("ID", typeof(string), typeof(StoryboardManager),
            new UIPropertyMetadata(string.Empty, IDChanged));

        static Dictionary<string, Storyboard> _storyboards = new Dictionary<string, Storyboard>();

        private static void IDChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = obj as Storyboard;
            if (sb == null)
                return;

            string key = e.NewValue as string;

            if (_storyboards.ContainsKey(key))
                _storyboards[key] = sb;
            else
                _storyboards.Add(key, sb);
        }

        public static void PlayStoryboard(string id)
        {
            if (!_storyboards.ContainsKey(id))
            {
                return;
            }
            Storyboard sb = _storyboards[id];

            sb.Begin();
        }

        public static void SetID(DependencyObject obj, string id)
        {
            obj.SetValue(IDProperty, id);
        }
        public static string GetID(DependencyObject obj)
        {
            return obj.GetValue(IDProperty) as string;
        }
    }

}
