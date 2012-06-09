using System;
using System.Collections.Generic;
using System.Linq;
using libAniDB.NET;

namespace libAniDB.NET {
	public partial class AniDBFile {
		[Flags]
		public enum StateMask : short
		{
			NoneUnset	= 0,
			CrcOk		= 1,
			CrcErr		= 2,
			V2			= 4,
			V3			= 8,
			V4			= 16,
			V5			= 32,
			Uncensored	= 64,
			Censored	= 128
		}

		public int? FID;

		public void AddData(AniDBFile fileResponse)
		{
			if (FID == null || FID == -1)
				FID = fileResponse.FID;

			else if (fileResponse.FID != FID)
				throw new ArgumentException("Data to add is for a different file");

			foreach (FMask.FMaskValues f in fileResponse.FMaskFields.Keys)
				SetFMaskValue(f, fileResponse.FMaskFields[f]);

			foreach(AMask.AMaskValues a in fileResponse.AMaskFields.Keys)
				SetAMaskValue(a, fileResponse.AMaskFields[a]);
		}

		public override string ToString()
		{
			string data = "FID: " + (FID != -1 ? FID.ToString() : "") + "\n";

			foreach(var f in FMaskNames.Keys)
			{
				data += FMaskNames[f] + ": ";

				if (!FMaskFields.ContainsKey(f))
				{
					data += "\n";
					continue;
				}

				if(FMaskTypes[f] == typeof(List<string>))
				{
					data += "\n";

					if (FMaskFields[f] == null)
						continue;

					foreach(string s in (List<string>) FMaskFields[f])
						data += " " + s + "\n";
				}
				else if (FMaskTypes[f] == typeof(List<int>))
				{
					data += "\n";

					if (FMaskFields[f] == null)
						continue;

					foreach(int i in (List<int>) FMaskFields[f])
						data += " " + i + "\n";
				}
				else if (FMaskTypes[f] == typeof(Dictionary<int, byte>))
				{
					data += "\n";

					if (FMaskFields[f] == null)
						continue;

					foreach(int i in ((Dictionary<int, byte>)FMaskFields[f]).Keys)
					{
						data += " " + i + " " + ((Dictionary<int, byte>) FMaskFields[f])[i] + "\n";
					}
				}
				else data += (FMaskFields[f].ToString() == "-1" ? "" : FMaskFields[f]) + "\n";
			}

			foreach(AMask.AMaskValues a in AMaskNames.Keys)
			{
				data += AMaskNames[a] + ": ";

				if(!AMaskFields.ContainsKey(a))
				{
					data += "\n";
					continue;
				}

				if (AMaskTypes[a] == typeof(List<string>))
				{
					data += "\n";

					if (AMaskFields[a] == null)
						continue;

					foreach (string s in (List<string>)AMaskFields[a])
						data += " " + s + "\n";
				}
				else data += (AMaskFields[a].ToString() == "-1" ? "" : AMaskFields[a]) + "\n";
			}

			return data;
		}

		public AniDBFile()
		{
			FID = null;

			FMaskFields = new Dictionary<FMask.FMaskValues, object>();
			AMaskFields = new Dictionary<AMask.AMaskValues, object>();
		}

		public AniDBFile(AniDBResponse fileResponse, FMask fMask, AMask aMask) : this()
		{
			if(fileResponse.ReturnCode != AniDBReturnCode.FILE)
				throw new ArgumentException("Response is not a FILE response");

			List<string> dataFields = new List<string>();
			foreach (string[] sa in fileResponse.DataFields)
				dataFields.AddRange(sa);

			FID = int.Parse(dataFields[0]);

			int currentIndex = 1;

			for (int i = 39; /* 8*5 - 1 ie. 40 bits */ i >= 0; i--)
			{
				if (currentIndex >= dataFields.Count) break;

				FMask.FMaskValues flag = (FMask.FMaskValues) ((long) Math.Pow(2, i));

				if (!fMask.Mask.HasFlag(flag)) continue;

				//Parse value
				object field = null;

				if(dataFields[currentIndex] != "")
					if (FMaskTypes[flag] == typeof (string))
						field = dataFields[currentIndex];
					else if (FMaskTypes[flag] == typeof (int))
						field = int.Parse(dataFields[currentIndex]);
					else if (FMaskTypes[flag] == typeof (short))
						field = short.Parse(dataFields[currentIndex]);
					else if (FMaskTypes[flag] == typeof (long))
						field = long.Parse(dataFields[currentIndex]);
					else if (FMaskTypes[flag] == typeof (StateMask))
						field = short.Parse(dataFields[currentIndex]);
					else if (FMaskTypes[flag] == typeof (Dictionary<int, byte>))
					{
						string[] splitString = dataFields[currentIndex].Split('\'');
						Dictionary<int, byte> otherEpisodes = new Dictionary<int, byte>();

						if (dataFields[currentIndex] != "")
							for (int j = 0; j < splitString.Length; j += 2)
								otherEpisodes.Add(int.Parse(splitString[j]), byte.Parse(splitString[j + 1]));

						field = otherEpisodes;
					}
					else if (FMaskTypes[flag] == typeof (List<string>))
						field = new List<string>(dataFields[currentIndex].Split('\''));
					else if (FMaskTypes[flag] == typeof (List<int>))
						field = dataFields[currentIndex].Split('\'').Select(s => int.Parse(s)).ToList();

				//Add value to Dictionary), probably unecessary checks
				if(field != null)
					if (FMaskFields.ContainsKey(flag))
						FMaskFields[flag] = field;
					else
						FMaskFields.Add(flag, field);

				currentIndex++;
			}

			for (int i = 31; i >= 0; i--)
			{
				if (currentIndex >= dataFields.Count) break;

				AMask.AMaskValues flag = (AMask.AMaskValues) ((uint) Math.Pow(2, i));

				if (!aMask.Mask.HasFlag(flag)) continue;

				object field = null;

				if (AMaskTypes[flag] == typeof(int))
					field = int.Parse(dataFields[currentIndex]);
				else if (AMaskTypes[flag] == typeof(string))
					field = dataFields[currentIndex];
				else if (AMaskTypes[flag] == typeof(List<string>))
					field = new List<string>(dataFields[currentIndex].Split('\''));

				if(field != null)
					if (AMaskFields.ContainsKey(flag))
						AMaskFields[flag] = field;
					else
						AMaskFields.Add(flag, field);

				currentIndex++;
			}
		}
	}
}
