using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Gma.System.MouseKeyHook;
using IrrKlang;

namespace consolaSFX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKeyboardMouseEvents mkEvents;
        private MainWindow main;
        public static ISoundEngine engine;
                public static bool capturingNextKey = false;
        public static List<Sfx> sfxList = new List<Sfx>();
        public static event Action<System.Windows.Forms.Keys> waitingKeyEvent;
        protected static void onWaitingKeyEvent(System.Windows.Forms.Keys keyInfo)
        {
            if(waitingKeyEvent!=null)
            {
                waitingKeyEvent(keyInfo);

            }
        }
            

        


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            logSystem.LogWriter.init();
            mkEvents = Hook.GlobalEvents();
            mkEvents.KeyDown += MkEvents_KeyDown;
            engine = new ISoundEngine();
            main = new consolaSFX.MainWindow();
            
            
            Application.Current.MainWindow = main;
            

            main.Show();
            

        }

        public static void playSound(Sfx sfx)
        {
            engine.Play2D(sfx.sfxPath);
        }

        private void MkEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(capturingNextKey)
            {
                capturingNextKey = false;
                onWaitingKeyEvent(e.KeyCode);
                return;


            }
            else
            {
                foreach(Sfx sfx in sfxList)
                {
                    if(e.KeyCode==sfx.sfxKey && e.Control==sfx.inclControl&&e.Shift==sfx.inclShift&&e.Alt==sfx.inclAlt)
                    {
                        playSound(sfx);

                    }
                }
            }
            
        }
    }
}
