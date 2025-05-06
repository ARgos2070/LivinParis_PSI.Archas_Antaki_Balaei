namespace PSI_application_C__web
{
    public class Test_Additionel
    {
        [Test]
        public void Test_affichage_tableau_test()
        {
            int[,] test_graphe = new int[,]
                        { { 0, 2, 4, 0 },
                          { 0, 0, 3, 1 },
                          { 0, 0, 0, 1 },
                          { 0, 0, 0, 0 },
                        };
            Graphe test_Graphe = new Graphe(test_graphe);
            test_Graphe.Affichage_Matrice();
            test_Graphe.Affichage_Noeud();
            Assert.Pass();
        }

        [TestCase(30)]
        [TestCase(60)]
        public void Test_convertisseur_radian(int selector)
        {
            Console.WriteLine(Graphe.degree_to_radian(selector));
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_distance_Id_Final_Simple(int selector)
        {
            Graphe lesmetros = new Graphe();
            switch (selector)
            {
                case 1: Console.WriteLine(lesmetros.DistanceNoeud_Simple(24,"Pont de Neuilly1"));
                    break;//aproximativement 1.8Km
                case 2: Console.WriteLine(lesmetros.DistanceNoeud_Simple(24,"Argentine1"));
                    break;//approximativement 4.2Km
                    default:break;
            }
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_distance_pos_final_Simple(int selector)
        {
            Graphe lesmetros = new Graphe();
            switch (selector)
            {
                case 1:
                    Console.WriteLine(lesmetros.DistanceNoeud_Simple(
                        i :24, 
                        Latitude_arrivee : 48.895763,
                        longitude_arrivee : 2.236315));
                    break;//aproximativement 1.8Km
                case 2:
                    Console.WriteLine(lesmetros.DistanceNoeud_Simple(
                        i : 21,
                        Latitude_arrivee: 48.886315,
                        longitude_arrivee: 2.263549));
                    break;//approximativement 4.2Km
                default: break;
            }
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_distance_Id_Final_Haversine(int selector)//failure les deux sont nuls
        {
            Graphe lesmetros = new Graphe();
            switch (selector)
            {
                case 1:
                    Console.WriteLine(lesmetros.DistanceNoeud_Haversine(24, "Pont de Neuilly1"));
                    break;//aproximativement 1.8Km
                case 2:
                    Console.WriteLine(lesmetros.DistanceNoeud_Haversine(24, "Argentine1"));
                    break;//approximativement 4.2Km
                default: break;
            }
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_distance_pos_final_Haversine(int selector)
        {
            Graphe lesmetros = new Graphe();
            switch (selector)
            {
                case 1:
                    Console.WriteLine(lesmetros.DistanceNoeud_Haversine(
                        i: 24,
                        Latitude_arrivee: 48.895763,
                        longitude_arrivee: 2.236315));
                    break;//aproximativement 1.8Km
                case 2:
                    Console.WriteLine(lesmetros.DistanceNoeud_Haversine(
                        i: 21,
                        Latitude_arrivee: 48.886315,
                        longitude_arrivee: 2.263549));
                    break;//approximativement 4.2Km
                default: break;
            }
            Assert.Pass();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Test_Station_la_plus_proche(bool Simple)
        {
            Graphe lesmetros = new Graphe();
            Console.WriteLine(lesmetros.Station_la_plus_proche(48.886365, 2.263476, Simple));
            Assert.Pass();
        }
    }
}