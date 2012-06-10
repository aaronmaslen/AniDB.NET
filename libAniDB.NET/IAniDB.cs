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

using System.Text;
using libAniDB.NET;

namespace libAniDB.NET
{
	/// <summary>
	/// Interface to AniDB class. Use AniDBFactory to get an instance.
	/// </summary>
	public interface IAniDB
	{
		/// <summary>
		/// Invoked after a packet is recieved and processed internally.
		/// If you want to do something with the packet before the library handles it, create a tagged request
		/// </summary>
		event AniDBUnTaggedResponseCallback ResponseRecieved;

		///// <summary>
		///// True if encryption is enabled
		///// </summary>
		//bool EncryptionEnabled { get; }

		/// <summary>
		/// True if logged in
		/// </summary>
		bool LoggedIn { get; }

		/// <summary>
		/// Auth with the AniDB server.
		/// </summary>
		/// <param name="user">User name</param>
		/// <param name="pass">Password</param>
		/// <param name="callback">Response callback</param>
		/// <param name="nat">Returns the detected</param>
		/// <param name="comp">Enable compression</param>
		/// <param name="mtu">Maximum Transmission Unit size of responses (in bytes). Valid values are in the range 400-1400</param>
		/// <param name="imgServer">Gets an image server domain name</param>
		/// <remarks>
		/// Possible Replies:
		/// <list type="bullet">
		///  <item><description>200 {str session_key} LOGIN ACCEPTED</description></item>
		///  <item><description>201 {str session_key} LOGIN ACCEPTED - NEW VERSION AVAILABLE</description></item>
		///  <item><description>500 LOGIN FAILED</description></item>
		///  <item><description>503 CLIENT VERSION OUTDATED</description></item>
		///  <item><description>504 CLIENT BANNED - {str reason}</description></item>
		///  <item><description>505 ILLEGAL INPUT OR ACCESS DENIED</description></item>
		///  <item><description>601 ANIDB OUT OF SERVICE - TRY AGAIN LATER</description></item>
		/// <listheader><description>when nat=1</description></listheader>
		///  <item><description>200 {str session_key} {str ip}:{int2 port} LOGIN ACCEPTED</description></item>
		///  <item><description>201 {str session_key} {str ip}:{int2 port} LOGIN ACCEPTED - NEW VERSION AVAILABLE</description></item>
		/// <listheader><description>when imgserver=1</description></listheader>
		///  <item><description>200 {str session_key} LOGIN ACCEPTED<br/>
		///   {str image server name}</description></item>
		///  <item><description>201 {str session_key} LOGIN ACCEPTED - NEW VERSION AVAILABLE<br/>
		///   {str image server name}</description></item>
		/// </list>
		/// </remarks>
		void Auth(string user, string pass, AniDBTaggedResponseCallback callback = null, bool nat = false, bool comp = false,
		          int mtu = 0, bool imgServer = false);

		/// <summary>
		/// Logs out, must be logged in
		/// </summary>
		/// <param name="callback">Response callback</param>
		void Logout(AniDBTaggedResponseCallback callback = null);

		/// <summary>
		/// Not Yet Implemented
		/// </summary>
		/// <param name="user">Use this user's API key to encrypt the session</param>
		/// <param name="callback">Response callback</param>
		void Encrypt(string user, AniDBTaggedResponseCallback callback = null);

		/// <summary>
		/// Ping Command. Can be used to detect whether the outgoing port number has been changed by a NAT router (set nat to true) or to keep a "connection" alive.
		/// </summary>
		/// <param name="nat">If true(default), returns the outgoing port number.</param>
		/// <param name="callback">Response callback</param>
		void Ping(bool nat = false, AniDBTaggedResponseCallback callback = null);

		/// <summary>
		/// Changes the encoding method.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="callback"></param>
		void ChangeEncoding(string name, AniDBTaggedResponseCallback callback = null);

		void Uptime(AniDBTaggedResponseCallback callback = null);
		void Version(AniDBTaggedResponseCallback callback = null);
		void Anime(int aID, string aMask = "", AniDBTaggedResponseCallback callback = null);
		void Anime(string aName, string aMask = "", AniDBTaggedResponseCallback callback = null);
		void AnimeDesc(int aID, int partNo, AniDBTaggedResponseCallback callback = null);
		void Calendar(AniDBTaggedResponseCallback callback = null);
		void Character(int charID, AniDBTaggedResponseCallback callback = null);
		void Creator(int creatorID, AniDBTaggedResponseCallback callback = null);
		void Episode(int eID, AniDBTaggedResponseCallback callback = null);
		void Episode(string aName, int epNo, AniDBTaggedResponseCallback callback = null);
		void Episode(int aID, int epNo, AniDBTaggedResponseCallback callback = null);
		void File(int fID, string fMask, string aMask, AniDBTaggedResponseCallback callback = null);

		void File(long size, string ed2K, string fMask, string aMask, AniDBTaggedResponseCallback callback = null);

		void File(string aName, string gName, int epNo, string fMask, string aMask,
		          AniDBTaggedResponseCallback callback = null);

		void File(string aName, int gID, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null);

		void File(int aID, string gName, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null);

		void File(int aID, int gID, int epNo, string fMask, string aMask, AniDBTaggedResponseCallback callback = null);

		void Group(int gID, AniDBTaggedResponseCallback callback = null);
		void Group(string gName, AniDBTaggedResponseCallback callback = null);
		void GroupStatus(int aID, int state = 0, AniDBTaggedResponseCallback callback = null);
	}

	public delegate void AniDBTaggedResponseCallback(AniDBResponse response, AniDBRequest request);

	public delegate void AniDBUnTaggedResponseCallback(AniDBResponse response);

	public enum AniDBReturnCode : short
	{
		// ReSharper disable InconsistentNaming
		LOGIN_ACCEPTED = 200, //a
		LOGIN_ACCEPTED_NEW_VER = 201, //a
		LOGGED_OUT = 203, //a
		RESOURCE = 205, //d
		STATS = 206, //b
		TOP = 207, //b
		UPTIME = 208, //b
		ENCRYPTION_ENABLED = 209, //c

		MYLIST_ENTRY_ADDED = 210, //a
		MYLIST_ENTRY_DELETED = 211, //a

		ADDED_FILE = 214, //e
		ADDED_STREAM = 215, //e

		ENCODING_CHANGED = 219, //c

		FILE = 220, //a
		MYLIST = 221, //a
		MYLIST_STATS = 222, //b

		ANIME = 230, //b
		ANIME_BEST_MATCH = 231, //b
		RANDOMANIME = 232, //b
		ANIME_DESCRIPTION = 233, //b

		EPISODE = 240, //b
		PRODUCER = 245, //b
		GROUP = 250, //b

		BUDDY_LIST = 253, //c
		BUDDY_STATE = 254, //c
		BUDDY_ADDED = 255, //c
		BUDDY_DELETED = 256, //c
		BUDDY_ACCEPTED = 257, //c
		BUDDY_DENIED = 258, //c

		VOTED = 260, //b
		VOTE_FOUND = 261, //b
		VOTE_UPDATED = 262, //b
		VOTE_REVOKED = 263, //b

		NOTIFICATION_ENABLED = 270, //a
		NOTIFICATION_NOTIFY = 271, //a
		NOTIFICATION_MESSAGE = 272, //a
		NOTIFICATION_BUDDY = 273, //c
		NOTIFICATION_SHUTDOWN = 274, //c
		PUSHACK_CONFIRMED = 280, //a
		NOTIFYACK_SUCCESSFUL_M = 281, //a
		NOTIFYACK_SUCCESSFUL_N = 282, //a
		NOTIFICATION = 290, //a
		NOTIFYLIST = 291, //a
		NOTIFYGET_MESSAGE = 292, //a
		NOTIFYGET_NOTIFY = 293, //a

		SENDMSG_SUCCESSFUL = 294, //a
		USER = 295, //d

		// AFFIRMATIVE/NEGATIVE 3XX

		PONG = 300, //a
		AUTHPONG = 301, //c
		NO_SUCH_RESOURCE = 305, //d
		API_PASSWORD_NOT_DEFINED = 309, //c

		FILE_ALREADY_IN_MYLIST = 310, //a
		MYLIST_ENTRY_EDITED = 311, //a
		MULTIPLE_MYLIST_ENTRIES = 312, //e

		SIZE_HASH_EXISTS = 314, //c
		INVALID_DATA = 315, //c
		STREAMNOID_USED = 316, //c

		NO_SUCH_FILE = 320, //a
		NO_SUCH_ENTRY = 321, //a
		MULTIPLE_FILES_FOUND = 322, //b

		NO_SUCH_ANIME = 330, //b
		NO_SUCH_ANIME_DESCRIPTION = 333, //b
		NO_SUCH_EPISODE = 340, //b
		NO_SUCH_PRODUCER = 345, //b
		NO_SUCH_GROUP = 350, //b

		BUDDY_ALREADY_ADDED = 355, //c
		NO_SUCH_BUDDY = 356, //c
		BUDDY_ALREADY_ACCEPTED = 357, //c
		BUDDY_ALREADY_DENIED = 358, //c

		NO_SUCH_VOTE = 360, //b
		INVALID_VOTE_TYPE = 361, //b
		INVALID_VOTE_VALUE = 362, //b
		PERMVOTE_NOT_ALLOWED = 363, //b
		ALREADY_PERMVOTED = 364, //b

		NOTIFICATION_DISABLED = 370, //a
		NO_SUCH_PACKET_PENDING = 380, //a
		NO_SUCH_ENTRY_M = 381, //a
		NO_SUCH_ENTRY_N = 382, //a

		NO_SUCH_MESSAGE = 392, //a
		NO_SUCH_NOTIFY = 393, //a
		NO_SUCH_USER = 394, //a


		// NEGATIVE 4XX


		NOT_LOGGED_IN = 403, //a

		NO_SUCH_MYLIST_FILE = 410, //a
		NO_SUCH_MYLIST_ENTRY = 411, //a


		// CLIENT SIDE FAILURE 5XX


		LOGIN_FAILED = 500, //a
		LOGIN_FIRST = 501, //a
		ACCESS_DENIED = 502, //a
		CLIENT_VERSION_OUTDATED = 503, //a
		CLIENT_BANNED = 504, //a
		ILLEGAL_INPUT_OR_ACCESS_DENIED = 505, //a
		INVALID_SESSION = 506, //a
		NO_SUCH_ENCRYPTION_TYPE = 509, //c
		ENCODING_NOT_SUPPORTED = 519, //c

		BANNED = 555, //a
		UNKNOWN_COMMAND = 598, //a


		// SERVER SIDE FAILURE 6XX


		INTERNAL_SERVER_ERROR = 600, //a
		ANIDB_OUT_OF_SERVICE = 601, //a
		SERVER_BUSY = 602, //d
		API_VIOLATION = 666, //a
		// ReSharper restore InconsistentNaming
	}
}
