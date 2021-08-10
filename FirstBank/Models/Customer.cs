using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace FirstBank.Models
{
  public class Customer
  {
    public Customer()
    {
      this.JoinEntities = new HashSet<CustomerAccount>();
    }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public virtual ICollection<CustomerAccount> JoinEntities { get; set; }
  }
}