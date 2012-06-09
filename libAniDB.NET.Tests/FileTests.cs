using System;
using System.Text;
using NUnit.Framework;

namespace libAniDB.NET.Tests
{
	[TestFixture]
	internal class FileTests
	{
		[Test]
		public void FMaskAMaskTest1()
		{
			AniDBFile.FMask.Byte1 fByte1 = AniDBFile.FMask.Byte1.AID | AniDBFile.FMask.Byte1.EID | AniDBFile.FMask.Byte1.GID |
			                               AniDBFile.FMask.Byte1.MyListID | AniDBFile.FMask.Byte1.OtherEpisodes;

			AniDBFile.FMask.Byte2 fByte2 = AniDBFile.FMask.Byte2.Size | AniDBFile.FMask.Byte2.ED2K | AniDBFile.FMask.Byte2.MD5 |
			                               AniDBFile.FMask.Byte2.SHA1 | AniDBFile.FMask.Byte2.CRC32;

			AniDBFile.FMask.Byte3 fByte3 = AniDBFile.FMask.Byte3.Quality | AniDBFile.FMask.Byte3.Source |
			                               AniDBFile.FMask.Byte3.AudioCodecs | AniDBFile.FMask.Byte3.AudioBitrates |
			                               AniDBFile.FMask.Byte3.VideoCodec | AniDBFile.FMask.Byte3.VideoBitrate |
			                               AniDBFile.FMask.Byte3.VideoResolution | AniDBFile.FMask.Byte3.FileType;

			AniDBFile.FMask.Byte4 fByte4 = AniDBFile.FMask.Byte4.DubLanguage | AniDBFile.FMask.Byte4.SubLanguage |
			                               AniDBFile.FMask.Byte4.Length | AniDBFile.FMask.Byte4.Description |
			                               AniDBFile.FMask.Byte4.AiredDate | AniDBFile.FMask.Byte4.AniDBFileName;


			AniDBFile.AMask.Byte3 aByte3 = AniDBFile.AMask.Byte3.EpNo | AniDBFile.AMask.Byte3.EpName |
			                               AniDBFile.AMask.Byte3.EpRomanjiName | AniDBFile.AMask.Byte3.EpKanjiName |
			                               AniDBFile.AMask.Byte3.EpisodeRating | AniDBFile.AMask.Byte3.EpisodeVoteCount;

			AniDBFile.AMask.Byte4 aByte4 = AniDBFile.AMask.Byte4.GroupName | AniDBFile.AMask.Byte4.GroupShortName;

			AniDBFile.FMask fMask = new AniDBFile.FMask(fByte1, fByte2, fByte3, fByte4);
			AniDBFile.AMask aMask = new AniDBFile.AMask(byte3: aByte3, byte4: aByte4);


			string response =
				"filetest 220 FILE\n802492|7477|122616|2812|0||145770786|736461a6137cab6712c20f6e6f27dba3|d16cfd78cf9e4871f006b7f5e6c34893|62af23e9791668f3395d943f75ba4e441499610d|dae73fdf|high|DTV|AAC|189|H264/AVC|611|848x480|mkv|japanese|english|1449||1285200000|Togainuno Chi - 01 - Void Dream / Lost - [gg](dae73fdf).mkv|01|Void Dream /Lost||??/LOST|494|2|gg|gg";

			Console.WriteLine(new AniDBFile(new AniDBResponse(Encoding.ASCII.GetBytes(response)), fMask, aMask));
		}
	}
}
