public class Entry
{
	public string Repo { get; set; }
	public DownloadCountUpdate[] Updates { get; set; }

	public Entry(string repo, DownloadCountUpdate[] updates)
    {
		Repo = repo;
		Updates = updates;
    }
}
