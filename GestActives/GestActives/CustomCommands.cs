using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestActives
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand BuscarNameCompany = new RoutedUICommand
            (
                "BuscarNameCompany",
                "BuscarNameCompany",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand ActualizarCompany = new RoutedUICommand
            (
                "ActualizarCompany",
                "ActualizarCompany",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand GrabarCompany = new RoutedUICommand
            (
                "GrabarCompany",
                "GrabarCompany",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand LimpiarCompany = new RoutedUICommand
            (
                "LimpiarCompany",
                "LimpiarCompany",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand EliminarCompany = new RoutedUICommand
            (
                "EliminarCompany",
                "EliminarCompany",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand BuscarIdPerson = new RoutedUICommand
            (
                "BuscarIdPerson",
                "BuscarIdPerson",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand GrabarPerson = new RoutedUICommand
            (
                "GrabarPersona",
                "GrabarPersona",
                typeof(CustomCommands)
            );

        public static readonly RoutedUICommand LimpiarPerson = new RoutedUICommand
            (
                "LimpiarPersona",
                "LimpiarPersona",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand EliminarPerson = new RoutedUICommand
            (
                "EliminarPersona",
                "EliminarPersona",
                typeof(CustomCommands)
            );

    }
}
