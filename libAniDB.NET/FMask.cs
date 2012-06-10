/* Copyright 2011 Aaron Maslen. All rights reserved.
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

namespace libAniDB.NET
{
	public partial class AniDBFile
	{
		public class FMask
		{
			[Flags]
			public enum Byte1 : byte
			{
				//Byte 1
				AID = 64,
				EID = 32,
				GID = 16,
				MyListID = 8,
				OtherEpisodes = 4,
				IsDeprecated = 2,
				State = 1,
				None = 0
			}

			[Flags]
			public enum Byte2 : byte
			{
				//Byte 2
				Size = 128,
				ED2K = 64,
				MD5 = 32,
				SHA1 = 16,
				CRC32 = 8,
				None = 0
			}

			[Flags]
			public enum Byte3 : byte
			{
				//Byte 3
				Quality = 128,
				Source = 64,
				AudioCodecs = 32,
				AudioBitrates = 16,
				VideoCodec = 8,
				VideoBitrate = 4,
				VideoResolution = 2,
				FileType = 1,
				None = 0
			}

			[Flags]
			public enum Byte4 : byte
			{
				//Byte 4
				DubLanguage = 128,
				SubLanguage = 64,
				Length = 32,
				Description = 16,
				AiredDate = 8,
				AniDBFileName = 1,
				None = 0
			}

			[Flags]
			public enum Byte5 : byte
			{
				//Byte 5
				MyListState = 128,
				MyListFileState = 64,
				MyListViewed = 32,
				MyListViewDate = 16,
				MyListStorage = 8,
				MyListSource = 4,
				MyListOther = 2,
				None = 0
			}

			[Flags]
			public enum FMaskValues : long
			{
				//Byte 1
				AID = (long)Byte1.AID << 8 * 4,
				EID = (long)Byte1.EID << 8 * 4,
				GID = (long)Byte1.GID << 8 * 4,
				MyListID = (long)Byte1.MyListID << 8 * 4,
				OtherEpisodes = (long)Byte1.OtherEpisodes << 8 * 4,
				IsDeprecated = (long)Byte1.IsDeprecated << 8 * 4,
				State = (long)Byte1.State << 8 * 4,

				//Byte 2
				Size = (long)Byte2.Size << 8 * 3,
				ED2K = (long)Byte2.ED2K << 8 * 3,
				MD5 = (long)Byte2.MD5 << 8 * 3,
				SHA1 = (long)Byte2.SHA1 << 8 * 3,
				CRC32 = (long)Byte2.CRC32 << 8 * 3,

				//Byte 3
				Quality = (long)Byte3.Quality << 8 * 2,
				Source = (long)Byte3.Source << 8 * 2,
				AudioCodecs = (long)Byte3.AudioCodecs << 8 * 2,
				AudioBitrates = (long)Byte3.AudioBitrates << 8 * 2,
				VideoCodec = (long)Byte3.VideoCodec << 8 * 2,
				VideoBitrate = (long)Byte3.VideoBitrate << 8 * 2,
				VideoResolution = (long)Byte3.VideoResolution << 8 * 2,
				FileType = (long)Byte3.FileType << 8 * 2,

				//Byte 4
				DubLanguage = (long)Byte4.DubLanguage << 8 * 1,
				SubLanguage = (long)Byte4.SubLanguage << 8 * 1,
				Length = (long)Byte4.Length << 8 * 1,
				Description = (long)Byte4.Description << 8 * 1,
				AiredDate = (long)Byte4.AiredDate << 8 * 1,
				AniDBFileName = (long)Byte4.AniDBFileName << 8 * 1,

				//Byte 5
				MyListState = (long)Byte5.MyListState << 8 * 0,
				MyListFileState = (long)Byte5.MyListFileState << 8 * 0,
				MyListViewed = (long)Byte5.MyListViewed << 8 * 0,
				MyListViewDate = (long)Byte5.MyListViewDate << 8 * 0,
				MyListStorage = (long)Byte5.MyListStorage << 8 * 0,
				MyListSource = (long)Byte5.MyListSource << 8 * 0,
				MyListOther = (long)Byte5.MyListOther << 8 * 0,

				None = 0
			}

			public FMaskValues Mask { get; private set; }

			public string MaskString
			{
				get { return Mask.ToString("x").Remove(0, 6); }
			}

			public FMask(Byte1 byte1 = Byte1.None, Byte2 byte2 = Byte2.None, Byte3 byte3 = Byte3.None,
			             Byte4 byte4 = Byte4.None, Byte5 byte5 = Byte5.None)
			{
				Mask = (FMaskValues)(((long)byte1 << 8 * 4) |
				                     ((long)byte2 << 8 * 3) |
				                     ((long)byte3 << 8 * 2) |
				                     ((long)byte4 << 8) |
				                     ((long)byte5));
			}

			public FMask(FMaskValues fMaskValues)
			{
				Mask = fMaskValues;
			}

			public override string ToString()
			{
				return MaskString;
			}
		}

		protected static readonly ReadOnlyDictionary<FMask.FMaskValues, Type> FMaskTypes =
			new ReadOnlyDictionary<FMask.FMaskValues, Type>(
				new Dictionary<FMask.FMaskValues, Type>
				{
					#region FMaskTypes
					{ FMask.FMaskValues.AID, typeof (int) },
					{ FMask.FMaskValues.EID, typeof (int) },
					{ FMask.FMaskValues.GID, typeof (int) },
					{ FMask.FMaskValues.MyListID, typeof (int) },
					{ FMask.FMaskValues.OtherEpisodes, typeof (Dictionary<int, byte>) },
					{ FMask.FMaskValues.IsDeprecated, typeof (short) },
					{ FMask.FMaskValues.State, typeof (StateMask) },
					{ FMask.FMaskValues.Size, typeof (long) },
					{ FMask.FMaskValues.ED2K, typeof (string) },
					{ FMask.FMaskValues.MD5, typeof (string) },
					{ FMask.FMaskValues.SHA1, typeof (string) },
					{ FMask.FMaskValues.CRC32, typeof (string) },
					{ FMask.FMaskValues.Quality, typeof (string) },
					{ FMask.FMaskValues.Source, typeof (string) },
					{ FMask.FMaskValues.AudioCodecs, typeof (List<string>) },
					{ FMask.FMaskValues.AudioBitrates, typeof (List<int>) },
					{ FMask.FMaskValues.VideoCodec, typeof (string) },
					{ FMask.FMaskValues.VideoBitrate, typeof (int) },
					{ FMask.FMaskValues.VideoResolution, typeof (string) },
					{ FMask.FMaskValues.FileType, typeof (string) },
					{ FMask.FMaskValues.DubLanguage, typeof (List<string>) },
					{ FMask.FMaskValues.SubLanguage, typeof (List<string>) },
					{ FMask.FMaskValues.Length, typeof (int) },
					{ FMask.FMaskValues.Description, typeof (string) },
					{ FMask.FMaskValues.AiredDate, typeof (int) },
					{ FMask.FMaskValues.AniDBFileName, typeof (string) },
					{ FMask.FMaskValues.MyListState, typeof (int) },
					{ FMask.FMaskValues.MyListFileState, typeof (int) },
					{ FMask.FMaskValues.MyListViewed, typeof (int) },
					{ FMask.FMaskValues.MyListViewDate, typeof (int) },
					{ FMask.FMaskValues.MyListStorage, typeof (string) },
					{ FMask.FMaskValues.MyListSource, typeof (string) },
					{ FMask.FMaskValues.MyListOther, typeof (string) },

					#endregion
				});

		public static readonly ReadOnlyDictionary<FMask.FMaskValues, string> FMaskNames =
			new ReadOnlyDictionary<FMask.FMaskValues, string>(
				new Dictionary<FMask.FMaskValues, string>
				{
					{ FMask.FMaskValues.AID, "AID" },
					{ FMask.FMaskValues.EID, "EID" },
					{ FMask.FMaskValues.GID, "GID" },
					{ FMask.FMaskValues.MyListID, "MyList ID" },
					{ FMask.FMaskValues.OtherEpisodes, "Other Episodes" },
					{ FMask.FMaskValues.IsDeprecated, "Deprecated" },
					{ FMask.FMaskValues.State, "State" },
					{ FMask.FMaskValues.Size, "Size" },
					{ FMask.FMaskValues.ED2K, "ED2K Hash" },
					{ FMask.FMaskValues.MD5, "MD5 Hash" },
					{ FMask.FMaskValues.SHA1, "SHA1 Hash" },
					{ FMask.FMaskValues.CRC32, "CRC32 Hash" },
					{ FMask.FMaskValues.Quality, "Quality" },
					{ FMask.FMaskValues.Source, "Source" },
					{ FMask.FMaskValues.AudioCodecs, "Audio Codecs" },
					{ FMask.FMaskValues.AudioBitrates, "Audio Bitrates" },
					{ FMask.FMaskValues.VideoCodec, "Video Codec" },
					{ FMask.FMaskValues.VideoBitrate, "Video Bitrate" },
					{ FMask.FMaskValues.VideoResolution, "Video Resolution" },
					{ FMask.FMaskValues.FileType, "File Extension" },
					{ FMask.FMaskValues.DubLanguage, "Dub Language(s)" },
					{ FMask.FMaskValues.SubLanguage, "Sub Language(s)" },
					{ FMask.FMaskValues.Length, "Length" },
					{ FMask.FMaskValues.Description, "Description" },
					{ FMask.FMaskValues.AiredDate, "Aired" },
					{ FMask.FMaskValues.AniDBFileName, "AniDB File Name" },
					{ FMask.FMaskValues.MyListState, "MyList State" },
					{ FMask.FMaskValues.MyListFileState, "MyList File State" },
					{ FMask.FMaskValues.MyListViewed, "MyList Viewed" },
					{ FMask.FMaskValues.MyListViewDate, "MyList View Date" },
					{ FMask.FMaskValues.MyListStorage, "MyList Storage" },
					{ FMask.FMaskValues.MyListSource, "MyList Source" },
					{ FMask.FMaskValues.MyListOther, "MyListOther" },
				});

		#region FMask public properties

		public int? AID
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.AID); }
			set { SetFMaskValue(FMask.FMaskValues.AID, value); }
		}

		public int? EID
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.EID); }
			set { SetFMaskValue(FMask.FMaskValues.EID, value); }
		}

		public int? GID
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.GID); }
			set { SetFMaskValue(FMask.FMaskValues.GID, value); }
		}

		public int? MyListID
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.MyListID); }
			set { SetFMaskValue(FMask.FMaskValues.MyListID, value); }
		}

		public Dictionary<int, byte> OtherEpisodes
		{
			get { return (Dictionary<int, byte>)GetFMaskValue(FMask.FMaskValues.OtherEpisodes); }
			set { SetFMaskValue(FMask.FMaskValues.OtherEpisodes, value); }
		}

		public short? IsDeprecated
		{
			get { return (short?)GetFMaskValue(FMask.FMaskValues.IsDeprecated); }
			set { SetFMaskValue(FMask.FMaskValues.IsDeprecated, value); }
		}

		public StateMask? State
		{
			get
			{
				//WTF: I can't cast to StateMask? if the value is not null for some reason
				if (GetFMaskValue(FMask.FMaskValues.State) == null)
					return (StateMask?)GetFMaskValue(FMask.FMaskValues.State);

				return (StateMask)GetFMaskValue(FMask.FMaskValues.State);
			}
			set { SetFMaskValue(FMask.FMaskValues.State, value); }
		}

		public long? Size
		{
			get { return (long?)GetFMaskValue(FMask.FMaskValues.Size); }
			set { SetFMaskValue(FMask.FMaskValues.Size, value); }
		}

		public string ED2K
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.ED2K); }
			set { SetFMaskValue(FMask.FMaskValues.ED2K, value); }
		}

		public string MD5
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.MD5); }
			set { SetFMaskValue(FMask.FMaskValues.MD5, value); }
		}

		public string SHA1
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.SHA1); }
			set { SetFMaskValue(FMask.FMaskValues.SHA1, value); }
		}

		public string CRC32
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.CRC32); }
			set { SetFMaskValue(FMask.FMaskValues.CRC32, value); }
		}

		public string Quality
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.Quality); }
			set { SetFMaskValue(FMask.FMaskValues.Quality, value); }
		}

		public string Source
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.Source); }
			set { SetFMaskValue(FMask.FMaskValues.Source, value); }
		}

		public List<string> AudioCodecs
		{
			get { return (List<string>)GetFMaskValue(FMask.FMaskValues.AudioCodecs); }
			set { SetFMaskValue(FMask.FMaskValues.AudioCodecs, value); }
		}

		public List<int> AudioBitrates
		{
			get { return (List<int>)GetFMaskValue(FMask.FMaskValues.AudioBitrates); }
			set { SetFMaskValue(FMask.FMaskValues.AudioBitrates, value); }
		}

		public string VideoCodec
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.VideoCodec); }
			set { SetFMaskValue(FMask.FMaskValues.VideoCodec, value); }
		}

		public int? VideoBitrate
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.VideoBitrate); }
			set { SetFMaskValue(FMask.FMaskValues.VideoBitrate, value); }
		}

		public string VideoResolution
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.VideoResolution); }
			set { SetFMaskValue(FMask.FMaskValues.VideoResolution, value); }
		}

		public string FileType
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.FileType); }
			set { SetFMaskValue(FMask.FMaskValues.FileType, value); }
		}

		public List<string> DubLanguage
		{
			get { return (List<string>)GetFMaskValue(FMask.FMaskValues.DubLanguage); }
			set { SetFMaskValue(FMask.FMaskValues.DubLanguage, value); }
		}

		public List<string> SubLanguage
		{
			get { return (List<string>)GetFMaskValue(FMask.FMaskValues.SubLanguage); }
			set { SetFMaskValue(FMask.FMaskValues.SubLanguage, value); }
		}

		public int? Length
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.Length); }
			set { SetFMaskValue(FMask.FMaskValues.Length, value); }
		}

		public string Description
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.Description); }
			set { SetFMaskValue(FMask.FMaskValues.Description, value); }
		}

		public int? AiredDate
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.AiredDate); }
			set { SetFMaskValue(FMask.FMaskValues.AiredDate, value); }
		}

		public string AniDBFileName
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.AniDBFileName); }
			set { SetFMaskValue(FMask.FMaskValues.AniDBFileName, value); }
		}

		public int? MyListState
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.MyListState); }
			set { SetFMaskValue(FMask.FMaskValues.MyListState, value); }
		}

		public int? MyListFileState
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.MyListFileState); }
			set { SetFMaskValue(FMask.FMaskValues.MyListFileState, value); }
		}

		public int? MyListViewed
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.MyListViewed); }
			set { SetFMaskValue(FMask.FMaskValues.MyListViewed, value); }
		}

		public int? MyListViewDate
		{
			get { return (int?)GetFMaskValue(FMask.FMaskValues.MyListViewDate); }
			set { SetFMaskValue(FMask.FMaskValues.MyListViewDate, value); }
		}

		public string MyListStorage
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.MyListStorage); }
			set { SetFMaskValue(FMask.FMaskValues.MyListStorage, value); }
		}

		public string MyListSource
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.MyListSource); }
			set { SetFMaskValue(FMask.FMaskValues.MyListSource, value); }
		}

		public string MyListOther
		{
			get { return (string)GetFMaskValue(FMask.FMaskValues.MyListOther); }
			set { SetFMaskValue(FMask.FMaskValues.MyListOther, value); }
		}

		#endregion

		protected Dictionary<FMask.FMaskValues, object> FMaskFields;

		public object GetFMaskValue(FMask.FMaskValues f)
		{
			return (FMaskFields.ContainsKey(f) ? FMaskFields[f] : null);
		}

		public void SetFMaskValue(FMask.FMaskValues f, object value)
		{
			if (value == null ||
			    (FMaskTypes[f] == typeof (int) && (int)value == -1) ||
			    (FMaskTypes[f] == typeof (short) && (short)value == -1) ||
			    (FMaskTypes[f] == typeof (long) && (long)value == -1) ||
			    (FMaskTypes[f] == typeof (string) && (string)value == "") ||
			    (FMaskTypes[f] == typeof (StateMask) && (StateMask)value == StateMask.NoneUnset))
			{
				if (FMaskFields.ContainsKey(f))
					FMaskFields.Remove(f);

				return;
			}

			if (FMaskFields.ContainsKey(f))
				FMaskFields[f] = value;
			else
				FMaskFields.Add(f, value);
		}
	}
}
