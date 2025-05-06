namespace PSI_application_C__web
{
    public class Test_Astar
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_Astar_non_metro(int selector)
        {
            int[,] test_graphe = new int[0, 0];
            string corrrect_answer = "";
            switch (selector)
            {
                case 1:
                    test_graphe = new int[,]
                        { { 0, 2, 4, 0 },
                          { 0, 0, 3, 1 },
                          { 0, 0, 0, 1 },
                          { 0, 0, 0, 0 },
                        };
                    break;
                case 2:
                    test_graphe = new int[,]
                        { { 0, 4, 2, 0, 0 },
                          { 0, 0, 3, 2, 3 },
                          { 0, 1, 0, 4, 5 },
                          { 0, 0, 0, 0, 0 },
                          { 0, 0, 0, 1, 0 },
                        };
                    break;
                case 3:
                    test_graphe = new int[,]
                        { { 0, 0, 20, 0, 3 },
                          { 0, 0,  0, 0, 0 },
                          { 0, 4,  0, 0, 0 },
                          { 0, 1,  0, 0, 0 },
                          { 0, 0, 10, 2, 0 },
                        };
                    break;
                default: break;
            }
            Graphe test_Graphe = new Graphe(test_graphe);
            List<string> parcourt = test_Graphe.Astar("A", "D", true);
            string answer = Stringify(parcourt);
            switch (selector)
            {
                case 1:
                    corrrect_answer = "A ; B ; D";
                    break;
                case 2:
                    corrrect_answer = "A ; C ; B ; D";
                    break;
                case 3:
                    corrrect_answer = "A ; E ; D";
                    break;
                default: break;
            }
            Assert.AreEqual(corrrect_answer, answer);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_Astar_metro_Simple(int selector)
        {
            Graphe lesmetros = new Graphe();
            List<string> parcourt = new List<string>();
            switch (selector)
            {
                case 1: parcourt = lesmetros.Astar("Ternes2", "Argentine1", true);  
                    break;
                case 2: parcourt = lesmetros.Astar("Mairie des Lilas11", "Rue de la Pompe9", true); 
                    break;
                default: break;
            }
            string answer = Stringify(parcourt);
            Console.WriteLine(answer);
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void Test_Astar_metro_Harersine(int selector)
        {
            Graphe lesmetros = new Graphe();
            List<string> parcourt = new List<string>();
            switch (selector)
            {
                case 1:
                    parcourt = lesmetros.Astar("Ternes2", "Argentine1", false);
                    break;
                case 2:
                    parcourt = lesmetros.Astar("Mairie des Lilas11", "Rue de la Pompe9", false);
                    break;
                default: break;
            }
            string answer = Stringify(parcourt);
            Console.WriteLine(answer);
            Assert.Pass();
        }

        string Stringify(List<string> list)
        {
            string answer = "";
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    answer += list[i];
                }
                else
                {
                    answer += list[i] + " ; ";
                }
            }
            return answer;
        }
    }
}