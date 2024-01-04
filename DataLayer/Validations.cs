using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Validations
    {
        public static void EmpValidation(Employees emp)
        {
            //Patterns
            Regex idPattern = new Regex(@"^\d{8}$");
            Regex celPattern = new Regex(@"^\d{9}$");

            if (emp == null)
                throw new Exception("Debe ingresar datos validos.");
            if (!idPattern.IsMatch(emp.ID.ToString()))
                throw new Exception("Formato de cedula incorrecto");
            if (emp.EmpUser.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre de usuario valido, debe contener al menos 3 caracteres.");
            if (emp.EmpPassword.Trim().Length != 8)
                throw new Exception("Debe ingresar una contraseña valida, debe contener 8 caracteres.");
            if (!celPattern.IsMatch(emp.Celphone))
                throw new Exception("Debe ingresar un numero de celular valido, debe contener 9 numeros.");
            if (emp.EmpName.Trim().Length < 2 || emp.EmpName.Trim().Length > 50)
                throw new Exception("Debe ingresar un nombre valido, conteniendo entre 2 y 50 caracteres.");
            if (emp.EmpLastName.Trim().Length < 2 || emp.EmpLastName.Trim().Length > 50)
                throw new Exception("Debe ingresar un apellido valido, conteniendo entre 2 y 50 caracteres.");
            if (emp.EmpAddress.Trim().Length < 3 || emp.EmpAddress.Trim().Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
        }
        public static void LicenseValidation(Licences li)
        {
            //Patterns
            Regex idPattern = new Regex(@"^[A-Z]$");
            Regex idPattern2 = new Regex(@"^[A-Z][0-9]$");

            if (li == null)
                throw new Exception("Debe ingresar datos validos.");
            if (!idPattern.IsMatch(li.Category) && !idPattern2.IsMatch(li.Category))
                throw new Exception("Formato de categoría incorrecto");
            if (li.LicenceDescription == null || li.LicenceDescription.Trim().Length < 3)
                throw new Exception("Debe ingresar una breve descripcion de la categoria, debe contener al menos 3 caracteres.");
        }
        public static void RoleValidation(Roles Ro)
        {
            //Patterns
            Regex idpatern = new Regex(@"[A-Z]{3}");

            if (Ro == null)
                throw new Exception("Debe ingresar datos validos.");
            if (!idpatern.IsMatch(Ro.Code))
                throw new Exception("Formato de rol incorrecto, debe incluir 3 letras mayusculas.");
            if (Ro.RolesDescription == null || Ro.RolesDescription.Trim().Length < 3)
                throw new Exception("Debe ingresar una breve descripcion del rol, debe contener al menos 3 caracteres.");
        }
        public static void StateValidation(States st)
        {
            if (st == null)
                throw new Exception("Debe ingresar datos validos.");
            if (st.StateName.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre valido, debe contener al menos 3 caracteres.");
        }
        public static void CityValidation(Cities ct)
        {
            if (ct == null)
                throw new Exception("Debe ingresar datos validos.");
            if (ct.CityName.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre valido, debe contener al menos 3 caracteres.");
        }
        public static void ZoneValidation(Zones zn)
        {
            if (zn == null)
                throw new Exception("Debe ingresar datos validos.");
            if (zn.ZoneName.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre valido, debe contener al menos 3 caracteres.");
            if (zn.ZoneShape.Length < 3)
                throw new Exception("Una zona debe contar con al menos 3 puntos que la delimiten.");
        }
        public static void BranchOValidation(BranchOffices bo)
        {
            //Pattern
            Regex PhonePattern = new Regex(@"^\d{8}$");

            if (bo == null)
                throw new Exception("Debe ingresar datos validos.");
            if (bo.BranchAddress.Trim().Length < 3 || bo.BranchAddress.Trim().Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
            /*if (!(bo.MarkerLocation != null))
                throw new Exception("Debe ingresar un marcador.");*/
            if (!PhonePattern.IsMatch(bo.Phone))
                throw new Exception("Debe ingresar un numero de telefono valido, debe contener 8 numeros.");
        }
        public static void VConditionValidation(VehiclesConditions ve)
        {
            if (ve == null)
                throw new Exception("Debe ingresar datos validos.");
            if (ve.CondName.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre valido, debe contener al menos 3 caracteres.");
        }
        public static void VehicleValidation(Vehicles v)
        {
            //Patterns
            Regex platePattern = new Regex(@"^[A-Z][A-Z][A-Z][0-9][0-9][0-9][0-9]$");
            Regex registrationPattern = new Regex(@"^\d{4,}$");

            if (v == null)
                throw new Exception("Debe ingresar datos validos.");
            if (!platePattern.IsMatch(v.Plate))
                throw new Exception("Formato de matricula incorrecto, debe estar compuesto de 3 letras y 4 numeros.");
            if (!registrationPattern.IsMatch(v.vRegistration))
                throw new Exception("Formato de padron incorrecto, debe tener al menos 4 numeros.");
            if (v.BrandModel.Trim().Length < 3)
                throw new Exception("Debe ingresar un nombre y un modelo valido, debe contener al menos 3 caracteres.");
            if (v.VehicleWeight < 0)
                throw new Exception("Debe ingresar una capacidad valida.");
        }
        public static void PackageTValidation(PackageTypes pt)
        {
            if (pt == null)
                throw new Exception("Debe ingresar datos validos.");
            if (pt.MinWeight < 0 || pt.MaxWeight < 0 || pt.MaxWeight < pt.MinWeight)
                throw new Exception("Debe ingresar un rango de peso valido.");
            if (pt.Amount <= 0)
                throw new Exception("Debe ingresar una monto valido.");
        }
        public static void CustomersValidation(Customers cu)
        {
            //Patterns
            Regex IDPattern = new Regex(@"^\d{8}$");
            Regex RUTPattern = new Regex(@"^\d{12}$");
            Regex celPattern = new Regex(@"^\d{9}$");

            if (cu == null)
                throw new Exception("Debe ingresar datos validos.");
            if (!IDPattern.IsMatch(cu.DocRut.ToString()) && !RUTPattern.IsMatch(cu.DocRut.ToString()))
                throw new Exception("Formato de documento incorrecto, si es una cedula de identidad debe contener 8 digitos y si se trata de un RUT 12 digitos.");
            if (cu.CustomerName.Trim().Length < 2 || cu.CustomerName.Trim().Length > 50)
                throw new Exception("Debe ingresar un nombre valido, conteniendo entre 2 y 50 caracteres.");
            if (cu.CLastName.Trim().Length < 2 || cu.CLastName.Trim().Length > 50)
                throw new Exception("Debe ingresar un apellido valido, conteniendo entre 2 y 50 caracteres.");
            if (cu.CliAddress.Trim().Length < 3 || cu.CliAddress.Trim().Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
            if (!celPattern.IsMatch(cu.Celphone))
                throw new Exception("Debe ingresar un numero de celular valido, debe contener 9 numeros.");
        }
        public static void DropOffValidation(DropOffPackages dop)
        {
            //Patterns
            Regex celPattern = new Regex(@"^\d{9}$");

            if (dop == null)
                throw new Exception("Debe ingresar datos validos.");
            if (dop.Shippments.Recipient.Trim().Length < 2 || dop.Shippments.Recipient.Trim().Length > 50)
                throw new Exception("Debe ingresar un nombre valido, conteniendo entre 2 y 50 caracteres.");
            if (!celPattern.IsMatch(dop.Shippments.RecipientCel))
                throw new Exception("Debe ingresar un numero de celular valido, debe contener 9 numeros.");
            if (dop.Shippments.TargetAddress.Trim().Length < 3 || dop.Shippments.TargetAddress.Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
            if (!(dop.Shippments.Latitude >= -90 && dop.Shippments.Latitude <= 90))
                throw new Exception("La latitud ingresada no es valida.");
            if (!(dop.Shippments.Longitude >= -180 && dop.Shippments.Longitude <= 180))
                throw new Exception("La longitud ingresada no es valida.");
        }
        public static void HomePickupValidation(HomePickups hpu)
        {
            //Patterns
            Regex celPattern = new Regex(@"^\d{9}$");

            if (hpu == null)
                throw new Exception("Debe ingresar datos validos.");
            if (hpu.Shippments.Recipient.Trim().Length < 2 || hpu.Shippments.Recipient.Trim().Length > 50)
                throw new Exception("Debe ingresar un nombre valido, conteniendo entre 2 y 50 caracteres.");
            if (!celPattern.IsMatch(hpu.Shippments.RecipientCel))
                throw new Exception("Debe ingresar un numero de celular valido, debe contener 9 numeros.");
            if (hpu.Shippments.TargetAddress.Trim().Length < 3 || hpu.Shippments.TargetAddress.Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
            if (!(hpu.Shippments.Latitude >= -90 && hpu.Shippments.Latitude <= 90))
                throw new Exception("La latitud ingresada no es valida.");
            if (!(hpu.Shippments.Longitude >= -180 && hpu.Shippments.Longitude <= 180))
                throw new Exception("La longitud ingresada no es valida.");
            if (hpu.StartTime < hpu.EndTime)
                throw new Exception("Debe ingresar un rango horario valido.");
            if (hpu.Shippments.TargetAddress.Trim().Length < 3 || hpu.Shippments.TargetAddress.Length > 50)
                throw new Exception("Debe ingresar una direccion valida, entre 3 y 50 caracteres.");
            //Latitud y longitud de el retiro
            if (!(hpu.Latitude >= -90 && hpu.Latitude <= 90))
                throw new Exception("La latitud ingresada no es valida.");
            if (!(hpu.Longitude >= -180 && hpu.Longitude <= 180))
                throw new Exception("La longitud ingresada no es valida.");
        }
        public static void PackageValidation(Packages pkg)
        {
            if (pkg == null)
                throw new Exception("Debe ingresar datos validos.");
            if (pkg.NOfPackages <= 0)
                throw new Exception("Debe ingresar una cantidad valida de paquetes.");
        }
        public static void StageValidation(Stages stg)
        {
            if (stg == null)
                throw new Exception("Debe ingresar datos validos.");
            if (stg.StageDescription.Trim().Length < 2)
                throw new Exception("Debe ingresar un nombre valido, conteniendo al menos 2 caracteres.");
        }
        public static void ShStageValidation(ShippmentStages sstg)
        {
            if (sstg == null)
                throw new Exception("Debe ingresar datos validos.");
        }
    }
}
