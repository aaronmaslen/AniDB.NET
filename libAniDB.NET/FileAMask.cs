using System;
using System.Collections.Generic;

namespace libAniDB.Net {
	public partial class AniDBFile
	{
		public class AMask {
			[Flags]
			public enum Byte1 : byte {
				TotalEpisodes = 128,
				HighestEpisodeNumber = 64,
				Year = 32,
				Type = 16,
				RelatedAIDList = 8,
				RelatedAIDType = 4,
				CategoryList = 2,
				None = 0
			}

			[Flags]
			public enum Byte2 : byte {
				RomanjiName = 128,
				KanjiName = 64,
				EnglishName = 32,
				OtherName = 16,
				ShortNameList = 8,
				SynonymList = 4,
				None = 0
			}

			[Flags]
			public enum Byte3 : byte {
				EpNo = 128,
				EpName = 64,
				EpRomanjiName = 32,
				EpKanjiName = 16,
				EpisodeRating = 8,
				EpisodeVoteCount = 4,
				None = 0
			}

			[Flags]
			public enum Byte4 : byte {
				GroupName = 128,
				GroupShortName = 64,
				DateAIDRecordUpdated = 1,
				None = 0
			}

			[Flags]
			public enum AMaskValues : uint {
				TotalEpisodes = (uint)Byte1.TotalEpisodes << 8 * 3,
				HighestEpisodeNumber = (uint)Byte1.HighestEpisodeNumber << 8 * 3,
				Year = (uint)Byte1.Year << 8 * 3,
				Type = (uint)Byte1.Type << 8 * 3,
				RelatedAIDList = (uint)Byte1.RelatedAIDList << 8 * 3,
				RelatedAIDType = (uint)Byte1.RelatedAIDType << 8 * 3,
				CategoryList = (uint)Byte1.CategoryList << 8 * 3,

				RomanjiName = (uint)Byte2.RomanjiName << 8 * 2,
				KanjiName = (uint)Byte2.KanjiName << 8 * 2,
				EnglishName = (uint)Byte2.EnglishName << 8 * 2,
				OtherName = (uint)Byte2.OtherName << 8 * 2,
				ShortNameList = (uint)Byte2.ShortNameList << 8 * 2,
				SynonymList = (uint)Byte2.SynonymList << 8 * 2,

				EpNo = (uint)Byte3.EpNo << 8 * 1,
				EpName = (uint)Byte3.EpName << 8 * 1,
				EpRomanjiName = (uint)Byte3.EpRomanjiName << 8 * 1,
				EpKanjiName = (uint)Byte3.EpKanjiName << 8 * 1,
				EpisodeRating = (uint)Byte3.EpisodeRating << 8 * 1,
				EpisodeVoteCount = (uint)Byte3.EpisodeVoteCount << 8 * 1,

				GroupName = (uint)Byte4.GroupName << 8 * 0,
				GroupShortName = (uint)Byte4.GroupShortName << 8 * 0,
				DateAIDRecordUpdated = (uint)Byte4.DateAIDRecordUpdated << 8 * 0,
				None = 0
			}

			public AMaskValues Mask { get; private set; }
			public string MaskString { get
			{
				return Mask.ToString("x");
			} }

			public AMask(Byte1 byte1 = Byte1.None, Byte2 byte2 = Byte2.None, Byte3 byte3 = Byte3.None,
						 Byte4 byte4 = Byte4.None) {
				Mask = (AMaskValues)(((int)byte1 << 8 * 3) |
					((int)byte2 << 8 * 2) |
					((int)byte3 << 8 * 1) |
					((int)byte4));
			}

			public AMask(AMaskValues aMaskValues) {
				Mask = aMaskValues;
			}

			public override string ToString() {
				return MaskString;
			}
		}

		protected static readonly ReadOnlyDictionary<AMask.AMaskValues, Type> AMaskTypes =
			new ReadOnlyDictionary<AMask.AMaskValues, Type>(
				new Dictionary<AMask.AMaskValues, Type>
				{
					#region AMaskTypes
					{AMask.AMaskValues.TotalEpisodes, typeof (int)},
					{AMask.AMaskValues.HighestEpisodeNumber, typeof (int)},
					{AMask.AMaskValues.Year, typeof (string)},
					{AMask.AMaskValues.Type, typeof (string)},
					{AMask.AMaskValues.RelatedAIDList, typeof (List<string>)},
					{AMask.AMaskValues.RelatedAIDType, typeof (List<string>)},
					{AMask.AMaskValues.CategoryList, typeof (List<string>)},
					{AMask.AMaskValues.RomanjiName, typeof (string)},
					{AMask.AMaskValues.KanjiName, typeof (string)},
					{AMask.AMaskValues.EnglishName, typeof (string)},
					{AMask.AMaskValues.OtherName, typeof (List<string>)},
					{AMask.AMaskValues.ShortNameList, typeof (List<string>)},
					{AMask.AMaskValues.SynonymList, typeof (List<string>)},
					{AMask.AMaskValues.EpNo, typeof (string)},
					{AMask.AMaskValues.EpName, typeof (string)},
					{AMask.AMaskValues.EpRomanjiName, typeof (string)},
					{AMask.AMaskValues.EpKanjiName, typeof (string)},
					{AMask.AMaskValues.EpisodeRating, typeof (int)},
					{AMask.AMaskValues.EpisodeVoteCount, typeof (int)},
					{AMask.AMaskValues.GroupName, typeof (string)},
					{AMask.AMaskValues.GroupShortName, typeof (string)},
					{AMask.AMaskValues.DateAIDRecordUpdated, typeof (int)}
					#endregion
				});

		public static readonly ReadOnlyDictionary<AMask.AMaskValues, string> AMaskNames =
			new ReadOnlyDictionary<AMask.AMaskValues, string>(
				new Dictionary<AMask.AMaskValues, string>
			    {
					{AMask.AMaskValues.TotalEpisodes, "Total Episodes"},
					{AMask.AMaskValues.HighestEpisodeNumber, "Highest Episode Number"},
					{AMask.AMaskValues.Year, "Year"},
					{AMask.AMaskValues.Type, "Type"},
					{AMask.AMaskValues.RelatedAIDList, "Related AIDs"},
					{AMask.AMaskValues.RelatedAIDType, "Related AID Types"},
					{AMask.AMaskValues.CategoryList, "Categories"},
					{AMask.AMaskValues.RomanjiName, "Romanji Name"},
					{AMask.AMaskValues.KanjiName, "Kanji Name"},
					{AMask.AMaskValues.EnglishName, "English Name"},
					{AMask.AMaskValues.OtherName, "Other Name"},
					{AMask.AMaskValues.ShortNameList, "Short Name List"},
					{AMask.AMaskValues.SynonymList, "Synonym List"},
					{AMask.AMaskValues.EpNo, "Episode No."},
					{AMask.AMaskValues.EpName, "Episode Name"},
					{AMask.AMaskValues.EpRomanjiName, "Episode Romanji Name"},
					{AMask.AMaskValues.EpKanjiName, "Episode Kanji Name"},
					{AMask.AMaskValues.EpisodeRating, "Episode Rating"},
					{AMask.AMaskValues.EpisodeVoteCount, "Episode Vote Count"},
					{AMask.AMaskValues.GroupName, "Group Name"},
					{AMask.AMaskValues.GroupShortName, "Group Short Name"},
					{AMask.AMaskValues.DateAIDRecordUpdated, "AID Record Updated"}
				});
			
		#region AMask public properties
		public int? TotalEpisodes {
			get { return (int?)GetAMaskValue(AMask.AMaskValues.TotalEpisodes); }
			set { SetAMaskValue(AMask.AMaskValues.TotalEpisodes, value); }
		}
		public int? HighestEpisodeNumber {
			get { return (int?)GetAMaskValue(AMask.AMaskValues.HighestEpisodeNumber); }
			set { SetAMaskValue(AMask.AMaskValues.HighestEpisodeNumber, value); }
		}
		public string Year {
			get { return (string)GetAMaskValue(AMask.AMaskValues.Year); }
			set { SetAMaskValue(AMask.AMaskValues.Year, value); }
		}
		public string Type {
			get { return (string)GetAMaskValue(AMask.AMaskValues.Type); }
			set { SetAMaskValue(AMask.AMaskValues.Type, value); }
		}
		public List<string> RelatedAIDList {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.RelatedAIDList); }
			set { SetAMaskValue(AMask.AMaskValues.RelatedAIDList, value); }
		}
		public List<string> RelatedAIDType {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.RelatedAIDType); }
			set { SetAMaskValue(AMask.AMaskValues.RelatedAIDType, value); }
		}
		public List<string> CategoryList {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.CategoryList); }
			set { SetAMaskValue(AMask.AMaskValues.CategoryList, value); }
		}
		public string RomanjiName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.RomanjiName); }
			set { SetAMaskValue(AMask.AMaskValues.RomanjiName, value); }
		}
		public string KanjiName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.KanjiName); }
			set { SetAMaskValue(AMask.AMaskValues.KanjiName, value); }
		}
		public string EnglishName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.EnglishName); }
			set { SetAMaskValue(AMask.AMaskValues.EnglishName, value); }
		}
		public List<string> OtherName {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.OtherName); }
			set { SetAMaskValue(AMask.AMaskValues.OtherName, value); }
		}
		public List<string> ShortNameList {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.ShortNameList); }
			set { SetAMaskValue(AMask.AMaskValues.ShortNameList, value); }
		}
		public List<string> SynonymList {
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.SynonymList); }
			set { SetAMaskValue(AMask.AMaskValues.SynonymList, value); }
		}
		public string EpNo {
			get { return (string)GetAMaskValue(AMask.AMaskValues.EpNo); }
			set { SetAMaskValue(AMask.AMaskValues.EpNo, value); }
		}
		public string EpName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.EpName); }
			set { SetAMaskValue(AMask.AMaskValues.EpName, value); }
		}
		public string EpRomanjiName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.EpRomanjiName); }
			set { SetAMaskValue(AMask.AMaskValues.EpRomanjiName, value); }
		}
		public string EpKanjiName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.EpKanjiName); }
			set { SetAMaskValue(AMask.AMaskValues.EpKanjiName, value); }
		}
		public int? EpisodeRating {
			get { return (int?)GetAMaskValue(AMask.AMaskValues.EpisodeRating); }
			set { SetAMaskValue(AMask.AMaskValues.EpisodeRating, value); }
		}
		public int? EpisodeVoteCount {
			get { return (int?)GetAMaskValue(AMask.AMaskValues.EpisodeVoteCount); }
			set { SetAMaskValue(AMask.AMaskValues.EpisodeVoteCount, value); }
		}
		public string GroupName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.GroupName); }
			set { SetAMaskValue(AMask.AMaskValues.GroupName, value); }
		}
		public string GroupShortName {
			get { return (string)GetAMaskValue(AMask.AMaskValues.GroupShortName); }
			set { SetAMaskValue(AMask.AMaskValues.GroupShortName, value); }
		}
		public int? DateAIDRecordUpdated {
			get { return (int?)GetAMaskValue(AMask.AMaskValues.DateAIDRecordUpdated); }
			set { SetAMaskValue(AMask.AMaskValues.DateAIDRecordUpdated, value); }
		}
		#endregion

		protected Dictionary<AMask.AMaskValues, object> AMaskFields;

		public object GetAMaskValue(AMask.AMaskValues a) {
			return (AMaskFields.ContainsKey(a) ? AMaskFields[a] : null);
		}

		public void SetAMaskValue(AMask.AMaskValues a, object value) {
			if (value == null ||
				(AMaskTypes[a] == typeof(int) && (int)value == -1) ||
				(AMaskTypes[a] == typeof(string) && (string)value == ""))
			{
				if (AMaskFields.ContainsKey(a))
					AMaskFields.Remove(a);

				return;
			}

			if (AMaskFields.ContainsKey(a))
				AMaskFields[a] = value;
			else
				AMaskFields.Add(a, value);
		}
	}
}
