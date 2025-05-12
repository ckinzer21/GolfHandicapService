using AutoMapper;
using AutoMapper.QueryableExtensions;
using GolfHandicap.Common;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Matches.Get
{
    public interface IGetGolfMatchHandler
    {
        Task<IEnumerable<GetGolfMatchResponse>> GetGolfMatchByYear(int year);
        Task<IEnumerable<GetGolfMatchResponse>> GetGolfMatchByYearAndWeek(int year, int week);
    }

    public class GetGolfMatchHandler : IGetGolfMatchHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;
        private readonly IMapper _mapper;

        public GetGolfMatchHandler(DataContext context, IGetHandicap getHandicap, IMapper mapper)
        {
            _context = context;
            _getHandicap = getHandicap;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetGolfMatchResponse>> GetGolfMatchByYear(int year)
        {
            var results = await _context.GolfOpponent
                .FromSqlInterpolated($@"SELECT G.GolferId
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
                              WHERE (G.GolferId < G2.GolferId
                                  OR G2.GolferId IS NULL)
                                  AND strftime('%Y', MS.Date) = {year.ToString()}")//ToString() is needed
                .AsNoTracking()
                .ProjectTo<GetGolfMatchResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return results;    
        }

        public async Task<IEnumerable<GetGolfMatchResponse>> GetGolfMatchByYearAndWeek(int year, int week)
        {
            var results = await _context.GolfOpponent
                .FromSqlInterpolated($@"SELECT G.GolferId
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
                              WHERE (G.GolferId < G2.GolferId
                                  OR G2.GolferId IS NULL)
                                  AND strftime('%Y', MS.Date) = {year.ToString()}
                                  AND Week = {week}")
                .AsNoTracking()
                .ProjectTo<GetGolfMatchResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var tasks = results.Select(async result =>
            {
                var handicap = await _getHandicap.GetIndexAndRounded(result.GolferId);
                var opponentHandicap = await _getHandicap.GetIndexAndRounded(result.OpponentGolferId);
                result.HandicapIndex = handicap.HandicapIndex;
                result.RoundedHandicap = handicap.RoundedHandicap;
                result.OpponentHandicapIndex = opponentHandicap.HandicapIndex;
                result.OpponentRoundedHandicap = opponentHandicap.RoundedHandicap;
            });

            await Task.WhenAll(tasks);

            return results;
        }
    }
}
