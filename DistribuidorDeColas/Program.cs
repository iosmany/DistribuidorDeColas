using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistribuidorDeColas
{
    class Program
    {
        static string beginAgain = "c";

        static List<Notificacion> notificaciones = new List<Notificacion>();
        static void Main(string[] args)
        {
            QueueThread.Iniciar();

            while (beginAgain == "c")
            {
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));

                QueueThread.Run();

                Console.WriteLine("Esperando mas notificaciones");
                Thread.Sleep(2000);

                notificaciones.Add(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender = "Beautylutio" }));
                notificaciones.Add(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender = "Beautylutio" }));
                notificaciones.Add(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender = "Beautylutio" }));
                notificaciones.Add(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender = "Beautylutio" }));
                notificaciones.Add(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender = "Beautylutio" }));
                DistribuidorColas.Instance.Agregar(notificaciones);

                QueueThread.Run();

                Console.WriteLine("Esperando mas notificaciones");
                Thread.Sleep(2000);
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
                DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));

                QueueThread.Run();

                Console.WriteLine("Proceso Terminado");

                Console.WriteLine("Presiona 'c' para continuar, u otra tecla para finalizar.");
                beginAgain = Console.ReadLine();
            }

            QueueThread.Stop();

            Thread.Sleep(1000);
            Console.WriteLine($"Thread State : {QueueThread.GetState()}");

            Console.ReadLine();
        }
    }
}
