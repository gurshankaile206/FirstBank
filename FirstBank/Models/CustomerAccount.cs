namespace FirstBank.Models
{
  public class CustomerAccount
  {
    public int CustomerId { get; set; }
    public int AccountId { get; set; }
    public int CustomerAccountId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Account Account { get; set; }
  }
}