using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpVitamins;

namespace libAniDB.NET
{
	public class AniDBRequest
	{
		public readonly string Command;
		public readonly IEnumerable<KeyValuePair<string, string>> ParValues;
		public readonly AniDBTaggedResponseCallback Callback;
		public readonly string Tag;

		public AniDBRequest(string command, AniDBTaggedResponseCallback callback = null,
		                    params KeyValuePair<string, string>[] args)
			: this(command, callback, args.ToDictionary(a => a.Key, a => a.Value)) {}

		public AniDBRequest(string command, AniDBTaggedResponseCallback callback,
		                    IEnumerable<KeyValuePair<string, string>> parValues)
		{
			Command = command;
			Callback = callback;

			Tag = ShortGuid.NewGuid().ToString();

			ParValues = parValues;
		}

		public override string ToString()
		{
			StringBuilder returnString = new StringBuilder(Command + " ");

			foreach (var s in ParValues)
				returnString.AppendFormat("{0}={1}&", s.Key, s.Value);

			if (Tag == "")
				returnString.Remove(returnString.Length - 1, 1); //Remove trailing ampersand
			else
				returnString.AppendFormat("tag={0}", Tag);

			return returnString.ToString();
		}

		public byte[] ToByteArray(Encoding encoding)
		{
			return encoding.GetBytes(ToString());
		}
	}
}
