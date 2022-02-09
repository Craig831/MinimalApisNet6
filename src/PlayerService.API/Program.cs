var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? "Data Source=playerservice.db";
builder.Services.AddSqlite<BaseballDbContext>(connectionString)
                .AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

await EnsureDb(app.Services, app.Logger);

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseSwagger();

app.MapGet("/error", () => Results.Problem("An error has occurred...", statusCode: 500))
    .ExcludeFromDescription();

app.MapGet("/", () => "It worked!!!  Add '/swagger/index.html' to the url to test endpoints")
    .ExcludeFromDescription();

// get all players
app.MapGet("/players", async (BaseballDbContext db) => await db.MlbPlayers.ToListAsync())
    .Produces<List<MlbPlayer>>(StatusCodes.Status200OK)
    .WithName("GetAllPlayers")
    .WithTags("Players");

// get a single player by id
app.MapGet("/players/{id}", async (int id, BaseballDbContext db) =>
    await db.MlbPlayers.FindAsync(id)
        is MlbPlayer player
            ? Results.Ok(player)
            : Results.NotFound())
        .WithName("GetPlayerById")
        .WithTags("Players")
        .Produces<MlbPlayer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

// get a collection of players using a search dto (first name, last name, nickname, position, status)
app.MapPost("/playersearch", async ([FromBody] PlayerSearchDTO searchPlayer, [FromServices] BaseballDbContext db, HttpResponse response) =>
{
    IQueryable<MlbPlayer> playerQuery = db.MlbPlayers;
    if (searchPlayer == null || 
       (string.IsNullOrEmpty(searchPlayer.FirstName) &&
        string.IsNullOrEmpty(searchPlayer.LastName) &&
        string.IsNullOrEmpty(searchPlayer.NickName) &&
        string.IsNullOrEmpty(searchPlayer.PositionId) &&
        string.IsNullOrEmpty(searchPlayer.Status)))
    {
        playerQuery = playerQuery.Where(p => p.PlayerLastName.StartsWith("a", StringComparison.CurrentCultureIgnoreCase));  // brewers as a default search
    }
    else
    {
        if (!string.IsNullOrEmpty(searchPlayer.FirstName))
        {
            playerQuery = playerQuery.Where(p => p.PlayerFirstName == searchPlayer.FirstName);
        }
        if (!string.IsNullOrEmpty(searchPlayer.LastName))
        {
            playerQuery = playerQuery.Where(p => p.PlayerLastName == searchPlayer.LastName);
        }
        if (!string.IsNullOrEmpty(searchPlayer.NickName))
        {
            playerQuery = playerQuery.Where(p => p.PlayerNickName == searchPlayer.NickName);
        }
        if (!string.IsNullOrEmpty(searchPlayer.PositionId))
        {
            playerQuery = playerQuery.Where(p => p.PositionId == searchPlayer.PositionId);
        }
        if (!string.IsNullOrEmpty(searchPlayer.Status))
        {
            playerQuery = playerQuery.Where(p => p.Status == searchPlayer.Status);
        }
    }

    List<MlbPlayer> players = await playerQuery.ToListAsync();
    if (players.Count == 0)
    {
        return Results.NoContent();
    }
    else
    {
        response.StatusCode = 200;
        return Results.Ok(players);
    }
})
    .Produces<List<MlbPlayer>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status204NoContent)
    .WithName("PlayerSearch")
    .WithTags("Players");

// get all teams
app.MapGet("/teams", async (BaseballDbContext db) => await db.MlbTeams.ToListAsync())
    .Produces<List<MlbTeam>>(StatusCodes.Status200OK)
    .WithName("GetAllTeams")
    .WithTags("Teams");

// get a collection of teams using a search dto (team name, team city)
app.MapPost("/teamsearch", async ([FromBody] TeamSearchDTO searchTeam, [FromServices] BaseballDbContext db, HttpResponse response) =>
{
    IQueryable<MlbTeam> teamQuery = db.MlbTeams;
    if (searchTeam != null &&
       (!string.IsNullOrEmpty(searchTeam.TeamName) ||
        !string.IsNullOrEmpty(searchTeam.TeamCity)))
    {
        if (!string.IsNullOrEmpty(searchTeam.TeamName))
        {
            teamQuery = teamQuery.Where(p => p.TeamName == searchTeam.TeamName);
        }
        if (!string.IsNullOrEmpty(searchTeam.TeamCity))
        {
            teamQuery = teamQuery.Where(p => p.TeamCity == searchTeam.TeamCity);
        }
    }

    List<MlbTeam> teams = await teamQuery.ToListAsync();
    if (teams.Count == 0)
    {
        return Results.NoContent();
    }
    else
    {
        response.StatusCode = 200;
        return Results.Ok(teams);
    }
})
    .Produces<List<MlbTeam>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status204NoContent)
    .WithName("TeamSearch")
    .WithTags("Teams");

// add new player
app.MapPost("/players", async ([FromBody] MlbPlayer addPlayer, [FromServices] BaseballDbContext db, HttpResponse response) =>
{
    MlbPlayer newPlayer = new MlbPlayer
    {
        PlayerFirstName = addPlayer.PlayerFirstName,
        PlayerLastName = addPlayer.PlayerLastName,
        PlayerFullName = addPlayer.PlayerFullName,
        PlayerNickName = addPlayer.PlayerNickName,
        PositionId = addPlayer.PositionId,
        Status = addPlayer.Status,
        MlbTeamId = addPlayer.MlbTeamId
    };

    db.MlbPlayers.Add(newPlayer);
    await db.SaveChangesAsync();
    response.StatusCode = 200;
    return Results.Created("/players", newPlayer);
})
    .Accepts<MlbPlayer>("application/json")
    .Produces<MlbPlayer>(StatusCodes.Status201Created)
    .WithName("AddPlayer")
    .WithTags("Players");

// update an existing player (nickname only)
app.MapPut("/players", [AllowAnonymous] async ([FromBody] MlbPlayer updatePlayer, [FromServices] BaseballDbContext db, HttpResponse response) =>
{
    if (updatePlayer == null) return Results.BadRequest();

    MlbPlayer player = db.MlbPlayers.SingleOrDefault(p => p.MlbPlayerId == updatePlayer.MlbPlayerId);

    if (player == null) return Results.NotFound();

    if (player.PlayerFirstName != updatePlayer.PlayerFirstName)
    {
        player.PlayerFirstName = updatePlayer.PlayerFirstName;
    }
    if (player.PlayerLastName != updatePlayer.PlayerLastName)
    {
        player.PlayerLastName = updatePlayer.PlayerLastName;
    }
    if (player.PlayerFullName != updatePlayer.PlayerFullName)
    {
        player.PlayerFullName = updatePlayer.PlayerFullName;
    }
    if (player.PlayerNickName != updatePlayer.PlayerNickName)
    {
        player.PlayerNickName = updatePlayer.PlayerNickName;
    }
    if (player.PositionId != updatePlayer.PositionId)
    {
        player.PositionId = updatePlayer.PositionId;
    }
    if (player.Status != updatePlayer.Status)
    {
        player.Status = updatePlayer.Status;
    }
    if (player.MlbTeamId != updatePlayer.MlbTeamId)
    {
        player.MlbTeamId = updatePlayer.MlbTeamId;
    }

    await db.SaveChangesAsync();
    response.StatusCode = 200;
    return Results.Created("/players", player);
})
    .Accepts<MlbPlayer>("application/json")
    .Produces<MlbPlayer>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("UpdatePlayer")
    .WithTags("Players");

app.UseSwaggerUI();

app.Run();

async Task EnsureDb(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Ensuring database exists and is up to date at connection string '{connectionString}'", connectionString);

    using var db = services.CreateScope().ServiceProvider.GetRequiredService<BaseballDbContext>();
    await db.Database.MigrateAsync();

    DbInitializer.Initialize(db);

}
