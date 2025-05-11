using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Matches.Get
{
    public interface IGetGolfMatchHandler
    {
        Task<IEnumerable<GolfOpponent>> GetGolfMatch(int year);
    }

    public class GetGolfMatchHandler : IGetGolfMatchHandler
    {
        private readonly DataContext _context;

        public GetGolfMatchHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GolfOpponent>> GetGolfMatch(int year)
        {
            var result = await _context.GolfOpponent
                .FromSqlRaw(@"SELECT G.GolferId
                                    ,G2.GolferId AS OpponentGolferId
                                    ,MS.MatchScheduleId
                                    ,G.Name
                                    ,G2.Name AS OpponentName
                                    ,MS.Week
                                    ,MS.Date
                              FROM GolfMatch GM
                              INNER JOIN Golfer G
                                  ON G.GolferId = GM.GolferId
                              INNER JOIN MatchSchedule MS
                                  ON MS.MatchScheduleId = GM.MatchScheduleId
                              LEFT JOIN GolfMatch GM2
                                  ON GM2.MatchScheduleId = MS.MatchScheduleId AND GM2.GolferId <> GM.GolferId
                              LEFT JOIN Golfer G2
                                  ON G2.GolferId = GM2.GolferId
                              WHERE G.GolferId < G2.GolferId
                                  OR G2.GolferId IS NULL")
                .ToListAsync();

            return result;    
        }
    }
}
