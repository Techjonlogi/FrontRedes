using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Vlc.DotNet.Wpf;

namespace LiberMusic_Client.Paginas
{
    /// <summary>
    /// Lógica de interacción para ReproducirCancion.xaml
    /// </summary>
    public partial class ReproducirCancion : Page
    {
       
        private readonly DirectoryInfo vlcLibDirectory;
        private VlcControl control;

        public ReproducirCancion()
        {
            InitializeComponent();

         
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            try
            {
                vlcLibDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            }
            catch (Exception) { 
            
            }

            }
        /*protected override void OnChanged (CancelEventArgs e)
        {
            this.control?.Dispose();
            //base.OnClosing(e);
        }*/

        private void OnPlayButtonClick(object sender, RoutedEventArgs e)
        {
            this.control?.Dispose();
            this.control = new VlcControl();
            this.ControlContainer.Content = this.control;
            this.control.SourceProvider.CreatePlayer(this.vlcLibDirectory);

            // This can also be called before EndInit
            this.control.SourceProvider.MediaPlayer.Log += (_, args) =>
            {
                string message = $"libVlc : {args.Level} {args.Message} @ {args.Module}";
                System.Diagnostics.Debug.WriteLine(message);
            };

            control.SourceProvider.MediaPlayer.Play(new Uri("http://192.168.100.51:4001/streaming/artistas/dreamtheater/whendreamanddayunite/dash/index.mpd"));
           // this.botonPausaResumen.Content = "pausar";
        }

    }
}
