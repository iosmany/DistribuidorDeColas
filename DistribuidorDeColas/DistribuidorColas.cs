using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace DistribuidorDeColas
{
    public class DistribuidorColas
    {
        static Lazy<DistribuidorColas> _instance = new Lazy<DistribuidorColas>(() => new DistribuidorColas());

        public static DistribuidorColas Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        internal DistribuidorColas()
        {
        }

        public void Agregar(Notificacion notificacion)
        {
            GestorColasAzure gestor = new GestorColasAzure(notificacion.Tipo.ToString());
            gestor.Agregar(notificacion);
        }

        public void Agregar(List<Notificacion> notificaciones)
        {
            GestorColasAzure gestor;
            foreach (var n in notificaciones.GroupBy(x=>x.Tipo))
            {
                gestor = new GestorColasAzure(n.Key.ToString());
                gestor.Agregar(n.ToList());
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
