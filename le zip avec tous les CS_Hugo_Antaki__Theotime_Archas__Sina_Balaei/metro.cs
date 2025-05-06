using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_application_C__web
{
    public class Metro
    {
        #region Atributs
        string nom_gare;
        float latitude;
        float longitude;
        int ligne_metro;
        #endregion

        public Metro(string nom_gare, float latitude, float longitude, int ligne_metro)
        {
            this.nom_gare = nom_gare;
            this.latitude = latitude;
            this.longitude = longitude;
            this.ligne_metro = ligne_metro;
        }

        public string GetmetroPk()
        {
            return this.nom_gare + this.ligne_metro;
        }

        public float Latitude { get { return this.latitude; } }
        public float Longitude{ get { return this.longitude; } }
    }
}
