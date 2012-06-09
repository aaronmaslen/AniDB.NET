using System;
using System.Collections.Generic;
using System.Globalization;

namespace libAniDB.NET
{
	public partial class AniDB : IAniDB
	{
		//--- Auth ---\\

		public void Auth(string user, string pass, AniDBTaggedResponseCallback callback = null, bool nat = false,
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

			SendCommand(new AniDBRequest("AUTH", callback, parValues));
		}


		public void Logout(AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("LOGOUT", callback));
		}


		public void Encrypt(string user, AniDBTaggedResponseCallback callback = null)
		{
			throw new NotImplementedException();
		}

		//--- Misc ---\\

		public void Ping(bool nat = false, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("PING", callback, new KeyValuePair<string, string>("nat", nat ? "1" : "0")));
		}

		public void ChangeEncoding(string name, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("ENCODING", callback, new KeyValuePair<string, string>("name", name)));
		}

		public void Uptime(AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("UPTIME", callback));
		}

		public void Version(AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("VERSION", callback));
		}

		//--- Data ---\\

		public void Anime(int aID, string aMask = "", AniDBTaggedResponseCallback callback = null)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>
			                                       { { "aid", aID.ToString(CultureInfo.InvariantCulture) } };

			if (aMask != "")
				parValues.Add("amask", aMask);

			SendCommand(new AniDBRequest("ANIME", callback, parValues));
		}

		public void Anime(string aName, string aMask = "", AniDBTaggedResponseCallback callback = null)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string> { { "aname", aName } };

			if (aMask != "")
				parValues.Add("amask", aMask);

			SendCommand(new AniDBRequest("ANIME", callback, parValues));
		}


		public void AnimeDesc(int aID, int partNo, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("ANIMEDESC", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("partNo", partNo.ToString(CultureInfo.InvariantCulture))));
		}


		public void Calendar(AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("CALENDAR", callback));
		}


		public void Character(int charID, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("CHARACTER", callback,
			                             new KeyValuePair<string, string>("charid", charID.ToString(CultureInfo.InvariantCulture))));
		}


		public void Creator(int creatorID, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("CREATOR", callback,
			                             new KeyValuePair<string, string>("creatorid",
			                                                              creatorID.ToString(CultureInfo.InvariantCulture))));
		}


		public void Episode(int eID, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("eID", callback,
			                             new KeyValuePair<string, string>("eid", eID.ToString(CultureInfo.InvariantCulture))));
		}

		public void Episode(string aName, int epNo, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("EPISODE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture))));
		}

		public void Episode(int aID, int epNo, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("EPISODE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture))));
		}


		public void File(int fID, string fMask, string aMask, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("fid", fID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}

		public void File(long size, string ed2K, string fMask, string aMask, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("size", size.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("ed2k", ed2K),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}

		public void File(string aName, string gName, int epNo, string fMask, string aMask,
		                 AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("gname", gName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}

		public void File(string aName, int gID, int epNo, string fMask, string aMask,
		                 AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aname", aName),
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}

		public void File(int aID, string gName, int epNo, string fMask, string aMask,
		                 AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("gname", gName),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}

		public void File(int aID, int gID, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("FILE", callback,
			                             new KeyValuePair<string, string>("aid", aID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("epno", epNo.ToString(CultureInfo.InvariantCulture)),
			                             new KeyValuePair<string, string>("fmask", fMask),
			                             new KeyValuePair<string, string>("amask", aMask)));
		}


		public void Group(int gID, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("GROUP", callback,
			                             new KeyValuePair<string, string>("gid", gID.ToString(CultureInfo.InvariantCulture))));
		}

		public void Group(string gName, AniDBTaggedResponseCallback callback = null)
		{
			SendCommand(new AniDBRequest("GROUP", callback,
			                             new KeyValuePair<string, string>("gname", gName)));
		}


		public void GroupStatus(int aID, int state = 0, AniDBTaggedResponseCallback callback = null)
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>
			                                       {
			                                       	{ "aid", aID.ToString(CultureInfo.InvariantCulture) }
			                                       };

			if (state > 0)
				parValues.Add("state", state.ToString(CultureInfo.InvariantCulture));

			SendCommand(new AniDBRequest("GROUPSTATUS", callback, parValues));
		}
	}
}
