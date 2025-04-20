USE base_livin_paris;

-- Afin de mieux se retrouver parmi tous les identifiants qui sont des entiers (même si les identifiants sont chacun ceux d'une table, 
-- donc ils sont propres à chaque table), on a fait un système pour mieux s'y retrouver quand on lit les données, quand l'identifiant 
-- commence par un certain chiffre, cela signifie que cet idientifiant represente une certaine table (comme on a peu de données, ce
-- système suffit pour mieux se retrouver), il n'a évidamment aucune incidence sur le fonctionnement de la base. Ainsi:
-- 1 (Utilisateur), 2 (Client), 3 (Cuisinier), 4 (Livreur), 5 (Plat), 6 (Commande), 7 (Livraison), 8 (Commentaire)

-- Insertion des utilisateurs
INSERT INTO Utilisateur (ID_utilisateur, Mot_de_passe_utilisateur, Nom_utilisateur, Prénom_utilisateur, Adresse_utilisateur, Num_tel_utilisateur, adresse_mail_utilisateur, Utilisateur_est_entreprise, Nom_entreprise, Nbre_signalements_contre_utilisateur)
VALUES
('user_medhy', 'pass123', 'Durand', 'Medhy', '15 Rue Cardinet, Paris', '0690123456', 'medhy.durand@email.com', FALSE, NULL, 0),
('user_sophie', 'pass456', 'Martin', 'Sophie', '20 Avenue Foch, Paris', '0601234567', 'sophie.martin@email.com', FALSE, NULL, 0),
('user_luc', 'pass789', 'Bernard', 'Luc', '30 Avenue des Champs-Elysées, Paris', '0612345678', 'luc.bernard@email.com', FALSE, NULL, 0),
('user_emma', 'pass147', 'Robert', 'Emma', '40 Rue test, Paris', '0623456789', 'emma.robert@email.com', FALSE, NULL, 0),
('user_paul', 'pass258', 'Durand', 'Paul', '50 Rue test, Paris', '0634567890', 'paul.durand@email.com', FALSE, NULL, 0),
('user_alice', 'pass369', 'Lemoine', 'Alice', '60 Rue test2, Paris', '0645678901', 'alice.lemoine@email.com', FALSE, NULL, 0),
('user_louis', 'pass951', 'Morel', 'Louis', '70 Rue test2, Paris', '0656789012', 'louis.morel@email.com', FALSE, NULL, 0),
('user_julie', 'pass753', 'Blanc', 'Julie', '80 Rue test3, Paris', '0656789012', 'julie.blanc@email.com', FALSE, NULL, 0),
('user_antoine', 'pass852', 'Garcia', 'Antoine', '90 Rue test3, Paris', '0678901234', 'antoine.garcia@email.com', FALSE, NULL, 0),
('user_nina', 'pass963', 'Fischer', 'Nina', '100 Rue test4, Paris', '0689012345', 'nina.fischer@email.com', FALSE, NULL, 0),
('user_ali', 'pass234', 'Mohammed', 'Ali', '110 Rue test4, Paris', '1234567890', 'ali.mohammed@boxeentreprise.com', TRUE, 'Boxe entreprise', 0),
('user_steve', 'pass654', 'Jobs', 'Steve', '112 Rue test5, Paris', '0678561289', 'steve.jobs@apple.com', TRUE, 'Apple Corporation', 0),
('user_charles', 'pass4', 'Leclerc', 'Charles', '152 Rue test5', '0645788956', 'charles.leclerc@restaurantleclerc.com', TRUE, 'Restaurant Leclerc', 0),
('user_giovanni', 'pass789', 'Maestria', 'Giovanni', '852 Rue test6', '0678946512', 'giovanni.maestria@pizzamama.com', TRUE, 'Les pizzas de la mama', 0);

-- Insertion des clients
INSERT INTO Client (ID_Client, ID_utilisateur, Nbre_commandes_passees_client)
VALUES
(201, 'user_medhy', 0),
(202, 'user_sophie', 0),
(203, 'user_luc', 0),
(204, 'user_emma', 0),
(205, 'user_paul', 0),
(206, 'user_ali', 0),
(207, 'user_steve', 0);

-- Insertion des cuisiniers
INSERT INTO Cuisinier (ID_Cuisinier, Nbre_plat_propose_Cuisinier, Plat_du_jour_Cuisinier, Nbre_commandes_cuisinees_cuisinier, ID_utilisateur)
VALUES
(301, 10, 'Pizza végétarienne', 0, 'user_alice'),
(302, 15, 'Sushi', 0, 'user_louis'),
(303, 8, 'Tacos', 0, 'user_julie'),
(304, 10, 'Tacos', 0, 'user_charles'),
(305, 8, 'Pizza marguarita', 0, 'user_giovanni');

-- Insertion des livreurs
INSERT INTO Livreur (ID_Livreur, Gain_Livreur, Nbre_commandes_livrees_livreur, ID_utilisateur)
VALUES
(401, 500.50, 0, 'user_antoine'),
(402, 350.75, 0, 'user_nina');

-- Insertion des plats
INSERT INTO Plat (ID_Plat, Nom_plat, Type_Plat, Pr_cmb_de_personnes_Plat, Prix_par_portion_Plat, nbre_portion_dispo_plat, date_fabrication_plat, date_péremption_plat, Nationalité_cuisine_Plat, Régime_alimentaire_Plat, Ingrédients_principaux_Plat, ID_Cuisinier)
VALUES
(501, 'Pizza végétarienne', 'Plat', 2, 12.50, 5, '2025-03-30', '2025-04-02', 'Italienne', 'Végétarien', 'Farine, Tomate, Fromage', 301),
(502, 'Sushi', 'Plat', 1, 15.00, 5, '2025-03-30', '2025-04-01', 'Japonaise', 'Poisson cru', 'Riz, Saumon, Algues', 302),
(503, 'Tacos', 'Plat', 1, 8.50, 5, '2025-03-30', '2025-04-03', 'Mexicaine', 'Viande', 'Tortilla, Bœuf, Fromage', 304),
(504, 'Pâtes Carbonara', 'Plat', 2, 10.00, 5, '2025-03-30', '2025-04-04', 'Italienne', 'Omnivore', 'Pâtes, Crème, Lardons', 305),
(505, 'Salade César', 'Entrée', 1, 9.00, 5, '2025-03-30', '2025-04-02', 'Américaine', 'Végétarien', 'Salade, Poulet, Parmesan', 304),
(506, 'Ratatouille', 'Plat', 2, 11.00, 5, '2025-03-30', '2025-04-05', 'Française', 'Végétarien', 'Aubergine, Courgette, Tomate', 301),
(507, 'Couscous', 'Plat', 4, 20.00, 5, '2025-03-30', '2025-04-07', 'Marocaine', 'Halal', 'Semoule, Viande, Légumes', 304),
(508, 'Burger', 'Plat', 1, 14.50, 5, '2025-03-30', '2025-04-03', 'Américaine', 'Omnivore', 'Pain, Steak, Fromage', 304),
(509, 'Paella', 'Plat', 3, 18.00, 5, '2025-03-30', '2025-04-06', 'Espagnole', 'Poisson', 'Riz, Fruits de mer, Safran', 303),
(510, 'Soupe Pho', 'Plat', 2, 13.00, 5, '2025-03-30', '2025-04-04', 'Vietnamienne', 'Sans gluten', 'Nouilles de riz, Bouillon, Bœuf', 304),
(511, 'Samosa', 'Entrée', 1, 5.00, 5, '2025-03-30', '2025-04-02', 'Indienne', 'Végétarien', 'Pâte, Légumes, Épices', 304),
(512, 'Poutine', 'Plat', 2, 10.00, 5, '2025-03-30', '2025-04-05', 'Canadienne', 'Omnivore', 'Frites, Fromage, Sauce brune', 303),
(513, 'Bruschetta', 'Entrée', 1, 6.50, 5, '2025-03-30', '2025-04-02', 'Italienne', 'Végétarien', 'Pain, Tomate, Basilic', 305),
(514, 'Gyoza', 'Entrée', 1, 8.00, 5, '2025-03-30', '2025-04-03', 'Japonaise', 'Viande', 'Pâte, Porc, Gingembre', 302),
(515, 'Pizza Margherita', 'Plat', 2, 12.50, 5, '2025-03-30', '2025-04-03', 'Italienne', 'Viande', 'Farine, Tomate, Fromage, Jambon', 305);

-- Insertion des commandes
INSERT INTO Commande (ID_Commande, Prix_Commande, Date_Commande, Taille_Commande, ID_Client)
VALUES
(601, 25.00, '2025-04-06', 2, 201),
(602, 15.00, '2025-04-06', 1, 201),
(603, 30.00, '2025-03-01', 3, 202),
(604, 18.00, '2024-03-12', 2, 202),
(605, 22.00, '2025-02-14', 2, 203),
(606, 12.50, '2025-01-25', 1, 203),
(607, 14.00, '2024-06-17', 1, 204),
(608, 20.00, '2024-12-24', 2, 204);

-- Insertion des liens entre commandes et plats
INSERT INTO Contient (ID_Plat, ID_Commande, nbre_portion_commendee_contient)
VALUES
(501, 601, 1),
(502, 602, 1),
(503, 603, 1),
(509, 605, 1),
(508, 607, 1);

-- Insertion des livraisons
INSERT INTO Livraison (ID_Livraison, Adresse_initiale_Livraison, Adresse_finale_Livraison, Prix_Livraison, Date_Livraison, ID_Commande, ID_Livreur)
VALUES
(701, '10 Rue A', '20 Rue B', 5.00, '2025-04-06', 601, 401),
(702, '30 Rue C', '40 Rue D', 4.50, '2025-04-06', 602, 402),
(703, '50 Rue E', '60 Rue F', 6.00, '2025-03-01', 603, 401),
(704, '70 Rue G', '80 Rue H', 3.50, '2024-03-12', 604, 401),
(705, '90 Rue I', '100 Rue J', 7.00, '2025-02-14', 605, 401),
(706, '110 Rue K', '120 Rue L', 5.50, '2025-01-25', 606, 401),
(707, '130 Rue M', '140 Rue N', 4.00, '2024-06-17', 607, 401),
(708, '150 Rue O', '160 Rue P', 6.50, '2024-12-24', 608, 401);

-- Insertion des commentaires
INSERT INTO Commentaire (ID_Commentaire, Note_Commentaire, Texte_Commentaire, ID_Client, ID_Plat)
VALUES
(801, 5, 'Excellent plat, la pizza était délicieuse et bien garnie.', 201, 501),
(802, 4, 'Les sushis étaient bons, mais un peu trop de riz à mon goût.', 201, 502),
(803, 3, 'Le tacos était correct, mais un peu sec.', 202, 503),
(804, 5, 'La paella était incroyable, pleine de saveurs et bien cuite.', 203, 509),
(805, 2, 'Un peu déçu par le burger, la viande était trop cuite.', 204, 508);
