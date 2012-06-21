using System.Collections.Generic;

namespace libAniDB.NET
{
	public interface IAnime {
		int? AID { get; set; }
		string Year { get; set; }
		string Type { get; set; }
		List<string> RelatedAIDList { get; set; }
		List<string> RelatedAIDType { get; set; }
		List<string> CategoryList { get; set; }
		List<string> CategoryWeightList { get; set; }
		string RomanjiName { get; set; }
		string KanjiName { get; set; }
		string EnglishName { get; set; }
		List<string> OtherName { get; set; }
		List<string> ShortNameList { get; set; }
		List<string> SynonymList { get; set; }
		int? Episodes { get; set; }
		int? HighestEpisodeNumber { get; set; }
		int? SpecialEpisodeCount { get; set; }
		int? AirDate { get; set; }
		int? EndDate { get; set; }
		string Url { get; set; }
		string PicName { get; set; }
		List<string> CategoryIdList { get; set; }
		int? Rating { get; set; }
		int? VoteCount { get; set; }
		int? TempRating { get; set; }
		int? TempVoteCount { get; set; }
		int? AverageReviewRating { get; set; }
		int? ReviewCount { get; set; }
		List<string> AwardList { get; set; }
		bool? IsR18Restricted { get; set; }
	}
}