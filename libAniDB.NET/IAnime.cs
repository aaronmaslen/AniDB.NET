using System.Collections.Generic;

namespace libAniDB.NET
{
	public interface IAnime {
		int? AID { get; set; }
		string Year { get; set; }
		string Type { get; set; }
		IList<string> RelatedAIDList { get; set; }
		IList<string> RelatedAIDType { get; set; }
		IList<string> CategoryList { get; set; }
		IList<string> CategoryWeightList { get; set; }
		string RomanjiName { get; set; }
		string KanjiName { get; set; }
		string EnglishName { get; set; }
		IList<string> OtherName { get; set; }
		IList<string> ShortNameList { get; set; }
		IList<string> SynonymList { get; set; }
		int? Episodes { get; set; }
		int? HighestEpisodeNumber { get; set; }
		int? SpecialEpisodeCount { get; set; }
		int? AirDate { get; set; }
		int? EndDate { get; set; }
		string Url { get; set; }
		string PicName { get; set; }
		IList<string> CategoryIdList { get; set; }
		int? Rating { get; set; }
		int? VoteCount { get; set; }
		int? TempRating { get; set; }
		int? TempVoteCount { get; set; }
		int? AverageReviewRating { get; set; }
		int? ReviewCount { get; set; }
		IList<string> AwardList { get; set; }
		bool? IsR18Restricted { get; set; }
	}
}