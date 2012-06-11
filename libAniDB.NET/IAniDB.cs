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

namespace libAniDB.NET
{
	/// <summary>
	/// Interface to AniDB class. Use AniDBFactory to get an instance.
	/// </summary>
	public interface IAniDB
	{
		///// <summary>
		///// True if encryption is enabled
		///// </summary>
		//bool EncryptionEnabled { get; }

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
		void Auth(string user, string pass, AniDBResponseCallback callback = null, bool nat = false, bool comp = false,
				  int mtu = 0, bool imgServer = false);

		/// <summary>
		/// Logs out, must be logged in
		/// </summary>
		/// <param name="callback">Response callback</param>
		void Logout(AniDBResponseCallback callback = null);

		/// <summary>
		/// Not Yet Implemented
		/// </summary>
		/// <param name="user">Use this user's API key to encrypt the session</param>
		/// <param name="callback">Response callback</param>
		void Encrypt(string user, AniDBResponseCallback callback = null);

		/// <summary>
		/// Ping Command. Can be used to detect whether the outgoing port number has been changed by a NAT router (set nat to true) or to keep a "connection" alive.
		/// </summary>
		/// <param name="nat">If true(default), returns the outgoing port number.</param>
		/// <param name="callback">Response callback</param>
		void Ping(bool nat = false, AniDBResponseCallback callback = null);

		/// <summary>
		/// Changes the encoding method.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="callback"></param>
		void ChangeEncoding(string name, AniDBResponseCallback callback = null);

		void Uptime(AniDBResponseCallback callback = null);
		void Version(AniDBResponseCallback callback = null);
		void Anime(int aID, string aMask = "", AniDBResponseCallback callback = null);
		void Anime(string aName, string aMask = "", AniDBResponseCallback callback = null);
		void AnimeDesc(int aID, int partNo, AniDBResponseCallback callback = null);
		void Calendar(AniDBResponseCallback callback = null);
		void Character(int charID, AniDBResponseCallback callback = null);
		void Creator(int creatorID, AniDBResponseCallback callback = null);
		void Episode(int eID, AniDBResponseCallback callback = null);
		void Episode(string aName, int epNo, AniDBResponseCallback callback = null);
		void Episode(int aID, int epNo, AniDBResponseCallback callback = null);
		void File(int fID, string fMask, string aMask, AniDBResponseCallback callback = null);

		void File(long size, string ed2K, string fMask, string aMask, AniDBResponseCallback callback = null);

		void File(string aName, string gName, int epNo, string fMask, string aMask,
				  AniDBResponseCallback callback = null);

		void File(string aName, int gID, int epNo, string fMask, string aMask, AniDBResponseCallback callback = null);

		void File(int aID, string gName, int epNo, string fMask, string aMask, AniDBResponseCallback callback = null);

		void File(int aID, int gID, int epNo, string fMask, string aMask, AniDBResponseCallback callback = null);

		void Group(int gID, AniDBResponseCallback callback = null);
		void Group(string gName, AniDBResponseCallback callback = null);
		void GroupStatus(int aID, int state = 0, AniDBResponseCallback callback = null);
	}

	public delegate void AniDBResponseCallback(AniDBResponse response, AniDBRequest request);
}
