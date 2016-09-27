using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ScoresDemo
{
    public class ScoresSource
    {
        private static string endpoint = "https://api.crowdscores.com/api/v1"; //API Endpoint

        //Get all the competitions available on the API
        public static async Task<ObservableCollection<Competition>> GetCompetitions()
        {
            var client = new HttpClient();

            //Header required to call the service (API key)
            client.DefaultRequestHeaders.Add("x-crowdscores-api-key", "91e57182c3954dc68a2e184c30271b4a");

            //build request URI
            var requestUri = endpoint + "/competitions";

            //Calls the service and returns a json string
            string response = await client.GetStringAsync(requestUri);

            //Converts json string into a ObservableCollection of JObject
            var jsonCompetitionsList = JsonConvert.DeserializeObject<ObservableCollection<JObject>>(response);

            //Build list of match objects based on json string
            var competitionsList = new ObservableCollection<Competition>();
            foreach (JObject competition in jsonCompetitionsList)
            {
                JToken regionToken;
                competition.TryGetValue("region", out regionToken);

                var newCompetition = new Competition()
                {
                    Id = competition.Value<int>("dbid"),
                    Name = competition.Value<string>("name"),
                    RegionName = regionToken.Value<string>("name")
                };
                competitionsList.Add(newCompetition);
            }

            return competitionsList;
        }

        //Get the last matches of a given competition (id)
        public static async Task<ObservableCollection<Match>> GetLastCompetitionMatches(string competitionId)
        {
            var client = new HttpClient();

            //Header required to call the service (API key)
            client.DefaultRequestHeaders.Add("x-crowdscores-api-key", "91e57182c3954dc68a2e184c30271b4a");

            //number of days to fetch (always fetching the last 7 days)
            var daysToFetch = DateTime.Now.AddDays(-7);

            //build request URI
            var requestUri = endpoint + "/matches?competition_id=" + competitionId + "&from=" + daysToFetch.ToString("yyyy-MM-ddThh:mm:ss");

            //Calls the service and returns a json string
            string response = await client.GetStringAsync(requestUri);

            //Converts json string into a ObservableCollection of JObject
            var jsonMatchesList = JsonConvert.DeserializeObject<ObservableCollection<JObject>>(response);
            
            //Build list of match objects based on json string
            var matchList = new ObservableCollection<Match>();
            foreach (JObject match in jsonMatchesList)
            {
                JToken homeTeamToken;
                JToken awayTeamToken;
                JToken venueToken;
                match.TryGetValue("venue", out venueToken);
                match.TryGetValue("homeTeam", out homeTeamToken);
                match.TryGetValue("awayTeam", out awayTeamToken);

                var startTimeMilis = match.Value<double>("start");
                TimeSpan time = TimeSpan.FromMilliseconds(startTimeMilis);
                DateTime matchStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                matchStartDateTime = matchStartDateTime.Add(time);

                var newMatch = new Match()
                {
                    AwayGoals = match.Value<int>("awayGoals"),
                    HomeGoals = match.Value<int>("homeGoals"),
                    HomeShirtImgUrl = homeTeamToken.Value<string>("shirtUrl"),
                    AwayShirtImgUrl = awayTeamToken.Value<string>("shirtUrl"),
                    HomeName = homeTeamToken.Value<string>("name"),
                    AwayName = awayTeamToken.Value<string>("name"),
                    VenueName = venueToken.Value<string>("name"),
                    StartTime = matchStartDateTime
                };

                matchList.Add(newMatch);
            }

            return matchList;
        }
    }
}