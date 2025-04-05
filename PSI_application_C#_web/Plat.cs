using MySql.Data.MySqlClient;

namespace PSI_application_C__web
{
    public class Plat
    {
        #region Attributs
        private int id_plat;
        private string nom_plat;
        private string type_plat;
        private int pr_cmb_de_personnes_plat;
        private double prix_par_portion_plat;
        private string date_fabrication_plat;
        private string date_perempetion_plat;
        private string nationalite_plat;
        private string regime_alimentaire_plat;
        private string ingredients_principaux_plat;
        private int id_cuisinier_plat;
        #endregion

        #region Constructeur
        public Plat(int id_plat, string nom_plat, string type_plat, int pr_cmb_de_personnes_plat, double prix_par_perso_plat,
                    string date_fabrication_plat, string date_perempetion_plat, string nationalite_plat, string regime_alimentaire_plat,
                    string ingredients_principaux_plat, int id_cuisinier_plat)
        {
            this.id_plat = id_plat;
            this.nom_plat = nom_plat;
            this.type_plat = type_plat;
            this.pr_cmb_de_personnes_plat = pr_cmb_de_personnes_plat;
            this.prix_par_portion_plat = prix_par_perso_plat;
            this.date_fabrication_plat = date_fabrication_plat;
            this.date_perempetion_plat = date_perempetion_plat;
            this.nationalite_plat = nationalite_plat;
            this.regime_alimentaire_plat = regime_alimentaire_plat;
            this.ingredients_principaux_plat = ingredients_principaux_plat;
            this.id_cuisinier_plat = id_cuisinier_plat;

        }
        #endregion

        #region Propriétés
        public int Id_plat
        {
            get { return this.id_plat; }
        }

        public string Nom_plat
        {
            get { return this.nom_plat; }
        }

        public string Type_plat
        {
            get { return this.type_plat; }
        }

        public int Pr_cmb_de_personnes_plat
        {
            get { return this.pr_cmb_de_personnes_plat; }
        }

        public double Prix_par_portion_plat
        {
            get { return this.prix_par_portion_plat; }
        }

        public string Date_fabrication_plat
        {
            get { return this.date_fabrication_plat; }
        }

        public string Date_perempetion_plat
        {
            get { return this.date_perempetion_plat; }
        }

        public string Nationalite_plat
        {
            get { return this.nationalite_plat; }
        }

        public string Regime_alimentaire_plat
        {
            get { return this.regime_alimentaire_plat; }
        }

        public string Ingredients_principaux_plat
        {
            get { return this.ingredients_principaux_plat; }
        }

        public int Id_cuisinier_plat
        {
            get { return this.id_cuisinier_plat; }
        }
        #endregion

        #region Méthodes
        public static int Identifiant_plat_determine_depuis_bdd()
        {
            try
            {
                int identifiant = 0;
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(ID_Plat) AS ID_plat_maximum FROM Plat;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_id_max = "";
                while (reader.Read())
                {
                    lecture_id_max = reader["ID_plat_maximum"].ToString();

                }
                if (String.IsNullOrEmpty(lecture_id_max))
                {
                    identifiant = 1;
                }
                else
                {
                    identifiant = int.Parse(lecture_id_max);
                    identifiant++;
                }
                reader.Close();
                connection.Close();
                return identifiant;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }

        public static void AjoutPlatBDD(Plat plat)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Plat (ID_Plat, Nom_plat, Type_Plat, Pr_cmb_de_personnes_Plat, Prix_par_portion_Plat, date_fabrication_plat, date_péremption_plat, Nationalité_cuisine_Plat, Régime_alimentaire_Plat, Ingrédients_principaux_Plat, ID_Cuisinier) VALUES (" +
                    plat.id_plat + ", '" +
                    plat.nom_plat + "', '" +
                    plat.type_plat + "', " +
                    plat.pr_cmb_de_personnes_plat + ", " +
                    plat.prix_par_portion_plat + ", '" +
                    plat.date_fabrication_plat + "', '" +
                    plat.date_perempetion_plat + "', '" +
                    plat.nationalite_plat + "', '" +
                    plat.regime_alimentaire_plat + "', '" +
                    plat.ingredients_principaux_plat + "', " +
                    plat.id_cuisinier_plat + ");";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void SupprimerPlat(Plat plat)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Plat WHERE ID_Plat = " + plat.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Récupère tous les plats enregistrés dans la bdd
        /// </summary>
        /// <returns>
        /// Une liste d'objets <see cref="Plat"/> représentant tous les plats stockés dans la table "Plat"
        /// Si une erreur survient lors de la lecture, une liste vide est retournée
        /// </returns>
        public static List<Plat> ObtenirTousLesPlats()
        {
            List<Plat> plats = new List<Plat>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Plat;";
                MySqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Je suis passé par là");
                while (reader.Read())
                {
                    
                    Plat plat = new Plat(
                        reader.GetInt32("ID_Plat"),
                        reader.GetString("Nom_plat"),
                        reader.GetString("Type_Plat"),
                        reader.GetInt32("Pr_cmb_de_personnes_Plat"),
                        reader.GetDouble("Prix_par_portion_Plat"),
                        reader.GetString("date_fabrication_plat"),
                        reader.GetString("date_péremption_plat"),
                        reader.GetString("Nationalité_cuisine_Plat"),
                        reader.GetString("Régime_alimentaire_Plat"),
                        reader.GetString("Ingrédients_principaux_Plat"),
                        reader.GetInt32("ID_Cuisinier")
                    );
                    plats.Add(plat);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return plats;
        }

        public static List<string> RechercherTousLesTuplesDuneColonne(string nom_colonne)
        {
            List<string> liste = new List<string>();
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT " + nom_colonne + " FROM Plat;";
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                string lecture_tuple = "";
                while (reader.Read())
                {
                    lecture_tuple = reader[nom_colonne].ToString();
                    if (!String.IsNullOrEmpty(lecture_tuple))
                    {
                        liste.Add(lecture_tuple);
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return liste;
        }


        public void MettreAjourAttributNom(string nouveau_nom)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Nom_plat = '" + nouveau_nom + "' WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributType(string nouveau_type)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Type_Plat = '" + nouveau_type + "' WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributPr_cmb_de_personnes(int nouveau_pr_cmb_de_personnes)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Pr_cmb_de_personnes_Plat = " + nouveau_pr_cmb_de_personnes + "WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributPrix_par_portion(double nouveau_prix_par_portion)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Prix_par_portion_Plat = " + nouveau_prix_par_portion + "WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributDate_fabrication(string nouveau_date_fabrication)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET date_fabrication_plat = '" + nouveau_date_fabrication + "' WHERE ID_Plat = " + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributDate_peremption(string nouveau_date_peremption)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET date_péremption_plat = '" + nouveau_date_peremption + "' WHERE ID_Plat = " + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributNationalite(string nouveau_nationalite)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Nationalité_cuisine_Plat = '" + nouveau_nationalite + "' WHERE ID_Plat = " + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributRegime_alimentaire(string nouveau_regime_alimentaire)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Régime_alimentaire_Plat = '" + nouveau_regime_alimentaire + "' WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }

        public void MettreAjourAttributIngredients_principaux(string nouveau_ingredients_principaux)
        {
            try
            {
                string ligneConnexion = "SERVER=localhost;PORT=3306;DATABASE=base_livin_paris;UID=root;PASSWORD=root";
                MySqlConnection connection = new MySqlConnection(ligneConnexion);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Plat SET Ingrédients_principaux_Plat = '" + nouveau_ingredients_principaux + "' WHERE ID_Plat =" + this.id_plat + ";";
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            { Console.WriteLine(e.ToString()); }
        }
        #endregion
    }
}
