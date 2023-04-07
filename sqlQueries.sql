-- create sports table
CREATE TABLE sports (
	sportsId int PRIMARY KEY IDENTITY,
	sportsName VARCHAR(50),
)
-- create tournament table
CREATE TABLE tournaments (
    tournementID int NOT NULL PRIMARY KEY IDENTITY,
    tournamentName VARCHAR(50) NOT NULL,
    sportsID int FOREIGN KEY REFERENCES sports(sportsId)
)
-- create scoreCard table
CREATE TABLE scoreCard (
    scoreCardID int NOT NULL PRIMARY KEY IDENTITY,
    MatchName VARCHAR(50) NOT NULL,
	teamAScore int NOT NULL,
	teamBScore int not NULL,
	winner VARCHAR(50),
    tournamentID int FOREIGN KEY REFERENCES tournaments(tournementID)
)
SELECT scoreCardID,MatchName,teamAScore,teamBScore,winner FROM scoreCard
--Trigger to update the winner
-- test query for viewing tournaments
SELECT *
FROM tournaments
INNER JOIN sports ON tournaments.sportsID=sports.sportsId