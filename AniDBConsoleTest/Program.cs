using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libAniDB.NET;

namespace AniDBConsoleTest
{
	static class Program
	{
		static void Main(string[] args)
		{
			AniDB aniDB = new AniDB(9001);

			aniDB.Ping(false, (res, req) =>
				                {
				                Console.WriteLine(req);
				                Console.WriteLine(res);
				                });

			Console.ReadKey();
		}
	}
}
