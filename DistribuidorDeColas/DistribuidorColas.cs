using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DistribuidorDeColas
{
    public class DistribuidorColas
    {
        static Lazy<DistribuidorColas> _instance = new Lazy<DistribuidorColas>(() => new DistribuidorColas());

        ConcurrentQueue<Notificacion> queue = new ConcurrentQueue<Notificacion>();

        public static DistribuidorColas Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        internal DistribuidorColas()
        {
            procesando = false;
        }

        public void Agregar(Notificacion notificacion)
        {
            queue.Enqueue(notificacion);
        }

        static bool procesando;

        public void Procesar()
        {
            var grouped = queue.GroupBy(x => x.Tipo).ToDictionary(x => x.Key, x => x);

            foreach (var key in grouped.Keys)
            {
                Console.WriteLine($"Procesando notificaciones para la cola {key.ToString()}");
                GestorColasAzure gestor = new GestorColasAzure(key.ToString());
                foreach (var notificacion in grouped[key])
                {
                    gestor.Agregar(notificacion);
                }
            }
        }
    }
    
    public class Notificacion
    {
        public Notificacion(TipoNotificacion tipo, string mensaje)
        {
            Tipo = tipo;
            TextoMensaje = mensaje;
        }

        public Notificacion(TipoNotificacion tipo, MensajeEmail mensaje)
        {
            Tipo = tipo;
            TextoMensaje = JsonConvert.SerializeObject(mensaje);
        }

        public Notificacion(TipoNotificacion tipo, MensajeSms mensaje)
        {
            Tipo = tipo;
            TextoMensaje = JsonConvert.SerializeObject(mensaje);
        }

        public TipoNotificacion Tipo { get; private set; }
        public string TextoMensaje { get; private set; }
    }

    public class MensajeEmail
    {
        public string Para { get; set; }
        public string From { get; set; }
        public string Asunto { get; set; }
        public string Body { get; set; }
        public bool EsHtml { get; set; }
    }

    public class MensajeSms
    {
        public string Para { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
    }

    public enum TipoNotificacion { emails, sms, logs, imagenes }
}
