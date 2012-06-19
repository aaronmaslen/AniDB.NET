/* Copyright 2012 Aaron Maslen. All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * 
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL
 * THE FOUNDATION OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using libAniDB.NET;

namespace libAniDB.NET
{
	public class Anime
	{
		[Flags]
		public enum DateFlag
		{
			None = 0,
			StartDateUnknownDay = 1,
			StartDateUnknownMonthDay = 2,
			EndDateUnknownDay = 4,
			EndDateUnknownMonthDay = 8,
			AnimeEnded = 16,
			StartDateUnknownYear = 32,
			EndDateUnknownYear = 64,
		}

		public class AMask
		{
			[Flags]
			public enum Byte1 : byte
			{
				AID = 128,
				DateFlags = 64,
				Year = 32,
				Type = 16,
				RelatedAIDList = 8,
				RelatedAIDType = 4,
				CategoryList = 2,
				CategoryWeightList = 1,
				None = 0,
			}

			[Flags]
			public enum Byte2 : byte
			{
				RomanjiName = 128,
				KanjiName = 64,
				EnglishName = 32,
				OtherName = 16,
				ShortNameList = 8,
				SynonymList = 4,
				None = 0,
			}

			[Flags]
			public enum Byte3 : byte
			{
				Episodes = 128,
				HighestEpisodeNumber = 64,
				SpecialEpCount = 32,
				AirDate = 16,
				EndDate = 8,
				Url = 4,
				PicName = 2,
				CategoryIdList = 1,
				None = 0,
			}

			[Flags]
			public enum Byte4 : byte
			{
				Rating = 128,
				VoteCount = 64,
				TempRating = 32,
				TempVoteCount = 16,
				AverageReviewRating = 8,
				ReviewCount = 4,
				AwardList = 2,
				IsR18Restricted = 1,
				None = 0,
			}

			[Flags]
			public enum Byte5 : byte
			{
				AnimePlanetId = 128,
				AnnId = 64,
				AllCinemaId = 32,
				AnimeNfoId = 16,
				DateRecordUpdated = 1,
				None = 0,
			}

			[Flags]
			public enum Byte6 : byte
			{
				CharacterIdList = 128,
				CreatorIdList = 64,
				MainCreatorIdList = 32,
				MainCreatorNameList = 16,
				None = 0,
			}

			[Flags]
			public enum Byte7 : byte
			{
				SpecialsCount = 128,
				CreditsCount = 64,
				OtherCount = 32,
				TrailerCount = 16,
				ParodyCount = 8,
				None = 0,
			}

			[Flags]
			public enum AMaskValues : ulong
			{
				AID = (ulong)Byte1.AID << 8 * 6,
				DateFlags = (ulong)Byte1.DateFlags << 8 * 6,
				Year = (ulong)Byte1.Year << 8 * 6,
				Type = (ulong)Byte1.Type << 8 * 6,
				RelatedAIDList = (ulong)Byte1.RelatedAIDList << 8 * 6,
				RelatedAIDType = (ulong)Byte1.RelatedAIDType << 8 * 6,
				CategoryList = (ulong)Byte1.CategoryList << 8 * 6,
				CategoryWeightList = (ulong)Byte1.CategoryWeightList << 8 * 6,

				RomanjiName = (ulong)Byte2.RomanjiName << 8 * 5,
				KanjiName = (ulong)Byte2.KanjiName << 8 * 5,
				EnglishName = (ulong)Byte2.EnglishName << 8 * 5,
				OtherName = (ulong)Byte2.OtherName << 8 * 5,
				ShortNameList = (ulong)Byte2.ShortNameList << 8 * 5,
				SynonymList = (ulong)Byte2.SynonymList << 8 * 5,

				Episodes = (ulong)Byte3.Episodes << 8 * 4,
				HighestEpisodeNumber = (ulong)Byte3.HighestEpisodeNumber << 8 * 4,
				SpecialEpCount = (ulong)Byte3.SpecialEpCount << 8 * 4,
				AirDate = (ulong)Byte3.AirDate << 8 * 4,
				EndDate = (ulong)Byte3.EndDate << 8 * 4,
				Url = (ulong)Byte3.Url << 8 * 4,
				PicName = (ulong)Byte3.PicName << 8 * 4,
				CategoryIdList = (ulong)Byte3.CategoryIdList << 8 * 4,

				Rating = (ulong)Byte4.Rating << 8 * 3,
				VoteCount = (ulong)Byte4.VoteCount << 8 * 3,
				TempRating = (ulong)Byte4.TempRating << 8 * 3,
				TempVoteCount = (ulong)Byte4.TempVoteCount << 8 * 3,
				AverageReviewRating = (ulong)Byte4.AverageReviewRating << 8 * 3,
				ReviewCount = (ulong)Byte4.ReviewCount << 8 * 3,
				AwardList = (ulong)Byte4.AwardList << 8 * 3,
				IsR18Restricted = (ulong)Byte4.IsR18Restricted << 8 * 3,

				AnimePlanetId = (ulong)Byte5.AnimePlanetId << 8 * 2,
				AnnId = (ulong)Byte5.AnnId << 8 * 2,
				AllCinemaId = (ulong)Byte5.AllCinemaId << 8 * 2,
				AnimeNfoId = (ulong)Byte5.AnimeNfoId << 8 * 2,
				DateRecordUpdated = (ulong)Byte5.DateRecordUpdated << 8 * 2,

				CharacterIdList = (ulong)Byte6.CharacterIdList << 8 * 1,
				CreatorIdList = (ulong)Byte6.CreatorIdList << 8 * 1,
				MainCreatorIdList = (ulong)Byte6.MainCreatorIdList << 8 * 1,
				MainCreatorNameList = (ulong)Byte6.MainCreatorNameList << 8 * 1,

				SpecialsCount = (ulong)Byte7.SpecialsCount,
				CreditsCount = (ulong)Byte7.CreditsCount,
				OtherCount = (ulong)Byte7.OtherCount,
				TrailerCount = (ulong)Byte7.TrailerCount,
				ParodyCount = (ulong)Byte7.ParodyCount,

				None = 0,
			}

			public AMaskValues Mask { get; private set; }

			public string MaskString
			{
				get { return Mask.ToString("x").Remove(0, 2); }
			}

			public AMask(Byte1 byte1 = Byte1.None, Byte2 byte2 = Byte2.None, Byte3 byte3 = Byte3.None,
			             Byte4 byte4 = Byte4.None, Byte5 byte5 = Byte5.None, Byte6 byte6 = Byte6.None,
			             Byte7 byte7 = Byte7.None)
			{
				Mask = (AMaskValues)
				       ((ulong)byte1 << 8 * 6 |
				        (ulong)byte2 << 8 * 5 |
				        (ulong)byte3 << 8 * 4 |
				        (ulong)byte4 << 8 * 3 |
				        (ulong)byte5 << 8 * 2 |
				        (ulong)byte6 << 8 * 1 |
				        (ulong)byte7 << 8 * 0);
			}

			public AMask(AMaskValues aMaskValues)
			{
				Mask = aMaskValues;
			}
		}

		public enum AIDType
		{
			Sequel = 1,
			Prequel = 2,
			SameSetting = 11,
			AlternativeSetting = 21,
			AlternativeVersion = 32,
			MusicVideo = 41,
			Character = 42,
			SideStory = 51,
			ParentStory = 52,
			Summary = 61,
			FullStory = 62,
			Other = 11,
		}

		protected static readonly ReadOnlyDictionary<AMask.AMaskValues, Type> AMaskTypes =
			new ReadOnlyDictionary<AMask.AMaskValues, Type>(
				new Dictionary<AMask.AMaskValues, Type>
				{
					#region AMaskTypes
					{ AMask.AMaskValues.AID, typeof (int) },
					{ AMask.AMaskValues.DateFlags, typeof(DateFlag) },
					{ AMask.AMaskValues.Year, typeof (string) },
					{ AMask.AMaskValues.Type, typeof (string) },
					{ AMask.AMaskValues.RelatedAIDList, typeof (List<string>) },
					{ AMask.AMaskValues.RelatedAIDType, typeof (List<AIDType>) },
					{ AMask.AMaskValues.CategoryList, typeof (List<string>) },
					{ AMask.AMaskValues.CategoryWeightList, typeof (List<string>) },
					{ AMask.AMaskValues.RomanjiName, typeof (string) },
					{ AMask.AMaskValues.KanjiName, typeof (string) },
					{ AMask.AMaskValues.EnglishName, typeof (string) },
					{ AMask.AMaskValues.OtherName, typeof (List<string>) },
					{ AMask.AMaskValues.ShortNameList, typeof (List<string>) },
					{ AMask.AMaskValues.SynonymList, typeof (List<string>) },
					{ AMask.AMaskValues.Episodes, typeof (int) },
					{ AMask.AMaskValues.HighestEpisodeNumber, typeof (int) },
					{ AMask.AMaskValues.SpecialEpCount, typeof (int) },
					{ AMask.AMaskValues.AirDate, typeof (int) },
					{ AMask.AMaskValues.EndDate, typeof (int) },
					{ AMask.AMaskValues.Url, typeof (string) },
					{ AMask.AMaskValues.PicName, typeof (string) },
					{ AMask.AMaskValues.CategoryIdList, typeof (List<string>) },
					{ AMask.AMaskValues.Rating, typeof (int) },
					{ AMask.AMaskValues.VoteCount, typeof (int) },
					{ AMask.AMaskValues.TempRating, typeof (int) },
					{ AMask.AMaskValues.TempVoteCount, typeof (int) },
					{ AMask.AMaskValues.AverageReviewRating, typeof (int) },
					{ AMask.AMaskValues.ReviewCount, typeof (int) },
					{ AMask.AMaskValues.AwardList, typeof (List<string>) },
					{ AMask.AMaskValues.IsR18Restricted, typeof (bool) },
					{ AMask.AMaskValues.AnimePlanetId, typeof (int) },
					{ AMask.AMaskValues.AnnId, typeof (int) },
					{ AMask.AMaskValues.AllCinemaId, typeof (int) },
					{ AMask.AMaskValues.AnimeNfoId, typeof (string) },
					{ AMask.AMaskValues.DateRecordUpdated, typeof (int) },
					{ AMask.AMaskValues.CharacterIdList, typeof (List<int>) },
					{ AMask.AMaskValues.CreatorIdList, typeof (List<int>) },
					{ AMask.AMaskValues.MainCreatorIdList, typeof (List<int>) },
					{ AMask.AMaskValues.MainCreatorNameList, typeof (List<string>) },
					{ AMask.AMaskValues.SpecialsCount, typeof (int) },
					{ AMask.AMaskValues.CreditsCount, typeof (int) },
					{ AMask.AMaskValues.OtherCount, typeof (int) },
					{ AMask.AMaskValues.TrailerCount, typeof (int) },
					{ AMask.AMaskValues.ParodyCount, typeof (int) },

					#endregion
				});

		public static readonly ReadOnlyDictionary<AMask.AMaskValues, string> AMaskNames =
			new ReadOnlyDictionary<AMask.AMaskValues, string>(
				new Dictionary<AMask.AMaskValues, string>
				{
					#region AMaskNames
					{ AMask.AMaskValues.AID, "AID" },
					{ AMask.AMaskValues.DateFlags, "Date Flags" },
					{ AMask.AMaskValues.Year, "Year" },
					{ AMask.AMaskValues.Type, "Type" },
					{ AMask.AMaskValues.RelatedAIDList, "Related AID List" },
					{ AMask.AMaskValues.RelatedAIDType, "Related AID Types" },
					{ AMask.AMaskValues.CategoryList, "Category List" },
					{ AMask.AMaskValues.CategoryWeightList, "Category List Weights" },
					{ AMask.AMaskValues.RomanjiName, "Romanji Name" },
					{ AMask.AMaskValues.KanjiName, "Kanji Name" },
					{ AMask.AMaskValues.EnglishName, "English Name" },
					{ AMask.AMaskValues.OtherName, "Other Names" },
					{ AMask.AMaskValues.ShortNameList, "Short Name List" },
					{ AMask.AMaskValues.SynonymList, "Synonym List" },
					{ AMask.AMaskValues.Episodes, "Episodes" },
					{ AMask.AMaskValues.HighestEpisodeNumber, "Highest Episode Number" },
					{ AMask.AMaskValues.SpecialEpCount, "Special Episodes" },
					{ AMask.AMaskValues.AirDate, "Air Date" },
					{ AMask.AMaskValues.EndDate, "End Date" },
					{ AMask.AMaskValues.Url, "URL" },
					{ AMask.AMaskValues.PicName, "Pic Name" },
					{ AMask.AMaskValues.CategoryIdList, "Category ID List" },
					{ AMask.AMaskValues.Rating, "Rating" },
					{ AMask.AMaskValues.VoteCount, "Vote Count" },
					{ AMask.AMaskValues.TempRating, "Temp. Rating" },
					{ AMask.AMaskValues.TempVoteCount, "Temp. Vote Count" },
					{ AMask.AMaskValues.AverageReviewRating, "Average Review Rating" },
					{ AMask.AMaskValues.ReviewCount, "Review Count" },
					{ AMask.AMaskValues.AwardList, "Award List" },
					{ AMask.AMaskValues.IsR18Restricted, "18+ Restricted" },
					{ AMask.AMaskValues.AnimePlanetId, "Anime Plant ID" },
					{ AMask.AMaskValues.AnnId, "ANN ID" },
					{ AMask.AMaskValues.AllCinemaId, "AllCinema ID" },
					{ AMask.AMaskValues.AnimeNfoId, "AnimeNFO ID" },
					{ AMask.AMaskValues.DateRecordUpdated, "Date Record Updated" },
					{ AMask.AMaskValues.CharacterIdList, "Character ID List" },
					{ AMask.AMaskValues.CreatorIdList, "Creator ID List" },
					{ AMask.AMaskValues.MainCreatorIdList, "Main Creator ID List" },
					{ AMask.AMaskValues.MainCreatorNameList, "Main Creator Name List" },
					{ AMask.AMaskValues.SpecialsCount, "Specials count" },
					{ AMask.AMaskValues.CreditsCount, "Credits count" },
					{ AMask.AMaskValues.OtherCount, "Other Count" },
					{ AMask.AMaskValues.TrailerCount, "Trailer Count" },
					{ AMask.AMaskValues.ParodyCount, "Parody Count" },

					#endregion
				});

		protected readonly Dictionary<AMask.AMaskValues, object> AMaskFields;

		#region AMask Public Fields

		public int? AID
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.AID); }

			set { SetAMaskValue(AMask.AMaskValues.AID, value); }
		}

		public DateFlag? DateFlags
		{
			get { return (DateFlag?) GetAMaskValue(AMask.AMaskValues.DateFlags); }
			set { SetAMaskValue(AMask.AMaskValues.DateFlags, value); }
		}

		public string Year
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.Year); }

			set { SetAMaskValue(AMask.AMaskValues.Year, value); }
		}

		public string Type
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.Type); }

			set { SetAMaskValue(AMask.AMaskValues.Type, value); }
		}

		public List<string> RelatedAIDList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.RelatedAIDList); }

			set { SetAMaskValue(AMask.AMaskValues.RelatedAIDList, value); }
		}

		public List<string> RelatedAIDType
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.RelatedAIDType); }

			set { SetAMaskValue(AMask.AMaskValues.RelatedAIDType, value); }
		}

		public List<string> CategoryList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.CategoryList); }

			set { SetAMaskValue(AMask.AMaskValues.CategoryList, value); }
		}

		public List<string> CategoryWeightList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.CategoryWeightList); }

			set { SetAMaskValue(AMask.AMaskValues.CategoryWeightList, value); }
		}

		public string RomanjiName
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.RomanjiName); }

			set { SetAMaskValue(AMask.AMaskValues.RomanjiName, value); }
		}

		public string KanjiName
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.KanjiName); }

			set { SetAMaskValue(AMask.AMaskValues.KanjiName, value); }
		}

		public string EnglishName
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.EnglishName); }

			set { SetAMaskValue(AMask.AMaskValues.EnglishName, value); }
		}

		public List<string> OtherName
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.OtherName); }

			set { SetAMaskValue(AMask.AMaskValues.OtherName, value); }
		}

		public List<string> ShortNameList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.ShortNameList); }

			set { SetAMaskValue(AMask.AMaskValues.ShortNameList, value); }
		}

		public List<string> SynonymList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.SynonymList); }

			set { SetAMaskValue(AMask.AMaskValues.SynonymList, value); }
		}

		public int? Episodes
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.Episodes); }

			set { SetAMaskValue(AMask.AMaskValues.Episodes, value); }
		}

		public int? HighestEpisodeNumber
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.HighestEpisodeNumber); }

			set { SetAMaskValue(AMask.AMaskValues.HighestEpisodeNumber, value); }
		}

		public int? SpecialEpCount
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.SpecialEpCount); }

			set { SetAMaskValue(AMask.AMaskValues.SpecialEpCount, value); }
		}

		public int? AirDate
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.AirDate); }

			set { SetAMaskValue(AMask.AMaskValues.AirDate, value); }
		}

		public int? EndDate
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.EndDate); }

			set { SetAMaskValue(AMask.AMaskValues.EndDate, value); }
		}

		public string Url
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.Url); }

			set { SetAMaskValue(AMask.AMaskValues.Url, value); }
		}

		public string PicName
		{
			get { return (string)GetAMaskValue(AMask.AMaskValues.PicName); }

			set { SetAMaskValue(AMask.AMaskValues.PicName, value); }
		}

		public List<string> CategoryIdList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.CategoryIdList); }

			set { SetAMaskValue(AMask.AMaskValues.CategoryIdList, value); }
		}

		public int? Rating
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.Rating); }

			set { SetAMaskValue(AMask.AMaskValues.Rating, value); }
		}

		public int? VoteCount
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.VoteCount); }

			set { SetAMaskValue(AMask.AMaskValues.VoteCount, value); }
		}

		public int? TempRating
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.TempRating); }

			set { SetAMaskValue(AMask.AMaskValues.TempRating, value); }
		}

		public int? TempVoteCount
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.TempVoteCount); }

			set { SetAMaskValue(AMask.AMaskValues.TempVoteCount, value); }
		}

		public int? AverageReviewRating
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.AverageReviewRating); }

			set { SetAMaskValue(AMask.AMaskValues.AverageReviewRating, value); }
		}

		public int? ReviewCount
		{
			get { return (int?)GetAMaskValue(AMask.AMaskValues.ReviewCount); }

			set { SetAMaskValue(AMask.AMaskValues.ReviewCount, value); }
		}

		public List<string> AwardList
		{
			get { return (List<string>)GetAMaskValue(AMask.AMaskValues.AwardList); }

			set { SetAMaskValue(AMask.AMaskValues.AwardList, value); }
		}

		public bool? IsR18Restricted
		{
			get { return (bool?)GetAMaskValue(AMask.AMaskValues.IsR18Restricted); }

			set { SetAMaskValue(AMask.AMaskValues.IsR18Restricted, value); }
		}

		#endregion

		public object GetAMaskValue(AMask.AMaskValues a)
		{
			return AMaskFields.ContainsKey(a) ? AMaskFields[a] : null;
		}

		public void SetAMaskValue(AMask.AMaskValues a, object value)
		{
			if (value == null)
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

		public Anime()
		{
			AMaskFields = new Dictionary<AMask.AMaskValues, object>();
		}

		public Anime(AniDBResponse response, AMask aMask) : this()
		{
			List<string> dataFields = new List<string>();
			foreach (string[] s in response.DataFields)
				dataFields.AddRange(s);

			int currentIndex = 0;

			for (int i = 55; i >= 0; i--)
			{
				if (currentIndex >= dataFields.Count) break;

				AMask.AMaskValues flag = (AMask.AMaskValues)((ulong)Math.Pow(2, i));

				object field = null;

				if (!aMask.Mask.HasFlag(flag)) continue;

				if (AMaskTypes[flag] == typeof (string))
					field = dataFields[currentIndex];
				else if (AMaskTypes[flag] == typeof (int))
					field = int.Parse(dataFields[currentIndex]);
				else if (AMaskTypes[flag] == typeof (bool))
					field = bool.Parse(dataFields[currentIndex]);
				else if (AMaskTypes[flag] == typeof (List<string>))
					//TODO: Make sure these are the only possibilities (and are the right choices)

					field = new List<string>(dataFields[currentIndex].Split(flag == AMask.AMaskValues.CategoryList ? ',' : '\''));
				else if (AMaskTypes[flag] == typeof (List<AIDType>))
				{
					field = new List<AIDType>();

					foreach (string s in dataFields[currentIndex].Split('\''))
						((List<AIDType>)field).Add((AIDType)int.Parse(s));
				}
				else if (AMaskTypes[flag] == typeof(DateFlag))
					field = (DateFlag) int.Parse(dataFields[currentIndex]);

				currentIndex++;

				if (field != null)
					AMaskFields.Add(flag, field);
			}
		}
	}
}
