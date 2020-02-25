using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FFmpeg.Commands
{
    static class ToolbarCommands
    {
        public static RoutedUICommand Quit { get; } = new RoutedUICommand("Quit", "Quit", typeof(ToolbarCommands));
        public static RoutedUICommand Minimize { get; } = new RoutedUICommand("Minimize", "Minimize", typeof(ToolbarCommands));
    }
}
