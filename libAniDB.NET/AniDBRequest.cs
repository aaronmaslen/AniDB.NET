using System;
using System.Collections.Generic;
using System.Text;

namespace libAniDB.NET
{
	public class AniDBRequest {
		public AniDBRequest(string command, AniDBTaggedResponseCallback callback = null,
		                    string tag = "", params string[] args)
		{
			Command = command;
			Callback = callback;
			Tag = tag;

			if (args.Length % 2 != 0)
				throw new ArgumentException("Wrong number of arguments");

			ParValues = new Dictionary<string, string>();

			for (int i = 0; i < args.Length; i += 2) {
				ParValues.Add(args[i], args[i + 1]);
			}
		}

		public AniDBRequest() {}

		public string Command;
		public Dictionary<string, string> ParValues;
		public AniDBTaggedResponseCallback Callback;
		public string Tag;

		public int Timeout;

		public override string ToString() {
			StringBuilder returnString = new StringBuilder(Command + " ");

			foreach (string s in ParValues.Keys)
				returnString.AppendFormat("{0}={1}&", s, ParValues[s]);

			if (Tag == "")
				returnString.Remove(returnString.Length - 1, 1); //Remove trailing ampersand
			else
				returnString.AppendFormat("tag={0}", Tag);

			return returnString.ToString();
		}

		public byte[] ToByteArray(Encoding encoding) {
			return encoding.GetBytes(ToString());
		}
	}
}
