using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;
using System.Data.Entity;

namespace LogicLayer
{
    internal class ZonesLogic : Interfaces.IZonesLogic
    {
        #region ZonesLogicSingleton
        private static ZonesLogic _instance = null;
        private ZonesLogic() { }
        public static ZonesLogic GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ZonesLogic();
            }
            return _instance;
        }
        #endregion

        public Zones ZoneSearch(int id)
        {
            return DbContextSingleton.TransporteContext.Zones.Where(z => z.IdZone == id).FirstOrDefault();
        }
        public void ZAdd(Zones zo)
        {
            try
            {
                Validations.ZoneValidation(zo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                DbContextSingleton.TransporteContext.Zones.Add(zo);
                DbContextSingleton.TransporteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(zo).State = System.Data.Entity.EntityState.Detached;
                if (ex.InnerException != null)
                {
                    throw new Exception("Ocurrio un error en BD al intentar agregar la nueva zona, por favor intente nuevamente.");
                }
                else
                    throw ex;
            }
        }
        public void ZModify(Zones zon)
        {
            try
            {
                Zones modZo = DbContextSingleton.TransporteContext.Zones.FirstOrDefault(z => z.IdZone == zon.IdZone);
                if (modZo != null)
                {
                    modZo.ZoneName = zon.ZoneName;
                    Validations.ZoneValidation(modZo);
                    DbContextSingleton.TransporteContext.SaveChanges();
                }
                else
                { 
                    throw new Exception("La zona que intenta modificar no es valida, intente nuevamente.");
                }
                DbContextSingleton.TransporteContext.Entry(modZo).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                DbContextSingleton.TransporteContext.Entry(zon).State = EntityState.Detached;
                throw ex;
            }
        }
        public void ZDelete(int zo)
        {
            try
            {
                Zones var = DbContextSingleton.TransporteContext.Zones.Where(z => z.IdZone == zo).FirstOrDefault();

                System.Data.SqlClient.SqlParameter _id = new System.Data.SqlClient.SqlParameter("@ID", zo);
                System.Data.SqlClient.SqlParameter _ret = new System.Data.SqlClient.SqlParameter("@ret", System.Data.SqlDbType.Int);
                _ret.Direction = System.Data.ParameterDirection.Output;
                DbContextSingleton.TransporteContext.Database.ExecuteSqlCommand("exec DeleteZone @ID, @ret output", _id, _ret);

                if ((int)_ret.Value == -1)
                    throw new Exception("No se encontro la zona, por favor intente nuevamente.");
                else if ((int)_ret.Value == -2)
                    throw new Exception("La zona tiene locales asignados en el sistema, no se puede realizar la baja.");
                else if ((int)_ret.Value == -3)
                    throw new Exception("Ocurrio un error interno al realizar la baja, por favor intente nuevamente mas tarde.");
                else
                    DbContextSingleton.TransporteContext.Entry(var).State = System.Data.Entity.EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Zones> ZoneListbyCity(int City)
        {
            try
            {
                List<Zones> zonesList = (from Z in DbContextSingleton.TransporteContext.Zones
                                         where Z.City == City
                                         orderby Z.ZoneName descending
                                         select Z).ToList();

                return zonesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
