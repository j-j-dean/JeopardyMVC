/*********************************************************************
 * 
 * JeopardyLibrary - MyDataInializer class
 * 
 * The methods in this class add data to the database Categories table,
 * Questions table, and Answers table.  The Questions entries relate to
 * an entry in the Categories table.  The Answers entries relate to
 * a question in the Questions table.
 * 
 * Those tables contain data to play the Jeopardy game.
 * 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using JeopardyLibrary.EF;
using JeopardyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace JeopardyLibrary.DataInialization
{
   public static class MyDataInitializer
    {

        /***************************************************************
         * Initialize data in the database
         * ************************************************************/
        public static void InitializeData(JeopardyContext context)
        {
            /***********************************************************
             * Place values in the Jeopardy Categories table
             * ********************************************************/
            var categories = new List<Categories>
            {
                new Categories {Name = "Christmas Movies", Active=true },
                new Categories {Name = "Sports", Active=true },
                new Categories {Name = "Colors", Active=true },
                new Categories {Name="Fruits & Vegetables", Active=true },
                new Categories {Name="Science & Nature", Active=true },
                new Categories {Name="American History", Active=true }
            };
            categories.ForEach(x => context.Categories.Add(x));
            context.SaveChanges();

            /************************************************************
             * Place values in the Jeopardy Questions table
             * *********************************************************/
            var questions = new List<Questions>
            {
                new Questions {Question = "Famous phrase-You'll shoot your eye out kid!", 
                    PointValue=100, Categories=categories[0]},
                new Questions {Question = "Christmas character wanting to be a dentist",
                    PointValue=200, Categories=categories[0]},
                new Questions {Question = "Not one of Elf's food groups:",
                    PointValue=300, Categories=categories[0] },
                new Questions {Question = "How many sizes does Grinch's heart grow",
                    PointValue=400, Categories=categories[0] },
                new Questions {Question="Christmas Vacation-Time Cousin Eddie unemployed",
                    PointValue=500, Categories=categories[0] },
                new Questions {Question="What MLB pitcher holds record for complete games",
                    PointValue=300, Categories=categories[1] },
                new Questions {Question="The name of a golf score of 4 under par on a hole",
                    PointValue = 400, Categories=categories[1] },
                new Questions {Question="Lambeau Field is Home to what team",
                    PointValue=100, Categories=categories[1] },
                new Questions {Question="City wins 3 major sport championships in same year",
                    PointValue=500, Categories=categories[1] },
                new Questions {Question="Who holds record for most NASCAR wins",
                    PointValue=200, Categories=categories[1] },
                new Questions {Question="On TWA Flight 800, what color were the black boxes",
                    PointValue=400, Categories=categories[2] },
                new Questions {Question="Flag color flown on ship where disease breaks out",
                    PointValue=300, Categories=categories[2] },
                new Questions {Question="In what country will you find the Yellow River",
                    PointValue=500, Categories=categories[2] },
                new Questions {Question="What colors mix to make purple",
                    PointValue=200, Categories=categories[2] },
                new Questions {Question="What color is Superman's cape",
                    PointValue=100, Categories=categories[2] },
                new Questions {Question="About half the sugar produced in U.S. comes from",
                    PointValue=200, Categories=categories[3] },
                new Questions{Question="Assume you are buying these even if sign says yams",
                    PointValue=100, Categories=categories[3] },
                new Questions {Question="Fruit name called muskmelon on an Italian estate",
                    PointValue=300, Categories=categories[3] },
                 new Questions {Question="Southern cocktail party pickled hors d'oeuvre",
                    PointValue=400, Categories=categories[3] },
                new Questions {Question="Cabbage relative great in salads & in baked chips",
                    PointValue=500, Categories=categories[3] },
                new Questions {Question="Earth is farthest from the sun during this month",
                    PointValue=200, Categories=categories[4] },
                new Questions {Question="The Molloy Deep is deepest point in this ocean",
                    PointValue=400, Categories=categories[4] },
                new Questions {Question="Appliance with 1000 watts/250 volts needs",
                    PointValue=300, Categories=categories[4] },
                new Questions {Question="Heat travels by 3 methods: conduction convection &",
                    PointValue=500, Categories=categories[4] },
                new Questions {Question="The absolute temperature scale is named for",
                    PointValue=100, Categories=categories[4] },
                new Questions {Question="In 1867 William Seward negotiated its purchase",
                    PointValue=100, Categories=categories[5] },
                new Questions {Question="On April 9 1865 Lee surrendered to Grant here",
                    PointValue=300, Categories=categories[5] },
                new Questions {Question="This European disease had last U.S. case in 1949",
                    PointValue=400, Categories=categories[5] },
                new Questions {Question="Sinking of USS Maine led U.S. to declare war on",
                    PointValue=500, Categories=categories[5] },
                new Questions {Question="Terrifying event often involving runs on the bank",
                    PointValue=200, Categories=categories[5] }
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();

            /************************************************************
             * Place values in the Jeopardy Answers table
             * *********************************************************/
            var answers = new List<Answers>
            {
                new Answers { Answer = "Home Alone", Question = questions[0], CorrectAnswer=false },
                new Answers { Answer = "A Christmas Story", Question = questions[0], CorrectAnswer=true },
                new Answers { Answer = "The Year Without a Santa Claus", Question = questions[0], CorrectAnswer = false },
                new Answers { Answer = "Christmas Vacation", Question = questions[0], CorrectAnswer=false },
                new Answers { Answer = "Dobby", Question = questions[1], CorrectAnswer=false },
                new Answers { Answer ="Ralphie", Question=questions[1], CorrectAnswer=false },
                new Answers { Answer = "Mr. Hinkle", Question=questions[1], CorrectAnswer=false },
                new Answers {Answer="Hermie", Question=questions[1], CorrectAnswer=true },

                new Answers {Answer="Sugar", Question=questions[2], CorrectAnswer=true },
                new Answers {Answer="Candy Canes", Question=questions[2], CorrectAnswer=false },
                new Answers {Answer="Syrup", Question=questions[2], CorrectAnswer=false },
                new Answers {Answer="Candy Corn", Question=questions[2], CorrectAnswer=false },

                new Answers {Answer="One", Question=questions[3], CorrectAnswer=false },
                new Answers {Answer="Two", Question=questions[3], CorrectAnswer=false },
                new Answers {Answer="Three", Question=questions[3], CorrectAnswer=true },
                new Answers {Answer="Four", Question=questions[3], CorrectAnswer=false },

                new Answers {Answer="One Year", Question=questions[4], CorrectAnswer=false },
                new Answers {Answer="Two Years", Question=questions[4], CorrectAnswer=false },
                new Answers {Answer="Five Years", Question=questions[4], CorrectAnswer=false },
                new Answers {Answer="Seven Years", Question=questions[4], CorrectAnswer=true },

                new Answers {Answer="Nolan Ryan", Question=questions[5], CorrectAnswer=false },
                new Answers {Answer="Cy Young", Question=questions[5], CorrectAnswer=true },
                new Answers {Answer="Ty Cobb", Question=questions[5], CorrectAnswer=false },
                new Answers {Answer="Babe Ruth", Question=questions[5], CorrectAnswer=false },

                new Answers {Answer="Condor", Question=questions[6], CorrectAnswer=true },
                new Answers {Answer="Albatross", Question=questions[6], CorrectAnswer=false },
                new Answers {Answer="Falcon", Question=questions[6], CorrectAnswer=false },
                new Answers {Answer="Vulture", Question=questions[6], CorrectAnswer=false },

                new Answers {Answer="Pittsburgh Steelers", Question=questions[7], CorrectAnswer=false },
                new Answers {Answer="Buffalo Bills", Question=questions[7], CorrectAnswer=false },
                new Answers {Answer="Green Bay Packers", Question=questions[7], CorrectAnswer=true },
                new Answers {Answer="Chicago Bears", Question=questions[7], CorrectAnswer=false },

                new Answers {Answer="Detroit", Question=questions[8], CorrectAnswer=true },
                new Answers {Answer="Los Angeles", Question=questions[8], CorrectAnswer=false },
                new Answers {Answer="New York", Question=questions[8], CorrectAnswer=false },
                new Answers {Answer="San Francisco", Question=questions[8], CorrectAnswer=false },

                new Answers {Answer="Jeff Gordon", Question=questions[9], CorrectAnswer=false },
                new Answers {Answer="Dale Earnhardt", Question=questions[9], CorrectAnswer=false },
                new Answers {Answer="Darrell Waltrip", Question=questions[9], CorrectAnswer=false },
                new Answers {Answer="Richard Petty", Question=questions[9], CorrectAnswer=true },

                new Answers {Answer="Black", Question=questions[10], CorrectAnswer=false },
                new Answers {Answer="White", Question=questions[10], CorrectAnswer=false },
                new Answers {Answer="Orange", Question=questions[10], CorrectAnswer=true },
                new Answers {Answer="Red", Question=questions[10], CorrectAnswer=false },

                new Answers {Answer="Yellow", Question=questions[11], CorrectAnswer=true },
                new Answers {Answer="Red", Question=questions[11], CorrectAnswer=false },
                new Answers {Answer="Black", Question=questions[11], CorrectAnswer=false },
                new Answers {Answer="Purple", Question=questions[11], CorrectAnswer=false },

                new Answers {Answer="United States", Question=questions[12], CorrectAnswer=false },
                new Answers {Answer="Brazil", Question=questions[12], CorrectAnswer=false },
                new Answers {Answer="Egypt", Question=questions[12], CorrectAnswer=false },
                new Answers {Answer="China", Question=questions[12], CorrectAnswer=true },

                new Answers {Answer="Blue and Green", Question=questions[13], CorrectAnswer=false },
                new Answers {Answer="Red and Blue", Question=questions[13], CorrectAnswer=true },
                new Answers {Answer="Red and Black", Question=questions[13], CorrectAnswer=false },
                new Answers {Answer="Black and Blue", Question=questions[13], CorrectAnswer=false },

                new Answers {Answer="Blue", Question=questions[14], CorrectAnswer=false },
                new Answers {Answer="Yellow", Question=questions[14], CorrectAnswer=false },
                new Answers {Answer="Red", Question=questions[14], CorrectAnswer=true },
                new Answers {Answer="Black", Question=questions[14], CorrectAnswer=false },

                new Answers {Answer="Beets", Question=questions[15], CorrectAnswer=true },
                new Answers {Answer="Potatoes", Question=questions[15], CorrectAnswer=false },
                new Answers {Answer="Carrots", Question=questions[15], CorrectAnswer=false },
                new Answers {Answer="Parsnips", Question=questions[15], CorrectAnswer=false },

                new Answers {Answer="Turnips", Question=questions[16], CorrectAnswer=false },
                new Answers {Answer="Parsnips", Question=questions[16], CorrectAnswer=false },
                new Answers {Answer="Sweet Potatoes", Question=questions[16], CorrectAnswer=true },
                new Answers {Answer = "Potatoes", Question=questions[16], CorrectAnswer=false },

                new Answers {Answer="Watermelon", Question=questions[17], CorrectAnswer=false },
                new Answers {Answer="Squash", Question=questions[17], CorrectAnswer=false },
                new Answers {Answer="Zuchini", Question=questions[17], CorrectAnswer=false },
                new Answers {Answer="Cantaloupe", Question=questions[17], CorrectAnswer=true },

                new Answers {Answer="Green Tomatoes", Question=questions[18], CorrectAnswer=false },
                new Answers {Answer="Okra", Question=questions[18],CorrectAnswer=true },
                new Answers {Answer="Peppers", Question=questions[18], CorrectAnswer=false },
                new Answers { Answer="Cucumbers", Question=questions[18], CorrectAnswer=false },

                new Answers {Answer="Turnip Greens", Question=questions[19], CorrectAnswer=false },
                new Answers {Answer="Mustard Greens", Question=questions[19], CorrectAnswer=false },
                new Answers {Answer="Kale", Question=questions[19], CorrectAnswer=true },
                new Answers {Answer="Spinach", Question=questions[19], CorrectAnswer=false },

                new Answers {Answer="June", Question=questions[20], CorrectAnswer=false },
                new Answers {Answer="July", Question=questions[20], CorrectAnswer=true },
                new Answers {Answer="December", Question=questions[20], CorrectAnswer=false },
                new Answers {Answer="January", Question=questions[20], CorrectAnswer=false },

                new Answers {Answer="Atlantic", Question=questions[21], CorrectAnswer=false },
                new Answers {Answer="Pacific", Question=questions[21], CorrectAnswer=false },
                new Answers {Answer="Arctic", Question=questions[21], CorrectAnswer=true },
                new Answers {Answer="Indian", Question=questions[21], CorrectAnswer=false },

                new Answers {Answer="2.5 Amps", Question=questions[22], CorrectAnswer=false },
                new Answers {Answer="40 Amps", Question=questions[22], CorrectAnswer=false },
                new Answers {Answer="25 Amps", Question=questions[22], CorrectAnswer=false },
                new Answers {Answer="4 Amps", Question=questions[22], CorrectAnswer=true },

                new Answers {Answer="Induction", Question=questions[23], CorrectAnswer=false },
                new Answers {Answer="Condensation", Question=questions[23], CorrectAnswer=false },
                new Answers {Answer="Saturation", Question=questions[23], CorrectAnswer=false },
                new Answers {Answer="Radiation", Question=questions[23], CorrectAnswer=true },

                new Answers {Answer="Celcius", Question=questions[24], CorrectAnswer=false },
                new Answers {Answer="Fahrenheit", Question=questions[24], CorrectAnswer=false },
                new Answers {Answer="Kelvin", Question=questions[24], CorrectAnswer=true },
                new Answers {Answer="Henry", Question=questions[24], CorrectAnswer=false },

                new Answers {Answer="Alaska", Question=questions[25], CorrectAnswer=true },
                new Answers {Answer="Lousiana", Question=questions[25], CorrectAnswer=false },
                new Answers {Answer="Texas", Question=questions[25], CorrectAnswer=false },
                new Answers {Answer="Florida", Question=questions[25], CorrectAnswer=false },

                new Answers {Answer="Yorktown", Question=questions[26], CorrectAnswer=false },
                new Answers {Answer="Gettysburg", Question=questions[26], CorrectAnswer=false },
                new Answers {Answer="Appomattox Courthouse", Question=questions[26], CorrectAnswer=true },
                new Answers {Answer="Antietam", Question=questions[26], CorrectAnswer=false },

                new Answers {Answer="Spanish Flu", Question=questions[27], CorrectAnswer=false },
                new Answers {Answer="Measles", Question=questions[27], CorrectAnswer=false },
                new Answers {Answer="Polio", Question=questions[27], CorrectAnswer=false },
                new Answers {Answer="Smallpox", Question=questions[27], CorrectAnswer=true },

                new Answers {Answer="Spain", Question=questions[28], CorrectAnswer=true },
                new Answers {Answer="Mexico", Question=questions[28], CorrectAnswer=false },
                new Answers {Answer="Germany", Question=questions[28], CorrectAnswer=false },
                new Answers {Answer="Britain", Question=questions[28], CorrectAnswer=false },

                new Answers {Answer="A Depression", Question=questions[29], CorrectAnswer=false },
                new Answers {Answer="A Crash", Question=questions[29], CorrectAnswer=false },
                new Answers {Answer="A Panic", Question=questions[29], CorrectAnswer=true },
                new Answers {Answer="A Recession", Question=questions[29], CorrectAnswer=false }
            };
            context.Answers.AddRange(answers);
            context.SaveChanges();
        }

        /*********************************************************************
         * Recreate the database
         * *******************************************************************/
        public static void RecreateDatabase(JeopardyContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        /*********************************************************************
         * Execute raw SQL command to delete selected table from the database
         * ******************************************************************/
        public static void ExecuteDeleteSql(JeopardyContext context, string tableName)
        {
            var rawSqlString = $"Delete from dbo.{tableName}";
            context.Database.ExecuteSqlRaw(rawSqlString);
        }

        /*********************************************************************
         * Clears the database tables
         * ******************************************************************/
        public static void ClearDatabaseData(JeopardyContext context)
        {
            ExecuteDeleteSql(context, "Categories");
            ExecuteDeleteSql(context, "Questions");
            ExecuteDeleteSql(context, "Answers");
        }
    }
}
