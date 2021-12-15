namespace knightsAndCastles.Models
{
  public class Castle
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Defense { get; set; }
    public string? CreatorId { get; set; }
    public Account Creator { get; set; }
  }
}