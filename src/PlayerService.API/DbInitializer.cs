namespace PlayerService.API
{
    public static class DbInitializer
    {
        public static void Initialize(BaseballDbContext context)
        {
            if (!context.MlbTeams.Any())
            {
                var teams = new MlbTeam[]
                {
                    new MlbTeam { MlbTeamId = 108, TeamCity = "Los Angelas", TeamAbbreviation = "LAA", TeamName = "Angels" },
                    new MlbTeam { MlbTeamId = 109, TeamCity = "Arizona", TeamAbbreviation = "ARI", TeamName = "D-backs" },
                    new MlbTeam { MlbTeamId = 110, TeamCity = "Baltimore", TeamAbbreviation = "BAL", TeamName = "Orioles" },
                    new MlbTeam { MlbTeamId = 111, TeamCity = "Boston", TeamAbbreviation = "BOS", TeamName = "Red Sox" },
                    new MlbTeam { MlbTeamId = 112, TeamCity = "Chicago", TeamAbbreviation = "CHC", TeamName = "Cubs" },
                    new MlbTeam { MlbTeamId = 113, TeamCity = "Cincinnati", TeamAbbreviation = "CIN", TeamName = "Reds" },
                    new MlbTeam { MlbTeamId = 114, TeamCity = "Cleveland", TeamAbbreviation = "CLE", TeamName = "Indians" },
                    new MlbTeam { MlbTeamId = 115, TeamCity = "Colorado", TeamAbbreviation = "COL", TeamName = "Rockies" },
                    new MlbTeam { MlbTeamId = 116, TeamCity = "Detroit", TeamAbbreviation = "DET", TeamName = "Tigers" },
                    new MlbTeam { MlbTeamId = 117, TeamCity = "Houston", TeamAbbreviation = "HOU", TeamName = "Astros" },
                    new MlbTeam { MlbTeamId = 118, TeamCity = "Kansas City", TeamAbbreviation = "KC", TeamName = "Royals" },
                    new MlbTeam { MlbTeamId = 119, TeamCity = "Los Angelas", TeamAbbreviation = "LAD", TeamName = "Dodgers" },
                    new MlbTeam { MlbTeamId = 120, TeamCity = "Washington", TeamAbbreviation = "WSH", TeamName = "Nationals" },
                    new MlbTeam { MlbTeamId = 121, TeamCity = "New York", TeamAbbreviation = "NYM", TeamName = "Mets" },
                    new MlbTeam { MlbTeamId = 133, TeamCity = "Oakland", TeamAbbreviation = "OAK", TeamName = "Athletics" },
                    new MlbTeam { MlbTeamId = 134, TeamCity = "Pittsburgh", TeamAbbreviation = "PIT", TeamName = "Pirates" },
                    new MlbTeam { MlbTeamId = 135, TeamCity = "San Diego", TeamAbbreviation = "SD", TeamName = "Padres" },
                    new MlbTeam { MlbTeamId = 136, TeamCity = "Seattle", TeamAbbreviation = "SEA", TeamName = "Mariners" },
                    new MlbTeam { MlbTeamId = 137, TeamCity = "San Francisco", TeamAbbreviation = "SF", TeamName = "Giants" },
                    new MlbTeam { MlbTeamId = 138, TeamCity = "St. Louis", TeamAbbreviation = "STL", TeamName = "Cardinals" },
                    new MlbTeam { MlbTeamId = 139, TeamCity = "Tampa Bay", TeamAbbreviation = "TB", TeamName = "Rays" },
                    new MlbTeam { MlbTeamId = 140, TeamCity = "Texas", TeamAbbreviation = "TEX", TeamName = "Rangers" },
                    new MlbTeam { MlbTeamId = 141, TeamCity = "Toronto", TeamAbbreviation = "TOR", TeamName = "Blue Jays" },
                    new MlbTeam { MlbTeamId = 142, TeamCity = "Minnesota", TeamAbbreviation = "MIN", TeamName = "Twins" },
                    new MlbTeam { MlbTeamId = 143, TeamCity = "Philadelphia", TeamAbbreviation = "PHI", TeamName = "Phillies" },
                    new MlbTeam { MlbTeamId = 144, TeamCity = "Atlanta", TeamAbbreviation = "ATL", TeamName = "Braves" },
                    new MlbTeam { MlbTeamId = 145, TeamCity = "Chicago", TeamAbbreviation = "CWS", TeamName = "White Sox" },
                    new MlbTeam { MlbTeamId = 146, TeamCity = "Miami", TeamAbbreviation = "MIA", TeamName = "Marlins" },
                    new MlbTeam { MlbTeamId = 147, TeamCity = "New York", TeamAbbreviation = "NYY", TeamName = "Yankees" },
                    new MlbTeam { MlbTeamId = 158, TeamCity = "Milwaukee", TeamAbbreviation = "MIL", TeamName = "Brewers" }
                };

                foreach(var team in teams)
                {
                    context.MlbTeams.Add(team);
                }
                context.SaveChanges();
            }
        }
    }
}
