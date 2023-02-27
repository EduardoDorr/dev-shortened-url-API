namespace DevEncurtaUrl.API.Entities
{
  public class ShortenedCustomLink
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Shortenedlink { get; set; }
    public string DestinationLink { get; set; }
    public string Code { get; set; }
    public string CreatedAt { get; set; }

    private ShortenedCustomLink() { }

    public ShortenedCustomLink(string title, string destinationLink, string domain)
    {
      var code = GenerateCode(title);

      Title = title;
      DestinationLink = destinationLink;
      Shortenedlink = GenerateShortenedLink(domain, code);
      Code = code;
      CreatedAt = DateTime.Now.ToShortDateString();
    }

    public void Update(string title, string destinationLink, string domain)
    {
      Title = title;
      DestinationLink = destinationLink;
      Code = GenerateCode(Title);
      Shortenedlink = GenerateShortenedLink(domain, Code);
    }

    private string GenerateCode(string title)
    {
      return title.Split(" ")[0];
    }

    private string GenerateShortenedLink(string domain, string code)
    {
      return $"{domain}/{code}";
    }
  }
}