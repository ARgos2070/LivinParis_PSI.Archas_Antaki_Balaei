using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using static PSI_application_C__web.Adresse_a_coordonees;

namespace PSI_application_C__web
{
    public class Graphe
    {
        #region Atributs
        int[,] matrice_dadjacence;
        Dictionary<int, INoeud> list_noeud;
        Dictionary<string, int> list_pk;
        string path_cp; //popule_table_connection_pied_txt
        string path_ct; //popule_table_connection_train_txt
        string path_s;  //popule_table_station_txt
        #endregion

        #region Methodes
        public Graphe()
        {
            if (!File.Exists("popule_table_connection_pied_txt.txt"))
            {
                Console.WriteLine("manque : popule_table_connection_pied_txt");
            }
            this.path_cp = "popule_table_connection_pied_txt.txt";
            if (!File.Exists("popule_table_connection_train_txt.txt"))
            {
                Console.WriteLine("manque : popule_table_connection_train_txt");
            }
            this.path_ct = "popule_table_connection_train_txt.txt";
            if (!File.Exists("popule_table_station_txt.txt"))
            {
                Console.WriteLine("manque : popule_table_station_txt");
            }
            this.path_s = "popule_table_station_txt.txt";
            this.list_noeud = new Dictionary<int, INoeud>();
            this.list_pk = new Dictionary<string, int>();
            string ligne;
            try
            {
                using (StreamReader lecteur = new StreamReader(path_s))
                {
                   
                    int count= 0;
                    while ((ligne = lecteur.ReadLine()) != null)
                    {
                        Match match = Regex.Match(ligne, @"\(\""([^\""]+)\"",\s*(\d+\.\d+),\s*(\d+\.\d+),(\d+)\)");
                        if (match.Success)
                        {
                            string nom_gare = match.Groups[1].Value;
                            float latitude = float.Parse(match.Groups[2].Value);
                            float longitude = float.Parse(match.Groups[3].Value); ;
                            int ligne_metro = int.Parse(match.Groups[4].Value); ;
                            Metro un_metro = new Metro(nom_gare, latitude, longitude, ligne_metro);
                            this.list_noeud.Add(count, new Noeud<Metro>(un_metro));
                            this.list_pk.Add(nom_gare + ligne_metro, count);                            
                            count++;
                        }

                    }
                }
                this.matrice_dadjacence = new int[list_noeud.Count, list_noeud.Count];
                using (StreamReader lecteur = new StreamReader(path_ct))
                {
                    ligne = "";
                    int ligne_metro = 0;
                    while ((ligne = lecteur.ReadLine()) != null)
                    {
                        Match matchM = Regex.Match(ligne, @"\(""([^""]+)"", ""([^""]+)"", (\d+), (\d+)\),?");
                        Match match_metro = Regex.Match(ligne, @"-- metro (\d+)");
                        if (matchM.Success)
                        {
                            string gare_depart = matchM.Groups[1].Value;
                            string gare_arrivee = matchM.Groups[2].Value;
                            bool orientee = true;
                            if (int.Parse(matchM.Groups[3].Value) == 0)
                            {
                                orientee = false;
                            }
                            int temps = int.Parse(matchM.Groups[4].Value);

                            matrice_dadjacence[
                                this.list_pk[gare_depart + ligne_metro],
                                this.list_pk[gare_arrivee+ ligne_metro]] = temps;
                            if (orientee == false)
                            {
                                matrice_dadjacence[
                                    this.list_pk[gare_arrivee + ligne_metro],
                                    this.list_pk[gare_depart + ligne_metro]] = temps;
                            }
                        }
                        else if (match_metro.Success)
                        {
                            ligne_metro = int.Parse(match_metro.Groups[1].Value);
                        }
                    }
                }
                using (StreamReader lecteur = new StreamReader(path_cp))
                {
                    ligne = "";
                    while ((ligne = lecteur.ReadLine()) != null)
                    {
                        Match match = Regex.Match(ligne, @"\(""([^""]+)"",(\d+),(\d+), (\d+)\)");
                        if (match.Success)
                        {
                            string gare_de_liaison = match.Groups[1].Value;
                            int ligne_metor_entree = int.Parse(match.Groups[2].Value);
                            int ligne_metor_sortie = int.Parse(match.Groups[3].Value);
                            int temps = int.Parse(match.Groups[4].Value);
                            //toujours non orientee
                            matrice_dadjacence[
                               this.list_pk[gare_de_liaison + ligne_metor_entree],
                               this.list_pk[gare_de_liaison + ligne_metor_sortie]] = temps;
                            matrice_dadjacence[
                                this.list_pk[gare_de_liaison + ligne_metor_sortie],
                                this.list_pk[gare_de_liaison + ligne_metor_entree]] = temps;
                        }

                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public Graphe(int[,] test_graph)
        {
            this.matrice_dadjacence = test_graph;
            int ascii_code;
            this.list_noeud = new Dictionary<int, INoeud>();
            this.list_pk = new Dictionary<string, int>();
            for(int i = 0; i < matrice_dadjacence.GetLength(0); i++)
            {
                ascii_code = 65 + i;
                list_noeud.Add(i, new Noeud<string>(Convert.ToString((char)ascii_code)));
                list_pk.Add(Convert.ToString((char)ascii_code), i);
            }
            this.path_cp = ""; 
            this.path_ct = "";
            this.path_s = "";
        }

        public async Task<List<string>> Chemin_le_plus_court(string street_depart,string ville_depart,string code_postal_depart,string pays_depart,
            string street_arrivee, string ville_arrivee, string code_postal_arrivee, string pays_arrivee)
        {
            GeocodingResult coords_depart = await Adresse_a_coordonees.GetCoordsGeocodingResult(street_depart, ville_depart, code_postal_depart, pays_depart);
            GeocodingResult coords_arrivee = await Adresse_a_coordonees.GetCoordsGeocodingResult(street_arrivee, ville_arrivee, code_postal_arrivee, pays_arrivee);

            string pk_station_depart = Station_la_plus_proche(coords_depart.Latitude, coords_depart.Longitude, true);
            string pk_station_arrivee = Station_la_plus_proche(coords_arrivee.Latitude, coords_arrivee.Longitude, true);

            return Dijikstra(pk_station_depart, pk_station_arrivee);
        }

            public async Task<int> Temps_le_plus_court(string street_depart, string ville_depart, string code_postal_depart, string pays_depart,
        string street_arrivee, string ville_arrivee, string code_postal_arrivee, string pays_arrivee)
        {
            GeocodingResult coords_depart = await Adresse_a_coordonees.GetCoordsGeocodingResult(street_depart, ville_depart, code_postal_depart, pays_depart);
            GeocodingResult coords_arrivee = await Adresse_a_coordonees.GetCoordsGeocodingResult(street_arrivee, ville_arrivee, code_postal_arrivee, pays_arrivee);

            string pk_station_depart = Station_la_plus_proche(coords_depart.Latitude, coords_depart.Longitude, true);
            string pk_station_arrivee = Station_la_plus_proche(coords_arrivee.Latitude, coords_arrivee.Longitude, true);

            List<string> resultat = Dijikstra(pk_station_depart, pk_station_arrivee);
            int temps_total = 0;

            for(int i = 0; i < resultat.Count-1 ; i++)
            {
                temps_total += this.matrice_dadjacence[this.list_pk[resultat[i]], this.list_pk[resultat[i + 1]]];
            }
            return temps_total;
        }

        public List<string> Dijikstra(string IdNoeudDepart, string IdNoeudArrivee) // ID = nom_gare + ligne_metro
        {
            int noeud_a_etudier = -1;
            int valeur_trouver;
            List<string> Parcourt = new List<string>();
            List<int> noeuds = this.list_noeud.Keys.ToList();
            Dictionary<int, int> min_distance = new Dictionary<int, int>();
            Dictionary<int, string> noeud_précédent = new Dictionary<int, string>();
            for (int i = 0; i < list_pk.Count; i++)
            {
                if (i != list_pk[IdNoeudDepart])
                {
                    min_distance.Add(noeuds[i], int.MaxValue);
                }
                else
                {
                    min_distance.Add(noeuds[i], 0);
                }
                noeud_précédent.Add(noeuds[i], "");
            }
            while (noeud_a_etudier != list_pk[IdNoeudArrivee])
            {

                noeud_a_etudier = Plus_petit_noeud_Dijkstra(min_distance, noeuds);
                for (int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
                {
                    if ((i != noeud_a_etudier) && matrice_dadjacence[noeud_a_etudier, i] != 0)
                    {
                        valeur_trouver = min_distance[noeud_a_etudier] + this.matrice_dadjacence[noeud_a_etudier, i];
                        if (valeur_trouver < min_distance[i])
                        {
                            min_distance[i] = valeur_trouver;
                            noeud_précédent[i] = this.list_noeud[noeud_a_etudier].GetPK();
                        }
                    }
                }
                noeuds.Remove(noeud_a_etudier);
            }
            string noeud_dans_parcourt = IdNoeudArrivee;
            while (noeud_dans_parcourt != IdNoeudDepart)
            {
                Parcourt.Add(noeud_dans_parcourt);
                noeud_dans_parcourt = noeud_précédent[list_pk[noeud_dans_parcourt]];
            }
            Parcourt.Add(noeud_dans_parcourt);
            return Parcourt;
        }

        private int Plus_petit_noeud_Dijkstra(Dictionary<int, int> min_distance, List<int> noeuds)
        {
            int min = 0;
            int start = 1;
            while (!noeuds.Contains(min))
            {
                min++;
                start++;
            }
            for(int i = start; i < min_distance.Count; i++)
            {
                if (min_distance[min] > min_distance[i] && noeuds.Contains(i))
                {
                    min = i;
                }
            }
            return min;
        }

        public List<string> Bellman_Ford(string IdNoeudDepart, string IdNoeudArrivee)
        {
            int valeur_trouver;
            bool repeat = false;
            int count = 0;
            List<string> Parcourt = new List<string>();
            Dictionary<int, int> min_distance = new Dictionary<int, int>();
            Dictionary<int, string> noeud_précédent = new Dictionary<int, string>();
            for (int i = 0; i < list_pk.Count; i++)
            {
                if (i != list_pk[IdNoeudDepart])
                {
                    min_distance.Add(i, int.MaxValue);
                }
                else
                {
                    min_distance.Add(i, 0);
                }
                noeud_précédent.Add(i, "");
            }
            while (repeat == false && count < min_distance.Count - 1)
            {
                repeat = true; 
                for (int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
                {
                    for (int j = 0; j < this.matrice_dadjacence.GetLength(1); j++)
                    {
                        if ((j != i) && matrice_dadjacence[i, j] != 0)
                        { 
                            valeur_trouver = min_distance[i] + this.matrice_dadjacence[i, j];
                            if (valeur_trouver < min_distance[j] && valeur_trouver > 0)
                            {
                                min_distance[j] = valeur_trouver;
                                noeud_précédent[j] = this.list_noeud[i].GetPK();
                                repeat = false; 
                            }
                        }
                    }
                }
                count++; 
            }
            string noeud_dans_parcourt = IdNoeudArrivee;
            while (noeud_dans_parcourt != IdNoeudDepart)
            {
                Parcourt.Add(noeud_dans_parcourt);
                noeud_dans_parcourt = noeud_précédent[list_pk[noeud_dans_parcourt]];
            }
            Parcourt.Add(noeud_dans_parcourt);
            return Parcourt;
        }

        public List<string> Astar(string IdNoeudDepart, string IdNoeudArrivee, bool Simple)
        {
            int noeud_a_etudier = -1;
            int valeur_trouver;
            List<string> Parcourt = new List<string>();
            List<int> noeuds = this.list_noeud.Keys.ToList();
            Dictionary<int, int> min_distance = new Dictionary<int, int>();
            Dictionary<int, string> noeud_précédent = new Dictionary<int, string>();
            for (int i = 0; i < list_pk.Count; i++)
            {
                if (i != list_pk[IdNoeudDepart])
                {
                    min_distance.Add(noeuds[i], int.MaxValue);
                }
                else
                {
                    min_distance.Add(noeuds[i], 0);
                }
                noeud_précédent.Add(noeuds[i], "");
            }
            while (noeud_a_etudier != list_pk[IdNoeudArrivee])
            {

                noeud_a_etudier = Plus_petit_noeud_Astar(min_distance, noeuds, IdNoeudArrivee, Simple);
                for (int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
                {
                    if ((i != noeud_a_etudier) && matrice_dadjacence[noeud_a_etudier, i] != 0)
                    {
                        valeur_trouver = min_distance[noeud_a_etudier] + this.matrice_dadjacence[noeud_a_etudier, i];
                        if (valeur_trouver < min_distance[i])
                        {
                            min_distance[i] = valeur_trouver;
                            noeud_précédent[i] = this.list_noeud[noeud_a_etudier].GetPK();
                        }
                    }
                }
                noeuds.Remove(noeud_a_etudier);
            }
            string noeud_dans_parcourt = IdNoeudArrivee;
            while (noeud_dans_parcourt != IdNoeudDepart)
            {
                Parcourt.Add(noeud_dans_parcourt);
                noeud_dans_parcourt = noeud_précédent[list_pk[noeud_dans_parcourt]];
            }
            Parcourt.Add(noeud_dans_parcourt);
            return Parcourt;
        }

        private int Plus_petit_noeud_Astar(Dictionary<int, int> min_distance, 
            List<int> noeuds, string IdNoeudArrivee, bool Simple)
        { 
            int min = 0;
            int start = 1;
            double distance_entre_point = 0;
            while (!noeuds.Contains(min))
            {
                min++;
                start++;
            }
            for (int i = start; i < min_distance.Count; i++)
            {
                if (Simple)
                {
                    distance_entre_point = this.DistanceNoeud_Simple(i, IdNoeudArrivee);
                }
                else
                {
                    distance_entre_point = this.DistanceNoeud_Haversine(i, IdNoeudArrivee);
                }
                if (min_distance[min] > min_distance[i] + distance_entre_point
                    && noeuds.Contains(i))
                {
                    min = i;
                }
            }
            return min;
        }

        public double DistanceNoeud_Haversine(int i, string IdNoeudArrivee = "", double Latitude_arrivee = 0,
            double longitude_arrivee = 0)
        {
            const double Rayon_terre = 6371.0;
            double Latitude_depart = degree_to_radian(this.list_noeud[i].GetLatitude());
            double longitude_depart = degree_to_radian(this.list_noeud[i].Getlongitude());
            if (IdNoeudArrivee != "")
            {
                int nb_arrivee = this.list_pk[IdNoeudArrivee];
                Latitude_arrivee = degree_to_radian(this.list_noeud[nb_arrivee].GetLatitude());
                longitude_arrivee = degree_to_radian(this.list_noeud[nb_arrivee].Getlongitude());
            }
            else
            {
                Latitude_arrivee = degree_to_radian(Latitude_arrivee);
                longitude_arrivee = degree_to_radian(longitude_arrivee);
            }
            double difference_latitude = Latitude_arrivee - Latitude_depart;
            double difference_longitude = longitude_arrivee - longitude_depart;

            double sin_carre_Latitude = Math.Sin(difference_latitude / 2) * 
                Math.Sin(difference_latitude / 2);
            double sin_carre_longitutde = Math.Sin(difference_longitude / 2) *
                Math.Sin(difference_longitude / 2);
            double Dans_le_racine = sin_carre_Latitude + Math.Cos(Latitude_arrivee) *
                Math.Cos(Latitude_depart) * sin_carre_longitutde;

            double distance = 2 * Rayon_terre * Math.Asin(Math.Sqrt(Dans_le_racine));

            return distance;
        }

        public double DistanceNoeud_Simple(int i, string IdNoeudArrivee = "", double Latitude_arrivee = 0,
            double longitude_arrivee = 0)
        {
            const double Rayon_terre = 6371.0;
            double Latitude_depart = degree_to_radian(this.list_noeud[i].GetLatitude());
            double longitude_depart = degree_to_radian(this.list_noeud[i].Getlongitude());
            if (IdNoeudArrivee != "")
            {
                int nb_arrivee = this.list_pk[IdNoeudArrivee];
                Latitude_arrivee = degree_to_radian(this.list_noeud[nb_arrivee].GetLatitude());
                longitude_arrivee = degree_to_radian(this.list_noeud[nb_arrivee].Getlongitude());
            }
            else
            {
                Latitude_arrivee = degree_to_radian(Latitude_arrivee);
                longitude_arrivee = degree_to_radian(longitude_arrivee);
            }
            double difference_latitude = Math.Abs(Latitude_arrivee - Latitude_depart);
            double difference_longitude = Math.Abs(longitude_arrivee - longitude_depart);
            return difference_latitude + difference_longitude;
        }

        static public double degree_to_radian(double degree)
        {
            return (degree * Math.PI) / 180.0;
        }

        public string Station_la_plus_proche(double Latitude_address, double longitude_address,
            bool Simple)
        {
            int station_min = 0;
            double distance_min = double.MaxValue;
            double distance = 0.0;
            for(int i = 0; i<list_pk.Count; i++)
            {
                if (Simple)
                {
                    distance = DistanceNoeud_Simple(
                        i: i, 
                        Latitude_arrivee: Latitude_address, 
                        longitude_arrivee: longitude_address) ;
                }
                else
                {
                    distance = DistanceNoeud_Haversine(
                        i: i,
                        Latitude_arrivee: Latitude_address,
                        longitude_arrivee: longitude_address);
                }
                if(distance < distance_min)
                {
                    distance_min = distance;
                    station_min = i;
                }
            }
            return this.list_noeud[station_min].GetPK();
        }

        public void Affichage_Matrice()
        {
            for(int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrice_dadjacence.GetLength(1); j++)
                {
                    Console.Write(this.matrice_dadjacence[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Affichage_Noeud()
        {
            for(int i = 0; i < this.list_noeud.Count; i++)
            {
                Console.Write(list_noeud[i].GetPK() + " ");
            }
            Console.WriteLine();
        }

        public void AfficherListInt(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + "; ");
            }
            Console.WriteLine();
        }

        public void AfficherListString(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + "; ");
            }
            Console.WriteLine();
        }

        public void AfficherDictionnaireInt(Dictionary<int, int> dictionaire)
        {
            for (int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
            {
                Console.Write(i + " :" + dictionaire[i] + "; ");
            }
            Console.WriteLine();
        }

        public void AfficherDictionnaireIntString(Dictionary<int, string> dictionaire)
        {
            for (int i = 0; i < this.matrice_dadjacence.GetLength(0); i++)
            {
                Console.Write(i + " :" + dictionaire[i] + "; ");
            }
            Console.WriteLine();
        }
        #endregion

        #region Propriété

        #endregion
    }
}
