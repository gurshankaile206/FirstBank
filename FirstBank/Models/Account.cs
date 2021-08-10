using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace FirstBank.Models
{
  public class Account
  {
    public Account()
    {
      this.JoinEntities = new HashSet<CustomerAccount>();
    }

    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public int Balance { get; set; }
    public virtual ICollection<CustomerAccount> JoinEntities { get; set; }
  }
}