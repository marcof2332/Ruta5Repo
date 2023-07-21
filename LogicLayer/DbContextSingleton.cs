using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    internal class DbContextSingleton
    {
        //Atributo de clase para guardar el contexto que maneja el EF
        private static TransporteEntities _transporteEntities = null;

        //Propiedad que me da acceso al contexto para trabajar 
        public static TransporteEntities TransporteContext
        {
            get
            {
                if (_transporteEntities == null)
                {
                    _transporteEntities = new TransporteEntities();
                    _transporteEntities.Configuration.ProxyCreationEnabled = false; //para que no de problemas en la serializacion
                }
                return _transporteEntities;
            }
        }
    }
}
