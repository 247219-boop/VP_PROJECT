using Microsoft.Data.Sqlite;

namespace BlazorApp2.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using var connection = new SqliteConnection("Data Source=Data/grocery.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT,
                Price REAL NOT NULL,
                ImageUrl TEXT,
                CategoryId INTEGER,
                CategoryName TEXT,
                ExpiryDate TEXT,
                IsOnSale INTEGER DEFAULT 0,
                DiscountPercentage INTEGER DEFAULT 0
            );
            """;

            command.ExecuteNonQuery();
            SeedProducts(connection);
        }

        private static void SeedProducts(SqliteConnection connection)
        {
            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM Products";

            var count = (long)checkCmd.ExecuteScalar();
            if (count > 0) return;

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText =
            """
            INSERT INTO Products
            (Name, Description, Price, ImageUrl, CategoryId, CategoryName, ExpiryDate)
            VALUES

            -- =======================
            -- KITCHEN (20 items)
            -- =======================
            ('Cooking Oil 1L','',550,'/images/oil.jpg',1,'Kitchen','2025-01-05'),
            ('Basmati Rice 5kg','',1200,'/images/rice.jpg',1,'Kitchen','2025-01-10'),
            ('Wheat Flour 5kg','',650,'/images/flour.jpg',1,'Kitchen','2025-01-12'),
            ('Sugar 1kg','',180,'/images/sugar.jpg',1,'Kitchen','2025-02-01'),
            ('Salt 1kg','',80,'/images/salt.jpg',1,'Kitchen','2026-01-01'),
            ('Red Chili Powder','',220,'/images/chili.jpg',1,'Kitchen','2025-03-01'),
            ('Turmeric Powder','',200,'/images/turmeric.jpg',1,'Kitchen','2025-03-01'),
            ('Tea Pack','',420,'/images/tea.jpg',1,'Kitchen','2025-06-01'),
            ('Coffee Jar','',520,'/images/coffee.jpg',1,'Kitchen','2025-06-01'),
            ('Dishwashing Liquid','',250,'/images/dishwash.jpg',1,'Kitchen','2025-12-01'),
            ('Dishwashing Sponge','',120,'/images/sponge.jpg',1,'Kitchen','2026-01-01'),
            ('Kitchen Towel Roll','',180,'/images/towel.jpg',1,'Kitchen','2026-01-01'),
            ('Aluminum Foil','',300,'/images/foil.jpg',1,'Kitchen','2026-01-01'),
            ('Plastic Wrap','',260,'/images/wrap.jpg',1,'Kitchen','2026-01-01'),
            ('Ketchup','',240,'/images/ketchup.jpg',1,'Kitchen','2025-04-01'),
            ('Mayonnaise','',260,'/images/mayo.jpg',1,'Kitchen','2025-04-01'),
            ('Cooking Vinegar','',190,'/images/vinegar.jpg',1,'Kitchen','2025-05-01'),
            ('Soy Sauce','',320,'/images/soy.jpg',1,'Kitchen','2025-05-01'),
            ('Baking Powder','',150,'/images/baking.jpg',1,'Kitchen','2025-06-01'),
            ('Corn Flour','',170,'/images/cornflour.jpg',1,'Kitchen','2025-06-01'),

            -- =======================
            -- BATHROOM (20 items)
            -- =======================
            ('Bath Soap','',150,'/images/soap.jpg',2,'Bathroom','2025-02-01'),
            ('Shampoo','',420,'/images/abcshampoo.jpg',2,'Bathroom','2025-02-05'),
            ('Conditioner','',380,'/images/conditioner.jpg',2,'Bathroom','2025-02-05'),
            ('Toothpaste','',220,'/images/toothpaste.jpg',2,'Bathroom','2025-03-01'),
            ('Toothbrush','',120,'/images/toothbrush.jpg',2,'Bathroom','2026-01-01'),
            ('Hand Wash','',200,'/images/handwash.jpg',2,'Bathroom','2025-04-01'),
            ('Face Wash','',320,'/images/facewash.jpg',2,'Bathroom','2025-04-01'),
            ('Body Lotion','',450,'/images/lotion.jpg',2,'Bathroom','2025-06-01'),
            ('Hair Oil','',300,'/images/hairoil.jpg',2,'Bathroom','2025-05-01'),
            ('Toilet Cleaner','',280,'/images/toiletcleaner.jpg',2,'Bathroom','2025-12-01'),
            ('Bathroom Cleaner','',260,'/images/bathcleaner.jpg',2,'Bathroom','2025-12-01'),
            ('Floor Cleaner','',310,'/images/floorcleaner.jpg',2,'Bathroom','2025-12-01'),
            ('Tissue Box','',160,'/images/tissues.jpg',2,'Bathroom','2026-01-01'),
            ('Bath Sponge','',160,'/images/bathsponge.jpg',2,'Bathroom','2026-01-01'),
            ('Liquid Hand Soap Refill','',260,'/images/handsoaprefill.jpg',2,'Bathroom','2025-08-01'),
            ('Toilet Paper Pack','',480,'/images/tissue.jpg',2,'Bathroom','2026-01-01'),
            ('Laundry Detergent','',650,'/images/detergent.jpg',2,'Bathroom','2025-12-01'),
            ('Mouthwash','',340,'/images/mouthwash.jpg',2,'Bathroom','2025-06-01'),
            ('Air Freshener','',290,'/images/freshener.jpg',2,'Bathroom','2025-06-01'),
            ('Bath Towel','',850,'/images/towelbath.jpg',2,'Bathroom','2026-01-01'),

            -- =======================
            -- FRUITS (20 items)
            -- =======================
            ('Apples','',300,'/images/apple.jpg',3,'Fruits','2024-12-22'),
            ('Bananas','',180,'/images/banana.jpg',3,'Fruits','2024-12-20'),
            ('Oranges','',250,'/images/orange.jpg',3,'Fruits','2024-12-23'),
            ('Mangoes','',450,'/images/mango.jpg',3,'Fruits','2024-12-28'),
            ('Grapes','',320,'/images/grapes.jpg',3,'Fruits','2024-12-24'),
            ('Pineapple','',380,'/images/pineapple.jpg',3,'Fruits','2024-12-26'),
            ('Strawberries','',520,'/images/strawberry.jpg',3,'Fruits','2024-12-21'),
            ('Watermelon','',220,'/images/watermelon.jpg',3,'Fruits','2024-12-25'),
            ('Papaya','',240,'/images/papaya.jpg',3,'Fruits','2024-12-25'),
            ('Guava','',200,'/images/guava.jpg',3,'Fruits','2024-12-24'),
            ('Peach','',360,'/images/peach.jpg',3,'Fruits','2024-12-23'),
            ('Plum','',340,'/images/plum.jpg',3,'Fruits','2024-12-23'),
            ('Pear','',300,'/images/pear.jpg',3,'Fruits','2024-12-22'),
            ('Kiwi','',420,'/images/kiwi.jpg',3,'Fruits','2024-12-22'),
            ('Pomegranate','',480,'/images/pomegranate.jpg',3,'Fruits','2024-12-24'),
            ('Cherry','',650,'/images/cherry.jpg',3,'Fruits','2024-12-21'),
            ('Blueberries','',720,'/images/blueberry.jpg',3,'Fruits','2024-12-21'),
            ('Avocado','',560,'/images/avocado.jpg',3,'Fruits','2024-12-23'),
            ('Coconut','',260,'/images/coconut.jpg',3,'Fruits','2024-12-26'),
            
            -- =======================
            -- SNACKS (20 items)
            -- =======================
             ('Dates','',500,'/images/dates.jpg',3,'Snacks','2025-01-10'),
            ('Potato Chips','',120,'/images/chips.jpg',4,'Snacks','2025-06-01'),
            ('Nachos','',180,'/images/nachos.jpg',4,'Snacks','2025-06-01'),
            ('Popcorn','',150,'/images/popcorn.jpg',4,'Snacks','2025-06-01'),
            ('Chocolate Cookies','',220,'/images/cookies.jpg',4,'Snacks','2025-06-01'),
            ('Biscuits','',100,'/images/biscuits.jpg',4,'Snacks','2025-06-01'),
            ('Salted Peanuts','',170,'/images/peanuts.jpg',4,'Snacks','2025-06-01'),
            ('Pretzels','',190,'/images/pretzels.jpg',4,'Snacks','2025-06-01'),
            ('Chocolate Bar','',250,'/images/chocolate.jpg
            
            ',4,'Snacks','2025-06-01'),
            ('Candy Pack','',140,'/images/candy.jpg',4,'Snacks','2025-06-01'),
            ('Cupcakes','',280,'/images/cupcake.jpg',4,'Snacks','2025-06-01'),
            ('Crackers','',130,'/images/crackers.jpg',4,'Snacks','2025-06-01'),
            ('Energy Bar','',210,'/images/energybar.jpg',4,'Snacks','2025-06-01'),
            ('Trail Mix','',320,'/images/trailmix.jpg',4,'Snacks','2025-06-01'),
            ('Cheese Balls','',200,'/images/cheeseballs.jpg',4,'Snacks','2025-06-01'),
            ('Jelly Beans','',160,'/images/jellybeans.jpg',4,'Snacks','2025-06-01'),
            ('Marshmallows','',180,'/images/marshmallow.jpg',4,'Snacks','2025-06-01'),
            ('Wafer Rolls','',190,'/images/wafer.jpg',4,'Snacks','2025-06-01'),
            ('Dry Cake','',260,'/images/drycake.jpg',4,'Snacks','2025-06-01'),
            ('Mini Donuts','',300,'/images/donuts.jpg',4,'Snacks','2025-06-01'),
            ('Snack Mix','',240,'/images/snackmix.jpg',4,'Snacks','2025-06-01'),

            -- =======================
            -- VEGETABLES (20 items)
            -- =======================
            ('Potatoes','',120,'/images/potato.jpg',5,'Vegetables','2025-01-05'),
            ('Tomatoes','',150,'/images/tomato.jpg',5,'Vegetables','2025-01-05'),
            ('Onions','',130,'/images/onion.jpg',5,'Vegetables','2025-01-05'),
            ('Carrots','',160,'/images/carrot.jpg',5,'Vegetables','2025-01-05'),
            ('Cabbage','',180,'/images/cabbage.jpg',5,'Vegetables','2025-01-05'),
            ('Spinach','',140,'/images/spinach.jpg',5,'Vegetables','2025-01-05'),
            ('Broccoli','',240,'/images/broccoli.jpg',5,'Vegetables','2025-01-05'),
            ('Cauliflower','',220,'/images/cauliflower.jpg',5,'Vegetables','2025-01-05'),
            ('Green Chili','',100,'/images/greenchili.jpg',5,'Vegetables','2025-01-05'),
            ('Garlic','',200,'/images/garlic.jpg',5,'Vegetables','2025-01-05'),
            ('Ginger','',260,'/images/ginger.jpg',5,'Vegetables','2025-01-05'),
            ('Bell Peppers','',280,'/images/bellpepper.jpg',5,'Vegetables','2025-01-05'),
            ('Cucumber','',120,'/images/cucumber.jpg',5,'Vegetables','2025-01-05'),
            ('Eggplant','',170,'/images/eggplant.jpg',5,'Vegetables','2025-01-05'),
            ('Pumpkin','',300,'/images/pumpkin.jpg',5,'Vegetables','2025-01-05'),
            ('Radish','',110,'/images/radish.jpg',5,'Vegetables','2025-01-05'),
            ('Peas','',190,'/images/peas.jpg',5,'Vegetables','2025-01-05'),
            ('Corn','',210,'/images/corn.jpg',5,'Vegetables','2025-01-05'),
            ('Lettuce','',160,'/images/lettuce.jpg',5,'Vegetables','2025-01-05'),
            ('Okra','',180,'/images/okra.jpg',5,'Vegetables','2025-01-05'),

            -- =======================
            -- DAIRY (20 items)
            -- =======================
            ('Milk 1L','',220,'/images/milk.jpg',6,'Dairy','2025-02-01'),
            ('Cheddar Cheese','',450,'/images/cheddar.jpg',6,'Dairy','2025-02-01'),
            ('Butter','',380,'/images/butter.jpg',6,'Dairy','2025-02-01'),
            ('Yogurt','',190,'/images/yogurt.jpg',6,'Dairy','2025-02-01'),
            ('Cream','',260,'/images/cream.jpg',6,'Dairy','2025-02-01'),
            ('Ice Cream','',550,'/images/icecream.jpg',6,'Dairy','2025-02-01'),
            ('Mozzarella Cheese','',480,'/images/mozzarella.jpg',6,'Dairy','2025-02-01'),
            ('Chocolate Milk','',240,'/images/chocolatemilk.jpg',6,'Dairy','2025-02-01'),
            ('Greek Yogurt','',320,'/images/greekyogurt.jpg',6,'Dairy','2025-02-01'),
            ('Whipping Cream','',370,'/images/whippingcream.jpg',6,'Dairy','2025-02-01'),
            ('Paneer','',420,'/images/paneer.jpg',6,'Dairy','2025-02-01'),
            ('Custard','',260,'/images/custard.jpg',6,'Dairy','2025-02-01'),
            ('Condensed Milk','',300,'/images/condensedmilk.jpg',6,'Dairy','2025-02-01'),
            ('Sour Cream','',340,'/images/sourcream.jpg',6,'Dairy','2025-02-01'),
            ('Vanilla Ice Cream','',600,'/images/vanillaicecream.jpg',6,'Dairy','2025-02-01'),
            ('Strawberry Yogurt','',280,'/images/strawberryyogurt.jpg',6,'Dairy','2025-02-01'),
            ('Milk Powder','',520,'/images/milkpowder.jpg',6,'Dairy','2025-02-01'),
            ('Feta Cheese','',470,'/images/feta.jpg',6,'Dairy','2025-02-01'),
            ('Chocolate Ice Cream','',620,'/images/chocoicecream.jpg',6,'Dairy','2025-02-01'),
            ('Fresh Cream','',350,'/images/freshcream.jpg',6,'Dairy','2025-02-01'),

            -- =======================
            -- BEVERAGES (20 items)
            -- =======================
            ('Mineral Water','',80,'/images/water.jpg',7,'Beverages','2025-08-01'),
            ('Orange Juice','',220,'/images/orangejuice.jpg',7,'Beverages','2025-08-01'),
            ('Apple Juice','',240,'/images/applejuice.jpg',7,'Beverages','2025-08-01'),
            ('Mango Juice','',260,'/images/mangojuice.jpg',7,'Beverages','2025-08-01'),
            ('Soft Drink','',150,'/images/softdrink.jpg',7,'Beverages','2025-08-01'),
            ('Energy Drink','',320,'/images/energydrink.jpg',7,'Beverages','2025-08-01'),
            ('Green Tea','',280,'/images/greentea.jpg',7,'Beverages','2025-08-01'),
            ('Black Coffee','',350,'/images/blackcoffee.jpg',7,'Beverages','2025-08-01'),
            ('Milkshake','',420,'/images/milkshake.jpg',7,'Beverages','2025-08-01'),
            ('Lemonade','',180,'/images/lemonade.jpg',7,'Beverages','2025-08-01'),
            ('Iced Tea','',250,'/images/icedtea.jpg',7,'Beverages','2025-08-01'),
            ('Coconut Water','',300,'/images/coconutwater.jpg',7,'Beverages','2025-08-01'),
            ('Protein Shake','',480,'/images/proteinshake.jpg',7,'Beverages','2025-08-01'),
            ('Hot Chocolate','',340,'/images/hotchocolate.jpg',7,'Beverages','2025-08-01'),
            ('Cola Drink','',160,'/images/cola.jpg',7,'Beverages','2025-08-01'),
            ('Sparkling Water','',200,'/images/sparklingwater.jpg',7,'Beverages','2025-08-01'),
            ('Fruit Punch','',290,'/images/fruitpunch.jpg',7,'Beverages','2025-08-01'),
            ('Tea Bottle','',190,'/images/teabottle.jpg',7,'Beverages','2025-08-01'),
            ('Cold Coffee','',360,'/images/coldcoffee.jpg',7,'Beverages','2025-08-01'),
            ('Strawberry Shake','',410,'/images/strawberryshake.jpg',7,'Beverages','2025-08-01'),

            -- =======================
            -- FROZEN FOODS (20 items)
            -- =======================
            ('Frozen Pizza','',850,'/images/frozenpizza.jpg',8,'Frozen Foods','2025-09-01'),
            ('Chicken Nuggets','',620,'/images/nuggets.jpg',8,'Frozen Foods','2025-09-01'),
            ('French Fries','',420,'/images/fries.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Peas','',240,'/images/frozenpeas.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Corn','',260,'/images/frozencorn.jpg',8,'Frozen Foods','2025-09-01'),
            ('Ice Cubes','',120,'/images/icecubes.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Burger Patty','',680,'/images/patty.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Sausage','',590,'/images/sausage.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Kebabs','',740,'/images/kebab.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Fish Fingers','',710,'/images/fishfingers.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Vegetables Mix','',360,'/images/frozenmix.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Paratha','',320,'/images/paratha.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Samosa','',300,'/images/samosa.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Spring Rolls','',420,'/images/springroll.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Shrimp','',950,'/images/shrimp.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Chicken Wings','',820,'/images/wings.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Meatballs','',670,'/images/meatballs.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Pasta','',540,'/images/frozenpasta.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Garlic Bread','',390,'/images/garlicbread.jpg',8,'Frozen Foods','2025-09-01'),
            ('Frozen Dessert','',560,'/images/frozendessert.jpg',8,'Frozen Foods','2025-09-01'),

            -- =======================
            -- BAKERY ITEMS (20 items)
            -- =======================
            ('White Bread','',180,'/images/bread.jpg',9,'Bakery','2025-03-01'),
            ('Brown Bread','',220,'/images/brownbread.jpg',9,'Bakery','2025-03-01'),
            ('Croissant','',260,'/images/croissant.jpg',9,'Bakery','2025-03-01'),
            ('Chocolate Cake','',1200,'/images/cake.jpg',9,'Bakery','2025-03-01'),
            ('Donuts','',320,'/images/donut.jpg',9,'Bakery','2025-03-01'),
            ('Muffins','',280,'/images/muffin.jpg',9,'Bakery','2025-03-01'),
            ('Bagels','',240,'/images/bagel.jpg',9,'Bakery','2025-03-01'),
            ('Cupcakes','',300,'/images/cupcakes.jpg',9,'Bakery','2025-03-01'),
            ('Garlic Bread','',350,'/images/garlicbreadbakery.jpg',9,'Bakery','2025-03-01'),
            ('Cheese Bread','',380,'/images/cheesebread.jpg',9,'Bakery','2025-03-01'),
            ('Swiss Roll','',420,'/images/swissroll.jpg',9,'Bakery','2025-03-01'),
            ('Cinnamon Roll','',390,'/images/cinnamonroll.jpg',9,'Bakery','2025-03-01'),
            ('Cookies Pack','',250,'/images/cookiespack.jpg',9,'Bakery','2025-03-01'),
            ('Apple Pie','',520,'/images/applepie.jpg',9,'Bakery','2025-03-01'),
            ('Brownies','',340,'/images/brownies.jpg',9,'Bakery','2025-03-01'),
            ('Cream Roll','',260,'/images/creamroll.jpg',9,'Bakery','2025-03-01'),
            ('Fruit Cake','',850,'/images/fruitcake.jpg',9,'Bakery','2025-03-01'),
            ('Pancakes','',430,'/images/pancake.jpg',9,'Bakery','2025-03-01'),
            ('Waffles','',460,'/images/waffles.jpg',9,'Bakery','2025-03-01'),
            ('Bun Pack','',200,'/images/buns.jpg',9,'Bakery','2025-03-01');
            """;

            insertCmd.ExecuteNonQuery();
        }
    }
}