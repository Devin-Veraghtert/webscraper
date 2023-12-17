using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;

namespace webscraping {
    class Youtube
    {
        public string? Title { get; set; }
        public string? Views { get; set; }
        public string? Creator { get; set; }
        public string? Url { get; set; }

    }

    class ItJobs
    {
        public string? Title { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public string? Keywords { get; set; }
        public string? Url { get; set; }
    }

    class Player
    {
        public string? Name { get; set; }
        public string? Team { get; set; }
        public string? ShirtNumber { get; set; }
        public string? Opponent { get; set; }
        public string? Date { get; set; }
        public string? Position { get; set; }
        public int? Minutes { get; set; }
        public int? Shots { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? YellowCards { get; set; }
        public int? RedCards { get; set; }
        public string? PassSuccessPercentage { get; set; }
        public string? Rating { get; set; }

    }

    class Team
    {
        public string? TeamName { get; set; }
        public string? Opponent { get; set; }
        public string? Date { get; set; }
        public string? League { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsAgainst { get; set; }
    }

    class Program
    {
        static void youtube()
        {
            Console.WriteLine("Wat wil je zoeken op youtube? ");
            string? input = Console.ReadLine();
            var inputLink = input.Replace(" ", "+");

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.youtube.com/results?search_query=" + inputLink + "&sp=CAISBAgBEAE%253D");
            Thread.Sleep(500);

            var rejectButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div[2]/div[6]/div[1]/ytd-button-renderer[1]/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]"));
            rejectButton.Click();
            Thread.Sleep(500);

            var allVideosPath = driver.FindElements(By.CssSelector("#contents > ytd-video-renderer"));

            var TitlePaths = driver.FindElements(By.CssSelector("h3.title-and-badge > a#video-title"));
            var ViewsPaths = driver.FindElements(By.CssSelector("ytd-video-meta-block.ytd-video-renderer > #metadata > #metadata-line > span:nth-child(3)"));
            var CreatorPaths = driver.FindElements(By.CssSelector("#channel-info > #channel-name > #container > #text-container > #text > a"));
            var UrlPaths = driver.FindElements(By.CssSelector("h3.title-and-badge > a#video-title"));

            Thread.Sleep(1000);

            List<Youtube> youtubeList = new List<Youtube>();

            for (int i = 0; i < allVideosPath.Count && i < 5; i++)
            {
                var title = TitlePaths.ElementAt(i).GetAttribute("title");
                var views = ViewsPaths.ElementAt(i).Text;
                var creator = CreatorPaths.ElementAt(i).Text;
                var url = UrlPaths.ElementAt(i).GetAttribute("href");

                Youtube youtube = new Youtube()
                {
                    Title = title,
                    Views = views,
                    Creator = creator,
                    Url = url,
                };

                Console.WriteLine(title);
                Console.WriteLine(views);
                Console.WriteLine(creator);
                Console.WriteLine(url);
                Console.WriteLine("\n");

                youtubeList.Add(youtube);
            }

            string jsonResult = JsonConvert.SerializeObject(youtubeList);
            File.WriteAllText(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\youtube\video.json", jsonResult);

            using (var writer = new StreamWriter(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\youtube\video.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(youtubeList);
            }

            Thread.Sleep(5000);
            driver.Quit();
        }

        static void itjobs()
        {
            Console.WriteLine("Welke job wil je opzoeken? ");
            string? input = Console.ReadLine();
            var inputLink = input.Replace(" ", "+");

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.ictjob.be/en/search-it-jobs?keywords=" + inputLink + "&sp=CAI%253D");
            Thread.Sleep(5000);

            var dateButton = driver.FindElement(By.XPath("//*[@id=\"sort-by-date\"]"));
            dateButton.Click();
            Thread.Sleep(10000);

            var allJobsPath = driver.FindElements(By.CssSelector("#search-result-body > div > ul > li"));

            var TitlePaths = driver.FindElements(By.CssSelector("ul > li > span.job-info > a > h2.job-title"));
            var CompanyPaths = driver.FindElements(By.CssSelector("ul > li > span.job-info > span.job-company"));
            var LocationPaths = driver.FindElements(By.CssSelector("ul > li > span.job-info > span.job-details > span.job-location > span > span"));
            var KeywordsPaths = driver.FindElements(By.CssSelector("ul > li > span.job-info > span.job-keywords"));
            var UrlPaths = driver.FindElements(By.CssSelector("ul > li > span.job-info > a"));

            Thread.Sleep(1000);

            List<ItJobs> itjobsList = new List<ItJobs>();

            for (int i = 0; i < allJobsPath.Count && i < 5; i++)
            {
                var title = TitlePaths.ElementAt(i).Text;
                var company = CompanyPaths.ElementAt(i).Text;
                var location = LocationPaths.ElementAt(i).Text;
                var keywords = KeywordsPaths.ElementAt(i).Text;
                var url = UrlPaths.ElementAt(i).GetAttribute("href");

                ItJobs itjobs = new ItJobs()
                {
                    Title = title,
                    Company = company,
                    Location = location,
                    Keywords = keywords,
                    Url = url,
                };

                Console.WriteLine(title);
                Console.WriteLine(company);
                Console.WriteLine(location);
                Console.WriteLine(keywords);
                Console.WriteLine(url);
                Console.WriteLine("\n");

                itjobsList.Add(itjobs);

            }

            string jsonResult = JsonConvert.SerializeObject(itjobsList);
            File.WriteAllText(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\itjobs\itjobs.json", jsonResult);

            using (var writer = new StreamWriter(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\itjobs\itjobs.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(itjobsList);
            }

            Thread.Sleep(5000);
            driver.Quit();
        }

        static void whoscored()
        {
            Console.WriteLine("Wil je een speler of team opzoeken? ");
            string? teamSpeler = Console.ReadLine();

            while (teamSpeler != "speler" && teamSpeler != "team")
            {
                Console.WriteLine("Wil je een (s)peler of (t)eam opzoeken? ");
                teamSpeler = Console.ReadLine();
            }

            if (teamSpeler == "speler")
            {
                Console.WriteLine("Welke speler wil je opzoeken? ");
            }
            else
            {
                Console.WriteLine("Welk team wil je opzoeken? ");
            }

            string? input = Console.ReadLine();
            var inputLink = input.Replace(" ", "+");

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.whoscored.com/Search/?t=" + inputLink);
            Thread.Sleep(500);

            var acceptButton = driver.FindElement(By.XPath("//*[@id=\"qc-cmp2-ui\"]/div[2]/div/button[2]"));
            acceptButton.Click();
            Thread.Sleep(500);

            var firstPlayerTeam = driver.FindElement(By.XPath("//*[@id=\"layout-wrapper\"]/div[3]/div[1]/div/table/tbody/tr[2]/td[1]/a"));
            firstPlayerTeam.Click();
            Thread.Sleep(500);

            if (teamSpeler == "team")
            {
                var allMatches = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div.divtable-row"));

                var homeTeamPath = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div > div.col12-lg-2.col12-m-2.col12-s-0.col12-xs-0.divtable-data.team.home > a"));
                var awayTeamPath = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div > div.col12-lg-2.col12-m-2.col12-s-0.col12-xs-0.divtable-data.team.away > a"));

                var datePath = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div > div.col12-lg-1.col12-m-1.col12-s-0.col12-xs-0.date.fourth-col-date.divtable-data"));
                var leaguePath = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div > div.col12-lg-1.col12-m-1.col12-s-1.col12-xs-1.tournament.divtable-data > a"));

                var goalsPath = driver.FindElements(By.CssSelector("#team-fixtures-summary > div > div > div.col12-lg-1.col12-m-1.col12-s-0.col12-xs-0.divtable-data.result > a"));

                var teamName = driver.FindElement(By.XPath("//*[@id=\"layout-wrapper\"]/div[3]/div[1]/div[1]/h1/span")).Text;

                List<Team> teamList = new List<Team>();

                for (int i = 0; i < allMatches.Count - 2; i++)
                {

                    var home = homeTeamPath.ElementAt(i).Text;
                    var away = awayTeamPath.ElementAt(i).Text;
                    var date = datePath.ElementAt(i).Text;
                    var league = leaguePath.ElementAt(i).GetAttribute("title");
                    var goals = goalsPath.ElementAt(i).Text;

                    var goalsScoredString = goals.Substring(0, 1);
                    var goalsAgainstString = goals.Substring(goals.Length - 1, 1);

                    int goalsScored = int.Parse(goalsScoredString);
                    int goalsAgainst = int.Parse(goalsAgainstString);

                    string opponent;

                    if (home == teamName)
                    {
                        opponent = away;
                    }
                    else
                    {
                        opponent = home;
                    }

                    Team team = new Team()
                    {
                        TeamName = teamName,
                        Opponent = opponent,
                        Date = date,
                        League = league,
                        GoalsScored = goalsScored,
                        GoalsAgainst = goalsAgainst,
                    };

                    Console.WriteLine(teamName);
                    Console.WriteLine(opponent);
                    Console.WriteLine(date);
                    Console.WriteLine(league);
                    Console.WriteLine(goalsScored);
                    Console.WriteLine(goalsAgainst);
                    Console.WriteLine("\n");

                    teamList.Add(team);

                }
                string jsonResult = JsonConvert.SerializeObject(teamList);
                File.WriteAllText(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\whoscored\team.json", jsonResult);

                using (var writer = new StreamWriter(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\whoscored\team.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(teamList);
                }
            }

            else
            {
                var matchStatistics = driver.FindElement(By.XPath("//*[@id=\"sub-navigation\"]/ul/li[2]/a"));
                matchStatistics.Click();
                Thread.Sleep(500);

                var allMatches = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr"));

                var playerName = driver.FindElement(By.XPath("//*[@id=\"layout-wrapper\"]/div[3]/div[1]/div[1]/h1")).Text;
                var teamName = driver.FindElement(By.XPath("//*[@id=\"layout-wrapper\"]/div[3]/div[1]/div[1]/div[2]/div[2]/div[2]/a")).Text;
                var shirtNumberPath = driver.FindElement(By.XPath("//*[@id=\"layout-wrapper\"]/div[3]/div[1]/div[1]/div[2]/div[2]/div[3]"));

                var opponentPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.col12-lg-2.col12-m-3.col12-s-4.col12-xs-5.grid-abs.overflow-text > a"));
                var datePath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.date"));
                var positionPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td:nth-child(4)"));
                var minutesPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.minsPlayed"));
                var shotsPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.shotsTotal"));
                var goalsPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.goalTotal"));
                var assistsPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.assist"));
                var yellowCardsPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.yellowCard"));
                var redCardsPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.redCard"));
                var passSuccessRatePath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.passSuccess"));
                var ratingPath = driver.FindElements(By.CssSelector("#player-table-statistics-body > tr > td.rating"));


                List<Player> playerList = new List<Player>();

                for (int i = 0; i < allMatches.Count - 2; i++)
                {
                    var shirtNumberString = shirtNumberPath.Text;
                    var date = datePath.ElementAt(i).Text;
                    var opponentString = opponentPath.ElementAt(i).Text;
                    var position = positionPath.ElementAt(i).Text;
                    var minutesString = minutesPath.ElementAt(i).Text;
                    var shotsString = shotsPath.ElementAt(i).Text;
                    var goalsString = goalsPath.ElementAt(i).Text;
                    var assistsString = assistsPath.ElementAt(i).Text;
                    var yellowCardsString = yellowCardsPath.ElementAt(i).Text;
                    var redCardsString = redCardsPath.ElementAt(i).Text;
                    var passSuccessRate = passSuccessRatePath.ElementAt(i).Text;
                    var rating = ratingPath.ElementAt(i).Text;

                    var shirtNumber = shirtNumberString.Substring(14);
                    var opponent = opponentString.Substring(0, opponentString.Length - 11);

                    if (goalsString.Equals("-"))
                    {
                        goalsString = "0";
                    }
                    if (assistsString.Equals("-"))
                    {
                        assistsString = "0";
                    }
                    if (shotsString.Equals("-"))
                    {
                        shotsString = "0";
                    }
                    if (yellowCardsString.Equals("-"))
                    {
                        yellowCardsString = "0";
                    }
                    if (redCardsString.Equals("-"))
                    {
                        redCardsString = "0";
                    }

                    int shots = int.Parse(shotsString);
                    int goals = int.Parse(goalsString);
                    int assists = int.Parse(assistsString);
                    int minutes = int.Parse(minutesString);
                    int yellowCards = int.Parse(yellowCardsString);
                    int redCards = int.Parse(redCardsString);

                    Player player = new Player()
                    {
                        Name = playerName,
                        Team = teamName,
                        ShirtNumber = shirtNumber,
                        Opponent = opponent,
                        Date = date,
                        Position = position,
                        Minutes = minutes,
                        Shots = shots,
                        Goals = goals,
                        Assists = assists,
                        YellowCards = yellowCards,
                        RedCards = redCards,
                        PassSuccessPercentage = passSuccessRate,
                        Rating = rating,

                    };

                    Console.WriteLine(playerName);
                    Console.WriteLine(teamName);
                    Console.WriteLine(shirtNumber);
                    Console.WriteLine(opponent);
                    Console.WriteLine(date);
                    Console.WriteLine(position);
                    Console.WriteLine(minutes);
                    Console.WriteLine(shots);
                    Console.WriteLine(goals);
                    Console.WriteLine(assists);
                    Console.WriteLine(yellowCards);
                    Console.WriteLine(redCards);
                    Console.WriteLine(passSuccessRate);
                    Console.WriteLine(rating);
                    Console.WriteLine("\n");

                    playerList.Add(player);

                }
                string jsonResult = JsonConvert.SerializeObject(playerList);
                File.WriteAllText(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\whoscored\player.json", jsonResult);

                using (var writer = new StreamWriter(@"C:\Users\devin\Documents\School\2ITF\DevOps & Security\Case Study\caseStudy\caseStudy\whoscored\player.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(playerList);
                }
            }
            Thread.Sleep(5000);
            driver.Quit();
        }

        static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("Van welke website wil je scrapen (youtube/itjobs/whoscored)? ");
                string? website = Console.ReadLine();

                while (website != "youtube" && website != "itjobs" && website != "whoscored")
                {
                    Console.WriteLine("Van welke website wil je scrapen (youtube/itjobs/whoscored)? ");
                    website = Console.ReadLine();
                }

                if (website == "youtube")
                {
                    youtube();
                }
                else if (website == "itjobs")
                {
                    itjobs();
                }
                else
                {
                    whoscored();
                }

                Console.Clear();
            }
        }       
    }
}