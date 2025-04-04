using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;

namespace Aide_Theo_ProjetLivPar
{
    internal class Download_image
    {
        public static void Main(string[] args)
        {

        }
        
        public static void Download_the_image()
        {
            try
            {
                int identification = 0;
                string ligneDeConnexion = "SERVER=localhost;PORT=3306;DATABASE=test_image" +
                    "UID=root;PASSWORD=Hug182023AASQL*";
                MySqlConnection connection = new MySqlConnection(ligneDeConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception e)
            {

            }
        }
    }
}
