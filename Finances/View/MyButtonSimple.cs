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

namespace Finances.View
{
    public class MyButtonSimple : Button
    {
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent("Tap", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(MyButtonSimple));
        //(имя события, вид маршрутизации, делегат события, владеющий событием класс)

        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TapEvent);
            RaiseEvent(newEventArgs);
        }

        protected override void OnClick()
        {
            RaiseTapEvent();
        }
    }
}
