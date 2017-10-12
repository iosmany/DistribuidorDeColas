using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistribuidorDeColas
{
    internal static class QueueThread
    {
        static EventWaitHandle waitSignal;
        static Thread execution;
        static long finalizar;

        public static void Iniciar()
        {
            Interlocked.Add(ref finalizar, 0L);

            waitSignal = new EventWaitHandle(false, EventResetMode.AutoReset);
            execution = new Thread(new ThreadStart(ProcessingQueue));
            execution.Start();
        }

        static void ProcessingQueue()
        {
            while (Interlocked.Read(ref finalizar) == 0L)
            {
                Console.WriteLine("Starting Processing");
                GestorColasAzure gestorEmails = new GestorColasAzure(TipoNotificacion.emails.ToString());
                GestorColasAzure gestorSms = new GestorColasAzure(TipoNotificacion.sms.ToString());
                GestorColasAzure gestorLogs = new GestorColasAzure(TipoNotificacion.logs.ToString());

                if (gestorEmails.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing emails notifications");
                }

                if (gestorSms.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing sms notifications");
                }

                if (gestorLogs.TieneElementosEnCola())
                {
                    Console.WriteLine("Processing logs");
                }

                Console.WriteLine("Waiting for signal");
                waitSignal.WaitOne();
            }
        }

        public static void Run()
        {
            waitSignal.Set();
        }

        public static void Stop()
        {
            Console.WriteLine("Ordering Finish");
            Interlocked.Exchange(ref finalizar, 1L);
            waitSignal.Set();
        }

        public static string GetState()
        {
            return execution != null ? execution.ThreadState.ToString() : "Thread null!!!!";
        }
    }
}
