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
using System.Globalization;

namespace libAniDB.NET
{
	public partial class AniDB : IAniDB
	{
		//--- Auth ---\\

		public void Auth(string user, string pass, AniDBResponseCallback callback, bool nat = false,
		                 bool comp = false, int mtu = 0, bool imgServer = false)
		{
			Dictionary<string, string> parValues =
				new Dictionary<string, string>
				{
					{ "user", user },
					{ "pass", pass },
					{ "protover", ProtocolVersion.ToString(CultureInfo.InvariantCulture) },
					{ "client", ClientName },
					{ "clientver", ClientVer.ToString(CultureInfo.InvariantCulture) },
				};
			if (nat)
				parValues.Add("nat", "1");
			if (comp)
				parValues.Add("comp", "1");
			if (_encoding != null)
				parValues.Add("enc", _encoding.WebName);
			if (mtu > 0)
				parValues.Add("mtu", mtu.ToString(CultureInfo.InvariantCulture));
			if (imgServer)
				parValues.Add("imgserver", "1");

			QueueCommand(new AniDBRequest("AUTH", callback, parValues));
		}


		public void Logout(AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("LOGOUT", callback));
		}


		public void Encrypt(string user, AniDBResponseCallback callback)
		{
			throw new NotImplementedException();
		}

		//--- Misc ---\\

		public void Ping(AniDBResponseCallback callback, bool nat = false)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>();

			if (nat)
				parValues.Add("nat", "1");

			QueueCommand(new AniDBRequest("PING", callback, parValues));
		}

		public void ChangeEncoding(string name, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("ENCODING", callback, new KeyValuePair<string, string>("name", name)));
		}

		public void Uptime(AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("UPTIME", callback));
		}

		public void Version(AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("VERSION", callback));
		}

		//--- Data ---\\

		public void Anime(int aID, AniDBResponseCallback callback, Anime.AMask aMask = null)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>
			                                       { { "aid", aID.ToString(CultureInfo.InvariantCulture) } };

			if (aMask != null)
				parValues.Add("amask", aMask.MaskString);

			QueueCommand(new AniDBRequest("ANIME", callback, parValues));
		}

		public void Anime(string aName, AniDBResponseCallback callback, Anime.AMask aMask = null)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string> { { "aname", aName } };

			if (aMask != null)
				parValues.Add("amask", aMask.MaskString);

			QueueCommand(new AniDBRequest("ANIME", callback, parValues));
		}


		public void AnimeDesc(int aID, int partNo, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("ANIMEDESC", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("partNo", partNo.ToString(CultureInfo.InvariantCulture))));
		}


		public void Calendar(AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("CALENDAR", callback));
		}


		public void Character(int charID, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("CHARACTER", callback,
			                             new KeyValuePair<string, string>("charid", charID.ToString(CultureInfo.InvariantCulture))));
		}


		public void Creator(int creatorID, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("CREATOR", callback,
			                             new KeyValuePair<string, string>("creatorid",
			                                                              creatorID.ToString(CultureInfo.InvariantCulture))));
		}


		public void Episode(int eID, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("eID", callback,
			                             new KeyValuePair<string, string>("eid", eID.ToString(CultureInfo.InvariantCulture))));
		}

		public void Episode(string aName, int epNo, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("EPISODE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture))));
		}

		public void Episode(int aID, int epNo, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("EPISODE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture))));
		}


		public void File(int fID, AniDBFile.FMask fMask, AniDBFile.AMask aMask, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("fid", fID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}

		public void File(long size, string ed2K, AniDBFile.FMask fMask, AniDBFile.AMask aMask, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("size", size.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("ed2k", ed2K),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}

		public void File(string aName, string gName, int epNo, AniDBFile.FMask fMask, AniDBFile.AMask aMask, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("gname", gName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}

		public void File(string aName, int gID, int epNo, AniDBFile.FMask fMask, AniDBFile.AMask aMask,
		                 AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}

		public void File(int aID, string gName, int epNo, AniDBFile.FMask fMask, AniDBFile.AMask aMask,
		                 AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("gname", gName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}

		public void File(int aID, int gID, int epNo, AniDBFile.FMask fMask, AniDBFile.AMask aMask, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask.MaskString),
			                             new KeyValuePair<string, string>("amask", aMask.MaskString)));
		}


		public void Group(int gID, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("GROUP", callback,
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture))));
		}

		public void Group(string gName, AniDBResponseCallback callback)
		{
			QueueCommand(new AniDBRequest("GROUP", callback,
			                             new KeyValuePair<string, string>("gname", gName)));
		}


		public void GroupStatus(int aID, AniDBResponseCallback callback, int state = 0)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>
			                                       {
			                                       	{ "aid", aID.ToString(CultureInfo.InvariantCulture) }
			                                       };

			if (state > 0)
				parValues.Add("state", state.ToString(CultureInfo.InvariantCulture));

			QueueCommand(new AniDBRequest("GROUPSTATUS", callback, parValues));
		}
	}
}
