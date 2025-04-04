namespace Aide_Theo_ProjetLivPar
{
    public class download_image_test
    {

        [TestCase(0)]
        [TestCase(1)]
        public async Task Recherche_coords(int selector)
        {
            string street = "";
            string ville = "";
            string code_postal = "";
            string pays = "";
            switch (selector)
            {
                case 0:
                    street = "12 rue Chartran";
                    ville = "Neuilly-sur-Seine";
                    code_postal = "92200";
                    pays = "France";
                    break;
                case 1:
                    street = "22 Sente des Châtaigniers";
                    ville = "Garches";
                    code_postal = "92380";
                    pays = "France";
                    break;
            }
            await Cherche_Coordonnée.GetCoords(street,ville,code_postal,pays);
            Assert.Pass();
        }
    }
}