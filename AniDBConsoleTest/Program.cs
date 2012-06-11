using System;
using libAniDB.NET;

namespace AniDBConsoleTest
{
	static class Program
	{
		static void Main(string[] args)
		{
			using (AniDB aniDB = new AniDB(9001))
			{

				aniDB.Ping(false, (res, req) =>
				                  {
				                  	Console.WriteLine("Request:");
				                  	Console.WriteLine(req);
				                  	Console.WriteLine("Response:");
				                  	Console.WriteLine(res.OriginalString);
				                  	Console.WriteLine(res);
				                  });

				Console.ReadKey();
			}
		}
	}
}
