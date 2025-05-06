using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Noeud<T> : INoeud
    {
        #region Atributs
        public T Value;
        #endregion

        #region Methode 
        public Noeud(T value)
        {
            this.Value = value;
        }

        public string GetPK()
        {
            if (typeof(T) == typeof(string))
            {
                return Value as string;
            }
            else if (typeof(T) == typeof(Metro))
            {
                Metro metroValue = Value as Metro;
                return metroValue?.GetmetroPk();
            }
            else
            {
                throw new InvalidOperationException("Unsupported type for GetPK.");
            }
        }

        public float GetLatitude()
        {
            if(typeof(T) == typeof(Metro))
            {
                Metro metroValue = this.Value as Metro;
                return metroValue.Latitude; 
            }
            else
            {
                return 0.0f; 
            }
        }

        public float Getlongitude()
        {
            if (typeof(T) == typeof(Metro))
            {
                Metro metroValue = this.Value as Metro;
                return metroValue.Longitude;
            }
            else
            {
                return 0.0f;
            }
        }
        #endregion
    }

    public interface INoeud
    {
        float GetLatitude();
        float Getlongitude();
        string GetPK();
    }
}
