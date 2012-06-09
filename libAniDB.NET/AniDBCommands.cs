using System;
using System.Collections.Generic;
using libAniDB.Net;

namespace libAniDB.NET
{
	internal partial class AniDB : IAniDB
	{
		//--- Auth ---\\

		public void Auth(string user, string pass, AniDBTaggedResponseCallback callback = null, string tag = "", bool nat = false, bool comp = false, int mtu = 0, bool imgServer = false)
		{
			Dictionary<string, string> parValues =
				new Dictionary<string, string>
			    {
			        {"user", user},
			        {"pass", pass},
			        {"protover", ProtocolVersion.ToString()},
			        {"client", ClientName},
			        {"clientver", ClientVer.ToString()},
			    };
			if (nat)
				parValues.Add("nat", "1");
			if (comp)
				parValues.Add("comp", "1");
			if (_encoding != null)
				parValues.Add("enc", _encoding.WebName);
			if (mtu > 0)
				parValues.Add("mtu", mtu.ToString());
			if (imgServer)
				parValues.Add("imgserver", "1");

			//SendCommand("AUTH", parValues, callback, tag);

			SendCommand(new AniDBRequest { Command = "AUTH", ParValues = parValues, Callback = callback, Tag = tag });
		}


		public void Logout(AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("LOGOUT", null, callback, tag);

			SendCommand(new AniDBRequest("LOGOUT", callback, tag));
		}


		public void Encrypt(string user, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			throw new NotImplementedException();

			/*SendCommand("ENCRYPT",
				new Dictionary<string, string>
					{
						{"user", user},
						{"type", "1"}
					}, callback);*/
		}

		//--- Misc ---\\

		public void Ping(bool nat = false, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("PING", new Dictionary<string, string>{{"nat", nat ? "1" : "0"}}, callback, tag);

			SendCommand(new AniDBRequest("PING", callback, tag, "nat", nat ? "1" : "0"));
		}

		public void ChangeEncoding(string name, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("ENCODING", new Dictionary<string, string>
			//                            {
			//                                {"name", name}
			//                            }, callback, tag);

			SendCommand(new AniDBRequest("ENCODING", callback, tag, "name", name));
		}

		public void Uptime(AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("UPTIME", null, callback, tag);

			SendCommand(new AniDBRequest("UPTIME", callback, tag));
		}

		public void Version(AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("VERSION", null, callback, tag);

			SendCommand(new AniDBRequest("VERSION", callback, tag));
		}

		//--- Data ---\\

		public void Anime(int aID, string aMask = "", AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			Dictionary<string, string> parValues = new Dictionary<string, string> { { "aid", aID.ToString() } };

			if (aMask != "")
				parValues.Add("amask", aMask);

			//SendCommand("ANIME", parValues, callback, tag);

			SendCommand(new AniDBRequest { Command = "ANIME", Callback = callback, Tag = tag, ParValues = parValues });
		}

		public void Anime(string aName, string aMask = "", AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			Dictionary<string, string> parValues = new Dictionary<string, string> { { "aname", aName } };

			if (aMask != "")
				parValues.Add("amask", aMask);

			//SendCommand("ANIME", parValues, callback, tag);

			SendCommand(new AniDBRequest { Command = "ANIME", Callback = callback, Tag = tag, ParValues = parValues });
		}


		public void AnimeDesc(int aID, int partNo, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("ANIMEDESC", new Dictionary<string, string>
			//                            {
			//                                {"aid", aID.ToString()},
			//                                {"part", partNo.ToString()}
			//                            }, callback, tag);

			SendCommand(new AniDBRequest("ANIMEDESC", callback, tag, "aid", aID.ToString(), "partNo", partNo.ToString()));
		}


		public void Calendar(AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("CALENDAR", null, callback, tag);

			SendCommand(new AniDBRequest("CALENDAR", callback, tag));
		}


		public void Character(int charID, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("CHARACTER", new Dictionary<string, string>
			//                            {
			//                                {"charid", charID.ToString()}
			//                            }, callback, tag);

			SendCommand(new AniDBRequest("CHARACTER", callback, tag, "charid", charID.ToString()));
		}


		public void Creator(int creatorID, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("CREATOR", new Dictionary<string, string>
			//                        {
			//                            {"creatorid", creatorID.ToString()}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("CREATOR", callback, tag, "creatorid", creatorID.ToString()));
		}


		public void Episode(int eID, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("EPISODE", new Dictionary<string, string>
			//                        {
			//                            {"eid", eID.ToString()}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("eID", callback, tag, "eid", eID.ToString()));
		}

		public void Episode(string aName, int epNo, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("EPISODE", new Dictionary<string, string>
			//                        {
			//                            {"aname", aName},
			//                            {"epno", epNo.ToString()}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("EPISODE", callback, tag, "aname", aName, "epno", epNo.ToString()));
		}

		public void Episode(int aID, int epNo, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("EPISODE", new Dictionary<string, string>
			//                        {
			//                            {"aid", aID.ToString()},
			//                            {"epno", epNo.ToString()}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("EPISODE", callback, tag, "aid", aID.ToString(), "epno", epNo.ToString()));
		}


		public void File(int fID, string fMask, string aMask, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"fid", fID.ToString()},
			//                            {"fmask", fMask},
			//                            {"amask", aMask},
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "fid", fID.ToString(), "fmask", fMask, "amask", aMask));
		}

		public void File(long size, string ed2K, string fMask, string aMask, AniDBTaggedResponseCallback callback = null,
						 string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"size", size.ToString()},
			//                            {"ed2k", ed2K},
			//                            {"fmask", fMask},
			//                            {"amask", aMask}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "size", size.ToString(), "ed2k", ed2K,
										 "fmask", fMask, "amask", aMask));
		}

		public void File(string aName, string gName, int epNo, string fMask, string aMask,
						 AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"aname", aName},
			//                            {"gname", gName},
			//                            {"fmask", fMask},
			//                            {"amask", aMask}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "aname", aName, "gname", gName, "epno", epNo.ToString(),
										 "fmask", fMask, "amask", aMask));
		}

		public void File(string aName, int gID, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null,
						 string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"aname", aName},
			//                            {"gid", gID.ToString()},
			//                            {"epno", epNo.ToString()},
			//                            {"fmask", fMask},
			//                            {"amask", aMask}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "aname", aName, "gid", gID.ToString(), "epno", epNo.ToString(),
										 "fmask", fMask, "amask", aMask));
		}

		public void File(int aID, string gName, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null,
						 string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"aid", aID.ToString()},
			//                            {"gname", gName},
			//                            {"epno", epNo.ToString()},
			//                            {"fmask", fMask},
			//                            {"amask", aMask}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "aid", aID.ToString(), "gname", gName, "epno", epNo.ToString(),
										 "fmask", fMask, "amask", aMask));
		}

		public void File(int aID, int gID, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null,
						 string tag = "")
		{
			//SendCommand("FILE", new Dictionary<string, string>
			//                        {
			//                            {"aid", aID.ToString()},
			//                            {"gid", gID.ToString()},
			//                            {"epno", epNo.ToString()},
			//                            {"fmask", fMask},
			//                            {"amask", aMask}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("FILE", callback, tag, "aid", aID.ToString(), "gid", gID.ToString(),
										 "epno", epNo.ToString(), "fmask", fMask, "amask", aMask));
		}


		public void Group(int gID, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("GROUP", new Dictionary<string, string>
			//                        {
			//                            {"gid", gID.ToString()}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("GROUP", callback, tag, "gid", gID.ToString()));
		}

		public void Group(string gName, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			//SendCommand("GROUP", new Dictionary<string, string>
			//                        {
			//                            {"gname", gName}
			//                        }, callback, tag);

			SendCommand(new AniDBRequest("GROUP", callback, tag, "gname", gName));
		}


		public void GroupStatus(int aID, int state = 0, AniDBTaggedResponseCallback callback = null, string tag = "")
		{
			Dictionary<string, string> parValues = new Dictionary<string, string>
			                                       	{
			                                       		{"aid", aID.ToString()}
			                                       	};

			if (state > 0)
				parValues.Add("state", state.ToString());

			//SendCommand("GROUPSTATUS", parValues, callback, tag);

			SendCommand(new AniDBRequest { Command = "GROUPSTATUS", Callback = callback, Tag = tag, ParValues = parValues });
		}
	}
}
