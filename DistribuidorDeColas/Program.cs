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
        static List<Notificacion> notificaciones = new List<Notificacion>();
        static void Main(string[] args)
        {
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.emails, new MensajeEmail() { Asunto = "Test", Body = "Esto es una prueba", EsHtml = false, From = "lorenkid@gmail.com", Para = "ios.rdgz@gmail.com" }));
            Console.WriteLine("Esperando mas notificaciones");
            Thread.Sleep(2000);
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender= "Beautylutio" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender= "Beautylutio" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender= "Beautylutio" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender= "Beautylutio" }));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.sms, new MensajeSms() { Para = "+34665329447", Body = "TEst", Sender= "Beautylutio" }));
            Console.WriteLine("Esperando mas notificaciones");
            Thread.Sleep(2000);
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));
            DistribuidorColas.Instance.Agregar(new Notificacion(TipoNotificacion.logs, $"Error: {DateTime.Now}, Exception: Error en registro"));

          

            Console.WriteLine("Proceso Terminado");
        }
    }
}
