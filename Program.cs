﻿using System.Globalization;
using System.Text.Json;

class Core 
{
	static void Main(string[] args)
	{
		var logFile = File.ReadAllText(args[0]);

		var splits = logFile.Split($"\r\ncommit ");

		List<Commit> commits = new();
		foreach (var split in splits)
		{
			var commit = new Commit(split.Split("\r\n"));
			commits.Add(commit);
		}

		var updates = commits.SelectMany(x => x.Updates);
		var includedMods = updates.Select(x => x.Repo).Distinct();

		List<Entry> entries = new();
		foreach (var mod in includedMods)
		{
			List<DownloadCountUpdate> countUpdates = new();
			foreach (var update in updates)
			{
				if (update.Repo == mod)
				{
					countUpdates.Add(new DownloadCountUpdate()
					{
						DownloadCount = update.DownloadCount,
						UnixTimestamp = ((DateTimeOffset)update.Time).ToUnixTimeSeconds()
					});
				}
			}

			var entry = new Entry(mod, countUpdates.ToArray());

			entries.Add(entry);
		}

		var json = JsonSerializer.Serialize(entries);

		Console.WriteLine(json);
	}

	public static int GetNthIndex(string s, char t, int n)
	{
		var count = 0;
		for (var i = 0; i < s.Length; i++)
		{
			if (s[i] == t)
			{
				count++;
				if (count == n)
				{
					return i;
				}
			}
		}

		return -1;
	}
}