using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AccountApp2023.CustUserCont
{
    public class HamburgerMenuItem : RadioButton
    {
        static HamburgerMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenuItem), new FrameworkPropertyMetadata(typeof(HamburgerMenuItem)));
        }
    }
}
