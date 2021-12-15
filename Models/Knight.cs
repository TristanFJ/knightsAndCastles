namespace knightsAndCastles.Models
{
  public class Knight
  {
    public int Id { get; set; }
    public string? Weapon { get; set; }
    public int Age { get; set; }
    public string? Surname { get; set; }
    public int CastleId { get; set; }
    public Castle Name { get; set; }
  }
}