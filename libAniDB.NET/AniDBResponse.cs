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
