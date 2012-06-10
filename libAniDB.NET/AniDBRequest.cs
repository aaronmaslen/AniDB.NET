﻿/* Copyright 2011 Aaron Maslen. All rights reserved.
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
