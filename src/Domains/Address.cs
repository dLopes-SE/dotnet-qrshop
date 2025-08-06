namespace dotnet_qrshop.Domains
{
  public class Address : BaseEntity
  {
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; }

    public string FullName { get; private set; }
    public string Phone {  get; private set; }
    public string Address_line1 { get; private set; }
    public string Address_line2 { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public string State_or_Province { get; private set; }
    public string Country { get; private set; }
    public bool IsFavourite { get; private set; }
  }
}
