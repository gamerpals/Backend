using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GamerpalsBackend.DataObjects.Models;
using GamerPalsBackend.DataObjects.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GamerPalsBackend.Generator
{
    class Program
    {
        public static void Main(string[] args)
        { }
        //private static MongoContext context = new MongoContext();
        //public static void Main(string[] args)
        //{

        //    CreateRegisterUser();

        //    for (int i = 0; i < 20; i++)
        //    {
        //        CreateActiveUser();
        //    }

        //    var users = context.Users.AsQueryable();
        //    foreach (User cache in users.AsQueryable())
        //    {
        //        Console.WriteLine(cache.ProfileName);
        //        foreach (var game in cache.GameIds)
        //        {
        //            Console.WriteLine(game.As<Game>().GameName);
        //        }
        //    }

        //    Console.ReadKey();
        //}

        //public static void GenerateApexLegendsParameters(ActiveSearch search)
        //{
        //    List<SearchParameter> parameters = new List<SearchParameter>();

        //    List<Parameter> possibleParameters = context.Parameters.AsQueryable().Where(param => param.Game.GameID == new ObjectId("6")).ToList();

        //    foreach (var par in possibleParameters)
        //    {
        //        SearchParameter cache = new SearchParameter
        //        {
        //            ParameterID = par.ParameterID,
        //            ActiveSearchID = search.ActiveSearchID
        //        };

        //        string[] legends = {"Bloodhound", "Gibraltar", "Lifeline", "Pathfinder", "Wraith", "Bangalore", "Caustic", "Mirage" };
        //        string[] playstyles = {"Aggressive", "Hiding", "LootingFirst", "Beginner" };
        //        switch (par.ParameterName)
        //        {
        //            case "MainLegend":
        //                cache.ParameterValue = legends[new Random().Next(legends.Length)];
        //                break;
        //            case "Level":
        //                cache.ParameterValue = new Random().Next(100).ToString();
        //                break;
        //            case "Wins":
        //                cache.ParameterValue = new Random().Next(100).ToString();
        //                break;
        //            case "Playstyle":
        //                cache.ParameterValue = playstyles[new Random().Next(playstyles.Length)];
        //                break;
        //            case "Kills":
        //                cache.ParameterValue = "" + new Random().Next(1000);
        //                break;
        //            default:
        //                Console.WriteLine("No implementation for param");
        //                break;
        //        }
        //        parameters.Add(cache);
        //    }

        //    search.Parameters = parameters;
        //}

        //public static void GenerateLOLParameters(ActiveSearch search)
        //{
        //    List<SearchParameter> parameters = new List<SearchParameter>();

        //    List<Parameter> possibleParameters = context.Parameters.AsQueryable().Where(param => param.Game.GameID == search.GameID).ToList();

        //    foreach (var par in possibleParameters)
        //    {
        //        SearchParameter cache = new SearchParameter
        //        {
        //            ParameterID = par.ParameterID,
        //            ActiveSearchID = search.ActiveSearchID
        //        };

        //        string[] lanes = { "Top", "Mid", "Jungle", "Braindead", "Support"};
        //        string[] ranks = { "Iron", "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Master", "Grandmaster", "Challenger" };
        //        string[] champs = {"Lee Sin", "Warwick", "Ekko", "Talon", "Zed" };
        //        string[] champtype = {"Tank", "Assassin", "Mage", "Bruiser", "Marksman" };
        //        switch (par.ParameterName)
        //        {
        //            case "Lane":
        //                cache.ParameterValue = lanes.GetRandomElement();
        //                break;
        //            case "Level":
        //                cache.ParameterValue = new Random().Next(30, 500).ToString();
        //                break;
        //            case "PlayingSeasons":
        //                cache.ParameterValue = new Random().Next(9).ToString();
        //                break;
        //            case "Rank":
        //                cache.ParameterValue = ranks.GetRandomElement();
        //                break;
        //            case "MainChamp":
        //                cache.ParameterValue = champs.GetRandomElement();
        //                break;
        //            case "ChampType":
        //                cache.ParameterValue = champtype.GetRandomElement();
        //                break;
        //            case "SecondLane":
        //                cache.ParameterValue = lanes.GetRandomElement();
        //                break;
        //            default:
        //                Console.WriteLine("No implementetion for param");
        //                break;
        //        }
        //        parameters.Add(cache);
        //    }

        //    search.Parameters = parameters;
        //}

        //public static User CreateRegisterUser()
        //{
        //    User u = new User { Username = "TestUser" + Random() };
        //    /*UserOptions opt = new UserOptions();
        //    opt.UserId = u.UserID;
        //    opt.UserOptionRoles = new List<UserOptionRoles>();

        //    var roles = new UserOptionRoles {Role = context.Roles.Single(role => role.RoleID == 2),RoleId = 2,UserOptionId = opt.UserOptionsID,UserOptions = opt};
        //    opt.UserOptionRoles.Add(roles); */
        //    var game = GetRandomGame();
        //    u.Games.Add(game);
        //    context.Users.InsertOne(u);
        //   // context.UserOptions.Add(opt);
        //    return u;
        //}

        //public static User CreateActiveUser()
        //{
        //    var user = CreateRegisterUser();
        //    ActiveSearch search = new ActiveSearch
        //    {
        //        Active = true, MaxPlayers = 4, SearchType = new SearchType { SearchTypeName = "Short"}, Server = user.Games.GetRandomElement().As<Game>().Servers.GetRandomElement().As<Server>()
        //    };
        //    search.Description = LoremIpsum(10, 50, 3, 20, new Random().Next(3));
        //    search.OwnerId = user.UserID;
        //    context.ActiveSearches.InsertOne(search);

        //    //AddSearchToUser(user, search);

        //    //switch (search.Server.ServerGame.GameID)
        //    //{
        //    //    case ObjectId.Empty:
        //    //        GenerateLOLParameters(search);
        //    //        break;
        //    //    case 6:
        //    //        GenerateApexLegendsParameters(search);
        //    //        break;
        //    //}



        //    return user;
        //}

        //public static ObjectId GetRandomGame()
        //{
        //    var games = context.Games.AsQueryable().AsEnumerable();
        //    var enumerable = games as Game[] ?? games.ToArray();
        //    return enumerable.ElementAt(new Random().Next(enumerable.Count())).GameID;
        //}

        public static int Random()
        {
            return new Random().Next(10000);
        }
        private static string LoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences, int numLines)
        {
            var words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                               + minSentences;
            int numWords = rand.Next(maxWords - minWords) + minWords;

            var sb = new StringBuilder();
            for (int p = 0; p < numLines; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { sb.Append(" "); }
                        string word = words[rand.Next(words.Length)];
                        if (w == 0) { word = word.Substring(0, 1).Trim().ToUpper() + word.Substring(1); }
                        sb.Append(word);
                    }
                    sb.Append(". ");
                }
                if (p < numLines - 1) sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    public static class Helper
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            return list[new Random().Next(list.Count())];
        }

        public static T GetRandomElement<T>(this T[] array)
        {
            return array[new Random().Next(array.Length)];
        }

        public static Game AsGame(this ObjectId id)
        {
            return new MongoContext().Games.FindAsync(filter => filter._id == id).Result.Single();
        }

        public static T As<T>(this ObjectId id)
        {
            var name = nameof(T);
            var context = new MongoContext();
            var coll = (IMongoCollection<T>)context.GetType().GetProperty(name).GetValue(context);
            return coll.Find(Builders<T>.Filter.Eq(name+"ID", id)).FirstOrDefaultAsync().Result;
        }
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GamerPals;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        } */
    }
}
