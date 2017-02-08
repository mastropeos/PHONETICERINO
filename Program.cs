using System;
using System.Diagnostics;
using System.Speech.Recognition;
using System.Threading;
using System.Runtime.InteropServices;

namespace DoItPleae
{
    class Program
    {
        const Int32 SW_MINIMIZE = 1;

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

        private static void MinimizeConsoleWindow()
        {
            IntPtr hWndConsole = GetConsoleWindow();
            ShowWindow(hWndConsole, SW_MINIMIZE);
        }

        static ManualResetEvent _completed = null;
        private static string strCmdText;

        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Title = "Voice Commands V 0.1";
            MinimizeConsoleWindow();
            Console.WriteLine("\t\t\t________________");
            Console.WriteLine("\t\t\t|Voice Commands|");
            Console.WriteLine("\t\t\t________________");
            _completed = new ManualResetEvent(false);
            SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("youtube")) { Name = "youtubeGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("tube")) { Name = "tubeGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("psaxe")) { Name = "psaxeGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("translate")) { Name = "translateGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("simiosi")) { Name = "simiosiGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("upload")) { Name = "uploadGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("jiji")) { Name = "jijiGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("cut")) { Name = "cutGrammar" });
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("cancel")) { Name = "cancelGrammar" });
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice(); // set the input of the speech recognizer to the default audio device
            _recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
            _completed.WaitOne(); // wait until speech recognition is completed
            _recognizer.Dispose(); // dispose the speech recognition engine


        }
        public static void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "youtube":
                case "tube":
                    doyoutube();
                    break;
                case "psaxe":
                    dogoogle();
                    break;
                case "translate":
                    dogoogletranslate();
                    break;
                case "simiosi":
                    dotxt();
                    break;
                case "upload":
                    dotinypic();
                    break;
                case "jiji":
                    doopgg();
                    break;
                case "cut":
                    doshutdown();
                    break;
                case "cancel":
                    doabort();
                    break;
            }

        }

        private static void dogoogletranslate()
        {
            Process.Start("https://translate.google.gr");
            Console.WriteLine("{0} : Translate.google.gr Redirecting", DateTime.Now.ToShortTimeString());
        }
        private static void dogoogle()
        {
            Process.Start("http://www.google.gr");
            Console.WriteLine("{0} : Google.gr Redirecting", DateTime.Now.ToShortTimeString());
        }
        private static void doyoutube()
        {
            Process.Start("http://www.youtube.com");
            Console.WriteLine("{0} : Youtube.com Redirecting", DateTime.Now.ToShortTimeString());
        }
        private static void dotinypic()
        {
            Process.Start("http://www.tinypic.com");
            Console.WriteLine("{0} : Tinypic.com Redirecting", DateTime.Now.ToShortTimeString());
        }
        private static void dotxt()
        {
            Process.Start("notepad.exe");
            Console.WriteLine("{0} : Launching Notepad...", DateTime.Now.ToShortTimeString());
        }
        private static void doopgg()
        {
            Process.Start("https://eune.op.gg");
            Console.WriteLine("{0} : OP.GG Redirecting", DateTime.Now.ToShortTimeString());
        }
        private static void doshutdown()
        {
            Process.Start("shutdown", "/s");
        }
        private static void doabort()
        {
            Process.Start("shutdown", "/a");

        }

    }

}