using Android;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Java.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
//using System.Windows.Forms;
using ReLogic.IO;
using ReLogic.OS;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;
using System.Threading;
using Terraria;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;

namespace Game1
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 _game;
        private View _view;
        public static AssetManager assets;
        public static ContentManager content;

        public const bool IsServer = false;

        public const bool IsXna = true;

        public const bool IsFna = false;

        public const bool IsDebug = false;

        public static Dictionary<string, string> LaunchParameters = new Dictionary<string, string>();

        private static int ThingsToLoad;

        private static int ThingsLoaded;

        public static bool LoadedEverything = true;

        public static IntPtr JitForcedMethodCache;

        public static void StartForceLoad()
        {
            if (!Main.SkipAssemblyLoad)
            {
                Thread thread = new Thread(ForceLoadThread);
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                LoadedEverything = true;
            }
        }

        public static void ForceLoadThread(object threadContext)
        {
            ForceLoadAssembly(Assembly.GetExecutingAssembly(), initializeStaticMembers: true);
            LoadedEverything = true;
        }

        private static void ForceJITOnAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                MethodInfo[] methods = types[i].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (MethodInfo methodInfo in methods)
                {
                    if (!methodInfo.IsAbstract && !methodInfo.ContainsGenericParameters && methodInfo.GetMethodBody() != null)
                    {
                        RuntimeHelpers.PrepareMethod(methodInfo.MethodHandle);
                    }
                }
                ThingsLoaded++;
            }
        }

        private static void ForceStaticInitializers(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsGenericType)
                {
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
            }
        }

        private static void ForceLoadAssembly(Assembly assembly, bool initializeStaticMembers)
        {
            ThingsToLoad = assembly.GetTypes().Length;
            //ForceJITOnAssembly(assembly);
            if (initializeStaticMembers)
            {
                //ForceStaticInitializers(assembly);
            }
        }

        private static void ForceLoadAssembly(string name, bool initializeStaticMembers)
        {
            Assembly assembly = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                if (assemblies[i].GetName().Name.Equals(name))
                {
                    assembly = assemblies[i];
                    break;
                }
            }
            if (assembly == null)
            {
                assembly = Assembly.Load(name);
            }
            ForceLoadAssembly(assembly, initializeStaticMembers);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Activity1.assets = this.Assets;

            /*Stream s = (assets.Open("Content/Sound Bank.xsb"));
            FileStream f1 = new FileStream("/sdcard/Sound Bank.xsb", FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryReader f2 = new BinaryReader(s);
            byte _b;
            for (int i = 0; i < f2.; i++)
            {
                _b = f2.ReadByte();
                f1.WriteByte(_b);
            }

            


            f1.Close();
            f2.Close();*/
            RequestedOrientation = ScreenOrientation.Landscape;
            RequestPermissions(new string[]
            {
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage
            }, 0);
            if (!System.IO.Directory.Exists("/sdcard/Content"))
            {
                string[] fileNames = assets.List("");
                foreach (string fileName in fileNames)
                {
                    try
                    {
                        Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName);
                        /*using (StreamReader sr = new StreamReader(assets.Open(fileName)))
                        {
                            string line;
                            using (StreamWriter sw = new StreamWriter("/sdcard/" + fileName))
                            {
                                line = sr.ReadToEnd();

                                 sw.Write(line);
                   
                            }
                        }*/
                        Stream stream = (assets.Open(fileName));
                        FileStream fs2 = new FileStream("/sdcard/" + fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                        BinaryReader fs1 = new BinaryReader(stream);
                        byte new_b;
                        while (true)
                        {
                            try
                            {
                                new_b = fs1.ReadByte();
                                fs2.WriteByte(new_b);
                            }
                            catch (Exception esub)
                            {
                                fs1.Close();
                                fs2.Close();
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        System.IO.Directory.CreateDirectory("/sdcard/" + fileName);
                        string[] subDirFileNames1 = assets.List(fileName);
                        foreach (string subDirFileName1 in subDirFileNames1)
                        {
                            try
                            {
                                Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName + "/" + subDirFileName1);
                                /*using (StreamReader sr1 = new StreamReader(assets.Open(fileName + "/" + subDirFileName1)))
                                {
                                    string line1;
                                    using (StreamWriter sw1 = new StreamWriter("/sdcard/" + fileName + "/" + subDirFileName1))
                                    {
                                        line1 = sr1.ReadToEnd();

                                        sw1.Write(line1);
                                    }
                                }*/
                                Stream stream = (assets.Open(fileName + "/" + subDirFileName1));
                                FileStream fs2 = new FileStream("/sdcard/" + fileName + "/" + subDirFileName1, FileMode.Create, FileAccess.Write, FileShare.None);
                                BinaryReader fs1 = new BinaryReader(stream);
                                byte new_b;
                                while (true)
                                {
                                    try
                                    {
                                        new_b = fs1.ReadByte();
                                        fs2.WriteByte(new_b);
                                    }
                                    catch (Exception esub)
                                    {
                                        fs1.Close();
                                        fs2.Close();
                                        break;
                                    }
                                }
                            }
                            catch (Exception e1)
                            {
                                System.IO.Directory.CreateDirectory("/sdcard/" + fileName + "/" + subDirFileName1);
                                string[] subDirFileNames2 = assets.List(fileName + "/" + subDirFileName1);
                                foreach (string subDirFileName2 in subDirFileNames2)
                                {
                                    try
                                    {
                                        Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName + "/" + subDirFileName1 + "/" + subDirFileName2);
                                        /*using (StreamReader sr2 = new StreamReader(assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2)))
                                        {
                                            string line2;
                                            using (StreamWriter sw2 = new StreamWriter("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2))
                                            {
                                                line2 = sr2.ReadToEnd();

                                                sw2.Write(line2);
                                            }
                                        }*/
                                        Stream stream = (assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2));
                                        FileStream fs2 = new FileStream("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2, FileMode.Create, FileAccess.Write, FileShare.None);
                                        BinaryReader fs1 = new BinaryReader(stream);
                                        byte new_b;
                                        while (true)
                                        {
                                            try
                                            {
                                                new_b = fs1.ReadByte();
                                                fs2.WriteByte(new_b);
                                            }
                                            catch (Exception esub)
                                            {
                                                fs1.Close();
                                                fs2.Close();
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception e2)
                                    {
                                        System.IO.Directory.CreateDirectory("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2);
                                        string[] subDirFileNames3 = assets.List(fileName + "/" + subDirFileName1 + "/" + subDirFileName2);
                                        foreach (string subDirFileName3 in subDirFileNames3)
                                        {
                                            try
                                            {
                                                Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3);
                                                /*using (StreamReader sr3 = new StreamReader(assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3)))
                                                {
                                                    string line3;
                                                    using (StreamWriter sw3 = new StreamWriter("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3))
                                                    {
                                                        line3 = sr3.ReadToEnd();

                                                        sw3.Write(line3);
                                                    }
                                                }*/
                                                Stream stream = (assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3));
                                                FileStream fs2 = new FileStream("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3, FileMode.Create, FileAccess.Write, FileShare.None);
                                                BinaryReader fs1 = new BinaryReader(stream);
                                                byte new_b;
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        new_b = fs1.ReadByte();
                                                        fs2.WriteByte(new_b);
                                                    }
                                                    catch (Exception esub)
                                                    {
                                                        fs1.Close();
                                                        fs2.Close();
                                                        break;
                                                    }
                                                }
                                            }
                                            catch (Exception e3)
                                            {
                                                System.IO.Directory.CreateDirectory("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3);
                                                string[] subDirFileNames4 = assets.List(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3);
                                                foreach (string subDirFileName4 in subDirFileNames4)
                                                {
                                                    try
                                                    {
                                                        Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4);
                                                        /*using (StreamReader sr4 = new StreamReader(assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4)))
                                                        {
                                                            string line4;
                                                            using (StreamWriter sw4 = new StreamWriter("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4))
                                                            {
                                                                line4 = sr4.ReadToEnd();

                                                                sw4.Write(line4);
                                                            }
                                                        }*/
                                                        Stream stream = (assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4));
                                                        FileStream fs2 = new FileStream("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4, FileMode.Create, FileAccess.Write, FileShare.None);
                                                        BinaryReader fs1 = new BinaryReader(stream);
                                                        byte new_b;
                                                        while (true)
                                                        {
                                                            try
                                                            {
                                                                new_b = fs1.ReadByte();
                                                                fs2.WriteByte(new_b);
                                                            } 
                                                            catch (Exception esub)
                                                            {
                                                                fs1.Close();
                                                                fs2.Close();
                                                                break;
                                                            }
                                                        }


                                                        

                                                        
                                                    }
                                                    catch (Exception e4)
                                                    {
                                                        System.IO.Directory.CreateDirectory("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4);
                                                        string[] subDirFileNames5 = assets.List(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4);
                                                        foreach (string subDirFileName5 in subDirFileNames5)
                                                        {
                                                            try
                                                            {
                                                                Android.Util.Log.WriteLine(Android.Util.LogPriority.Error, "terraria", fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5);
                                                                /*using (StreamReader sr5 = new StreamReader(assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5)))
                                                                {
                                                                    string line5;
                                                                    using (StreamWriter sw5 = new StreamWriter("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5))
                                                                    {
                                                                        line5 = sr5.ReadToEnd();

                                                                        sw5.Write(line5);
                                                                    }
                                                                }*/
                                                                
                                                                Stream stream = (assets.Open(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5));
                                                                FileStream fs2 = new FileStream("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5, FileMode.Create, FileAccess.Write, FileShare.None);
                                                                BinaryReader fs1 = new BinaryReader(stream);
                                                                byte new_b;
                                                                while (true)
                                                                {
                                                                    try
                                                                    {
                                                                        new_b = fs1.ReadByte();
                                                                        fs2.WriteByte(new_b);
                                                                    }
                                                                    catch (Exception esub)
                                                                    {
                                                                        fs1.Close();
                                                                        fs2.Close();
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception e5)
                                                            {
                                                                System.IO.Directory.CreateDirectory("/sdcard/" + fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5);
                                                                string[] subDirFileNames6 = assets.List(fileName + "/" + subDirFileName1 + "/" + subDirFileName2 + "/" + subDirFileName3 + "/" + subDirFileName4 + "/" + subDirFileName5);

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Main main = new Main();
            try
            {
                Lang.InitializeLegacyLocalization();
                //SocialAPI.Initialize();
                LaunchInitializer.LoadParameters(main);
                _view = main.Services.GetService(typeof(View)) as View;
                Main.OnEnginePreload += StartForceLoad;
                SetContentView(_view);
                main.Run();
            }
            catch (Exception e)
            {
                //DisplayException(e);
            }
            /*Thread.CurrentThread.Name = "Main Thread";
            if (monoArgs)
            {
                args = Utils.ConvertMonoArgsToDotNet(args);
            }*/
            if (false)
            {
                Main.OnEngineLoad += delegate
                {
                    Main.instance.IsMouseVisible = false;
                };
            }
            //LaunchParameters = Utils.ParseArguements(args);
            //ThreadPool.SetMinThreads(8, 8);
            //LanguageManager.Instance.SetLanguage(GameCulture.DefaultCulture);
            //InitializeConsoleOutput();
            //SetupLogging();
            //Platform.Get<IWindowService>().SetQuickEditEnabled(false);
            /*using Main main = new Main();
            try
            {
                //Lang.InitializeLegacyLocalization();
                //SocialAPI.Initialize();
                //LaunchInitializer.LoadParameters(main);
                _view = main.Services.GetService(typeof(View)) as View;

                SetContentView(_view);
                //Main.OnEnginePreload += StartForceLoad;
                main.Run();
            }
            catch (Exception e)
            {
                //DisplayException(e);
            }*/
            /*Game1 main = new Game1();
            try
            {
                //Lang.InitializeLegacyLocalization();
                //SocialAPI.Initialize();
                //LaunchInitializer.LoadParameters(main);
                _view = main.Services.GetService(typeof(View)) as View;

                SetContentView(_view);
                main.Run();
            }
            catch (Exception e)
            {
                //DisplayException(e);
            }*/
        }
    }
}
