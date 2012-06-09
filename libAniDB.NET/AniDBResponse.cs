using System;
using System.Collections.Generic;
using System.Text;

namespace libAniDB.NET
{
	public class AniDBResponse
	{
		public readonly string Tag;
		public readonly AniDBReturnCode ReturnCode;
		public readonly string ReturnString;

		public string[][] DataFields { get; private set; }

		public readonly string OriginalString;

		public AniDBResponse(byte[] responseData, Encoding encoding = null)
		{
			OriginalString = encoding == null
			                 	? Encoding.ASCII.GetString(responseData)
			                 	: encoding.GetString(responseData);

			string[] responseLines = OriginalString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

			short returnCode;

			string[] response =
				responseLines[0].Split(new[] { ' ' }, short.TryParse(responseLines[0].Split(' ')[0], out returnCode) ? 2 : 3);

			Tag = response.Length == 3 ? response[0] : "";
			ReturnCode = (AniDBReturnCode)(response.Length == 3 ? short.Parse(response[1]) : returnCode);
			ReturnString = response.Length == 3 ? response[2] : response[1];

			List<string[]> datafields = new List<string[]>();

			for (int i = 1; i < responseLines.Length; i++)
				datafields.Add(responseLines[i].Split('|'));

			DataFields = datafields.ToArray();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("Tag: {0}\n", Tag);
			sb.AppendFormat("Return Code: {0}\n", ReturnCode);
			sb.AppendFormat("Return String: {0}\n", ReturnString);

			sb.Append("Data fields:");

			for (int i = 0; i < DataFields.Length; i++)
			{
				sb.Append("\n" + i + ":\n");
				for (int j = 0; j < DataFields[i].Length; j++)
					sb.AppendFormat(j != DataFields[i].Length - 1 ? " {0}\n" : " {0}", DataFields[i][j]);
			}

			return sb.ToString();
		}
	}
}
