using System;
using System.Threading;

namespace DistribuidorDeColas
{
    internal static class QueueThread
    {
        static EventWaitHandle autoReset;
        static Thread execution;
        static long finalizar;

        public static void Iniciar()
        {
            Interlocked.Add(ref finalizar, 0L);

            autoReset = new EventWaitHandle(false, EventResetMode.AutoReset);
            execution = new Thread(new ThreadStart(ProcessingQueue));
            execution.Start();
        }

        static void ProcessingQueue()
        {
            while (Interlocked.Read(ref finalizar) == 0L)
            {
                Console.WriteLine("Starting Processing");
                GestorColasAzure gestorEmails = new GestorColasAzure(TipoNotificacion.emails.ToString());
                while (gestorEmails.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing emails notifications");
                    gestorEmails.ProcesaMensajes();
                }
                GestorColasAzure gestorSms = new GestorColasAzure(TipoNotificacion.sms.ToString());
                while (gestorSms.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing sms notifications");
                    gestorSms.ProcesaMensajes();
                }
                GestorColasAzure gestorLogs = new GestorColasAzure(TipoNotificacion.logs.ToString());
                while (gestorLogs.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing logs");
                    gestorLogs.ProcesaMensajes();
                }
                Console.WriteLine("Waiting for signal");
                autoReset.WaitOne();
            }
        }

        public static void Run()
        {
            Console.WriteLine($"Thread State : {execution.ThreadState.ToString()}");
            autoReset.Set();
        }

        public static void Stop()
        {
            Console.WriteLine("Ordering Finish");
            Interlocked.Exchange(ref finalizar, 1L);
            autoReset.Set();
        }

        public static string GetState()
        {
            return execution != null ? execution.ThreadState.ToString() : "Thread null!!!!";
        }
    }
}
