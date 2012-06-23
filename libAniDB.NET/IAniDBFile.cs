using System.Collections.Generic;

namespace libAniDB.NET
{
	public interface IAniDBFile {
		int? FID { get; set; }
		int? AID { get; set; }
		int? EID { get; set; }
		int? GID { get; set; }
		int? MyListID { get; set; }
		Dictionary<int, byte> OtherEpisodes { get; set; }
		short? IsDeprecated { get; set; }
		AniDBFile.StateMask? State { get; set; }
		long? Size { get; set; }
		string ED2K { get; set; }
		string MD5 { get; set; }
		string SHA1 { get; set; }
		string CRC32 { get; set; }
		string VideoColorDepth { get; set; }
		string Quality { get; set; }
		string Source { get; set; }
		List<string> AudioCodecs { get; set; }
		List<int> AudioBitrates { get; set; }
		string VideoCodec { get; set; }
		int? VideoBitrate { get; set; }
		string VideoResolution { get; set; }
		string FileExtension { get; set; }
		List<string> DubLanguage { get; set; }
		List<string> SubLanguage { get; set; }
		int? Length { get; set; }
		string Description { get; set; }
		int? AiredDate { get; set; }
		string AniDBFileName { get; set; }
		int? MyListState { get; set; }
		int? MyListFileState { get; set; }
		int? MyListViewed { get; set; }
		int? MyListViewDate { get; set; }
		string MyListStorage { get; set; }
		string MyListSource { get; set; }
		string MyListOther { get; set; }
		int? TotalEpisodes { get; set; }
		int? HighestEpisodeNumber { get; set; }
		string Year { get; set; }
		string Type { get; set; }
		List<string> RelatedAIDList { get; set; }
		List<string> RelatedAIDType { get; set; }
		List<string> CategoryList { get; set; }
		string RomanjiName { get; set; }
		string KanjiName { get; set; }
		string EnglishName { get; set; }
		List<string> OtherName { get; set; }
		List<string> ShortNameList { get; set; }
		List<string> SynonymList { get; set; }
		string EpNo { get; set; }
		string EpName { get; set; }
		string EpRomanjiName { get; set; }
		string EpKanjiName { get; set; }
		int? EpisodeRating { get; set; }
		int? EpisodeVoteCount { get; set; }
		string GroupName { get; set; }
		string GroupShortName { get; set; }
		int? DateAIDRecordUpdated { get; set; }
	}
}