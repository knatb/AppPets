using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Examen_AppPets.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init("sVrC8bRwsv6jBtghdZ3K~Adt3Cr4hNUOHPiy1UfEMpQ~AqEKz2NnWcUp1qQSx9xkkXUvgLhJT6_8fnmrQOrtvcd8bd9ber7Uf-KLDnJxPqgv");

            LoadApplication(new Examen_AppPets.App());
        }
    }
}
