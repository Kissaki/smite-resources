namespace KCode.SMITEAPI.Reference
{
    public static class Methods
    {
        // A quick way of validating access to the Hi-Rez API.
        public static readonly Method Ping = new Method(path: "/ping[ResponseFormat]", description: "A quick way of validating access to the Hi-Rez API.");
        // A required step to Authenticate the developerId/signature for further API use.
        public static readonly Method Createsession = new Method(path: "/createsession[ResponseFormat]/{developerId}/{signature}/{timestamp}", description: "A required step to Authenticate the developerId/signature for further API use.");
        // A means of validating that a session is established.
        public static readonly Method Testsession = new Method(path: "/testsession[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}", description: "A means of validating that a session is established.");
        // Returns API Developer daily usage limits and the current status against those limits.
        public static readonly Method Getdataused = new Method(path: "/getdataused[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}", description: "Returns API Developer daily usage limits and the current status against those limits.");
        // Function returns UP/DOWN status for the primary game/platform environments.  Data is cached once a minute.
        public static readonly Method Gethirezserverstatus = new Method(path: "/gethirezserverstatus[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}", description: "Function returns UP/DOWN status for the primary game/platform environments.  Data is cached once a minute.");
        // Function returns information about current deployed patch. Currently, this information only includes patch version.
        public static readonly Method Getpatchinfo = new Method(path: "/getpatchinfo[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}", description: "Function returns information about current deployed patch. Currently, this information only includes patch version.");
        // Returns all Gods and their various attributes.
        public static readonly Method Getgods = new Method(path: "/getgods[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{languageCode}", description: "Returns all Gods and their various attributes.");
        // Returns all Champions and their various attributes. [PaladinsAPI only]
        public static readonly Method Getchampions = new Method(path: "/getchampions[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{languageCode}", description: "Returns all Champions and their various attributes. [PaladinsAPI only]");
        // Returns all Champion cards. [PaladinsAPI only]
        public static readonly Method Getchampioncards = new Method(path: "/getchampioncards[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{championId}/{languageCode}", description: "Returns all Champion cards. [PaladinsAPI only]");
        // Returns the current season’s leaderboard for a god/queue combination.  [SmiteAPI; only queues 440, 450, 451]
        public static readonly Method Getgodleaderboard = new Method(path: "/getgodleaderboard[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godId}/{queue}", description: "Returns the current season’s leaderboard for a god/queue combination.  [SmiteAPI; only queues 440, 450, 451]");
        // Returns the current season’s leaderboard for a champion/queue combination.  [PaladinsAPI; only queue 428]
        public static readonly Method Getchampionleaderboard = new Method(path: "/getchampionleaderboard[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godId}/{queue}", description: "Returns the current season’s leaderboard for a champion/queue combination.  [PaladinsAPI; only queue 428]");
        // Returns all available skins for a particular God.
        public static readonly Method Getgodskins = new Method(path: "/getgodskins[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godId}/{languageCode}", description: "Returns all available skins for a particular God.");
        // Returns all available skins for a particular Champion. [PaladinsAPI only]
        public static readonly Method Getchampionskins = new Method(path: "/getchampionskins[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godId}/{languageCode}", description: "Returns all available skins for a particular Champion. [PaladinsAPI only]");
        // Returns the Recommended Items for a particular God.  [SmiteAPI only]
        public static readonly Method Getgodrecommendeditems = new Method(path: "/getgodrecommendeditems[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godid}/{languageCode}", description: "Returns the Recommended Items for a particular God.  [SmiteAPI only]");
        // Returns the Recommended Items for a particular Champion. [PaladinsAPI only; Osbsolete - no data returned]
        public static readonly Method Getchampionecommendeditems = new Method(path: "/getchampionecommendeditems[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{godid}/{languageCode}", description: "Returns the Recommended Items for a particular Champion. [PaladinsAPI only; Osbsolete - no data returned]");
        // Returns all Items and their various attributes.
        public static readonly Method Getitems = new Method(path: "/getitems[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{languagecode}", description: "Returns all Items and their various attributes.");
        // Returns league and other high level data for a particular player.
        public static readonly Method Getplayer = new Method(path: "/getplayer[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{player}/{portalId}", description: "Returns league and other high level data for a particular player.");
        // Returns league and other high level data for a particular player.
        public static readonly Method Getplayer2 = new Method(path: "/getplayer[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{player}", description: "Returns league and other high level data for a particular player.");
        // Returns league and other high level data for a particular CSV set of up to 20 playerIds.  [PaladinsAPI only]
        public static readonly Method Getplayerbatch = new Method(path: "/getplayerbatch[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId,playerId,playerId,...playerId}", description: "Returns league and other high level data for a particular CSV set of up to 20 playerIds.  [PaladinsAPI only]");
        // Function returns a list of Hi-Rez playerId values (expected list size = 1) for playerName provided.  The playerId returned is expected to be used in various other endpoints to represent the player/individual regardless of platform.
        public static readonly Method Getplayeridbyname = new Method(path: "/getplayeridbyname[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerName}", description: "Function returns a list of Hi-Rez playerId values (expected list size = 1) for playerName provided.  The playerId returned is expected to be used in various other endpoints to represent the player/individual regardless of platform.");
        // Function returns a list of Hi-Rez playerId values (expected list size = 1) for {portalId}/{portalUserId} combination provided.  The playerId returned is expected to be used in various other endpoints to represent the player/individual regardless of platform.
        public static readonly Method Getplayeridbyportaluserid = new Method(path: "/getplayeridbyportaluserid[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{portalId}/{portalUserId}", description: "Function returns a list of Hi-Rez playerId values (expected list size = 1) for {portalId}/{portalUserId} combination provided.  The playerId returned is expected to be used in various other endpoints to represent the player/individual regardless of platform.");
        // Function returns a list of Hi-Rez playerId values for {portalId}/{portalUserId} combination provided.  The appropriate playerId extracted from this list by the API end user is expected to be used in various other endpoints to represent the player/individual regardless of platform.
        public static readonly Method Getplayeridsbygamertag = new Method(path: "/getplayeridsbygamertag[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{portalId}/{gamerTag}", description: "Function returns a list of Hi-Rez playerId values for {portalId}/{portalUserId} combination provided.  The appropriate playerId extracted from this list by the API end user is expected to be used in various other endpoints to represent the player/individual regardless of platform.");
        // Meaningful only for the Paladins Xbox API.  Paladins Xbox data and Paladins Switch data is stored in the same DB.  Therefore a Paladins Gamer Tag value could be the same as a Paladins Switch Gamer Tag value.  Additionally, there could be multiple identical Paladins Switch Gamer Tag values.  The purpose of this method is to return all Player ID data associated with the playerName (gamer tag) parameter.  The expectation is that the unique player_id returned could then be used in subsequent method calls.  [PaladinsAPI only]
        public static readonly Method Getplayeridinfoforxboxandswitch = new Method(path: "/getplayeridinfoforxboxandswitch[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerName}", description: "Meaningful only for the Paladins Xbox API.  Paladins Xbox data and Paladins Switch data is stored in the same DB.  Therefore a Paladins Gamer Tag value could be the same as a Paladins Switch Gamer Tag value.  Additionally, there could be multiple identical Paladins Switch Gamer Tag values.  The purpose of this method is to return all Player ID data associated with the playerName (gamer tag) parameter.  The expectation is that the unique player_id returned could then be used in subsequent method calls.  [PaladinsAPI only]");
        // Returns the Smite User names of each of the player’s friends.  [PC only]
        public static readonly Method Getfriends = new Method(path: "/getfriends[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Returns the Smite User names of each of the player’s friends.  [PC only]");
        // Returns the Rank and Worshippers value for each God a player has played.
        public static readonly Method Getgodranks = new Method(path: "/getgodranks[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Returns the Rank and Worshippers value for each God a player has played.");
        // Returns the Rank and Worshippers value for each Champion a player has played. [PaladinsAPI only]
        public static readonly Method Getchampionranks = new Method(path: "/getchampionranks[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Returns the Rank and Worshippers value for each Champion a player has played. [PaladinsAPI only]");
        // Returns deck loadouts per Champion. [PaladinsAPI only]
        public static readonly Method Getplayerloadouts = new Method(path: "/getplayerloadouts[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/playerId}/{languageCode}", description: "Returns deck loadouts per Champion. [PaladinsAPI only]");
        // Returns select achievement totals (Double kills, Tower Kills, First Bloods, etc) for the specified playerId.
        public static readonly Method Getplayerachievements = new Method(path: "/getplayerachievements[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Returns select achievement totals (Double kills, Tower Kills, First Bloods, etc) for the specified playerId.");
        // Returns player status as follows:
        public static readonly Method Getplayerstatus = new Method(path: "/getplayerstatus[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Returns player status as follows: ");
        // Gets recent matches and high level match statistics for a particular player
        public static readonly Method Getmatchhistory = new Method(path: "/getmatchhistory[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}", description: "Gets recent matches and high level match statistics for a particular player");
        // Returns match summary statistics for a (player, queue) combination grouped by gods played.
        public static readonly Method Getqueuestats = new Method(path: "/getqueuestats[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{playerId}/{queue}", description: "Returns match summary statistics for a (player, queue) combination grouped by gods played.");
        // Returns player_id values for all names and/or gamer_tags containing the “searchPlayer” string.
        public static readonly Method Searchplayers = new Method(path: "/searchplayers[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{searchPlayer}", description: "Returns player_id values for all names and/or gamer_tags containing the “searchPlayer” string. ");
        // Returns information regarding a particular match.  Rarely used in lieu of getmatchdetails().
        public static readonly Method Getdemodetails = new Method(path: "/getdemodetails[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{match_id}", description: "Returns information regarding a particular match.  Rarely used in lieu of getmatchdetails().");
        // Returns the statistics for a particular completed match.
        public static readonly Method Getmatchdetails = new Method(path: "/getmatchdetails[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{match_id}", description: "Returns the statistics for a particular completed match.");
        // Returns the statistics for a particular set of completed matches.  NOTE:  There is a byte limit to the amount of data returned; please limit the CSV parameter to 5 to 10 matches because of this and for Hi-Rez DB Performance reasons.
        public static readonly Method Getmatchdetailsbatch = new Method(path: "/getmatchdetailsbatch[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{match_id,match_id,match_id,...match_id}", description: "Returns the statistics for a particular set of completed matches.  NOTE:  There is a byte limit to the amount of data returned; please limit the CSV parameter to 5 to 10 matches because of this and for Hi-Rez DB Performance reasons.");
        // {date}/{hour}
        public static readonly Method Getmatchidsbyqueue = new Method(path: "/getmatchidsbyqueue[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{queue}/", description: "{date}/{hour}");
        // Returns player info
        public static readonly Method Getmatchplayerdetails = new Method(path: "/getmatchplayerdetails[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}/{match_id}", description: "Returns player info");
    }
}
